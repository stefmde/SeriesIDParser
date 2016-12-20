
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
using SeriesIDParser.Worker;

[assembly:InternalsVisibleTo("SeriesIDParser.Test")]
namespace SeriesIDParser.Models
{
	public class ParserResult
	{
		#region Fields
		private readonly ParserSettings _parserSettings;
		#endregion Fields

		#region PropertiesCache
		private string _cacheFullTitle;
		private string _cacheParsedString;
		private string _cacheIDString;
		#endregion PropertiesCache

		#region ctor
		internal ParserResult(string originalString, ParserSettings parserSettings, string audioCodec, string videoCodec, TimeSpan processingDuration, IEnumerable<ResolutionsMap> resolutions, int season, IEnumerable<int> episodes, int year, char detectedOldSpacingChar, Exception exception, bool isSeries, IEnumerable<string> removedTokens, State state, string fileExtension, string title, string episodeTitle, string releaseGroup)
		{
			_parserSettings = parserSettings;
			AudioCodec = audioCodec;
			VideoCodec = videoCodec;
			Episodes = episodes;
			EpisodeTitle = episodeTitle;
			ProcessingDuration = processingDuration;
			ReleaseGroup = releaseGroup;
			Resolutions = resolutions;
			Season = season;
			Year = year;
			DetectedOldSpacingChar = detectedOldSpacingChar;
			Exception = exception;
			IsSeries = isSeries;
			RemovedTokens = removedTokens;
			State = state;
			FileExtension = fileExtension;
			OriginalString = originalString;
			Title = title;
		}

		/// <summary>
		/// Use only for unit tests. Do NOT set to public
		/// </summary>
		//internal ParserResult()
		//{

		//}
		#endregion

