
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

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SeriesIDParser.Worker;

[assembly:InternalsVisibleTo("SeriesIDParser.Test")]
namespace SeriesIDParser.Models
{
	/// <summary>
	/// The settings who can give to the SeriesIDParser to modify the behavior. Object is prefilled with the default values and list entries
	/// </summary>
	//[Serializable]
	public class ParserSettings
	{
		/// <summary>
		/// Prefill ctor
		/// </summary>
		public ParserSettings(bool preFillLists = false)
		{
			if (!preFillLists)
			{
				return;
			}

			// ### Resolution Detection
			DetectUltraHD8kTokens = new List<string>() {"8k", "4320k"};
			DetectUltraHDTokens = new List<string>() {"NetflixUHD", "UHD", "2160p"};
			DetectFullHDTokens = new List<string>() {"FullHD", "1080p", "1080i"};
			DetectHDTokens = new List<string>() {"HDTV", "720p", "HD"};
			DetectSDTokens = new List<string>() {"DVDRIP", "DVD", "SD"};

			// ### Fileextensions
			FileExtensions = new List<string>()
			{
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
			};

			// ### Remove 
			RemoveAndListTokens = new List<string>()
			{
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
				"AmazonHD",
				"German",
				"Remastered",
				"HDRip",
				"Remux"
			};

			//_removeAndListTokensOnLanguageParserIsDisabled = new List<string>() { "GERMAN" };

			RemoveWithoutListTokens = new List<string>()
			{
				" ",
				"MIRROR",
				"WEB",
				"BD9",
				"DD20",
				"DD51",
				"DD5.1",
				"Web-DL",
				"DL",
				"HDDVDRip",
				"AC3D",
				"THEATRICAL"
			};

			// ### Parsing
			VideoCodecs = new List<string>()
			{
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
				"MPEG2"
			};

			AudioCodecs = new List<string>() {"DTSHD", "DTS", "AAC", "MP3", "MPEG3", "MPEG-3"};

			//_languages = new List<string>() { "English", "Chinese", "Hindi", "Spanish",
			//"French", "Arabic", "Russian", "Portuguese",
			//"Bengalese", "German", "Japanese", "Korean" };

			// ### Other Stuff
			PossibleSpacingChars = new List<char>() {'.', ',', '-', ' ', '+', '*'};
		}

		internal ParserSettings()
		{

		}

		#region Properties

		// ### Parsing
		// ############################################################

		#region Parsing

		/// <summary>
		/// Defines the regex-string that parses the season and episode id from the input string. Default: 'S(?<season>\d{1,4})(E(?<episode>\d{1,4}))+'
		/// </summary>
		public string SeasonAndEpisodeParseRegex { get; set; } = @"S(?<season>\d{1,4})(E(?<episode>\d{1,4}))+";

		/// <summary>
		/// Defines the regex-string that parses the year from the input string. Default: '(\d{4})'
		/// </summary>
		public string YearParseRegex { get; set; } = @"(\d{4})";


