
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace SeriesIDParser
{
	/// <summary>
	/// The properties for the ResolutionOutputBehavior
	/// </summary>
	public enum ResolutionOutputBehavior
	{
		AllFoundResolutions,
		HighestResolution,
		LowestResolution
	}

	/// <summary>
	/// The settings who can give to the SeriesIDParser to modify the behavior. Object is prefilled with the default values and list entries
	/// </summary>
	[Serializable]
	public class ParserSettings
	{


		/// <summary>
		/// Prefill ctor
		/// </summary>
		public ParserSettings(bool preFillLists = false)
		{
			if (preFillLists)
			{
				// ### Resolution Detection
				_detectUltraHD8kTokens = new List<string>() { "8k", "4320k" };
				_detectUltraHDTokens = new List<string>() { "NetflixUHD", "UHD", "2160p" };
				_detectFullHDTokens = new List<string>() { "FullHD", "1080p" };
				_detectHDTokens = new List<string>() { "HDTV", "720p", "HD" };
				_detectSDTokens = new List<string>() { "DVDRIP", "DVD", "XVID", "SD" };

				// ### Fileextensions
				_fileExtensions = new List<string>() { "3G2", "3GP", "AMV", "ASF", "AVI", "DRC", "F4A", "F4B", "F4P", "F4V",
												"FLV", "GIF", "GIFV", "M2V", "M4P", "M4V", "MKV", "MNG", "MOV", "MP2",
												"MP4", "MPE", "MPEG", "MPG", "MPV", "MXF", "NSV", "OGG", "OGV", "QT",
												"RM", "RMVB", "ROQ", "SVI", "VOB", "WEBM", "WMV", "YUV" };

				// ### Remove and Replace
				_removeAndListTokens = new List<string>() { "DUBBED", "SYNCED", "AVC", "German", "iTunes",
													"DTS", "BluRay", "x264", "x265", "h264",
													"AC3D", "AAC", "WebHD", "Netflix", "WebRIP",
													"DOKU", "EXTENDED", "DTSHD", "UNRATED" };

				_removeWithoutListTokens = new List<string>() { " ", "MIRROR", "WEB", "BD9", "DD20",
														"DD51", "DL" };

				// ### Other Stuff
				_possibleSpacingChars = new List<char>() { '.', ',', '-', ' ', '+', '*' };
			}
		}

		public ParserSettings()
		{

		}
		#region Properties
		// ### Parsing
		// ############################################################
		#region Parsing

		private string _seasonAndEpisodeParseRegex = @"S(?<season>\d{1,4})E(?<episode>\d{1,4})";
		/// <summary>
		/// Defines the regex-string that parses the season and episode id from the input string. Default: 'S(?<season>\d{1,4})E(?<episode>\d{1,4})'
		/// </summary>
		public string SeasonAndEpisodeParseRegex
		{
			get { return _seasonAndEpisodeParseRegex; }
			set { _seasonAndEpisodeParseRegex = value; }
		}


		private List<string> _fileExtensions = new List<string>();
		/// <summary>
		/// Defines the prefilled list with the posible extension to garantee a better detection
		/// </summary>
		public List<string> FileExtensions
		{
			get { return _fileExtensions; }
			set { _fileExtensions = value; }
		}


		private List<char> _possibleSpacingChars = new List<char>();
		/// <summary>
		/// Defines the prefilled list with the posible spacing chars for the given string to detect the char
		/// </summary>
		public List<char> PossibleSpacingChars
		{
			get { return _possibleSpacingChars; }
			set { _possibleSpacingChars = value; }
		}


		private char _releaseGroupSeperator = '-';
		/// <summary>
		/// Defines the prefilled list with the posible spacing chars for the given string to detect the char
		/// </summary>
		public char ReleaseGroupSeperator
		{
			get { return _releaseGroupSeperator; }
			set { _releaseGroupSeperator = value; }
		}
		#endregion Parsing


		// ### Output
		// ############################################################
		#region Output
		private char _newSpacingChar = '.';
		/// <summary>
		/// Defines the new spacing char between the tokens in the output string. Default: '.'
		/// </summary>
		public char NewSpacingChar
		{
			get { return _newSpacingChar; }
			set { _newSpacingChar = value; }
		}


		private string _idStringFormater = "S{0:00}E{1:00}";
		/// <summary>
		/// Defines the String.Format-string that formats the season and episode id-string. Do NOT use larger than four digits per octet. Default: 'S{0:00}E{1:00}'
		/// </summary>
		public string IDStringFormater
		{
			get { return _idStringFormater; }
			set { _idStringFormater = value; }
		}


		private string _resolutionStringUnknown = "UNKNOWN";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: 'UNKNOWN'
		/// </summary>
		public string ResolutionStringUnknown
		{
			get { return _resolutionStringUnknown; }
			set { _resolutionStringUnknown = value; }
		}


		private string _resolutionStringSD = "SD";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: 'SD'
		/// </summary>
		public string ResolutionStringSD
		{
			get { return _resolutionStringSD; }
			set { _resolutionStringSD = value; }
		}


		private string _resolutionStringHD = "720p";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '720p'
		/// </summary>
		public string ResolutionStringHD
		{
			get { return _resolutionStringHD; }
			set { _resolutionStringHD = value; }
		}


		private string _resolutionStringFullHD = "1080p";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '1080p'
		/// </summary>
		public string ResolutionStringFullHD
		{
			get { return _resolutionStringFullHD; }
			set { _resolutionStringFullHD = value; }
		}


		private string _resolutionStringUltraHD = "2160p";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '2160p'
		/// </summary>
		public string ResolutionStringUltraHD
		{
			get { return _resolutionStringUltraHD; }
			set { _resolutionStringUltraHD = value; }
		}


		private string _resolutionStringUltraHD8K = "4320p";
		/// <summary>
		/// Defines the string who is convertet from the enum Resolutions to something readable for e.g. the ParsedString. Default: '4320p'
		/// </summary>
		public string ResolutionStringUltraHD8k
		{
			get { return _resolutionStringUltraHD8K; }
			set { _resolutionStringUltraHD8K = value; }
		}

	
		private ResolutionOutputBehavior _resolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
		/// <summary>
		/// Defines how the resolution is formated in the output strings e.g. ParsedString. Default: LowesrResolution
		/// </summary>
		public ResolutionOutputBehavior ResolutionStringOutput
		{
			get { return _resolutionStringOutput; }
			set { _resolutionStringOutput = value; }
		}
		#endregion Output


		// ### Resolution Detection
		// ############################################################
		#region ResolutionDetection
		private List<string> _detectUltraHD8kTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list to detect the UHD8k strings
		/// </summary>
		public List<string> DetectUltraHD8kTokens
		{
			get { return _detectUltraHD8kTokens; }
			set { _detectUltraHD8kTokens = value; }
		}


		private List<string> _detectUltraHDTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list to detect the UHD4k strings
		/// </summary>
		public List<string> DetectUltraHDTokens
		{
			get { return _detectUltraHDTokens; }
			set { _detectUltraHDTokens = value; }
		}


		private List<string> _detectFullHDTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list to detect the FullHD strings
		/// </summary>
		public List<string> DetectFullHDTokens
		{
			get { return _detectFullHDTokens; }
			set { _detectFullHDTokens = value; }
		}


		private List<string> _detectHDTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list to detect the HD strings
		/// </summary>
		public List<string> DetectHDTokens
		{
			get { return _detectHDTokens; }
			set { _detectHDTokens = value; }
		}


		private List<string> _detectSDTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list to detect the SD strings
		/// </summary>
		public List<string> DetectSDTokens
		{
			get { return _detectSDTokens; }
			set { _detectSDTokens = value; }
		}
		#endregion ResolutionDetection


		// ### Remove and Replace
		// ############################################################
		#region RemoveAndReplace
		private List<string> _removeAndListTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list with the tokens who should removed from the title string but re-added to the parsed string and the deletedtokens list
		/// </summary>
		public List<string> RemoveAndListTokens
		{
			get { return _removeAndListTokens; }
			set { _removeAndListTokens = value; }
		}


		private List<string> _removeWithoutListTokens = new List<string>();
		/// <summary>
		/// Defines the prefilled list with the tokens who should removed from the title string
		/// </summary>
		public List<string> RemoveWithoutListTokens
		{
			get { return _removeWithoutListTokens; }
			set { _removeWithoutListTokens = value; }
		}



		private List<KeyValuePair<string, string>> _replaceRegexAndListTokens = new List<KeyValuePair<string, string>>();
		/// <summary>
		/// Defines the empty list with the KeyValuePair to replace (regex, replace)
		/// </summary>
		public List<KeyValuePair<string, string>> ReplaceRegexAndListTokens
		{
			get { return _replaceRegexAndListTokens; }
			set { _replaceRegexAndListTokens = value; }
		}


		private List<KeyValuePair<string, string>> _replaceRegexWithoutListTokens = new List<KeyValuePair<string, string>>();
		/// <summary>
		/// Defines the empty list with the KeyValuePair to replace (regex, replace)
		/// </summary>
		public List<KeyValuePair<string, string>> ReplaceRegexWithoutListTokens
		{
			get { return _replaceRegexWithoutListTokens; }
			set { _replaceRegexWithoutListTokens = value; }
		}
		#endregion RemoveAndReplace


		// ### De/Serialization
		// ############################################################
		#region DeSerialisazion

		/// <summary>
		/// Serializes this object to a xml string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="obj">The object that should be converted to an xml string</param>
		/// <returns>The xml string representing this object</returns>
		public static string SerializeToXML(ParserSettings obj)
		{
			//try
			//{
				string data = string.Empty;
				XmlSerializer x = new XmlSerializer(obj.GetType());
				using (MemoryStream ms = new MemoryStream())
				{
					x.Serialize(ms, obj);
					ms.Position = 0;
					using (StreamReader sr = new StreamReader(ms, Encoding.Default))
					{
						data = sr.ReadToEnd();
					}
				}

				return data;
			//}
			//catch (Exception)
			//{
			//	throw;
			//}
		}


		/// <summary>
		/// Deserializes this object from a xml string
		/// </summary>
		/// <param name="xml">The xml string representing this object</param>
		/// <returns>The object generated out of the xml content</returns>
		public static ParserSettings DeSerializeFromXML(string xml)
		{
			//try
			//{
				XmlSerializer x = new XmlSerializer(typeof(ParserSettings));
				byte[] xmlBytes = Encoding.Default.GetBytes(xml);
				using (MemoryStream ms = new MemoryStream(xmlBytes))
				{
					return (ParserSettings)x.Deserialize(ms);
				}
			//}
			//catch
			//{
			//	throw;
			//}
		}
		#endregion DeSerialisazion

		// ### Remove and Replace
		// ############################################################
		#region OtherStuff
		private bool _throwExceptionInsteadOfErrorFlag = false;
		/// <summary>
		/// Throws an exception instead of setting an error state. Default: false
		/// </summary>
		public bool ThrowExceptionInsteadOfErrorFlag
		{
			get { return _throwExceptionInsteadOfErrorFlag; }
			set { _throwExceptionInsteadOfErrorFlag = value; }
		}

		#endregion OtherStuff
		#endregion Properties

		
	}
}
