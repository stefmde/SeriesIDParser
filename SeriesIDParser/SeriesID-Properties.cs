
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
	public partial class SeriesID
	{
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


		private string _releaseGroup;
		/// <summary>
		/// Contains the release group string if countained in the source. null on error
		/// </summary>
		public string ReleaseGroup
		{
			get
			{
				if (_state.HasFlag(State.OK_SUCCESS))
				{
					return _releaseGroup;
				}
				else
				{
					return null;
				}
			}

			internal set { _releaseGroup = value; }
		}
		#endregion Properties

	}
}
