
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	public partial class SeriesID
	{
		// TODO implement better failsafe
		#region Properties
		#region Computed
		/// <summary>
        /// Returns the full series string for Series, title for movies and string.Empty on error
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
							sb.Append(IDString);
						}

						if (!string.IsNullOrEmpty(_episodeTitle))
						{
							sb.Append(_parserSettings.NewSpacingChar);
							sb.Append(_episodeTitle);
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
                    return string.Empty;
				}
			}
		}

		//private string _parsedString;
		/// <summary>
        /// Contains the string that was computed by the parser. string.Empty on error
		/// </summary>
		public string ParsedString
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{
					StringBuilder sb = new StringBuilder();
					sb.Append(FullTitle);

					if (_year > -1)
					{
						sb.Append(_parserSettings.NewSpacingChar);
						sb.Append(_year);
					}

					sb.Append(_parserSettings.NewSpacingChar);
					sb.Append(Helper.GetResolutionString(_parserSettings, _resolutions));

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
					return string.Empty;
				}
			}
		}


		/// <summary>
        /// Contains the ID-String of a series e.g. S01E05. string.Empty on error
		/// </summary>
		public string IDString
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS) && _isSeries)
				{
                    StringBuilder sb = new StringBuilder();
				    sb.Append(string.Format(_parserSettings.IDStringFormaterSeason, _season));

				    foreach (int episode in _episodes)
				    {
                        sb.Append(string.Format(_parserSettings.IDStringFormaterEpisode, episode));
				    }

					return sb.ToString();
				}
				else
				{
                    return string.Empty;
				}
			}
		}
		#endregion Computed

		#region Default

        // TODO dynamic based on episode count
        private bool _isMultiEpisode = false;
        /// <summary>
        /// Shows if a Episode is a MultiEpisode with more than one Episode in one file. Default: false
        /// </summary>
        public bool IsMultiEpisode
        {
            get
            {
                return _state.HasFlag(State.OK_SUCCESS) && _isMultiEpisode;
            }

            internal set { _isMultiEpisode = value; }
        }


		private string _audioCodec = string.Empty;
		/// <summary>
        /// Contains the audiocodec if one is found. string.Empty on error
		/// </summary>
		public string AudioCodec
		{
			get {
			    return _state.HasFlag(State.OK_SUCCESS) ? _audioCodec : null;
			}

		    internal set { _audioCodec = value; }
		}


		private string _videoCodec = string.Empty;
		/// <summary>
        /// Contains the videocodec if one is found. string.Empty on error
		/// </summary>
		public string VideoCodec
		{
			get {
			    return _state.HasFlag(State.OK_SUCCESS) ? _videoCodec : null;
			}

		    internal set { _videoCodec = value; }
		}


		private List<string> _removedTokens = new List<string>();
		/// <summary>
		/// Contains tokens whoi are removed by the parser as string list
		/// </summary>
		public IEnumerable<string> RemovedTokens
		{
			get { return _removedTokens; }
			internal set { _removedTokens = new List<string>(value); }
		}


		private string _fileExtension = string.Empty;
		/// <summary>
        /// Contains the file-extension or string.Empty
		/// </summary>
		public string FileExtension
		{
			get { return _fileExtension; }
			internal set { _fileExtension = value; }
		}


        private string _originalString = string.Empty;
		/// <summary>
		/// Contains the string that is given to the parser
		/// </summary>
		public string OriginalString
		{
			get { return _originalString; }
			internal set { _originalString = value; }
		}


		private char _detectedOldSpacingChar;
		/// <summary>
		/// Contains the char who are detected as the old spacing char
		/// </summary>
		public char DetectedOldSpacingChar
		{
			get { return _detectedOldSpacingChar; }
			internal set { _detectedOldSpacingChar = value; }
		}


        private string _title = string.Empty;
		/// <summary>
		/// Contains the series title or the movie name, depends on IsSeries
		/// </summary>
		public string Title
		{
			get { return _title; }
			internal set { _title = value; }
		}


		private State _state = State.UNKNOWN;
		/// <summary>
		/// Contains the state of the object e.g. OK_SUCCESS
		/// </summary>
		public State State
		{
			get { return _state; }
			internal set { _state = value; }
		}


		private string _episodeTitle = string.Empty;
		/// <summary>
        /// Contains the eposide title if object state is series. string.Empty on error
		/// </summary>
		public string EpisodeTitle
		{
			get
			{
			    //return FailSafeProperties<string>(_episodeTitle);
                return _state.HasFlag(State.OK_SUCCESS) && _isSeries ? _episodeTitle : string.Empty;
			}

		    internal set { _episodeTitle = value; }
		}


		private int _season = -1;
		/// <summary>
		/// Contains the season id if object state is series. -1 on error
		/// </summary>
		public int Season
		{
			get { return _state.HasFlag(State.OK_SUCCESS) && _isSeries ? _season : -1; }

		    internal set { _season = value; }
		}


        // Todo remove in future releases
        [Obsolete("Use _episodes which can contain more than one episode. Will be removed in future releases")]
        private int _episode = -1;
        /// <summary>
        /// Contains the eposide id if object state is series. -1 on error
        /// </summary>
        [Obsolete("Use Episodes which can contain more than one episode. Will be removed in future releases")]
        public int Episode
        {
            get { return _state.HasFlag(State.OK_SUCCESS) && _isSeries ? _episode : -1; }

            internal set { _episode = value; }
        }


        private List<int> _episodes = new List<int>();
        /// <summary>
        /// Contains the eposide id if object state is series.
        /// </summary>
        public IEnumerable<int> Episodes
        {
            get
            {
                return _state.HasFlag(State.OK_SUCCESS) && _isSeries ? _episodes : new List<int>();
            }

            internal set { _episodes = new List<int>(value); }
        }


		private bool _isSeries;
		/// <summary>
		/// Specifies if the object contains a series or a movie. Default: false
		/// </summary>
		public bool IsSeries
		{
			get { return _isSeries; }
			internal set { _isSeries = value; }
		}


		private int _year = -1;
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public int Year
		{
			get { return _state.HasFlag(State.OK_SUCCESS) ? _year : -1; }

		    internal set { _year = value; }
		}


		private TimeSpan _processingDuration = new TimeSpan();
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public TimeSpan ProcessingDuration
		{
			get {
			    return _state.HasFlag(State.OK_SUCCESS) ? _processingDuration : new TimeSpan();
			}

		    internal set { _processingDuration = value; }
		}


		private List<ResolutionsMap> _resolutions = new List<ResolutionsMap>();
		/// <summary>
		/// Returns the resolution as enum. UNKNOWN on error
		/// </summary>
        public IEnumerable<ResolutionsMap> Resolutions
		{
			get {
			    return _state.HasFlag(State.OK_SUCCESS) ? _resolutions : new List<ResolutionsMap>() { ResolutionsMap.UNKNOWN };
			}

		    internal set { _resolutions = new List<ResolutionsMap>(value); }
		}


		private Exception _exception = null;
		/// <summary>
		/// Contains the Exception if any occours. Default: null
		/// </summary>
		public Exception Exception
		{
			get { return _exception; }
			internal set { _exception = value; }
		}


		private string _releaseGroup;
		/// <summary>
        /// Contains the release group string if countained in the source. string.Empty on error
		/// </summary>
		public string ReleaseGroup
		{
			get {
			    return _state.HasFlag(State.OK_SUCCESS) ? _releaseGroup : string.Empty;
			}

		    internal set { _releaseGroup = value; }
		}
		#endregion Default
		#endregion Properties

	}
}
