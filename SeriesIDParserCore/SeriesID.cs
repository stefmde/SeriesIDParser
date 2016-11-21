
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

[assembly: InternalsVisibleTo("SeriesIDParserCore_Test")]
namespace SeriesIDParserCore
{
	/// <summary>
	/// The result object representing the series or movie string
	/// </summary>
	public partial class SeriesID
	{
		private readonly ParserSettings _parserSettings = new ParserSettings(true);
		private DateTime _parseStartTime = new DateTime();

		/// <summary>
		/// ctor with optional settings. Null settings are overriden with default settings
		/// </summary>
		/// <param name="settings"></param>
		public SeriesID(ParserSettings settings = null)
		{
			if (settings != null)
			{
				this._parserSettings = settings;
			}
		}


		/// <summary>
		/// </summary>
		/// <returns>FullTitle and resolution. string.Empty on error</returns>
		public override string ToString()
		{
			if (_state.HasFlag(State.OK_SUCCESS))
			{
				return FullTitle + " -- " + Helper.GetResolutionString(_parserSettings, _resolutions);
			}
			else
			{
				return string.Empty;
			}
		}



		// ############################################################
		// ### Core Function
		// ############################################################

		/// <summary>
		/// The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie string who get parsed. Must be atleast five chars</param>
		/// <returns>The SeriesID object that represents the series or movie string</returns>
		public SeriesID Parse(string input)
		{
			ResetObject();
			_originalString = input.Trim();
			string fullTitle = _originalString;
			bool warningOrErrorOccurred = false;
			Regex removeRegex;
			_detectedOldSpacingChar = Helper.GetSpacingChar(input, _parserSettings);

			try
			{
				if (_originalString.Length >= 5)
				{
					// Todo remove fileextension instantly
					_fileExtension = Helper.GetFileExtension(input, _parserSettings.FileExtensions);
					_resolutions = Helper.GetResolutions(_parserSettings, _detectedOldSpacingChar, ref fullTitle);
					_releaseGroup = Helper.GetReleaseGroup(fullTitle, _parserSettings, _fileExtension);
					fullTitle = Helper.RemoveReleaseGroup(fullTitle, _parserSettings, _releaseGroup);
					_removedTokens = Helper.RemoveTokens(_parserSettings, _detectedOldSpacingChar, ref fullTitle);

					_audioCodec = Helper.FindTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.AudioCodecs, true).LastOrDefault();
					if (_audioCodec == null)
					{
						_audioCodec = string.Empty;
					}
					else
					{
						_removedTokens.Add(_audioCodec);
					}

					_videoCodec = Helper.FindTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.VideoCodecs, true).LastOrDefault();
					if (_videoCodec == null)
					{
						_videoCodec = string.Empty;
					}
					else
					{
						_removedTokens.Add(_videoCodec);
					}

					_removedTokens = _removedTokens.OrderBy(x => x).ToList();

					// remove fileextension
					if (_fileExtension != null)
					{
						removeRegex = new Regex(_fileExtension, RegexOptions.IgnoreCase);
						fullTitle = removeRegex.Replace(fullTitle, "");
					}

					// Get and remove year
					_year = Helper.GetYear(fullTitle, _parserSettings.YearParseRegex);
					fullTitle = fullTitle.Replace(_year.ToString(), "");

					// Remove double spacer
					while (fullTitle.Contains(_detectedOldSpacingChar.ToString() + _detectedOldSpacingChar.ToString()))
					{
						fullTitle = fullTitle.Replace(_detectedOldSpacingChar.ToString() + _detectedOldSpacingChar.ToString(), _detectedOldSpacingChar.ToString());
					}

					// Convert to new spacer
					fullTitle = fullTitle.Replace(_detectedOldSpacingChar.ToString(), _parserSettings.NewSpacingChar.ToString());

					// Remove trailing spacer
					fullTitle = fullTitle.Trim().Trim(_detectedOldSpacingChar).Trim(_parserSettings.NewSpacingChar);


					// ###################################################################
					// ### Try get Season and Episode ID
					// ###################################################################
					_title = Helper.GetTitle(_parserSettings, _detectedOldSpacingChar, fullTitle, ref warningOrErrorOccurred, ref _state);
					_episodeTitle = Helper.GetEpisodeTitle(_parserSettings, fullTitle);
					_isSeries = Helper.IsSeries(_parserSettings, fullTitle);
					_season = Helper.GetSeasonID(_parserSettings, fullTitle);
					_episodes = Helper.GetEpisodeIDs(_parserSettings, fullTitle);
					_isMultiEpisode = _episodes.Count > 1;
				}
				else
				{
					// ERROR
					_state |= State.ERR_EMPTY_OR_TO_SHORT_ARGUMENT;
					_resolutions = Helper.MaintainUnknownResolution(_resolutions);
					return this;
				}

				// SERIES
				if (!warningOrErrorOccurred)
				{
					_state |= State.OK_SUCCESS;
				}

				_resolutions = Helper.MaintainUnknownResolution(_resolutions);
				_processingDuration = DateTime.Now - _parseStartTime;

				return this;
			}
			catch (Exception ex)
			{
				_exception = ex;

				// Throw exception if the flag is set
				if (_parserSettings.ThrowExceptionInsteadOfErrorFlag)
				{
					throw;
				}
				else
				{
					// ERROR
					_state |= State.ERR_UNKNOWN_ERROR;
					return this;
				}
			}
		}





		// ############################################################
		// ### Local Helper Functions
		// ############################################################
		#region HelperFunctions
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
			_removedTokens.Clear();
			_resolutions = new List<ResolutionsMap>();
			_season = -1;
			_state = State.UNKNOWN;
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
