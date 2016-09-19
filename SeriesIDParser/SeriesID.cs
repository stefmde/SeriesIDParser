
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

[assembly: InternalsVisibleTo("SeriesIDParser_Test")]
namespace SeriesIDParser
{
	/// <summary>
	/// Representing the series or movie resolution
	/// </summary>
	public enum ResolutionsMap
	{
		UNKNOWN = 0,
		SD_Any = 1,
		HD_720p = 2,
		FullHD_1080p = 3,
		UltraHD_2160p = 4,
		UltraHD8K_4320p = 5
	}

	/// <summary>
	/// Representing the object success state
	/// </summary>
	[Flags]
	public enum State
	{
		UNKNOWN = 0,
		OK_SUCCESS = 1,
		WARN_ERR_OR_WARN_OCCURRED = 2,
		WARN_NO_TITLE_FOUND = 4,
		ERR_EMPTY_OR_TO_SHORT_ARGUMENT = 8,
		ERR_ID_NOT_FOUND = 16,
		ERR_UNKNOWN_ERROR = 32
	}

	/// <summary>
	/// The result object representing the series or movie string
	/// </summary>
	public partial class SeriesID
	{
		private ParserSettings _parserSettings = null;
		private DateTime _parseStartTime = new DateTime();

		/// <summary>
		/// Representing the object for error state
		/// </summary>
		/// <param name="state"></param>
		private SeriesID(State state)
		{
			this._state = state;
		}

		/// <summary>
		/// ctor with optional settings. Null settings are overriden with default settings
		/// </summary>
		/// <param name="settings"></param>
		public SeriesID(ParserSettings settings = null)
		{
			if (settings == null)
			{
				this._parserSettings = new ParserSettings(true);
			}
			else
			{
				this._parserSettings = settings;
			}
		}


