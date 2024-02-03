// MIT License
// 
// Copyright(c) 2016 - 2024
// Stefan Müller, Stefm, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
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


using System.Collections.Generic;
using SeriesIDParser.Worker;

namespace SeriesIDParser.Models;

public interface IParserSettings
{
	/// <summary>
	///     Defines the regex-string that parses the season and episode id from the input string.
	///     Default: 'S(?&lt;season&gt;\d{1,4})(E(?&lt;episode&gt;\d{1,4}))+'
	/// </summary>
	string SeasonAndEpisodeParseRegex { get; set; }

	/// <summary>
	///     Defines the regex-string that parses the year from the input string. Default: '(\d{4})'
	/// </summary>
	string YearParseRegex { get; set; }

	/// <summary>
	///     Defines the prefilled list with the possible extension to guarantee a better detection
	/// </summary>
	List<string> FileExtensions { get; set; }

	/// <summary>
	///     Defines the prefilled list with the possible VideoCodecs
	/// </summary>
	List<string> VideoCodecs { get; set; }

	/// <summary>
	///     Defines the prefilled list with the possible AudioCodecs
	/// </summary>
	List<string> AudioCodecs { get; set; }

	/// <summary>
	///     Defines the prefilled list with the possible spacing chars for the given string to detect the char
	/// </summary>
	List<char> PossibleSpacingChars { get; set; }

	/// <summary>
	///     Defines the prefilled list with the possible spacing chars for the given string to detect the char
	/// </summary>
	char ReleaseGroupSeparator { get; set; }

	/// <summary>
	///     Defines the List of ICoreParser's who are disabled by you. You should only need this if you implement your own
	///     CoreParserModules
	/// </summary>
	ListOfICoreParser DisabledCoreParserModules { get; set; }

	/// <summary>
	///     Defines the prefilled list with the languages for the output. Language parsing is unstable!
	/// </summary>
	List<string> Languages { get; set; }

	/// <summary>
	///     Activates the parsing of the language. Default: false. Language parsing is unstable!
	/// </summary>
	bool ActivateLanguageParser { get; set; }

	/// <summary>
	///     Removes the language token if it is found. Default: false. Language parsing is unstable!
	/// </summary>
	bool RemoveLanguageTokenOnFound { get; set; }

	/// <summary>
	///     Prefilled tokens list who should be removed from input string if the language parser is disabled. Language parsing
	///     is unstable!
	/// </summary>
	List<string> RemoveAndListTokensOnLanguageParserIsDisabled { get; set; }

	/// <summary>
	///     Defines the new spacing char between the tokens in the output string. Default: '.'
	/// </summary>
	char NewSpacingChar { get; set; }

	/// <summary>
	///     Defines the String.Format-string that formats the season for the id-string. Do NOT use larger than four digits per
	///     octet. Default: 'S{0:00}'
	/// </summary>
	string IDStringFormatterSeason { get; set; }

	/// <summary>
	///     Defines the String.Format-string that formats the episode for the id-string. Do NOT use larger than four digits per
	///     octet. Default: 'E{1:00}'
	/// </summary>
	string IDStringFormatterEpisode { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: 'Unknown'
	/// </summary>
	string ResolutionStringUnknown { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: 'SD'
	/// </summary>
	string ResolutionStringSD { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '720p'
	/// </summary>
	string ResolutionStringHD { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '1080p'
	/// </summary>
	string ResolutionStringFullHD { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '2160p'
	/// </summary>
	string ResolutionStringUltraHD { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '4320p'
	/// </summary>
	string ResolutionStringUltraHD8k { get; set; }

	/// <summary>
	///     Defines how the resolution is formatted in the output strings e.g. ParsedString. Default: LowestResolution
	/// </summary>
	ResolutionOutputBehavior ResolutionStringOutput { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D'
	/// </summary>
	string DimensionalString3DAny { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D.HSBS'
	/// </summary>
	string DimensionalString3DHSBS { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D.HOU'
	/// </summary>
	string DimensionalString3DHOU { get; set; }

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: empty
	/// </summary>
	string DimensionalString2DAny { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the HSBS 3D strings
	/// </summary>
	List<string> DetectHsbs3DTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the HOU 3D strings
	/// </summary>
	List<string> DetectHou3DTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the Any 3D strings
	/// </summary>
	List<string> DetectAny3DTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the UHD8k strings
	/// </summary>
	List<string> DetectUltraHD8kTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the UHD4k strings
	/// </summary>
	List<string> DetectUltraHDTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the FullHD strings
	/// </summary>
	List<string> DetectFullHDTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the HD strings
	/// </summary>
	List<string> DetectHDTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list to detect the SD strings
	/// </summary>
	List<string> DetectSDTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list with the tokens who should removed from the title string but re-added to the parsed
	///     string and the DetectedTokens list
	/// </summary>
	List<string> RemoveAndListTokens { get; set; }

	/// <summary>
	///     Defines the prefilled list with the tokens who should removed from the title string
	/// </summary>
	List<string> RemoveWithoutListTokens { get; set; }

	/// <summary>
	///     Defines the empty list with the KeyValuePair to replace (regex, replace)
	/// </summary>
	List<KeyValuePair<string, string>> ReplaceRegexAndListTokens { get; set; }

	/// <summary>
	///     Defines the empty list with the KeyValuePair to replace (regex, replace)
	/// </summary>
	List<KeyValuePair<string, string>> ReplaceRegexWithoutListTokens { get; set; }

	/// <summary>
	///     Enables the internal object cache. This gives a speed up on multiple access of the same object but a little slow
	///     down on single access.
	///     See Enum doc for more info. Default: CacheMode.Advanced
	/// </summary>
	CacheMode CacheMode { get; set; }

	/// <summary>
	///     Caps the object count stored in the cache to reduce the RAM impact. Default: 10.000
	/// </summary>
	int CacheItemCountLimit { get; set; }

	/// <summary>
	///     Throws an exception instead of setting an error state. Default: false
	/// </summary>
	bool ThrowExceptionInsteadOfErrorFlag { get; set; }
}