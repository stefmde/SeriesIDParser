﻿// MIT License
// 
// Copyright(c) 2016 - 2024
// Stefan (StefmDE) Müller, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
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

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Models;

/// <summary>
///     This object is part of the SeriesIDParser and contains the result for a single parsed string
/// </summary>
public class ParserResult : IParserResult
{
	#region PropertiesCache
	private string _cacheFullTitle;
	private string _cacheParsedString;
	private string _cacheIDString;
	#endregion PropertiesCache

	#region ctor
	internal ParserResult( string originalString, IParserSettings parserSettings, string audioCodec, string videoCodec, TimeSpan processingDuration, List<ResolutionsMap> resolutions, int season,
							List<int> episodes, int year, char detectedOldSpacingChar, Exception exception, bool isSeries, List<string> removedTokens, State state,
							List<CoreParserModuleStateResult> moduleStates, string fileExtension, FileInfo fileInfo, string title, string episodeTitle, string releaseGroup,
							DimensionalType dimensionalType )
	{
		ParserSettingsUsed = parserSettings;
		AudioCodec = audioCodec;
		VideoCodec = videoCodec;
		Episodes = episodes;
		EpisodeTitle = episodeTitle;
		ProcessingDuration = processingDuration;
		ReleaseGroup = releaseGroup;
		Resolutions = resolutions;
		Season = season;
		Year = year;
		OldSpacingChar = detectedOldSpacingChar;
		Exception = exception;
		IsSeries = isSeries;
		RemovedTokens = removedTokens;
		State = state;
		ModuleStates = moduleStates;
		FileExtension = fileExtension;
		OriginalString = originalString;
		Title = title;
		DimensionalType = dimensionalType;
		Is3D = (int)DimensionalType > 1;
		FileInfo = fileInfo;
	}

	/// <summary>
	///     Use only for unit tests. Do NOT set to public
	/// </summary>
	/// <summary>
	///     Returns the full series string for Series, title for movies. Property is cached. string.Empty on error
	/// </summary>
	public string FullTitle
	{
		get
		{
			if (State != State.Success && State != State.Notice)
			{
				return string.Empty;
			}

			if (!string.IsNullOrEmpty( _cacheFullTitle ))
			{
				return _cacheFullTitle;
			}

			var sb = new StringBuilder();

			if (IsSeries)
			{
				if (!string.IsNullOrEmpty( Title ))
				{
					sb.Append( Title );
				}

				if (!string.IsNullOrEmpty( IDString ))
				{
					sb.Append( ParserSettingsUsed.NewSpacingChar );
					sb.Append( IDString );
				}

				if (!string.IsNullOrEmpty( _episodeTitle ))
				{
					sb.Append( ParserSettingsUsed.NewSpacingChar );
					sb.Append( _episodeTitle );
				}

				_cacheFullTitle = sb.ToString();

				return _cacheFullTitle;
			}

			return Title;
		}
	}

	/// <summary>
	///     Contains the string that was computed by the parser. Property is cached. string.Empty on error
	/// </summary>
	public string ParsedString
	{
		get
		{
			if (State == State.Success || State == State.Notice)
			{
				if (!string.IsNullOrEmpty( _cacheParsedString ))
				{
					return _cacheParsedString;
				}

				var sb = new StringBuilder();
				sb.Append( FullTitle );

				if (_year > -1)
				{
					sb.Append( ParserSettingsUsed.NewSpacingChar );
					sb.Append( _year );
				}

				sb.Append( ParserSettingsUsed.NewSpacingChar );
				sb.Append( HelperWorker.GetResolutionString( ParserSettingsUsed, _resolutions ) );

				var dimensionalType = HelperWorker.GetDimensionalTypeString( ParserSettingsUsed, DimensionalType );
				if (!string.IsNullOrEmpty( dimensionalType ))
				{
					sb.Append( ParserSettingsUsed.NewSpacingChar );
					sb.Append( dimensionalType );
				}

				if (RemovedTokens != null && RemovedTokens.Any())
				{
					foreach (var remToken in RemovedTokens)
					{
						sb.Append( ParserSettingsUsed.NewSpacingChar + remToken );
					}
				}

				if (!string.IsNullOrEmpty( FileExtension ))
				{
					sb.Append( FileExtension );
				}

				_cacheParsedString = sb.ToString();

				return _cacheParsedString;
			}

			return string.Empty;
		}
	}

	/// <summary>
	///     Contains the ID-String of a series e.g. S01E05. Property is cached. string.Empty on error
	/// </summary>
	public string IDString
	{
		get
		{
			if ((State == State.Success || State == State.Notice) && IsSeries)
			{
				if (!string.IsNullOrEmpty( _cacheIDString ))
				{
					return _cacheIDString;
				}

				var sb = new StringBuilder();
				sb.Append( string.Format( ParserSettingsUsed.IDStringFormatterSeason, _season ) );

				foreach (var episode in _episodes)
				{
					sb.Append( string.Format( ParserSettingsUsed.IDStringFormatterEpisode, episode ) );
				}

				_cacheIDString = sb.ToString();

				return _cacheIDString;
			}

			return string.Empty;
		}
	}

