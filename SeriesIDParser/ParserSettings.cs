
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

namespace SeriesIDParser
{
	/// <summary>
	/// The settings who can give to the SeriesIDParser to modify the behavior. Object is prefilled with the default values and list entries
	/// </summary>
	public class ParserSettings
	{
		// TODO
		// Move Prefill-Data to Ressource or something like that
		// Maybe Add De/Serializer


		/// <summary>
		/// Prefill ctor
		/// </summary>
		public ParserSettings()
		{
			// ### Resolution Detection
			_detectUltraHD8kTokens.Add("8k");
			_detectUltraHD8kTokens.Add("4320k");

			_detectUltraHDTokens.Add("UHD");
			_detectUltraHDTokens.Add("2160p");

			_detectFullHDTokens.Add("FullHD");
			_detectFullHDTokens.Add("1080p");

			_detectHDTokens.Add("HDTV");
			_detectHDTokens.Add("720p");
			_detectHDTokens.Add("HD");

			_detectSDTokens.Add("DVDRIP");
			_detectSDTokens.Add("DVD");
			_detectSDTokens.Add("XVID");
			_detectSDTokens.Add("SD");

			// ## Fileextensions
			_fileExtensions.Add("3G2");
			_fileExtensions.Add("3GP");
			_fileExtensions.Add("AMV");
			_fileExtensions.Add("ASF");
			_fileExtensions.Add("AVI");
			_fileExtensions.Add("DRC");
			_fileExtensions.Add("F4A");
			_fileExtensions.Add("F4B");
			_fileExtensions.Add("F4P");
			_fileExtensions.Add("F4V");
			_fileExtensions.Add("FLV");
			_fileExtensions.Add("GIF");
			_fileExtensions.Add("GIFV");
			_fileExtensions.Add("M2V");
			_fileExtensions.Add("M4P");
			_fileExtensions.Add("M4V");
			_fileExtensions.Add("MKV");
			_fileExtensions.Add("MNG");
			_fileExtensions.Add("MOV");
			_fileExtensions.Add("MP2");
			_fileExtensions.Add("MP4");
			_fileExtensions.Add("MPE");
			_fileExtensions.Add("MPEG");
			_fileExtensions.Add("MPG");
			_fileExtensions.Add("MPV");
			_fileExtensions.Add("MXF");
			_fileExtensions.Add("NSV");
			_fileExtensions.Add("OGG");
			_fileExtensions.Add("OGV");
			_fileExtensions.Add("QT");
			_fileExtensions.Add("RM");
			_fileExtensions.Add("RMVB");
			_fileExtensions.Add("ROQ");
			_fileExtensions.Add("SVI");
			_fileExtensions.Add("VOB");
			_fileExtensions.Add("WEBM");
			_fileExtensions.Add("WMV");
			_fileExtensions.Add("YUV");

			// ### Remove and Replace
			_removeAndListTokens.Add("DUBBED");
			_removeAndListTokens.Add("SYNCED");
			_removeAndListTokens.Add("AVC");
			_removeAndListTokens.Add("German");
			_removeAndListTokens.Add("iTunes");
			_removeAndListTokens.Add("DTS");
			_removeAndListTokens.Add("BluRay");
			_removeAndListTokens.Add("x264");
			_removeAndListTokens.Add("x265");
			_removeAndListTokens.Add("h264");
			_removeAndListTokens.Add("AC3D");
			_removeAndListTokens.Add("AAC");
			_removeAndListTokens.Add("WebHD");
			_removeAndListTokens.Add("Netflix");
			_removeAndListTokens.Add("WebRIP");
			_removeAndListTokens.Add("DOKU");
			_removeAndListTokens.Add("EXTENDED");

			_removeWithoutListTokens.Add(" ");
			_removeWithoutListTokens.Add(".-.");
			_removeWithoutListTokens.Add("MIRROR");
			_removeWithoutListTokens.Add("WEB");
			_removeWithoutListTokens.Add("BD9");
			_removeWithoutListTokens.Add("DD20");
			_removeWithoutListTokens.Add("DD51");
			_removeWithoutListTokens.Add("DL");

			// ### Other Stuff
			_possibleSpacingChars.Add('.');
			_possibleSpacingChars.Add(',');
			_possibleSpacingChars.Add('-');
			_possibleSpacingChars.Add(' ');
			_possibleSpacingChars.Add('+');
			_possibleSpacingChars.Add('*');
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
		#endregion RemoveAndReplace


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
