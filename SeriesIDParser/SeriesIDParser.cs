
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
		public static SeriesID Parse(string episodeName)
		{
			string fullTitle = episodeName;
			string seriesTitle = string.Empty;
			string episodeTitle = string.Empty;
			int season = 0;
			int episode = 0;
			Resolutions resolution = Resolutions.UNKNOWN;

			if (episodeName.Length > 0)
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
						if (episodeName.ToUpper().Contains(item))
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
						if (episodeName.ToUpper().Contains(item))
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
						if (episodeName.ToUpper().Contains(item))
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
						if (episodeName.ToUpper().Contains(item))
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
						if (episodeName.ToUpper().Contains(item))
						{
							resolutionFound = true;
							resolution = Resolutions.SD_Any;

							Regex removeRegex = new Regex(item, RegexOptions.IgnoreCase);
							fullTitle = removeRegex.Replace(fullTitle, "");
						}
					}
				}



				// ###################################################################
				// ### Try get plain title
				// ###################################################################
				string tmpTitle = string.Empty;
				if (episodeName.Length > 30)
				{
					tmpTitle = episodeName.Substring(episodeName.Length - 20, 20);

					if (tmpTitle.Count(x => x == '-') > 0)
					{
						fullTitle = episodeName.Substring(0, episodeName.LastIndexOf("-"));
					}
				}

				foreach (string item in Settings.Default.Remove_Token)
				{
					Regex removeRegex = new Regex(item, RegexOptions.IgnoreCase);
					fullTitle = removeRegex.Replace(fullTitle, "");
				}

				while (fullTitle.Contains(".."))
				{
					fullTitle = fullTitle.Replace("..", ".");
				}

				if (fullTitle[fullTitle.Length - 1] == '.')
				{
					fullTitle = fullTitle.Substring(0, fullTitle.Length - 1);
				}


				// ###################################################################
				// ### Try get Season and Episode ID
				// ###################################################################
				Regex seRegex = new Regex(@"S(?<season>\d{1,4})E(?<episode>\d{1,4})");
				Match match = seRegex.Match(episodeName.ToUpper());
				if (match.Success)
				{
					seriesTitle = fullTitle.Substring(0, match.Index - 1);
					episodeTitle = fullTitle.Substring(match.Index + match.Length + 1);
					int.TryParse(match.Groups["season"].Value, out season);
					int.TryParse(match.Groups["episode"].Value, out episode);
				}
				else
				{
					return new SeriesID(state: State.ERR_ID_NOT_FOUND);
				}
			}
			else
			{
				return new SeriesID( state: State.ERR_EMPTY_ARGUMENT);
			}

			return new SeriesID(State.OK_SUCCESS, seriesTitle, episodeTitle, season, episode, resolution);
		}

    }
}