		#region Properties
		#region PropertiesComputed
		/// <summary>
		/// Returns the full series string for Series, title for movies. Property is cached. string.Empty on error
		/// </summary>
		public string FullTitle
		{
			get
			{
				if (!State.HasFlag(State.OkSuccess))
				{
					return string.Empty;
				}

				if (!string.IsNullOrEmpty(_cacheFullTitle))
				{
					return _cacheFullTitle;
				}

				StringBuilder sb = new StringBuilder();

				if (IsSeries)
				{
					if (!string.IsNullOrEmpty(Title))
					{
						sb.Append(Title);
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

					_cacheFullTitle = sb.ToString();

					return _cacheFullTitle;
				}
				else
				{
					return Title;
				}
			}
		}

		//internal string _parsedString;
		/// <summary>
		/// Contains the string that was computed by the parser. Property is cached. string.Empty on error
		/// </summary>
		public string ParsedString
		{
			get
			{
				if (State.HasFlag(State.OkSuccess))
				{
					if (!string.IsNullOrEmpty(_cacheParsedString))
					{
						return _cacheParsedString;
					}

					StringBuilder sb = new StringBuilder();
					sb.Append(FullTitle);

					if (_year > -1)
					{
						sb.Append(_parserSettings.NewSpacingChar);
						sb.Append(_year);
					}

					sb.Append(_parserSettings.NewSpacingChar);
					sb.Append(HelperWorker.GetResolutionString(_parserSettings, _resolutions));

					if (RemovedTokens != null && RemovedTokens.Any())
					{
						foreach (string remToken in RemovedTokens)
						{
							sb.Append(_parserSettings.NewSpacingChar + remToken);
						}
					}

					if (!string.IsNullOrEmpty(FileExtension))
					{
						sb.Append(FileExtension);
					}

					_cacheParsedString = sb.ToString();

					return _cacheParsedString;
				}
				else
				{
					return string.Empty;
				}
			}
		}


		/// <summary>
		/// Contains the ID-String of a series e.g. S01E05. Property is cached. string.Empty on error
		/// </summary>
		public string IDString
		{
			get
			{
				if (State.HasFlag(State.OkSuccess) && IsSeries)
				{
					if (!string.IsNullOrEmpty(_cacheIDString))
					{
						return _cacheIDString;
					}

					StringBuilder sb = new StringBuilder();
					sb.Append(string.Format(_parserSettings.IDStringFormaterSeason, _season));

					foreach (int episode in _episodes)
					{
						sb.Append(string.Format(_parserSettings.IDStringFormaterEpisode, episode));
					}

					_cacheIDString = sb.ToString();

					return _cacheIDString;
				}
				else
				{
					return string.Empty;
				}
			}
		}


		/// <summary>
		/// Shows if a Episode is a MultiEpisode with more than one Episode in one file. Default: false
		/// </summary>
		public bool IsMultiEpisode => State.HasFlag(State.OkSuccess) && Episodes.Count() > 1;
		#endregion PropertiesComputed

		#region PropertiesDefault
		/// <summary>
		/// Contains the ParserSettings object used to generate this result object
		/// </summary>
		public ParserSettings ParserSettingsUsed => _parserSettings;


		/// <summary>
		/// Contains tokens whoi are removed by the parser as string list
		/// </summary>
		public IEnumerable<string> RemovedTokens { get; private set; }


		/// <summary>
		/// Contains the FileInfo that is given to the parser
		/// </summary>
		public FileInfo FileInfo { get; internal set; }


		/// <summary>
		/// Contains the file-extension or string.Empty
		/// </summary>
		public string FileExtension { get; private set; } = string.Empty;


		/// <summary>
		/// Contains the string that is given to the parser
		/// </summary>
		public string OriginalString { get; private set; } = string.Empty;


		/// <summary>
		/// Contains the char who are detected as the old spacing char
		/// </summary>
		public char DetectedOldSpacingChar { get; private set; }


		/// <summary>
		/// Contains the series title or the movie name, depends on IsSeries
		/// </summary>
		public string Title { get; private set; } = string.Empty;


		/// <summary>
		/// Contains the state of the object e.g. OK_SUCCESS
		/// </summary>
		public State State { get; private set; } = State.Unknown;


		/// <summary>
		/// Contains the Exception if any occours. Default: null
		/// </summary>
		public Exception Exception { get; private set; } = null;


		/// <summary>
		/// Specifies if the object contains a series or a movie. Default: false
		/// </summary>
		public bool IsSeries { get; private set; }


		private string _audioCodec;
		/// <summary>
		/// Contains the audiocodec if one is found. string.Empty on error
		/// </summary>
		public string AudioCodec
		{
			get
			{
				return State.HasFlag(State.OkSuccess) ? _audioCodec : string.Empty;
			}

			private set { _audioCodec = value ?? String.Empty; }
		}


		private string _videoCodec;
		/// <summary>
		/// Contains the videocodec if one is found. string.Empty on error
		/// </summary>
		public string VideoCodec
		{
			get
			{
				return State.HasFlag(State.OkSuccess) ? _videoCodec : null;
			}

			private set { _videoCodec = value ?? String.Empty; }
		}


		private string _episodeTitle;
		/// <summary>
		/// Contains the eposide title if object state is series. string.Empty on error
		/// </summary>
		public string EpisodeTitle
		{
			get
			{
				//return FailSafeProperties<string>(_episodeTitle);
				return State.HasFlag(State.OkSuccess) && IsSeries ? _episodeTitle : string.Empty;
			}

			private set { _episodeTitle = value; }
		}


		private int _season;
		/// <summary>
		/// Contains the season id if object state is series. -1 on error
		/// </summary>
		public int Season
		{
			get { return State.HasFlag(State.OkSuccess) && IsSeries ? _season : -1; }

			private set { _season = value; }
		}


		private List<int> _episodes;
		/// <summary>
		/// Contains the eposide id if object state is series.
		/// </summary>
		public IEnumerable<int> Episodes
		{
			get
			{
				return State.HasFlag(State.OkSuccess) && IsSeries ? _episodes : new List<int>();
			}

			private set { _episodes = value != null ? new List<int>(value) : new List<int>(); }
		}


		private int _year;
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public int Year
		{
			get { return State.HasFlag(State.OkSuccess) ? _year : -1; }

			private set { _year = value; }
		}


		private TimeSpan _processingDuration;
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		public TimeSpan ProcessingDuration
		{
			get
			{
				return State.HasFlag(State.OkSuccess) ? _processingDuration : new TimeSpan();
			}

			private set { _processingDuration = value; }
		}


		private List<ResolutionsMap> _resolutions;
		/// <summary>
		/// Returns the resolution as enum. UNKNOWN on error
		/// </summary>
		public IEnumerable<ResolutionsMap> Resolutions
		{
			get
			{
				return State.HasFlag(State.OkSuccess) ? _resolutions : new List<ResolutionsMap>() { ResolutionsMap.Unknown };
			}

			private set { _resolutions = new List<ResolutionsMap>(value); }
		}


		private string _releaseGroup;
		/// <summary>
		/// Contains the release group string if countained in the source. string.Empty on error
		/// </summary>
		public string ReleaseGroup
		{
			get
			{
				return State.HasFlag(State.OkSuccess) ? _releaseGroup : string.Empty;
			}

			private set { _releaseGroup = value; }
		}
		#endregion PropertiesDefault
		#endregion Properties

		#region ClassFunctions
		/// <summary>
		/// </summary>
		/// <returns>FullTitle and resolution. string.Empty on error</returns>
		public override string ToString()
		{
			if (State.HasFlag(State.OkSuccess))
			{
				return FullTitle + " -- " + HelperWorker.GetResolutionString(_parserSettings, _resolutions);
			}
			else
			{
				return string.Empty;
			}
		}
		#endregion ClassFunctions
	}
}
