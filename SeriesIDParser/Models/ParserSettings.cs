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


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SeriesIDParser.Worker;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Models;

/// <summary>
///     The settings who can give to the SeriesIDParser to modify the behavior. Object is prefilled with the default values
///     and list entries
/// </summary>
public class ParserSettings : IParserSettings
{
	/// <summary>
	///     Prefill ctor
	/// </summary>
	/// <param name="preFillLists">Set to true if the lists should be prefilled with the default values</param>
	public ParserSettings( bool preFillLists = false )
	{
		if (!preFillLists)
		{
			return;
		}

		// ### Resolution Detection
		DetectUltraHD8kTokens = ["8k", "4320p"];
		DetectUltraHDTokens = ["NetflixUHD", "UHD", "2160p", "Ultra.HD", "UltraHD"];
		DetectFullHDTokens = ["NetflixHD", "FullHD", "1080p", "1080i"];
		DetectHDTokens = ["HDTV", "720p", "HD"];
		DetectSDTokens = ["DVDR", "DVDRIP", "DVD", "SD"];

		// ### Dimensional Detection
		DetectAny3DTokens = ["3D"];
		DetectHou3DTokens = ["HOU", "H-OU", "3DHOU", "3D-HOU"];
		DetectHsbs3DTokens = ["HSBS", "H-SBS", "3DHSBS", "3D-HSBS"];

		// ### FileExtensions
		FileExtensions =
		[
			".3G2",
			".3GP",
			".AMV",
			".ASF",
			".AVI",
			".DRC",
			".F4A",
			".F4B",
			".F4P",
			".F4V",
			".FLV",
			".GIF",
			".GIFV",
			".M2TS",
			".M2V",
			".M4P",
			".M4V",
			".MKV",
			".MNG",
			".MOV",
			".MP2",
			".MP4",
			".MPE",
			".MPEG",
			".MPG",
			".MPV",
			".MTS",
			".MXF",
			".NSV",
			".OGG",
			".OGV",
			".QT",
			".RM",
			".RMVB",
			".ROQ",
			".SVI",
			".VOB",
			".WEBM",
			".WMV",
			".YUV"
		];

		// ### Remove 
		RemoveAndListTokens =
		[
			"DUBBED",
			"SYNCED",
			"iTunes",
			"BluRay",
			"WebHD",
			"Netflix",
			"WebRIP",
			"DOKU",
			"EXTENDED",
			"UNRATED",
			"UNCUT",
			"AmazonHD",
			"German",
			"Remastered",
			"HDRip",
			"Remux",
			"10bit",
			"PAL",
			"7.1",
			"5.1",
			"BT2020",
			"BT.2020",
			"HDR",
			"SDR",
			"TrueHD",
			"FS",
			"DD20",
			"DD51",
			"DD5.1",
			"DD+51",
			"35mm"
		];

		RemoveWithoutListTokens =
		[
			" ",
			"MIRROR",
			"WEB",
			"BD9",
			"Web-DL",
			"DL",
			"HDDVDRip",
			"THEATRICAL",
			"RETAIL",
			"3D",
			"READ.NFO"
		];

		// ### Parsing
		VideoCodecs =
		[
			"x264",
			"h264",
			"x265",
			"AVC",
			"XviD",
			"FFmpeg",
			"VP7",
			"VP8",
			"VP9",
			"MPEG-4",
			"MPEG-2",
			"MPEG4",
			"MPEG2",
			"HEVC"
		];

		AudioCodecs = ["DTSHD", "DTS", "AAC", "MP3", "MPEG3", "MPEG-3", "TrueHD.Atmos", "TrueHD", "AC3D", "AC3"];

		// ### Other Stuff
		PossibleSpacingChars = ['.', ',', '-', ' ', '+', '*'];
		DisabledCoreParserModules = [];
	}

	internal ParserSettings()
	{
	}

	#region Properties
	// ### Parsing
	// ############################################################

	#region Parsing
	/// <summary>
	///     Defines the regex-string that parses the season and episode id from the input string.
	///     Default: 'S(?&lt;season&gt;\d{1,4})(E(?&lt;episode&gt;\d{1,4}))+'
	/// </summary>
	public string SeasonAndEpisodeParseRegex { get; set; } = @"S(?<season>\d{1,4})(E(?<episode>\d{1,4}))+";