		/// <summary>
		/// </summary>
		/// <returns>FullTitle and resolution. Null on error</returns>
		public override string ToString()
		{
			if (_state.HasFlag(State.OK_SUCCESS))
			{
				return FullTitle + " -- " + Helper.GetResolutionString(_parserSettings, _resolutions);
			}
			else
			{
				return null;
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
			Regex removeRegex = null;
			_detectedOldSpacingChar = Helper.GetSpacingChar(input, _parserSettings);

			try
			{
				if (_originalString.Length >= 5)
				{
					_fileExtension = Helper.GetFileExtension(input, _parserSettings.FileExtensions);


					// ###################################################################
					// ### Try get resolution
					// ###################################################################

					List<ResolutionsMap> foundResolutions = new List<ResolutionsMap>();

					// Try get 8K
					foundResolutions.AddRange(Helper.GetResolutionByResMap(_parserSettings.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, _detectedOldSpacingChar, ref fullTitle));

					// Try get 4K
					foundResolutions.AddRange(Helper.GetResolutionByResMap(_parserSettings.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, _detectedOldSpacingChar, ref fullTitle));

					// Try get FullHD
					foundResolutions.AddRange(Helper.GetResolutionByResMap(_parserSettings.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, _detectedOldSpacingChar, ref fullTitle));

					// Try get HD
					foundResolutions.AddRange(Helper.GetResolutionByResMap(_parserSettings.DetectHDTokens, ResolutionsMap.HD_720p, _detectedOldSpacingChar, ref fullTitle));

					// Try get SD
					foundResolutions.AddRange(Helper.GetResolutionByResMap(_parserSettings.DetectSDTokens, ResolutionsMap.SD_Any, _detectedOldSpacingChar, ref fullTitle));

					foreach (ResolutionsMap res in foundResolutions)
					{
						if (!_resolutions.Contains(res))
						{
							_resolutions.Add(res);
						}
					}



					// ###################################################################
					// ### Try get plain title
					// ###################################################################

					// remove hoster token
					string tmpTitle = string.Empty;
					if (fullTitle.Length > 30)
					{
						tmpTitle = fullTitle.Substring(fullTitle.Length - 20, 20);

						if (tmpTitle.Count(x => x == _parserSettings.ReleaseGroupSeperator ) > 0)
						{
							int seperatorIndex = fullTitle.LastIndexOf(_parserSettings.ReleaseGroupSeperator);
							_releaseGroup = fullTitle.Substring(seperatorIndex+1).Replace("." + _fileExtension, "").Trim();
							fullTitle = fullTitle.Substring(0, seperatorIndex).Trim();
						}
					}

					// Remove tokens
					List<string> foundTokens = new List<string>();
					foundTokens.AddRange(Helper.RemoveTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.RemoveAndListTokens, true));
					foundTokens.AddRange(Helper.RemoveTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.RemoveWithoutListTokens, false));

					foundTokens.AddRange( Helper.ReplaceTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.ReplaceRegexAndListTokens, true));
					foundTokens.AddRange( Helper.ReplaceTokens(ref fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.ReplaceRegexWithoutListTokens, false));
					foreach (string item in foundTokens)
					{
						if (_removedTokens != null && !_removedTokens.Any(x => x.ToLower() == item.ToLower()))
						{
							_removedTokens.Add(item);
						}
					}

					// Sort removedTokensList
					_removedTokens = _removedTokens.OrderBy(x => x).ToList();

					// remove fileextension
					removeRegex = null;
					if (_fileExtension != null)
					{
						removeRegex = new Regex(_fileExtension, RegexOptions.IgnoreCase);
						fullTitle = removeRegex.Replace(fullTitle, "");
					}

					// Get and remove year
					_year = Helper.GetYear(fullTitle);
					fullTitle = fullTitle.Replace(_year.ToString(), "");

					// Remove double spacer
					while (fullTitle.Contains(_detectedOldSpacingChar.ToString() + _detectedOldSpacingChar.ToString()))
					{
						fullTitle = fullTitle.Replace(_detectedOldSpacingChar.ToString() + _detectedOldSpacingChar.ToString(), _detectedOldSpacingChar.ToString());
					}

					// Convert to new spacer
					fullTitle = fullTitle.Replace(_detectedOldSpacingChar.ToString(), _parserSettings.NewSpacingChar.ToString());

					// Remove starting and trailing spaces
					fullTitle = fullTitle.Trim();

					// Remove trailing spacer
					fullTitle = fullTitle.Trim(_detectedOldSpacingChar.ToString().LastOrDefault<char>());
					fullTitle = fullTitle.Trim(_parserSettings.NewSpacingChar);



					// ###################################################################
					// ### Try get Season and Episode ID
					// ###################################################################
					Regex seRegex = new Regex(_parserSettings.SeasonAndEpisodeParseRegex);
					Match match = seRegex.Match(_originalString.ToUpper());
					if (match.Success)
					{
						if (match.Index > 0)
						{
							_title = fullTitle.Substring(0, match.Index - 1).Replace(_detectedOldSpacingChar.ToString(), _parserSettings.NewSpacingChar.ToString());
						}
						else
						{
							_state |= State.WARN_ERR_OR_WARN_OCCURRED | State.WARN_NO_TITLE_FOUND;
							warningOrErrorOccurred = true;
						}

						// Get Episode title if there is one
						int episodeTitleStartIndex = match.Index + match.Length + 1;
						if (fullTitle.Length > episodeTitleStartIndex)
						{
							_episodeTitle = fullTitle.Substring(episodeTitleStartIndex);
						}
						else
						{
							_episodeTitle = null;
						}

						int.TryParse(match.Groups["season"].Value, out _season);
						int.TryParse(match.Groups["episode"].Value, out _episode);
						_isSeries = true;
					}
					else
					{
						// MOVIE
						_title = fullTitle;

						if (!warningOrErrorOccurred)
						{
							_state |= State.OK_SUCCESS;
						}
						MaintainUnknownResolution();
						_processingDuration = DateTime.Now - _parseStartTime;
						return this;
					}
				}
				else
				{
					// ERROR
					_state |= State.ERR_EMPTY_OR_TO_SHORT_ARGUMENT;
					MaintainUnknownResolution();
					return this;
				}

				// SERIES
				if (!warningOrErrorOccurred)
				{
					_state |= State.OK_SUCCESS;
				}

				MaintainUnknownResolution();
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
			this._episode = -1;
			this._episodeTitle = null;
			this._exception = null;
			this._fileExtension = null;
			this._isSeries = false;
			this._originalString = null;
			this._removedTokens.Clear();
			this._resolutions = new List<ResolutionsMap>();
			this._season = -1;
			this._state = State.UNKNOWN;
			this._title = null;
			this._year = -1;
			this._detectedOldSpacingChar = new char();
			this._processingDuration = new TimeSpan();
			this._parseStartTime = DateTime.Now;
			this._releaseGroup = null;
		}

		/// <summary>
		/// Function Adds a Unknown Resolution mark or removes it if needed
		/// </summary>
		private void MaintainUnknownResolution()
		{
			if (_resolutions.Count == 0)
			{
				_resolutions.Add(ResolutionsMap.UNKNOWN);
			}
			else
			{
				_resolutions.Remove(ResolutionsMap.UNKNOWN);
			}
		}

		#endregion HelperFunctions
	}
}
