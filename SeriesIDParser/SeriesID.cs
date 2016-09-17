
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
	public class SeriesID
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

		// ############################################################
		// ### Properties
		// ############################################################
		// TODO implement better failsafe
		#region Properties
		/// <summary>
		/// Returns the full series string for Series, title for movies and null on error
		/// </summary>
		public string FullTitle
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{
					StringBuilder sb = new StringBuilder();

					if (_isSeries)
					{
						if (!string.IsNullOrEmpty(_title))
						{
							sb.Append(_title);
						}

						if (!string.IsNullOrEmpty(IDString))
						{
							sb.Append(_parserSettings.NewSpacingChar);
							sb.Append( IDString);
						}

						if (!string.IsNullOrEmpty(_episodeTitle))
						{
							sb.Append(_parserSettings.NewSpacingChar);
							sb.Append( _episodeTitle);
						}

						return sb.ToString();
					}
					else
					{
						return _title;
					}
				}
				else
				{
					return null;
				}
			}
		}


		private List<string> _removedTokens = new List<string>();
		/// <summary>
		/// Contains tokens whoi are removed by the parser as string list
		/// </summary>
		public List<string> RemovedTokens
		{
			get
			{
				return _removedTokens;
			}

			internal set { _removedTokens = value; }
		}


		private string _fileExtension;
		/// <summary>
		/// Contains the file-extension or null
		/// </summary>
		public string FileExtension
		{
			get
			{
				return _fileExtension;
			}

			internal set { _fileExtension = value; }
		}


		private string _originalString;
		/// <summary>
		/// Contains the string that is given to the parser
		/// </summary>
		public string OriginalString
		{
			get
			{
				return _originalString;
			}

			internal set { _originalString = value; }
		}


		private char _detectedOldSpacingChar;
		/// <summary>
		/// Contains the char who are detected as the old spacing char
		/// </summary>
		public char DetectedOldSpacingChar
		{
			get
			{
				return _detectedOldSpacingChar;
			}

			internal set { _detectedOldSpacingChar = value; }
		}


		//private string _parsedString;
		/// <summary>
		/// Contains the string that was computed by the parser. Null on error
		/// </summary>
		public string ParsedString
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{
					StringBuilder sb = new StringBuilder();
					//string tempString = string.Empty;

					sb.Append(FullTitle);

					if (_year > -1)
					{
						sb.Append(_parserSettings.NewSpacingChar);
						sb.Append(_year);
					}

					sb.Append(_parserSettings.NewSpacingChar);
					sb.Append(GetResolutionString(_parserSettings, _resolutions));

					if (_removedTokens != null && _removedTokens.Count > 0)
					{
						foreach (string remToken in _removedTokens)
						{
							sb.Append(_parserSettings.NewSpacingChar + remToken);
						}
					}

					if (!string.IsNullOrEmpty(_fileExtension))
					{
						sb.Append(".");
						sb.Append(_fileExtension);
					}

					return sb.ToString();
				}
				else
				{
					return null;
				}
			}
		}


		


		private string _title;
		/// <summary>
		/// Contains the series title or the movie name, depends on IsSeries
		/// </summary>
		public string Title
		{
			get
			{
				return _title;
			}

			internal set { _title = value; }
		}


		private State _state;
		/// <summary>
		/// Contains the state of the object e.g. OK_SUCCESS
		/// </summary>
		public State State
		{
			get
			{
				return _state;
			}

			internal set { _state = value; }
		}


		private string _episodeTitle;
		/// <summary>
		/// Contains the eposide title if object state is series. null on error
		/// </summary>
		public string EpisodeTitle
		{
			get
			{
				//return FailSafeProperties<string>(_episodeTitle);
				if (_state.HasFlag(State.OK_SUCCESS) && _isSeries)
				{
					return _episodeTitle;
				}
				else
				{
					return null;
				}
			}

			internal set { _episodeTitle = value; }
		}


		private int _season = -1;
		/// <summary>
		/// Contains the season id if object state is series. -1 on error
		/// </summary>
		public int Season
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS) && _isSeries)
				{
					return _season;
				}
				else
				{
					return -1;
				}
			}

			internal set { _season = value; }
		}


		private int _episode = -1;
		/// <summary>
		/// Contains the eposide id if object state is series. -1 on error
		/// </summary>
		public int Episode
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS) && _isSeries)
				{
					return _episode;
				}
				else
				{
					return -1;
				}
			}

			internal set { _episode = value; }
		}


		private bool _isSeries;
		/// <summary>
		/// Specifies if the object contains a series or a movie. Default: false
		/// </summary>
		public bool IsSeries
		{
			get
			{
				return _isSeries;
			}

			internal set { _isSeries = value; }
		}


		private int _year = -1;
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public int Year
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{

					return _year;
				}
				else
				{
					return -1;
				}
			}

			internal set { _year = value; }
		}



		private TimeSpan _processingDuration = new TimeSpan();
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public TimeSpan ProcessingDuration
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{

					return _processingDuration;
				}
				else
				{
					return new TimeSpan();
				}
			}

			internal set { _processingDuration = value; }
		}


		/// <summary>
		/// Contains the ID-String of a series e.g. S01E05. Null on error
		/// </summary>
		public string IDString
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS) && _isSeries)
				{
					return string.Format(_parserSettings.IDStringFormater, _season, _episode);
				}
				else
				{
					return null;
				}
			}
		}


		private IList<ResolutionsMap> _resolutions = new List<ResolutionsMap>();
		/// <summary>
		/// Returns the resolution as enum. UNKNOWN on error
		/// </summary>
		public IList<ResolutionsMap> Resolutions
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{
					return _resolutions;
				}
				else
				{
					return new List<ResolutionsMap>() { ResolutionsMap.UNKNOWN };
				}
			}

			internal set { _resolutions = value; }
		}


		private Exception _exception = null;
		/// <summary>
		/// Contains the Exception if any occours. Default: null
		/// </summary>
		public Exception Exception
		{
			get
			{
				return _exception;
			}

			internal set { _exception = value; }
		}
		#endregion Properties


		/// <summary>
		/// </summary>
		/// <returns>FullTitle and resolution. Null on error</returns>
		public override string ToString()
		{
			if (_state.HasFlag(State.OK_SUCCESS))
			{
				return FullTitle + " -- " + GetResolutionString(_parserSettings, _resolutions);
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
			_detectedOldSpacingChar = GetSpacingChar(input);

			try
			{
				if (_originalString.Length >= 5)
				{
					_fileExtension = GetFileExtension(input, _parserSettings.FileExtensions);

					// ###################################################################
					// ### Try get resolution
					// ###################################################################

					// Try get 8K
					GetResolutionByResMap(_parserSettings.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, _detectedOldSpacingChar, ref fullTitle);

					// Try get 4K
					GetResolutionByResMap(_parserSettings.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, _detectedOldSpacingChar, ref fullTitle);

					// Try get FullHD
					GetResolutionByResMap(_parserSettings.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, _detectedOldSpacingChar, ref fullTitle);

					// Try get HD
					GetResolutionByResMap(_parserSettings.DetectHDTokens, ResolutionsMap.HD_720p, _detectedOldSpacingChar, ref fullTitle);

					// Try get SD
					GetResolutionByResMap(_parserSettings.DetectSDTokens, ResolutionsMap.SD_Any, _detectedOldSpacingChar, ref fullTitle);



					// ###################################################################
					// ### Try get plain title
					// ###################################################################

					// remove hoster token
					string tmpTitle = string.Empty;
					if (fullTitle.Length > 30)
					{
						tmpTitle = fullTitle.Substring(fullTitle.Length - 20, 20);

						if (tmpTitle.Count(x => x == '-') > 0)
						{
							fullTitle = fullTitle.Substring(0, fullTitle.LastIndexOf("-")).Trim();
						}
					}

					// Remove tokens
					fullTitle = RemoveTokens(fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.RemoveAndListTokens, true);
					fullTitle = RemoveTokens(fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.RemoveWithoutListTokens, false);
					fullTitle = ReplaceTokens(fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.ReplaceRegexAndListTokens, true);
					fullTitle = ReplaceTokens(fullTitle, _detectedOldSpacingChar.ToString(), _parserSettings.ReplaceRegexWithoutListTokens, false);

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
					_year = GetYear(fullTitle);
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
		// ### Helper Functions
		// ############################################################
		#region HelperFunctions
		/// <summary>
		/// Clears and resets the object for the new execution
		/// </summary>
		internal void ResetObject()
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
			_parseStartTime = DateTime.Now;
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


		/// <summary>
		/// Gets the Resolutions depending on the found Resolutions and the ParserSettings
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="res"></param>
		/// <returns></returns>
		internal string GetResolutionString(ParserSettings settings, IList<ResolutionsMap> res)
		{
			string output = ResolutionsMap.UNKNOWN.ToString() + settings.NewSpacingChar;

			if (res != null && res.Count > 0 && !(res.Count == 1 && res.Contains(ResolutionsMap.UNKNOWN)))
			{
				res.Remove(ResolutionsMap.UNKNOWN);
				res = res.OrderBy(x => x).ToList();			

				if (settings.ResolutionStringOutput == ResolutionOutputBehavior.HighestResolution)
				{
					// Highest
					if (res.Contains(ResolutionsMap.UltraHD8K_4320p))
					{
						output = settings.ResolutionStringUltraHD8k;
					}
					else if (res.Contains(ResolutionsMap.UltraHD_2160p))
					{
						output = settings.ResolutionStringUltraHD;
					}
					else if (res.Contains(ResolutionsMap.FullHD_1080p))
					{
						output = settings.ResolutionStringFullHD;
					}
					else if (res.Contains(ResolutionsMap.HD_720p))
					{
						output = settings.ResolutionStringHD;
					}
					else if (res.Contains(ResolutionsMap.SD_Any))
					{
						output = settings.ResolutionStringSD;
					}
					else
					{
						output = settings.ResolutionStringUnknown;
					}
				}
				else if (settings.ResolutionStringOutput == ResolutionOutputBehavior.LowestResolution)
				{
					// Lowest
					if (res.Contains(ResolutionsMap.SD_Any))
					{
						output = settings.ResolutionStringSD;
					}
					else if (res.Contains(ResolutionsMap.HD_720p))
					{
						output = settings.ResolutionStringHD;
					}
					else if (res.Contains(ResolutionsMap.FullHD_1080p))
					{
						output = settings.ResolutionStringFullHD;
					}
					else if (res.Contains(ResolutionsMap.UltraHD_2160p))
					{
						output = settings.ResolutionStringUltraHD;
					}
					else if (res.Contains(ResolutionsMap.UltraHD8K_4320p))
					{
						output = settings.ResolutionStringUltraHD8k;
					}
					else
					{
						output = settings.ResolutionStringUnknown;
					}
				}
				else
				{
					// All
					output = string.Empty;

					foreach (ResolutionsMap itm in res)
					{
						if (itm == ResolutionsMap.SD_Any)
						{
							output += settings.ResolutionStringSD + settings.NewSpacingChar;
						}
						else if (itm == ResolutionsMap.HD_720p)
						{
							output += settings.ResolutionStringHD + settings.NewSpacingChar;
						}
						else if (itm == ResolutionsMap.FullHD_1080p)
						{
							output += settings.ResolutionStringFullHD + settings.NewSpacingChar;
						}
						else if (itm == ResolutionsMap.UltraHD_2160p)
						{
							output += settings.ResolutionStringUltraHD + settings.NewSpacingChar;
						}
						else if (itm == ResolutionsMap.UltraHD8K_4320p)
						{
							output += settings.ResolutionStringUltraHD8k + settings.NewSpacingChar;
						}
					}
				}
			}

			output = output.TrimEnd(settings.NewSpacingChar);

			return output;
		}

		/// <summary>
		/// Adds a Resolution to the list if its not already contained
		/// </summary>
		/// <param name="res"></param>
		private void AddUniqueResolutionToList(ResolutionsMap res)
		{
			if (!_resolutions.Contains(res))
			{
				_resolutions.Add(res);
			}
		}

		/// <summary>
		/// Adds a token who is removed and should be tracked to a list if it is not contained
		/// </summary>
		/// <param name="token">String token to add to list</param>
		private void AddRemovedToken(string token)
		{
			if (_removedTokens != null && !_removedTokens.Any(x => x.ToLower() == token.ToLower()))
			{
				_removedTokens.Add(token);
			}
		}

		/// <summary>
		/// Tries to get the year from a given string. Have to be between now and 1900
		/// </summary>
		/// <param name="title">String who should be analized</param>
		/// <returns>-1 or the found year</returns>
		internal int GetYear(string title)
		{
			int year = -1;

			Regex yearRegex = new Regex(@"(\d{4})");
			MatchCollection matches = yearRegex.Matches(title);

			foreach (Match match in matches)
			{
				if (match.Success)
				{
					int tempYear = -1;

					if (int.TryParse(match.Value, out tempYear))
					{
						if (tempYear >= 1900 && tempYear <= DateTime.Now.Year)
						{
							year = tempYear;
							break;
						}
					}
				}
			}

			return year;
		}

		/// <summary>
		/// Gets the resolutions of a given string
		/// </summary>
		/// <param name="ResMap">The token that could match the given resolution</param>
		/// <param name="res">The given resolution wo is targeted by the ResMap tokens</param>
		/// <param name="oldSpacingChar">the spacing char in the given string</param>
		/// <param name="fullTitle">The given string who should be analized</param>
		private void GetResolutionByResMap(List<string> ResMap, ResolutionsMap res, char oldSpacingChar, ref string fullTitle)
		{
			string spacer = Regex.Escape(oldSpacingChar.ToString());

			Regex removeRegex = null;
			foreach (string item in ResMap)
			{
				removeRegex = new Regex(spacer + item + spacer, RegexOptions.IgnoreCase);

				if (removeRegex.IsMatch(fullTitle) )
				{
					// Search with spacer but remove without spacer
					removeRegex = new Regex(item, RegexOptions.IgnoreCase);
					AddUniqueResolutionToList(res);

					fullTitle = removeRegex.Replace(fullTitle, "");
				}
			}
		}

		internal T FailSafeProperties<T>(T value, params bool[] checks)
		{
			if (checks.Contains(false) || false == _state.HasFlag(State.OK_SUCCESS))
			{
				return default(T);
				//TODO throw exception
			}

			return value;

		}

		internal struct CharRating
		{
			public char Char;
			public int Count;
		}

		/// <summary>
		/// Tries to get the spacing char from a given string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <returns>returns an empty string or the spacing char</returns>
		internal char GetSpacingChar(string input)
		{
			char foundChar = new char();

			List<CharRating> charRating = new List<CharRating>();
			foreach (char item in _parserSettings.PossibleSpacingChars)
			{
				charRating.Add(new CharRating { Char = item, Count = 0 });
			}
			
			for (int i = 0; i < charRating.Count; i++)
			{
				charRating[i] = new CharRating { Char = charRating[i].Char, Count = input.Count(f => f == charRating[i].Char) };
			}

			charRating = charRating.OrderBy(x => x.Count).ToList();

			int sum = charRating.Sum(x => x.Count);
			int largestOne = charRating.LastOrDefault().Count;

			// if largest one count is larger than the others additive -> 51% +
			if (largestOne > sum - largestOne)
			{
				foundChar = charRating.LastOrDefault().Char;
			}

			return foundChar;
		}


		/// <summary>
		/// Tries to get the file extension from a given string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="extensions">The list with the posible extensions</param>
		/// <returns>returns null or the found extension</returns>
		internal string GetFileExtension(string input, List<string> extensions)
		{
			string extension = null;
			string tempExtension = Path.GetExtension(input).TrimStart('.');

			if (extensions.Any(x => x.ToUpper() == tempExtension.ToUpper()))
			{
				extension = tempExtension.ToLower();
			}

			return extension;
		}

		/// <summary>
		/// Removes tokens from the string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="oldSpacingChar">The spacing char from the given string</param>s
		/// <param name="removeTokens">List with the tokens who should be removed (regex possible)</param>
		/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
		/// <returns>returns the cleaned string</returns>
		internal string RemoveTokens(string input, string oldSpacingChar, List<string> removeTokens, bool addRemovedToList)
		{
			string ret = input;

			if (input == null || oldSpacingChar == null || removeTokens == null || removeTokens.Count == 0)
			{
				// Input error
				ret =  null;
			}
			else
			{
				string spacerEscaped = Regex.Escape(oldSpacingChar.ToString());

				Regex removeRegex = null;
				foreach (string removeToken in removeTokens)
				{
					removeRegex = new Regex(spacerEscaped + removeToken + spacerEscaped, RegexOptions.IgnoreCase);

					// Regex
					while (removeRegex.IsMatch(ret))
					{
						ret = removeRegex.Replace(ret, oldSpacingChar);

						if (addRemovedToList)
						{
							AddRemovedToken(removeToken);
						}
					}

					// Plain
					while (ret.EndsWith(oldSpacingChar + removeToken))
					{
						// Search with spacer but remove without spacer
						removeRegex = new Regex(removeToken, RegexOptions.IgnoreCase);

						ret = removeRegex.Replace(ret, oldSpacingChar);

						if (addRemovedToList)
						{
							AddRemovedToken(removeToken);
						}
					}
				}
			}

			return ret;
		}


		/// <summary>
		/// Replaces tokens from the string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="oldSpacingChar">The spacing char from the given string</param>
		/// <param name="replaceList">List with the tokens who contains the search and the replaxe string (regex possible)</param>
		/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
		/// <returns></returns>
		internal string ReplaceTokens(string input, string oldSpacingChar, List<KeyValuePair<string, string>> replaceList, bool addRemovedToList)
		{
			string ret = input;

			if (replaceList == null || replaceList.Count == 0)
			{
				ret = input;
			}
			else if (input == null || oldSpacingChar == null )
			{
				// Input error
				ret = null;
			}
			else
			{
				string spacerEscaped = Regex.Escape(oldSpacingChar.ToString());

				Regex removeRegex = null;
				foreach (KeyValuePair<string, string> replacePair in replaceList)
				{
					removeRegex = new Regex(spacerEscaped + replacePair.Key + spacerEscaped, RegexOptions.IgnoreCase);

					// Regex
					while (removeRegex.IsMatch(ret))
					{
						ret = removeRegex.Replace(ret, replacePair.Value);

						if (addRemovedToList)
						{
							AddRemovedToken(replacePair.Key);
						}
					}

					// Plain
					while (ret.EndsWith(oldSpacingChar + replacePair.Key))
					{
						// Search with spacer but remove without spacer
						removeRegex = new Regex(replacePair.Key, RegexOptions.IgnoreCase);

						ret = removeRegex.Replace(ret, replacePair.Value);

						if (addRemovedToList)
						{
							AddRemovedToken(replacePair.Key);
						}
					}
				}
			}

			return ret;
		}

		#endregion HelperFunctions
	}
}
