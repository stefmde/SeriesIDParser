
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
	public static class SeriesIDParser
	{
		public static SeriesID Parse( string input )
		{
			string parseString = input.Trim();
			string fullTitle = parseString.Trim();
			string title = string.Empty;
			string episodeTitle = string.Empty;
			bool isSeries = false;
			int season = 0;
			int episode = 0;
			int year = -1;
			Resolutions resolution = Resolutions.UNKNOWN;

			if (parseString.Length >= 5)
			{

				// ###################################################################
				// ### Remove fileextension if apply
				// ###################################################################
				//if (Path.HasExtension(fullTitle))
				//{
				//	string extension = Path.GetExtension(fullTitle);
				//	fullTitle = fullTitle.Replace(extension, "");
				//}


				// ###################################################################
				// ### Try get resolution
				// ###################################################################
				bool resolutionFound = false;


				if (!resolutionFound && Settings.Default.ResMap_UltraHD8K != null)
				{
					foreach (string item in Settings.Default.ResMap_UltraHD8K)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.UltraHD8K_4320p;
						}
					}
				}

				if (!resolutionFound && Settings.Default.ResMap_UltraHD != null)
				{
					foreach (string item in Settings.Default.ResMap_UltraHD)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.UltraHD_2160p;
						}
					}
				}

				if (!resolutionFound && Settings.Default.ResMap_FullHD != null)
				{
					foreach (string item in Settings.Default.ResMap_FullHD)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.FullHD_1080p;
						}
					}
				}

				if (!resolutionFound && Settings.Default.ResMap_HD != null)
				{
					foreach (string item in Settings.Default.ResMap_HD)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.HD_720p;
						}
					}
				}

				if (!resolutionFound && Settings.Default.ResMap_SD != null)
				{
					foreach (string item in Settings.Default.ResMap_SD)
					{
						if (parseString.ToUpper().Contains( item ))
						{
							resolutionFound = true;
							resolution = Resolutions.SD_Any;

							Regex removeRegex = new Regex( item, RegexOptions.IgnoreCase );
							fullTitle = removeRegex.Replace( fullTitle, "" );
						}
					}
				}



				// ###################################################################
				// ### Try get plain title
				// ###################################################################
				string tmpTitle = string.Empty;
				if (parseString.Length > 30)
				{
					tmpTitle = parseString.Substring( parseString.Length - 20, 20 );

					if (tmpTitle.Count( x => x == '-' ) > 0)
					{
						fullTitle = parseString.Substring( 0, parseString.LastIndexOf( "-" ) );
					}
				}

				foreach (string item in Settings.Default.Remove_Token)
				{
					Regex removeRegex = new Regex( item, RegexOptions.IgnoreCase );
					fullTitle = removeRegex.Replace( fullTitle, "" );
				}


				// Get and remove year
				year = GetYear( fullTitle );
				fullTitle = fullTitle.Replace( year.ToString(), "" );


				while (fullTitle.Contains( ".." ))
				{
					fullTitle = fullTitle.Replace( "..", "." );
				}

				fullTitle = fullTitle.Trim();

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
					title = fullTitle.Substring( 0, match.Index - 1 );
					episodeTitle = fullTitle.Substring( match.Index + match.Length + 1 );
					int.TryParse( match.Groups["season"].Value, out season );
					int.TryParse( match.Groups["episode"].Value, out episode );
					isSeries = true;
				}
				else
				{
					// MOVIE
					return new SeriesID( State.OK_SUCCESS, isSeries, fullTitle, year: year, resolution: resolution );
				}
			}
			else
			{
				// ERROR
				return new SeriesID( state: State.ERR_EMPTY_OR_TO_SHORT_ARGUMENT );
			}

			// SERIES
			return new SeriesID( State.OK_SUCCESS, isSeries, title, episodeTitle, year, season, episode, resolution );
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
