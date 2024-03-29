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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SeriesIDParser.Models;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Worker;

internal static class HelperWorker
{
	/// <summary>
	///     Gets all CoreParsers as active Interface
	/// </summary>
	/// <returns></returns>
	internal static List<ICoreParser> GetAllCoreParsers( List<ICoreParser> disabledCoreParsers = null )
	{
		var interfaceType = typeof(ICoreParser);
		var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany( s => s.GetTypes() ).Where( x => interfaceType.IsAssignableFrom( x ) && !x.IsInterface && x.IsClass );
		List<ICoreParser> coreParserModules = [];

		foreach (var type in types)
		{
			if (disabledCoreParsers == null || disabledCoreParsers.All( x => x.GetType() != type ))
			{
				coreParserModules.Add( (ICoreParser)Activator.CreateInstance( type ) );
			}
		}

		coreParserModules = coreParserModules.OrderByDescending( x => x.Priority ).ToList();

		return coreParserModules;
	}

	/// <summary>
	///     Gets the Resolutions depending on the found Resolutions and the ParserSettings
	/// </summary>
	/// <param name="settings"></param>
	/// <param name="res"></param>
	/// <returns></returns>
	internal static string GetResolutionString( IParserSettings settings, List<ResolutionsMap> res )
	{
		var output = ResolutionsMap.Unknown.ToString() + settings.NewSpacingChar;

		if (res != null && res.Count > 0 && !(res.Count == 1 && res.Contains( ResolutionsMap.Unknown )))
		{
			res.Remove( ResolutionsMap.Unknown );
			res = res.OrderBy( x => x ).ToList();

			if (settings.ResolutionStringOutput == ResolutionOutputBehavior.HighestResolution)
			{
				// Highest
				if (res.Contains( ResolutionsMap.UltraHD8K_4320p ))
				{
					output = settings.ResolutionStringUltraHD8k;
				}
				else if (res.Contains( ResolutionsMap.UltraHD_2160p ))
				{
					output = settings.ResolutionStringUltraHD;
				}
				else if (res.Contains( ResolutionsMap.FullHD_1080p ))
				{
					output = settings.ResolutionStringFullHD;
				}
				else if (res.Contains( ResolutionsMap.HD_720p ))
				{
					output = settings.ResolutionStringHD;
				}
				else if (res.Contains( ResolutionsMap.SD_Any ))
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
				if (res.Contains( ResolutionsMap.SD_Any ))
				{
					output = settings.ResolutionStringSD;
				}
				else if (res.Contains( ResolutionsMap.HD_720p ))
				{
					output = settings.ResolutionStringHD;
				}
				else if (res.Contains( ResolutionsMap.FullHD_1080p ))
				{
					output = settings.ResolutionStringFullHD;
				}
				else if (res.Contains( ResolutionsMap.UltraHD_2160p ))
				{
					output = settings.ResolutionStringUltraHD;
				}
				else if (res.Contains( ResolutionsMap.UltraHD8K_4320p ))
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

				foreach (var itm in res)
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

		output = output.TrimEnd( settings.NewSpacingChar );

		return output;
	}

	/// <summary>
	///     Tries to get the year from a given string. Have to be between now and 1900
	/// </summary>
	/// <param name="title">String who should be analyzed</param>
	/// <param name="regex">The regex string who parses the year</param>
	/// <returns>-1 or the found year</returns>
	internal static int GetYear( string title, string regex )
	{
		var year = -1;

		var yearRegex = new Regex( regex );
		var matches = yearRegex.Matches( title );

		foreach (Match match in matches)
		{
			if (!match.Success)
			{
				continue;
			}

			if (int.TryParse( match.Value, out var tempYear ) && tempYear >= 1900 && tempYear <= DateTime.Now.Year)
			{
				year = tempYear;
				break;
			}
		}

		return year;
	}

	private struct CharRating
	{
		internal char Char;
		internal int Count;
	}

	/// <summary>
	///     Tries to get the spacing char from a given string
	/// </summary>
	/// <param name="input">The string who should be analyzed</param>
	/// <param name="settings">ParserSettings to get the PossibleSpacingChars</param>
	/// <returns>returns an empty string or the spacing char</returns>
	internal static char GetSpacingChar( string input, IParserSettings settings )
	{
		var foundChar = new char();

		var charRating = settings.PossibleSpacingChars.Select( item => new CharRating { Char = item, Count = 0 } ).ToList();

		for (var i = 0; i < charRating.Count; i++)
		{
			charRating[i] = new CharRating { Char = charRating[i].Char, Count = input.Count( f => f == charRating[i].Char ) };
		}

		charRating = charRating.OrderBy( x => x.Count ).ToList();

		var sum = charRating.Sum( x => x.Count );
		var largestOne = charRating.LastOrDefault().Count;

		// if largest one count is larger than the others additive -> 51% +
		if (largestOne > sum - largestOne)
		{
			foundChar = charRating.LastOrDefault().Char;
		}

		return foundChar;
	}

	/// <summary>
	///     Tries to get the file extension from a given string
	/// </summary>
	/// <param name="input">The string who should be analyzed</param>
	/// <param name="extensions">The list with the possible extensions</param>
	/// <returns>returns null or the found extension</returns>
	internal static string GetFileExtension( string input, IEnumerable<string> extensions )
	{
		var extension = string.Empty;
		if (string.IsNullOrEmpty( input ))
		{
			return extension;
		}

		var tempExtension = Path.GetExtension( input );

		if (extensions.Any( x => x.Equals( tempExtension, StringComparison.OrdinalIgnoreCase ) ))
		{
			extension = tempExtension.ToLower();
		}

		return extension;
	}

	/// <summary>
	///     find and removes tokens from the ref input string
	/// </summary>
	/// <param name="fullTitle">The string who should be analyzed</param>
	/// <param name="oldSpacingChar">The spacing char from the given string</param>
	/// <param name="removeTokens">List with the tokens who should be removed (regex possible)</param>
	/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
	/// <param name="removeTokensFromInput">Should the found token should be removed from the ref input?</param>
	/// <returns>returns the cleaned string</returns>
	internal static IEnumerable<string> FindTokens( ref string fullTitle, string oldSpacingChar, List<string> removeTokens, bool addRemovedToList, bool removeTokensFromInput = true )
	{
		var ret = fullTitle;
		List<string> foundTokens = [];

		if (fullTitle == null || oldSpacingChar == null || removeTokens == null || removeTokens.Count == 0)
		{
			// Input error
			ret = string.Empty;
		}
		else
		{
			var spacerEscaped = Regex.Escape( oldSpacingChar );

			foreach (var removeToken in removeTokens)
			{
				var removeRegex = new Regex( spacerEscaped + removeToken + spacerEscaped, RegexOptions.IgnoreCase );

				// Regex
				while (removeRegex.IsMatch( ret ))
				{
					ret = removeRegex.Replace( ret, oldSpacingChar );

					if (addRemovedToList)
					{
						foundTokens.Add( removeToken );
					}
				}

				// Plain
				while (ret.EndsWith( oldSpacingChar + removeToken, StringComparison.OrdinalIgnoreCase ))
				{
					// Search with spacer but remove without spacer
					removeRegex = new Regex( removeToken, RegexOptions.IgnoreCase );

					ret = removeRegex.Replace( ret, oldSpacingChar );

					if (addRemovedToList)
					{
						foundTokens.Add( removeToken );
					}
				}
			}
		}

		fullTitle = ret;

		return foundTokens;
	}

	/// <summary>
	///     Replaces tokens from the string
	/// </summary>
	/// <param name="input">The string who should be analyzed</param>
	/// <param name="oldSpacingChar">The spacing char from the given string</param>
	/// <param name="replaceList">List with the tokens who contains the search and the replace string (regex possible)</param>
	/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
	/// <returns></returns>
	internal static List<string> ReplaceTokens( ref string input, string oldSpacingChar, List<KeyValuePair<string, string>> replaceList, bool addRemovedToList )
	{
		var ret = input;
		List<string> foundTokens = [];
		if (replaceList == null || !replaceList.Any())
		{
			ret = input;
		}
		else if (input == null || oldSpacingChar == null)
		{
			// Input error
			ret = string.Empty;
		}
		else
		{
			var spacerEscaped = Regex.Escape( oldSpacingChar );
			foreach (var replacePair in replaceList)
			{
				var removeRegex = new Regex( spacerEscaped + replacePair.Key + spacerEscaped, RegexOptions.IgnoreCase );

				// Regex
				while (removeRegex.IsMatch( ret ))
				{
					ret = removeRegex.Replace( ret, replacePair.Value );
					if (addRemovedToList)
					{
						foundTokens.Add( replacePair.Key );
					}
				}

				// Plain
				while (ret.EndsWith( oldSpacingChar + replacePair.Key, StringComparison.OrdinalIgnoreCase ))
				{
					// Search with spacer but remove without spacer
					removeRegex = new Regex( replacePair.Key, RegexOptions.IgnoreCase );
					ret = removeRegex.Replace( ret, replacePair.Value );
					if (addRemovedToList)
					{
						foundTokens.Add( replacePair.Key );
					}
				}
			}
		}

		input = ret;
		return foundTokens;
	}

	/// <summary>
	///     Function Adds a Unknown Resolution mark or removes it if needed
	/// </summary>
	/// <param name="resolutions"></param>
	/// <returns></returns>
	internal static List<ResolutionsMap> MaintainUnknownResolution( List<ResolutionsMap> resolutions )
	{
		if (resolutions.Count == 0)
		{
			resolutions.Add( ResolutionsMap.Unknown );
		}
		else
		{
			resolutions.Remove( ResolutionsMap.Unknown );
		}

		return resolutions;
	}

	/// <summary>
	///     Get Resolutions from fullTitle string
	/// </summary>
	/// <param name="ps"></param>
	/// <param name="oldSpacer"></param>
	/// <param name="fullTitle"></param>
	/// <returns></returns>
	internal static List<ResolutionsMap> GetResolutions( IParserSettings ps, char oldSpacer, ref string fullTitle )
	{
		List<ResolutionsMap> tempFoundResolutions = [];
		List<ResolutionsMap> foundResolutions = [];

		// Try get 8K
		tempFoundResolutions.AddRange( GetResolutionByResMap( ps.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, oldSpacer, ref fullTitle ) );

		// Try get 4K
		tempFoundResolutions.AddRange( GetResolutionByResMap( ps.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, oldSpacer, ref fullTitle ) );

		// Try get FullHD
		tempFoundResolutions.AddRange( GetResolutionByResMap( ps.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, oldSpacer, ref fullTitle ) );

		// Try get HD
		tempFoundResolutions.AddRange( GetResolutionByResMap( ps.DetectHDTokens, ResolutionsMap.HD_720p, oldSpacer, ref fullTitle ) );

		// Try get SD
		tempFoundResolutions.AddRange( GetResolutionByResMap( ps.DetectSDTokens, ResolutionsMap.SD_Any, oldSpacer, ref fullTitle ) );

		foreach (var res in tempFoundResolutions)
		{
			if (!foundResolutions.Contains( res ))
			{
				foundResolutions.Add( res );
			}
		}

		return foundResolutions;
	}

	/// <summary>
	///     Gets the resolutions of a given string
	/// </summary>
	/// <param name="resolutionMap">The token that could match the given resolution</param>
	/// <param name="res">The given resolution wo is targeted by the ResMap tokens</param>
	/// <param name="oldSpacingChar">the spacing char in the given string</param>
	/// <param name="fullTitle">The given string who should be analyzed</param>
	private static List<ResolutionsMap> GetResolutionByResMap( List<string> resolutionMap, ResolutionsMap res, char oldSpacingChar, ref string fullTitle )
	{
		var spacer = Regex.Escape( oldSpacingChar.ToString() );
		List<ResolutionsMap> foundResolutions = [];

		foreach (var item in resolutionMap)
		{
			var removeRegex = new Regex( spacer + item + spacer, RegexOptions.IgnoreCase );

			if (!removeRegex.IsMatch( fullTitle ))
			{
				continue;
			}

			// Search with spacer but remove without spacer
			removeRegex = new Regex( item, RegexOptions.IgnoreCase );
			foundResolutions.Add( res );

			fullTitle = removeRegex.Replace( fullTitle, "" );
		}

		return foundResolutions;
	}

	/// <summary>
	///     Gets the DimensionalType from fullTitle
	/// </summary>
	/// <param name="ps"></param>
	/// <param name="oldSpacer"></param>
	/// <param name="fullTitle"></param>
	/// <returns></returns>
	internal static DimensionalType GetDimensionalType( IParserSettings ps, char oldSpacer, ref string fullTitle )
	{
		List<DimensionalType> tempDimensionalTypes = [];
		DimensionalType foundDimensionalType;

		// Try get 3D HSBS
		tempDimensionalTypes.AddRange( GetDimensionalTypeByResMap( ps.DetectHsbs3DTokens, DimensionalType.Dimension_3DHSBS, oldSpacer, ref fullTitle ) );

		// Try get 3D HOU
		tempDimensionalTypes.AddRange( GetDimensionalTypeByResMap( ps.DetectHou3DTokens, DimensionalType.Dimension_3DHOU, oldSpacer, ref fullTitle ) );

		// Try get 3D Any
		if (tempDimensionalTypes.Count == 0)
		{
			tempDimensionalTypes.AddRange( GetDimensionalTypeByResMap( ps.DetectAny3DTokens, DimensionalType.Dimension_3DAny, oldSpacer, ref fullTitle ) );
		}

		if (tempDimensionalTypes.Count == 0)
		{
			foundDimensionalType = DimensionalType.Dimension_2DAny;
		}
		else if (tempDimensionalTypes.Count == 1)
		{
			foundDimensionalType = tempDimensionalTypes.LastOrDefault();
		}
		else
		{
			foundDimensionalType = DimensionalType.Dimension_3DAny;
		}

		return foundDimensionalType;
	}

	/// <summary>
	///     Gets the DimensionalType By a given string
	/// </summary>
	/// <param name="dimensionalTypeMap"></param>
	/// <param name="type"></param>
	/// <param name="oldSpacingChar"></param>
	/// <param name="fullTitle"></param>
	/// <returns></returns>
	private static List<DimensionalType> GetDimensionalTypeByResMap( List<string> dimensionalTypeMap, DimensionalType type, char oldSpacingChar, ref string fullTitle )
	{
		var spacer = Regex.Escape( oldSpacingChar.ToString() );
		List<DimensionalType> foundDimensionalType = [];

		foreach (var item in dimensionalTypeMap)
		{
			var removeRegex = new Regex( spacer + item + spacer, RegexOptions.IgnoreCase );

			if (!removeRegex.IsMatch( fullTitle ))
			{
				continue;
			}

			// Search with spacer but remove without spacer
			removeRegex = new Regex( item, RegexOptions.IgnoreCase );
			foundDimensionalType.Add( type );

			fullTitle = removeRegex.Replace( fullTitle, "" );
		}

		return foundDimensionalType;
	}

	/// <summary>
	/// </summary>
	/// <param name="ps"></param>
	/// <param name="oldSpacer"></param>
	/// <param name="fullTitle"></param>
	/// <returns></returns>
	internal static List<string> RemoveTokens( IParserSettings ps, char oldSpacer, ref string fullTitle )
	{
		List<string> tempFoundTokens = [];
		List<string> foundResolutions = [];

		tempFoundTokens.AddRange( FindTokens( ref fullTitle, oldSpacer.ToString(), ps.RemoveAndListTokens, true ) );
		tempFoundTokens.AddRange( FindTokens( ref fullTitle, oldSpacer.ToString(), ps.RemoveWithoutListTokens, false ) );

		tempFoundTokens.AddRange( ReplaceTokens( ref fullTitle, oldSpacer.ToString(), ps.ReplaceRegexAndListTokens, true ) );
		tempFoundTokens.AddRange( ReplaceTokens( ref fullTitle, oldSpacer.ToString(), ps.ReplaceRegexWithoutListTokens, false ) );

		foreach (var item in tempFoundTokens)
		{
			if (!foundResolutions.Any( x => x.Equals( item, StringComparison.OrdinalIgnoreCase ) ))
			{
				foundResolutions.Add( item );
			}
		}

		return foundResolutions;
	}

	/// <summary>
	///     Trys to get the release group
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <param name="fileExtension">The file extension is there is one without the dot</param>
	/// <returns>the release group string if there is one or string.Empty</returns>
	internal static string GetReleaseGroup( string fullTitle, ParserSettings ps, string fileExtension )
	{
		var group = string.Empty;

		if (fullTitle.Length <= 30)
		{
			return group;
		}

		var tmpTitle = fullTitle.Substring( fullTitle.Length - 20, 20 );

		if (tmpTitle.Count( x => x == ps.ReleaseGroupSeparator ) > 0)
		{
			var separatorIndex = fullTitle.LastIndexOf( ps.ReleaseGroupSeparator );
			group = string.IsNullOrEmpty( fileExtension ) ? fullTitle.Substring( separatorIndex + 1 ) : fullTitle.Substring( separatorIndex + 1 ).Replace( fileExtension, "" );
		}

		return group.Trim();
	}

	/// <summary>
	///     Removes the ReleaseGroup from the input string
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <param name="releaseGroup">The found ReleaseGroup</param>
	/// <returns>The output string without the ReleaseGroup</returns>
	internal static string RemoveReleaseGroup( string fullTitle, ParserSettings ps, string releaseGroup )
	{
		var retVal = fullTitle;
		var separatorIndex = fullTitle.LastIndexOf( ps.ReleaseGroupSeparator );

		if (separatorIndex > 30)
		{
			retVal = fullTitle.Substring( 0, separatorIndex ).Trim();
		}

		return retVal;
	}

	/// <summary>
	///     Trys to get the episode title from the input string
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <returns>The episode title if one is fround or string.Empty</returns>
	internal static string GetEpisodeTitle( IParserSettings ps, string fullTitle )
	{
		var seRegex = new Regex( ps.SeasonAndEpisodeParseRegex );
		var match = seRegex.Match( fullTitle.ToUpper() );
		var episodeTitle = string.Empty;

		if (!IsSeries( ps, fullTitle ))
		{
			return episodeTitle;
		}

		var episodeTitleStartIndex = match.Index + match.Length + 1;

		if (fullTitle.Length > episodeTitleStartIndex)
		{
			episodeTitle = fullTitle.Substring( episodeTitleStartIndex );
		}

		return episodeTitle;
	}

	internal static string GetTitle( IParserSettings ps, char oldSpacingChar, string fullTitle )
	{
		var seRegex = new Regex( ps.SeasonAndEpisodeParseRegex );
		var match = seRegex.Match( fullTitle.ToUpper() );
		var title = string.Empty;

		if (IsSeries( ps, fullTitle ))
		{
			if (match.Index > 0)
			{
				title = fullTitle.Substring( 0, match.Index - 1 ).Replace( oldSpacingChar.ToString(), ps.NewSpacingChar.ToString() );
			}
		}
		else
		{
			title = fullTitle;
		}

		return title;
	}

	/// <summary>
	///     Gets the season ID from a given string. Default/Error: -1
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <returns>The season id as int</returns>
	internal static int GetSeasonID( IParserSettings ps, string fullTitle )
	{
		var season = -1;

		var seRegex = new Regex( ps.SeasonAndEpisodeParseRegex );
		var match = seRegex.Match( fullTitle.ToUpper() );

		if (IsSeries( ps, fullTitle ))
		{
			int.TryParse( match.Groups["season"].Value, out season );
		}

		return season;
	}

	/// <summary>
	///     Gets the EpisodeID's from a given string as int list. Default/Error: -1
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <returns>Episode's as int list</returns>
	internal static List<int> GetEpisodeIDs( IParserSettings ps, string fullTitle )
	{
		List<int> episodes = [];

		var seRegex = new Regex( ps.SeasonAndEpisodeParseRegex );
		var match = seRegex.Match( fullTitle.ToUpper() );

		if (IsSeries( ps, fullTitle ))
		{
			var cc = match.Groups["episode"].Captures;
			var temp = -1;

			episodes.AddRange( from object item in cc where int.TryParse( item.ToString(), NumberStyles.Number, new CultureInfo( CultureInfo.CurrentCulture.ToString() ), out temp ) select temp );
		}

		return episodes;
	}

	/// <summary>
	///     Checks if a given string is a series string
	/// </summary>
	/// <param name="ps">Currently used settings for the parser</param>
	/// <param name="fullTitle">The fullTitle who worked on</param>
	/// <returns>Bool if is series or not</returns>
	internal static bool IsSeries( IParserSettings ps, string fullTitle )
	{
		var isSeries = false;
		var seRegex = new Regex( ps.SeasonAndEpisodeParseRegex );
		var match = seRegex.Match( fullTitle.ToUpper() );

		if (match.Success)
		{
			isSeries = true;
		}

		return isSeries;
	}

	/// <summary>
	///     Gets all series and movie files in a directory
	/// </summary>
	/// <param name="path"></param>
	/// <param name="parserSettings"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	internal static IEnumerable<string> GetSeriesAndMovieFiles( string path, ParserSettings parserSettings = null, SearchOption searchOption = SearchOption.AllDirectories )
	{
		List<string> files = [];

		parserSettings ??= new ParserSettings();

		if (path == null)
		{
			return files;
		}

		files = Directory.GetFiles( path, "*.*", searchOption ).Select( d => d ).Where( f => parserSettings.FileExtensions.Select( d => d.ToLower() ).Contains( Path.GetExtension( f ).ToLower() ) )
						.ToList();

		return files;
	}

	/// <summary>
	///     Gets all series and movie file infos in a directory
	/// </summary>
	/// <param name="path"></param>
	/// <param name="parserSettings"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	internal static IEnumerable<FileInfo> GetSeriesAndMovieFileInfos( string path, IParserSettings parserSettings = null, SearchOption searchOption = SearchOption.AllDirectories )
	{
		List<FileInfo> files = [];

		parserSettings ??= new ParserSettings();

		if (path == null)
		{
			return files;
		}

		var di = new DirectoryInfo( path );

		files = di.GetFiles( "*.*", searchOption ).Select( d => d ).Where( f => parserSettings.FileExtensions.Select( d => d.ToLower() ).Contains( f.Extension.ToLower() ) ).ToList();

		return files;
	}

	public static string GetDimensionalTypeString( IParserSettings parserSettings, DimensionalType dimensionalType )
	{
		return dimensionalType switch
		{
			DimensionalType.Dimension_3DAny => parserSettings.DimensionalString3DAny,
			DimensionalType.Dimension_2DAny => parserSettings.DimensionalString2DAny,
			DimensionalType.Dimension_3DHSBS => parserSettings.DimensionalString3DHSBS,
			DimensionalType.Dimension_3DHOU => parserSettings.DimensionalString3DHou,
			_ => string.Empty
		};
	}
}