		/// <summary>
		/// Defines the prefilled list with the posible extension to garantee a better detection
		/// </summary>
		public List<string> FileExtensions { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list with the posible Videocodecs
		/// </summary>
		public List<string> VideoCodecs { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list with the posible Audiocodecs
		/// </summary>
		public List<string> AudioCodecs { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list with the posible spacing chars for the given string to detect the char
		/// </summary>
		public List<char> PossibleSpacingChars { get; set; } = new List<char>();


		/// <summary>
		/// Defines the prefilled list with the posible spacing chars for the given string to detect the char
		/// </summary>
		public char ReleaseGroupSeperator { get; set; } = '-';


		// ### Language ############################################################
		/// <summary>
		/// Defines the prefilled list with the languages for the output. Language parsing is Instable!
		/// </summary>
		public List<string> Languages { get; set; } = new List<string>();


		/// <summary>
		/// Activates the parsing of the language. Default: false. Language parsing is Instable!
		/// </summary>
		public bool ActivateLanguageParser { get; set; } = false;


		/// <summary>
		/// Removes the language token if it is found. Default: false. Language parsing is Instable!
		/// </summary>
		public bool RemoveLanguageTokenOnFound { get; set; } = false;

		/// <summary>
		/// Prefilled tokens list who should be removed from input string if the language parser is disabled. Language parsing is Instable!
		/// </summary>
		public List<string> RemoveAndListTokensOnLanguageParserIsDisabled { get; set; } = new List<string>();

		#endregion Parsing


		// ### Output
		// ############################################################

		#region Output

		/// <summary>
		/// Defines the new spacing char between the tokens in the output string. Default: '.'
		/// </summary>
		public char NewSpacingChar { get; set; } = '.';


		/// <summary>
		/// Defines the String.Format-string that formats the season for the id-string. Do NOT use larger than four digits per octet. Default: 'S{0:00}'
		/// </summary>
		public string IDStringFormaterSeason { get; set; } = "S{0:00}";


		/// <summary>
		/// Defines the String.Format-string that formats the episode for the id-string. Do NOT use larger than four digits per octet. Default: 'E{1:00}'
		/// </summary>
		public string IDStringFormaterEpisode { get; set; } = "E{0:00}";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: 'Unknown'
		/// </summary>
		public string ResolutionStringUnknown { get; set; } = "Unknown";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: 'SD'
		/// </summary>
		public string ResolutionStringSD { get; set; } = "SD";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '720p'
		/// </summary>
		public string ResolutionStringHD { get; set; } = "720p";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '1080p'
		/// </summary>
		public string ResolutionStringFullHD { get; set; } = "1080p";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '2160p'
		/// </summary>
		public string ResolutionStringUltraHD { get; set; } = "2160p";


		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '4320p'
		/// </summary>
		public string ResolutionStringUltraHD8k { get; set; } = "4320p";


		/// <summary>
		/// Defines how the resolution is formated in the output strings e.g. ParsedString. Default: LowesrResolution
		/// </summary>
		public ResolutionOutputBehavior ResolutionStringOutput { get; set; } = ResolutionOutputBehavior.LowestResolution;

		#endregion Output


		// ### Resolution Detection
		// ############################################################

		#region ResolutionDetection

		/// <summary>
		/// Defines the prefilled list to detect the UHD8k strings
		/// </summary>
		public List<string> DetectUltraHD8kTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list to detect the UHD4k strings
		/// </summary>
		public List<string> DetectUltraHDTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list to detect the FullHD strings
		/// </summary>
		public List<string> DetectFullHDTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list to detect the HD strings
		/// </summary>
		public List<string> DetectHDTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list to detect the SD strings
		/// </summary>
		public List<string> DetectSDTokens { get; set; } = new List<string>();

		#endregion ResolutionDetection


		// ### Remove and Replace
		// ############################################################

		#region RemoveAndReplace

		/// <summary>
		/// Defines the prefilled list with the tokens who should removed from the title string but re-added to the parsed string and the deletedtokens list
		/// </summary>
		public List<string> RemoveAndListTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the prefilled list with the tokens who should removed from the title string
		/// </summary>
		public List<string> RemoveWithoutListTokens { get; set; } = new List<string>();


		/// <summary>
		/// Defines the empty list with the KeyValuePair to replace (regex, replace)
		/// </summary>
		public List<KeyValuePair<string, string>> ReplaceRegexAndListTokens { get; set; } =
			new List<KeyValuePair<string, string>>();


		/// <summary>
		/// Defines the empty list with the KeyValuePair to replace (regex, replace)
		/// </summary>
		public List<KeyValuePair<string, string>> ReplaceRegexWithoutListTokens { get; set; } =
			new List<KeyValuePair<string, string>>();

		#endregion RemoveAndReplace


		// ### Cache
		// ############################################################

		#region Cache

		/// <summary>
		/// Enables the internal object cache. This gives a speed up on multiple access of the same object but a little slow down on single access. 
		/// See Enum doc for more info. Default: CacheMode.Advanced
		/// </summary>
		public CacheMode CacheMode { get; set; } = CacheMode.Advanced;

		/// <summary>
		/// Caps the object count stored in the cache to reduce the RAM impact. Default: 10.000
		/// </summary>
		public int CacheItemCountLimit { get; set; } = 10000;

		#endregion Cache


		// ### De/Serialization
		// ############################################################

		#region DeSerialisazion

		/// <summary>
		/// Serializes this object to a xml string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <returns>The xml string representing this object</returns>
		public static string SerializeToXML(ParserSettings parserSettings)
		{
			return ParserSettingsWorker.SerializeToXML(parserSettings);
		}


		/// <summary>
		/// Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		public static string SerializeToJson(ParserSettings parserSettings,
			JsonSerializerSettings jsonSerializerSettings = null)
		{
			return ParserSettingsWorker.SerializeToJson(parserSettings, jsonSerializerSettings);
		}


		/// <summary>
		/// Deserializes this object from a xml string
		/// </summary>
		/// <param name="xml">The xml string representing this object</param>
		/// <returns>The object generated out of the xml content</returns>
		public static ParserSettings DeSerializeFromXML(string xml)
		{
			return ParserSettingsWorker.DeSerializeFromXML(xml);
		}


		/// <summary>
		/// Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="json">The json string representing this object</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		public static ParserSettings DeSerializeFromJson(string json, JsonSerializerSettings jsonSerializerSettings = null)
		{
			return ParserSettingsWorker.DeSerializeFromJson(json, jsonSerializerSettings);
		}

		#endregion DeSerialisazion


		// ### Remove and Replace
		// ############################################################

		#region OtherStuff

		/// <summary>
		/// Throws an exception instead of setting an error state. Default: false
		/// </summary>
		public bool ThrowExceptionInsteadOfErrorFlag { get; set; } = false;

		#endregion OtherStuff

		#endregion Properties
	}
}
