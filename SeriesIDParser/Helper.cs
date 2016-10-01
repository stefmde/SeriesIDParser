
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeriesIDParser
{
	internal static class Helper
	{


		/// <summary>
		/// Gets the Resolutions depending on the found Resolutions and the ParserSettings
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="res"></param>
		/// <returns></returns>
		internal static string GetResolutionString(ParserSettings settings, IList<ResolutionsMap> res)
		{
			// TODO: Only 72% Covered
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
		/// Tries to get the year from a given string. Have to be between now and 1900
		/// </summary>
		/// <param name="title">String who should be analized</param>
		/// <returns>-1 or the found year</returns>
		internal static int GetYear(string title)
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



		//internal T FailSafeProperties<T>(T value, params bool[] checks)
		//{
		//	if (checks.Contains(false) || false == _state.HasFlag(State.OK_SUCCESS))
		//	{
		//		return default(T);
		//		//TODO throw exception
		//	}

		//	return value;

		//}

		internal struct CharRating
		{
			internal char Char;
			internal int Count;
		}

		/// <summary>
		/// Tries to get the spacing char from a given string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <returns>returns an empty string or the spacing char</returns>
		internal static char GetSpacingChar(string input, ParserSettings settings)
		{
			char foundChar = new char();

			List<CharRating> charRating = new List<CharRating>();
			foreach (char item in settings.PossibleSpacingChars)
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
		internal static string GetFileExtension(string input, List<string> extensions)
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
		/// find and removes tokens from the ref input string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="oldSpacingChar">The spacing char from the given string</param>s
        /// <param name="removeTokens">List with the tokens who should be removed (regex possible)</param>
        /// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
        /// <param name="removeTokensFromInput">Should the found token should be removed from the ref input?</param>
		/// <returns>returns the cleaned string</returns>
		internal static List<string> FindTokens(ref string input, string oldSpacingChar, List<string> removeTokens, bool addRemovedToList, bool removeTokensFromInput = true)
		{
			// TODO: Only 95% Covered
			string ret = input;
			List<string> foundTokens = new List<string>();

			if (input == null || oldSpacingChar == null || removeTokens == null || removeTokens.Count == 0)
			{
				// Input error
				ret = string.Empty;
			}
			else
			{
				string spacerEscaped = Regex.Escape(oldSpacingChar.ToString());

				foreach (string removeToken in removeTokens)
				{
                    Regex removeRegex = removeRegex = new Regex(spacerEscaped + removeToken + spacerEscaped, RegexOptions.IgnoreCase);

					// Regex
					while (removeRegex.IsMatch(ret))
					{
						ret = removeRegex.Replace(ret, oldSpacingChar);

						if (addRemovedToList)
						{
							foundTokens.Add(removeToken);
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
							foundTokens.Add(removeToken);
						}
					}
				}
			}

			input = ret;

			return foundTokens;
		}












		/// <summary>
		/// Replaces tokens from the string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="oldSpacingChar">The spacing char from the given string</param>
		/// <param name="replaceList">List with the tokens who contains the search and the replaxe string (regex possible)</param>
		/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
		/// <returns></returns>
		internal static List<string> ReplaceTokens(ref string input, string oldSpacingChar, List<KeyValuePair<string, string>> replaceList, bool addRemovedToList)
		{
			// TODO: Only 72% Covered
			string ret = input;
			List<string> foundTokens = new List<string>();


			if (replaceList == null || (replaceList != null && replaceList.Count == 0))
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
				string spacerEscaped = Regex.Escape(oldSpacingChar.ToString());

				foreach (KeyValuePair<string, string> replacePair in replaceList)
				{
                    Regex removeRegex = removeRegex = new Regex(spacerEscaped + replacePair.Key + spacerEscaped, RegexOptions.IgnoreCase);

					// Regex
					while (removeRegex.IsMatch(ret))
					{
						ret = removeRegex.Replace(ret, replacePair.Value);

						if (addRemovedToList)
						{
							foundTokens.Add(replacePair.Key);
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
							foundTokens.Add(replacePair.Key);
						}
					}
				}
			}

			input = ret;

			return foundTokens;
		}





		/// <summary>
		/// Gets the resolutions of a given string
		/// </summary>
		/// <param name="resolutionMap">The token that could match the given resolution</param>
		/// <param name="res">The given resolution wo is targeted by the ResMap tokens</param>
		/// <param name="oldSpacingChar">the spacing char in the given string</param>
		/// <param name="fullTitle">The given string who should be analized</param>
		internal static List<ResolutionsMap> GetResolutionByResMap(List<string> resolutionMap, ResolutionsMap res, char oldSpacingChar, ref string fullTitle)
		{
			string spacer = Regex.Escape(oldSpacingChar.ToString());
			List<ResolutionsMap> foundResolutions = new List<ResolutionsMap>();

			foreach (string item in resolutionMap)
			{
                Regex removeRegex = new Regex(spacer + item + spacer, RegexOptions.IgnoreCase);

				if (removeRegex.IsMatch(fullTitle))
				{
					// Search with spacer but remove without spacer
					removeRegex = new Regex(item, RegexOptions.IgnoreCase);
					foundResolutions.Add(res);

					fullTitle = removeRegex.Replace(fullTitle, "");
				}
			}

			return foundResolutions;
		}



        /// <summary>
        /// Function Adds a Unknown Resolution mark or removes it if needed
        /// </summary>
        internal static List<ResolutionsMap> MaintainUnknownResolution(List<ResolutionsMap> resolutions)
        {
            if (resolutions.Count == 0)
            {
                resolutions.Add(ResolutionsMap.UNKNOWN);
            }
            else
            {
                resolutions.Remove(ResolutionsMap.UNKNOWN);
            }

            return resolutions;
        }



		internal static List<ResolutionsMap> GetResolutions(ParserSettings ps, char oldSpacer, ref string fullTitle)
		{
			List<ResolutionsMap> tempFoundResolutions = new List<ResolutionsMap>();
			List<ResolutionsMap> foundResolutions = new List<ResolutionsMap>();

			// Try get 8K
			tempFoundResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, oldSpacer, ref fullTitle));

			// Try get 4K
			tempFoundResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, oldSpacer, ref fullTitle));

			// Try get FullHD
			tempFoundResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, oldSpacer, ref fullTitle));

			// Try get HD
			tempFoundResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectHDTokens, ResolutionsMap.HD_720p, oldSpacer, ref fullTitle));

			// Try get SD
			tempFoundResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectSDTokens, ResolutionsMap.SD_Any, oldSpacer, ref fullTitle));

			foreach (ResolutionsMap res in tempFoundResolutions)
			{
				if (!foundResolutions.Contains(res))
				{
					foundResolutions.Add(res);
				}
			}

			return foundResolutions;
		}




	    internal static List<string> RemoveTokens(ParserSettings ps, char oldSpacer, ref string fullTitle)
		{
			List<string> tempFoundResolutions = new List<string>();
			List<string> foundResolutions = new List<string>();

			tempFoundResolutions.AddRange(Helper.FindTokens(ref fullTitle, oldSpacer.ToString(), ps.RemoveAndListTokens, true));
			tempFoundResolutions.AddRange(Helper.FindTokens(ref fullTitle, oldSpacer.ToString(), ps.RemoveWithoutListTokens, false));

			tempFoundResolutions.AddRange(Helper.ReplaceTokens(ref fullTitle, oldSpacer.ToString(), ps.ReplaceRegexAndListTokens, true));
			tempFoundResolutions.AddRange(Helper.ReplaceTokens(ref fullTitle, oldSpacer.ToString(), ps.ReplaceRegexWithoutListTokens, false));

			foreach (string item in tempFoundResolutions)
			{
                if (!foundResolutions.Any(x => x.Equals(item, StringComparison.OrdinalIgnoreCase)))
				{
					foundResolutions.Add(item);
				}
			}

			return foundResolutions;
		}



		internal static string GetReleaseGroup(ref string fullTitle, ParserSettings ps, string fileExtension)
		{
			string group = string.Empty;

			string tmpTitle = string.Empty;
			if (fullTitle.Length > 30)
			{
				tmpTitle = fullTitle.Substring(fullTitle.Length - 20, 20);

				if (tmpTitle.Count(x => x == ps.ReleaseGroupSeperator) > 0)
				{
					int seperatorIndex = fullTitle.LastIndexOf(ps.ReleaseGroupSeperator);
					group = fullTitle.Substring(seperatorIndex + 1).Replace("." + fileExtension, "").Trim();
					fullTitle = fullTitle.Substring(0, seperatorIndex).Trim();
				}
			}

			return group;
		}









        internal static string GetEpisodeTitle(ParserSettings ps, string fullTitle)
	    {
            Regex seRegex = new Regex(ps.SeasonAndEpisodeParseRegex);
            Match match = seRegex.Match(fullTitle.ToUpper());
	        string episodeTitle = string.Empty;

            if (IsSeries(ps, fullTitle))
            {
                int episodeTitleStartIndex = match.Index + match.Length + 1;

                if (fullTitle.Length > episodeTitleStartIndex)
                {
                    episodeTitle = fullTitle.Substring(episodeTitleStartIndex);
                }
            }

	        return episodeTitle;
	    }


	    internal static string GetTitle(ParserSettings ps, char oldSpacingChar, string fullTitle, ref bool warningOrError, ref State state)
	    {
            Regex seRegex = new Regex(ps.SeasonAndEpisodeParseRegex);
            Match match = seRegex.Match(fullTitle.ToUpper());
	        string title = string.Empty;

            if (IsSeries(ps, fullTitle))
            {
                if (match.Index > 0)
                {
                    title = fullTitle.Substring(0, match.Index - 1).Replace(oldSpacingChar.ToString(), ps.NewSpacingChar.ToString());
                }
                else
                {
                    state |= State.WARN_ERR_OR_WARN_OCCURRED | State.WARN_NO_TITLE_FOUND;
                    warningOrError = true;
                }
            }
            else
            {
                title = fullTitle;

                if (!warningOrError)
                {
                    state |= State.OK_SUCCESS;
                }
            }

            return title;
	    }


        internal static int GetSeasonID(ParserSettings ps, string fullTitle)
        {
            int season = -1;

            Regex seRegex = new Regex(ps.SeasonAndEpisodeParseRegex);
            Match match = seRegex.Match(fullTitle.ToUpper());

            if (IsSeries(ps, fullTitle))
            {
                int.TryParse(match.Groups["season"].Value, out season);
            }

            return season;
        }


        internal static int GetEpisodeID(ParserSettings ps, string fullTitle)
        {
            int episode = -1;
            List<int> episodes = GetEpisodeIDs(ps, fullTitle);

            if (episodes.Count > 0)
            {
                episode = episodes.FirstOrDefault();
            }

            return episode;
        }
        
        
        internal static List<int> GetEpisodeIDs(ParserSettings ps, string fullTitle)
        {
            List<int> episodes = new List<int>();

            Regex seRegex = new Regex(ps.SeasonAndEpisodeParseRegex);
            Match match = seRegex.Match(fullTitle.ToUpper());

            if (IsSeries(ps, fullTitle))
            {
                CaptureCollection cc = match.Groups["episode"].Captures;
                int temp = -1;
                
                foreach (object item in cc)
                {
                    if (int.TryParse(item.ToString(), NumberStyles.Number,
                        new CultureInfo(CultureInfo.CurrentCulture.ToString()), out temp))
                    {
                        episodes.Add(temp);
                    }
                }
            }

            return episodes;
        }




        internal static bool IsSeries(ParserSettings ps, string fullTitle)
	    {
	        bool isSeries = false;
            Regex seRegex = new Regex(ps.SeasonAndEpisodeParseRegex);
            Match match = seRegex.Match(fullTitle.ToUpper());

            if (match.Success)
            {
                isSeries = true;
            }

            return isSeries;
	    }

	}
}