	/// <summary>
	///     Defines the regex-string that parses the year from the input string. Default: '(\d{4})'
	/// </summary>
	public string YearParseRegex { get; set; } = @"(\d{4})";

	/// <summary>
	///     Defines the prefilled list with the possible extension to guarantee a better detection
	/// </summary>
	public List<string> FileExtensions { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list with the possible VideoCodecs
	/// </summary>
	public List<string> VideoCodecs { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list with the possible AudioCodecs
	/// </summary>
	public List<string> AudioCodecs { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list with the possible spacing chars for the given string to detect the char
	/// </summary>
	public List<char> PossibleSpacingChars { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list with the possible spacing chars for the given string to detect the char
	/// </summary>
	public char ReleaseGroupSeparator { get; set; } = '-';

	/// <summary>
	///     Defines the List of ICoreParser's who are disabled by you. You should only need this if you implement your own
	///     CoreParserModules
	/// </summary>
	public ListOfICoreParser DisabledCoreParserModules { get; set; }

	// ### Language ############################################################
	/// <summary>
	///     Defines the prefilled list with the languages for the output. Language parsing is unstable!
	/// </summary>
	public List<string> Languages { get; set; } = [];

	/// <summary>
	///     Activates the parsing of the language. Default: false. Language parsing is unstable!
	/// </summary>
	public bool ActivateLanguageParser { get; set; } = false;

	/// <summary>
	///     Removes the language token if it is found. Default: false. Language parsing is unstable!
	/// </summary>
	public bool RemoveLanguageTokenOnFound { get; set; } = false;

	/// <summary>
	///     Prefilled tokens list who should be removed from input string if the language parser is disabled. Language parsing
	///     is unstable!
	/// </summary>
	public List<string> RemoveAndListTokensOnLanguageParserIsDisabled { get; set; } = [];
	#endregion Parsing

	// ### Output
	// ############################################################

	#region Output
	/// <summary>
	///     Defines the new spacing char between the tokens in the output string. Default: '.'
	/// </summary>
	public char NewSpacingChar { get; set; } = '.';

	/// <summary>
	///     Defines the String.Format-string that formats the season for the id-string. Do NOT use larger than four digits per
	///     octet. Default: 'S{0:00}'
	/// </summary>
	public string IDStringFormatterSeason { get; set; } = "S{0:00}";

	/// <summary>
	///     Defines the String.Format-string that formats the episode for the id-string. Do NOT use larger than four digits per
	///     octet. Default: 'E{1:00}'
	/// </summary>
	public string IDStringFormatterEpisode { get; set; } = "E{0:00}";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: 'Unknown'
	/// </summary>
	public string ResolutionStringUnknown { get; set; } = "Unknown";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: 'SD'
	/// </summary>
	public string ResolutionStringSD { get; set; } = "SD";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '720p'
	/// </summary>
	public string ResolutionStringHD { get; set; } = "720p";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '1080p'
	/// </summary>
	public string ResolutionStringFullHD { get; set; } = "1080p";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '2160p'
	/// </summary>
	public string ResolutionStringUltraHD { get; set; } = "2160p";

	/// <summary>
	///     Defines the string who is converted from the enum Resolutions to something readable for e.g. the ParsedString.
	///     Default: '4320p'
	/// </summary>
	public string ResolutionStringUltraHD8k { get; set; } = "4320p";

	/// <summary>
	///     Defines how the resolution is formatted in the output strings e.g. ParsedString. Default: LowestResolution
	/// </summary>
	public ResolutionOutputBehavior ResolutionStringOutput { get; set; } = ResolutionOutputBehavior.LowestResolution;

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D'
	/// </summary>
	public string DimensionalString3DAny { get; set; } = "3D";

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D.HSBS'
	/// </summary>
	public string DimensionalString3DHSBS { get; set; } = "3D.HSBS";

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: '3D.HOU'
	/// </summary>
	public string DimensionalString3DHOU { get; set; } = "3D.HOU";

	/// <summary>
	///     Defines the string who is converted from the enum DimensionalType to something readable for e.g. the ParsedString.
	///     Default: empty
	/// </summary>
	public string DimensionalString2DAny { get; set; } = "";
	#endregion Output

	// ### Dimensional Detection
	// ############################################################

	#region DimensionalDetection
	/// <summary>
	///     Defines the prefilled list to detect the HSBS 3D strings
	/// </summary>
	public List<string> DetectHsbs3DTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the HOU 3D strings
	/// </summary>
	public List<string> DetectHou3DTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the Any 3D strings
	/// </summary>
	public List<string> DetectAny3DTokens { get; set; } = [];
	#endregion DimensionalDetection

	// ### Resolution Detection
	// ############################################################

	#region ResolutionDetection
	/// <summary>
	///     Defines the prefilled list to detect the UHD8k strings
	/// </summary>
	public List<string> DetectUltraHD8kTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the UHD4k strings
	/// </summary>
	public List<string> DetectUltraHDTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the FullHD strings
	/// </summary>
	public List<string> DetectFullHDTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the HD strings
	/// </summary>
	public List<string> DetectHDTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list to detect the SD strings
	/// </summary>
	public List<string> DetectSDTokens { get; set; } = [];
	#endregion ResolutionDetection

	// ### Remove and Replace
	// ############################################################

	#region RemoveAndReplace
	/// <summary>
	///     Defines the prefilled list with the tokens who should removed from the title string but re-added to the parsed
	///     string and the DetectedTokens list
	/// </summary>
	public List<string> RemoveAndListTokens { get; set; } = [];

	/// <summary>
	///     Defines the prefilled list with the tokens who should removed from the title string
	/// </summary>
	public List<string> RemoveWithoutListTokens { get; set; } = [];

	/// <summary>
	///     Defines the empty list with the KeyValuePair to replace (regex, replace)
	/// </summary>
	public List<KeyValuePair<string, string>> ReplaceRegexAndListTokens { get; set; } = [];

	/// <summary>
	///     Defines the empty list with the KeyValuePair to replace (regex, replace)
	/// </summary>
	public List<KeyValuePair<string, string>> ReplaceRegexWithoutListTokens { get; set; } = [];
	#endregion RemoveAndReplace

	// ### Cache
	// ############################################################

	#region Cache
	/// <summary>
	///     Enables the internal object cache. This gives a speed up on multiple access of the same object but a little slow
	///     down on single access.
	///     See Enum doc for more info. Default: CacheMode.Advanced
	/// </summary>
	public CacheMode CacheMode { get; set; } = CacheMode.Advanced;

	/// <summary>
	///     Caps the object count stored in the cache to reduce the RAM impact. Default: 10.000
	/// </summary>
	public int CacheItemCountLimit { get; set; } = 10000;
	#endregion Cache

	// ### De/Serialization
	// ############################################################

	#region DeSerialisazion
	/// <summary>
	///     Serializes this object to a xml string that could be stored in a file or somewhere else
	/// </summary>
	/// <param name="parserSettings">The object that should be converted to an xml string</param>
	/// <returns>The xml string representing this object</returns>
	public static string SerializeToXml( ParserSettings parserSettings )
	{
		return ParserSettingsWorker.SerializeToXml( parserSettings );
	}

	/// <summary>
	///     Serializes this object to a json string that could be stored in a file or somewhere else
	/// </summary>
	/// <param name="parserSettings">The object that should be converted to an xml string</param>
	/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
	/// <returns>The json string representing this object</returns>
	public static string SerializeToJson( ParserSettings parserSettings, JsonSerializerSettings jsonSerializerSettings = null )
	{
		return ParserSettingsWorker.SerializeToJson( parserSettings, jsonSerializerSettings );
	}

	/// <summary>
	///     Deserializes this object from a xml string
	/// </summary>
	/// <param name="xml">The xml string representing this object</param>
	/// <returns>The object generated out of the xml content</returns>
	public static ParserSettings DeserializeFromXml( string xml )
	{
		return ParserSettingsWorker.DeserializeFromXml( xml );
	}

	/// <summary>
	///     Serializes this object to a json string that could be stored in a file or somewhere else
	/// </summary>
	/// <param name="json">The json string representing this object</param>
	/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
	/// <returns>The json string representing this object</returns>
	public static ParserSettings DeSerializeFromJson( string json, JsonSerializerSettings jsonSerializerSettings = null )
	{
		return ParserSettingsWorker.DeserializeFromJson( json, jsonSerializerSettings );
	}
	#endregion DeSerialisazion

	// ### Remove and Replace
	// ############################################################

	#region OtherStuff
	/// <summary>
	///     Throws an exception instead of setting an error state. Default: false
	/// </summary>
	public bool ThrowExceptionInsteadOfErrorFlag { get; set; }
	#endregion OtherStuff
	#endregion Properties
}