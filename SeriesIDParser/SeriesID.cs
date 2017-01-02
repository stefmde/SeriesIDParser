
// MIT License

// Copyright(c) 2016
// Stefan Müller, Stefm, https://gitlab.com/u/Stefm

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SeriesIDParser.Caching;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

namespace SeriesIDParser
{
	/// <summary>
	/// The result object representing the series or movie string
	/// </summary>
	public class SeriesID
	{
		#region Fields

		private readonly ParserSettings _parserSettings = new ParserSettings(true);
		private DateTime _parseStartTime = new DateTime();
		private State _state;
		private List<ResolutionsMap> _resolutions;
		private string _originalString;
		private char _detectedOldSpacingChar;
		private string _fileExtension;
		private string _releaseGroup;
		private List<string> _removedTokens;
		private string _audioCodec;
		private string _videoCodec;
		private int _year;
		private string _title;
		private string _episodeTitle;
		private bool _isSeries;
		private List<int> _episodes;
		private int _season;
		private TimeSpan _processingDuration;
		private Exception _exception;
		private FileInfo _fileInfo;
		private bool _cacheEnabled;

		#endregion Fields

		/// <summary>
		/// ctor with optional settings. Null settings are overriden with default settings
		/// </summary>
		/// <param name="settings"></param>
		public SeriesID(ParserSettings settings = null)
		{
			if (settings != null)
			{
				_parserSettings = settings;
			}

			if (_parserSettings.CacheMode == CacheMode.Advanced || _parserSettings.CacheMode == CacheMode.Simple)
			{
				_cacheEnabled = true;
			}

			if (_cacheEnabled && !MediaDataCache.InstanceExists)
			{
				MediaDataCache.Create(_parserSettings);
			}
		}



		// ############################################################
		// ### Wrapper Functions
		// ############################################################
		#region WrapperFunctions
		/// <summary>
		/// The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie string who get parsed. Must be atleast five chars</param>
		/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
		public ParserResult Parse(string input)
		{
			if (_cacheEnabled)
			{
				ParserResult parserResult;
				if (MediaDataCache.Instance.TryGetAsParserResult(input, out parserResult))
				{
					return parserResult;
				}
			}

			MediaData mediaData = CoreParser(input);

			if (_cacheEnabled)
			{
				MediaDataCache.Instance.Add(input, mediaData);
			}

			return mediaData.ToParserResult(_parserSettings);
		}



		/// <summary>
		/// The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie FileInfo who get parsed.</param>
		/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
		public ParserResult Parse(FileInfo input)
		{
			_fileInfo = input;
			return CoreParser(input?.Name ?? String.Empty).ToParserResult(_parserSettings);
		}



		/// <summary>
		/// Get all files in a path parsed
		/// </summary>
		/// <param name="path"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public IEnumerable<ParserResult> ParsePath(string path, SearchOption searchOption = SearchOption.AllDirectories)
		{
			List<ParserResult> results = new List<ParserResult>();

			if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
			{
				return results;
			}

			List<string> files = new List<string>(HelperWorker.GetSeriesAndMovieFiles(path, _parserSettings, searchOption));

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);

				if (_cacheEnabled)
				{
					MediaData mediaData;
					if (MediaDataCache.Instance.TryGet(fileName, out mediaData))
					{
						results.Add(mediaData.ToParserResult(_parserSettings));
						continue;
					}
				}

				results.Add(CoreParser(fileName).ToParserResult(_parserSettings));
			}