	/// <summary>
	///     Shows if a Episode is a MultiEpisode with more than one Episode in one file. Default: false
	/// </summary>
	public bool IsMultiEpisode => (State == State.Success || State == State.Notice) && Episodes.Count() > 1;
	#endregion PropertiesComputed

	#region PropertiesDefault
	/// <summary>
	///     Contains the ParserSettings object used to generate this result object
	/// </summary>
	public IParserSettings ParserSettingsUsed { get; internal set; }

	/// <summary>
	///     Contains the state information provided by each module
	/// </summary>
	public List<CoreParserModuleStateResult> ModuleStates { get; internal set; }

	/// <summary>
	///     Contains tokens which are removed by the parser as string list
	/// </summary>
	public List<string> RemovedTokens { get; private set; }

	/// <summary>
	///     Contains the FileInfo that is given to the parser
	/// </summary>
	public FileInfo FileInfo { get; internal set; }

	/// <summary>
	///     Contains the file-extension or string.Empty
	/// </summary>
	public string FileExtension { get; private set; }

	/// <summary>
	///     Contains the string that is given to the parser
	/// </summary>
	public string OriginalString { get; private set; }

	/// <summary>
	///     Contains the char who are detected as the old spacing char
	/// </summary>
	public char OldSpacingChar { get; private set; }

	/// <summary>
	///     Contains the series title or the movie name, depends on IsSeries
	/// </summary>
	public string Title { get; private set; }

	/// <summary>
	///     Contains the state of the object e.g. OK_SUCCESS
	/// </summary>
	public State State { get; private set; }

	/// <summary>
	///     Contains the dimensionalType of the object e.g. Dimension_3DHou
	/// </summary>
	public DimensionalType DimensionalType { get; private set; }

	/// <summary>
	///     Contains the Exception if any occurs. Default: null
	/// </summary>
	public Exception Exception { get; private set; }

	/// <summary>
	///     Specifies if the object contains a series or a movie. Default: false
	/// </summary>
	public bool IsSeries { get; private set; }

	/// <summary>
	///     Specifies if the movie is a 3D series or a movie. Default: false
	/// </summary>
	public bool Is3D { get; private set; }

	private readonly string _audioCodec;

	/// <summary>
	///     Contains the AudioCodec if one is found. string.Empty on error
	/// </summary>
	public string AudioCodec
	{
		get => State == State.Success || State == State.Notice ? _audioCodec : string.Empty;
		private init => _audioCodec = value ?? string.Empty;
	}

	private readonly string _videoCodec;

	/// <summary>
	///     Contains the VideoCodec if one is found. string.Empty on error
	/// </summary>
	public string VideoCodec
	{
		get => State == State.Success || State == State.Notice ? _videoCodec : null;
		private init => _videoCodec = value ?? string.Empty;
	}

	private readonly string _episodeTitle;

	/// <summary>
	///     Contains the episode title if object state is series. string.Empty on error
	/// </summary>
	public string EpisodeTitle
	{
		get => (State == State.Success || State == State.Notice) && IsSeries ? _episodeTitle : string.Empty;
		private init => _episodeTitle = value;
	}

	private readonly int _season;

	/// <summary>
	///     Contains the season id if object state is series. -1 on error
	/// </summary>
	public int Season
	{
		get => (State == State.Success || State == State.Notice) && IsSeries ? _season : -1;
		private init => _season = value;
	}

	private readonly List<int> _episodes;

	/// <summary>
	///     Contains the episode id if object state is series.
	/// </summary>
	public List<int> Episodes
	{
		get => (State == State.Success || State == State.Notice) && IsSeries ? _episodes : [];
		private init => _episodes = value != null ? [..value] : [];
	}

	private readonly int _year;

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	public int Year
	{
		get => State == State.Success || State == State.Notice ? _year : -1;
		private init => _year = value;
	}

	private readonly TimeSpan _processingDuration;

	/// <summary>
	///     Returns the year of the episode or movie if contained, otherwise -1
	/// </summary>
	public TimeSpan ProcessingDuration
	{
		get => State == State.Success || State == State.Notice ? _processingDuration : new TimeSpan();
		private init => _processingDuration = value;
	}

	private readonly List<ResolutionsMap> _resolutions;

	/// <summary>
	///     Returns the resolution as enum. UNKNOWN on error
	/// </summary>
	public List<ResolutionsMap> Resolutions
	{
		get => State == State.Success || State == State.Notice ? _resolutions : [ResolutionsMap.Unknown];
		private init => _resolutions = [..value];
	}

	private readonly string _releaseGroup;

	/// <summary>
	///     Contains the release group string if contained in the source. string.Empty on error
	/// </summary>
	public string ReleaseGroup
	{
		get => State == State.Success || State == State.Notice ? _releaseGroup : string.Empty;
		private init => _releaseGroup = value;
	}
	#endregion PropertiesDefault

	#region ClassFunctions
	/// <summary>
	/// </summary>
	/// <returns>FullTitle and resolution. string.Empty on error</returns>
	public override string ToString()
	{
		if (State == State.Success || State == State.Notice)
		{
			return FullTitle + " -- " + HelperWorker.GetResolutionString( ParserSettingsUsed, _resolutions );
		}

		return string.Empty;
	}
	#endregion ClassFunctions
}