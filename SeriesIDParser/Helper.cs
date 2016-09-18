
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
	public static class Helper
	{


		/// <summary>
		/// Gets the Resolutions depending on the found Resolutions and the ParserSettings
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="res"></param>
		/// <returns></returns>
		internal static string GetResolutionString(ParserSettings settings, IList<ResolutionsMap> res)
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
			public char Char;
			public int Count;
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
		/// Removes tokens from the string
		/// </summary>
		/// <param name="input">The string who should be analized</param>
		/// <param name="oldSpacingChar">The spacing char from the given string</param>s
		/// <param name="removeTokens">List with the tokens who should be removed (regex possible)</param>
		/// <param name="addRemovedToList">Should the removed tokens be added to the removed list?</param>
		/// <returns>returns the cleaned string</returns>
		internal static List<string> RemoveTokens(ref string input, string oldSpacingChar, List<string> removeTokens, bool addRemovedToList)
		{
			string ret = input;
			List<string> foundTokens = new List<string>();

			if (input == null || oldSpacingChar == null || removeTokens == null || removeTokens.Count == 0)
			{
				// Input error
				ret = null;
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
							foundTokens.Add(removeToken);
							//AddRemovedToken(removeToken);
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
							//AddRemovedToken(removeToken);
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
			string ret = input;
			List<string> foundTokens = new List<string>();


			if (replaceList == null || (replaceList != null && replaceList.Count == 0))
			{
				ret = input;
			}
			else if (input == null || oldSpacingChar == null)
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
							foundTokens.Add(replacePair.Key);
							//AddRemovedToken(replacePair.Key);
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
							//AddRemovedToken(replacePair.Key);
						}
					}
				}
			}

			input = ret;

			return foundTokens;
		}



	}
}