			return results;
		}



		/// <summary>
		/// Get all files in a path parsed
		/// </summary>
		/// <param name="path"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public IEnumerable<ParserResult> ParsePath(DirectoryInfo path, SearchOption searchOption = SearchOption.AllDirectories)
		{
			return ParsePath(path?.FullName ?? String.Empty, searchOption);
		}
		#endregion WrapperFunctions



		// ############################################################
		// ### Core Function
		// ############################################################
		private MediaData CoreParser(string input)
		{
			ResetObject();
			_originalString = input;
			string fullTitle = _originalString.Trim();
			bool warningOrErrorOccurred = false;
			_detectedOldSpacingChar = HelperWorker.GetSpacingChar(input, _parserSettings);

			try
			{
				if (_originalString.Length < 5)
				{
					// ERROR
					_state |= State.ErrEmptyOrToShortArgument;
					_resolutions = HelperWorker.MaintainUnknownResolution(_resolutions);
					return GenerateMediaData();
				}

				// Todo remove fileextension instantly
				_fileExtension = HelperWorker.GetFileExtension(input, _parserSettings.FileExtensions);
				_resolutions = HelperWorker.GetResolutions(_parserSettings, _detectedOldSpacingChar, ref fullTitle);
				_releaseGroup = HelperWorker.GetReleaseGroup(fullTitle, _parserSettings, _fileExtension);
				fullTitle = HelperWorker.RemoveReleaseGroup(fullTitle, _parserSettings, _releaseGroup);
				_removedTokens = HelperWorker.RemoveTokens(_parserSettings, _detectedOldSpacingChar, ref fullTitle);

				fullTitle = GetCodecs(fullTitle);

				_removedTokens = _removedTokens.OrderBy(x => x).ToList();

				// remove fileextension
				if (_fileExtension != null)
				{
					Regex removeRegex = new Regex(_fileExtension, RegexOptions.IgnoreCase);
					fullTitle = removeRegex.Replace(fullTitle, "");
				}

				// Get and remove year
				_year = HelperWorker.GetYear(fullTitle, _parserSettings.YearParseRegex);
				fullTitle = fullTitle.Replace(_year.ToString(), "");

				// Remove double spacer
				while (fullTitle.Contains(_detectedOldSpacingChar + _detectedOldSpacingChar.ToString()))
				{
					fullTitle = fullTitle.Replace(_detectedOldSpacingChar + _detectedOldSpacingChar.ToString(),
						_detectedOldSpacingChar.ToString());
				}

				// Convert to new spacer
				fullTitle = fullTitle.Replace(_detectedOldSpacingChar.ToString(), _parserSettings.NewSpacingChar.ToString());

				// Remove trailing spacer
				fullTitle = fullTitle.Trim().Trim(_detectedOldSpacingChar).Trim(_parserSettings.NewSpacingChar);

				// Try get Season and Episode ID
				warningOrErrorOccurred = GetSeriesDetails(fullTitle, warningOrErrorOccurred);

				// SERIES
				if (!warningOrErrorOccurred)
				{
					_state |= State.OkSuccess;
				}

				_resolutions = HelperWorker.MaintainUnknownResolution(_resolutions);
				_processingDuration = DateTime.Now - _parseStartTime;

				return GenerateMediaData();
			}
			catch (Exception ex)
			{
				_exception = ex;

				// Throw exception if the flag is set
				if (_parserSettings.ThrowExceptionInsteadOfErrorFlag)
				{
					throw;
				}

				// ERROR
				_state |= State.ErrUnknownError;
				return GenerateMediaData();
			}
		}



		// ############################################################
		// ### Local Helper Functions
		// ############################################################
		#region HelperFunctions
		/// <summary>
		/// Get audio and video codec
		/// </summary>
		/// <param name="fullTitle"></param>
		/// <returns></returns>
		private string GetCodecs(string fullTitle)
		{
			_audioCodec =
				HelperWorker.FindTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.AudioCodecs, true)
					.LastOrDefault();
			if (_audioCodec != null)
			{
				_removedTokens.Add(_audioCodec);
			}

			_videoCodec =
				HelperWorker.FindTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.VideoCodecs, true)
					.LastOrDefault();
			if (_videoCodec != null)
			{
				_removedTokens.Add(_videoCodec);
			}

			return fullTitle;
		}



		/// <summary>
		/// Get Series Details
		/// </summary>
		/// <param name="fullTitle"></param>
		/// <param name="warningOrErrorOccurred"></param>
		/// <returns></returns>
		private bool GetSeriesDetails(string fullTitle, bool warningOrErrorOccurred)
		{
			_title = HelperWorker.GetTitle(_parserSettings, _detectedOldSpacingChar, fullTitle, ref warningOrErrorOccurred, ref _state);
			_episodeTitle = HelperWorker.GetEpisodeTitle(_parserSettings, fullTitle);
			_isSeries = HelperWorker.IsSeries(_parserSettings, fullTitle);
			_season = HelperWorker.GetSeasonID(_parserSettings, fullTitle);
			_episodes = HelperWorker.GetEpisodeIDs(_parserSettings, fullTitle);
			return warningOrErrorOccurred;
		}



		///// <summary>
		///// ctor wrapper for ParserResult
		///// </summary>
		///// <returns></returns>
		//private ParserResult GenerateResult()
		//{
		//	return new ParserResult(_originalString, _parserSettings, _audioCodec, _videoCodec, _processingDuration,
		//		_resolutions, _season, _episodes, _year, _detectedOldSpacingChar, _exception, _isSeries, _removedTokens,
		//		_state, _fileExtension, _title, _episodeTitle, _releaseGroup);
		//}


		/// <summary>
		/// ctor wrapper for ParserResult
		/// </summary>
		/// <returns></returns>
		private MediaData GenerateMediaData()
		{
			return new MediaData()
			{
				OriginalString = _originalString,
				AudioCodec = _audioCodec,
				VideoCodec = _videoCodec,
				ProcessingDuration = _processingDuration,
				Resolutions = _resolutions,
				Season = _season,
				Episodes = _episodes,
				Year = _year,
				DetectedOldSpacingChar = _detectedOldSpacingChar,
				Exception = _exception,
				IsSeries = _isSeries,
				RemovedTokens = _removedTokens,
				State = _state,
				FileExtension = _fileExtension,
				Title = _title,
				EpisodeTitle = _episodeTitle,
				ReleaseGroup = _releaseGroup,
				FileInfo = _fileInfo
			};
		}



		/// <summary>
		/// Clears and resets the object for the new execution
		/// </summary>
		private void ResetObject()
		{
			_episodeTitle = string.Empty;
			_exception = null;
			_fileExtension = string.Empty;
			_isSeries = false;
			_originalString = string.Empty;
			_removedTokens = new List<string>();
			_resolutions = new List<ResolutionsMap>();
			_season = -1;
			_state = State.Unknown;
			_title = string.Empty;
			_year = -1;
			_detectedOldSpacingChar = new char();
			_processingDuration = new TimeSpan();
			_parseStartTime = DateTime.Now;
			_releaseGroup = string.Empty;
		}
		#endregion HelperFunctions
	}
}
