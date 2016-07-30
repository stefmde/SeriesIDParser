﻿
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeriesIDParser
{
	/// <summary>
	/// Parses series or movie strings to detailed object
	/// </summary>
	public static class SeriesIDParser
	{
		private static List<string> _removedTokens = new List<string>();

		/// <summary>
		/// The primary parsing function
		/// </summary>
		/// <param name="input">The series or movie string who get parsed. Must be atleast five chars</param>
		/// <returns>The SeriesID object that represents the series or movie string</returns>
		public static SeriesID Parse( string input )
		{
			_removedTokens.Clear();
			string parseString = input.Trim();
			string fullTitle = parseString.Trim();
			string title = string.Empty;
			string episodeTitle = string.Empty;
			string fileExtension = string.Empty;
			bool isSeries = false;
			bool warningOrErrorOccurred = false;
			int season = 0;
			int episode = 0;
			int year = -1;
			Resolutions resolution = Resolutions.UNKNOWN;
			State state = new State();

			if (parseString.Length >= 5)
			{

				// ###################################################################
				// ### Find file extension
				// ###################################################################
				foreach (string ext in Settings.Default.FileExtensions)
				{
					if (input.ToUpper().EndsWith(ext.ToUpper()))
					{
						fileExtension = ext.ToLower();
						break;
					}
				}


				// ###################################################################
				// ### Try get resolution
				// ###################################################################
				bool resolutionFound = false;
				Regex removeRegex = null;

				// Try get 8K
				if (!resolutionFound && Settings.Default.ResMap_UltraHD8K != null)
				{
					foreach (string item in Settings.Default.ResMap_UltraHD8K)
					{
						if (parseString.ToUpper().Contains( item.ToUpper()))
						{
							resolutionFound = true;
							resolution = Resolutions.UltraHD8K_4320p;

							removeRegex = new Regex(item, RegexOptions.IgnoreCase);
							fullTitle = removeRegex.Replace(fullTitle, "");
							break;
						}
					}
				}
				
				// Try get 4K
				if (!resolutionFound && Settings.Default.ResMap_UltraHD != null)
				{
					foreach (string item in Settings.Default.ResMap_UltraHD)
					{
						if (parseString.ToUpper().Contains( item.ToUpper()))
						{
							resolutionFound = true;
							resolution = Resolutions.UltraHD_2160p;

							removeRegex = new Regex(item, RegexOptions.IgnoreCase);
							fullTitle = removeRegex.Replace(fullTitle, "");
							break;
						}
					}
				}

				// Try get FullHD
				if (!resolutionFound && Settings.Default.ResMap_FullHD != null)
				{
					foreach (string item in Settings.Default.ResMap_FullHD)
					{
						if (parseString.ToUpper().Contains( item.ToUpper() ))
						{
							resolutionFound = true;
							resolution = Resolutions.FullHD_1080p;

							removeRegex = new Regex(item, RegexOptions.IgnoreCase);
							fullTitle = removeRegex.Replace(fullTitle, "");
							break;
						}
					}
				}

				// Try get HD
				if (!resolutionFound && Settings.Default.ResMap_HD != null)
				{
					foreach (string item in Settings.Default.ResMap_HD)
					{
						if (parseString.ToUpper().Contains( item.ToUpper()))
						{
							resolutionFound = true;
							resolution = Resolutions.HD_720p;

							removeRegex = new Regex(item, RegexOptions.IgnoreCase);
							fullTitle = removeRegex.Replace(fullTitle, "");
							break;
						}
					}
				}

				// Try get SD
				if (!resolutionFound && Settings.Default.ResMap_SD != null)
				{
					foreach (string item in Settings.Default.ResMap_SD)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.SD_Any;

							removeRegex = new Regex( item, RegexOptions.IgnoreCase );
							fullTitle = removeRegex.Replace( fullTitle, "" );
							break;
						}
					}
				}



				// ###################################################################
				// ### Try get plain title
				// ###################################################################

				// remove hoster token
				string tmpTitle = string.Empty;
				if (fullTitle.Length > 30)
				{
					tmpTitle = fullTitle.Substring(fullTitle.Length - 20, 20 );

					if (tmpTitle.Count( x => x == '-' ) > 0)
					{
						fullTitle = fullTitle.Substring( 0, fullTitle.LastIndexOf( "-" ) );
					}
				}

				// Remove tokens
				removeRegex = null;
				foreach (string removeToken in Settings.Default.RemoveToken)
				{
					removeRegex = new Regex(removeToken, RegexOptions.IgnoreCase );

					if (removeRegex.IsMatch(fullTitle))
					{
						fullTitle = removeRegex.Replace(fullTitle, "");
						AddRemovedToken(removeToken);
					}
				}

				// Sort removedTokensList
				_removedTokens = _removedTokens.OrderBy(x => x).ToList();

				// remove fileextension
				removeRegex = new Regex(fileExtension, RegexOptions.IgnoreCase);
				fullTitle = removeRegex.Replace(fullTitle, "");

				// Get and remove year
				year = GetYear( fullTitle );
				if (year >= 1900)
				{
					fullTitle = fullTitle.Replace(year.ToString(), "");
				}

				// Remove double dots
				while (fullTitle.Contains(".."))
				{
					fullTitle = fullTitle.Replace("..", ".");
				}

				// Remove spaces
				if (Settings.Default.ReplaceSpaces)
				{
					while (fullTitle.Contains(" "))
					{
						fullTitle = fullTitle.Replace(" ", ".");
					}
				}

				// Remove .-.
				if (Settings.Default.ReplaceDotHyphenDot)
				{
					while (fullTitle.Contains(".-."))
					{
						fullTitle = fullTitle.Replace(".-.", ".");
					}
				}

				// Remove starting and trailing spaces
				fullTitle = fullTitle.Trim();

				// Remove trailing dot
				if (fullTitle[fullTitle.Length - 1] == '.')
				{
					fullTitle = fullTitle.Substring( 0, fullTitle.Length - 1 );
				}


				// ###################################################################
				// ### Try get Season and Episode ID
				// ###################################################################
				Regex seRegex = new Regex( @"S(?<season>\d{1,4})E(?<episode>\d{1,4})" );
				Match match = seRegex.Match( parseString.ToUpper() );
				if (match.Success)
				{
					if (match.Index > 0)
					{
						title = fullTitle.Substring(0, match.Index - 1);
					}
					else
					{
						state |= State.WARN_ERR_OR_WARN_OCCURRED | State.WARN_NO_TITLE_FOUND;
						warningOrErrorOccurred = true;
					}

					// Get Episode title if there is one
					int episodeTitleStartIndex = match.Index + match.Length + 1;
					if (fullTitle.Length > episodeTitleStartIndex)
					{
						episodeTitle = fullTitle.Substring( episodeTitleStartIndex );
					}
					else
					{
						episodeTitle = null;
					}
					
					int.TryParse( match.Groups["season"].Value, out season );
					int.TryParse( match.Groups["episode"].Value, out episode );
					isSeries = true;
				}
				else
				{
					// MOVIE
					if (!warningOrErrorOccurred)
					{
						state |= State.OK_SUCCESS;
					}

					return new SeriesID(state, isSeries, input, fullTitle, year: year, resolution: resolution, removedTokens: _removedTokens, fileExtension: fileExtension );
				}
			}
			else
			{
				// ERROR
				state |= State.ERR_EMPTY_OR_TO_SHORT_ARGUMENT;
				return new SeriesID( state: state);
			}

			// SERIES
			if (!warningOrErrorOccurred)
			{
				state |= State.OK_SUCCESS;
			}

			return new SeriesID(state, isSeries, input, title, episodeTitle, year, season, episode, resolution, removedTokens: _removedTokens, fileExtension: fileExtension);
		}


		private static void AddRemovedToken(string token)
		{
			if (!_removedTokens.Any(x => x.ToLower() == token.ToLower()))
			{
				_removedTokens.Add(token);
			}
		}

		private static int GetYear( string title )
		{
			int year = -1;

			Regex yearRegex = new Regex( @"(\d{4})" );
			MatchCollection matches = yearRegex.Matches( title );

			foreach (Match match in matches)
			{
				if (match.Success)
				{
					int tempYear = -1;

					if (int.TryParse( match.Value, out tempYear ))
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

	}
}
