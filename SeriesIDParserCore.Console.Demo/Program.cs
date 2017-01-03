// 
// MIT License
// 
// Copyright(c) 2016 - 2017
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
using System.Linq;
using System.Reflection;
using SeriesIDParserCore.Models;
// ReSharper disable MissingXmlDoc

namespace SeriesIDParserCore.Console.Demo
{
	// ReSharper disable once UnusedMember.Global
	public class Program
	{
		public static void Main( string[] args )
		{
			System.Console.WriteLine("SeriesIDParserCore assembly version " + typeof(SeriesID).GetTypeInfo().Assembly.GetName().Version );
			System.Console.WriteLine( Environment.NewLine );

			System.Console.Write( "Example: Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD-c0nFuSed.mkv" );
			System.Console.WriteLine( Environment.NewLine );

			System.Console.Write( "Enter string to parse: " );
			string inputLine = System.Console.ReadLine();

			System.Console.WriteLine( "Try to parse: \"" + inputLine + "\"" );
			System.Console.WriteLine( Environment.NewLine );

			// Creating the parser object with default settings(empty ctor)
			SeriesID serieparserResult = new SeriesID();
			ParserResult parserResult = serieparserResult.Parse( inputLine );

			// Use the following three lines as a example for editing the parser settings
			//ParserSettings parserSettings = new ParserSettings();
			//parserSettings.NewSpacingChar = '-';
			//SeriesID seriesId2 = new SeriesID(parserSettings);
			//ParserResult parserResult2 = seriesId2.Parse(inputLine);


			// Use the folowwing line to use the fluent syntax
			//ParserResult parserResult3 = new SeriesID().Parse(inputLine);


			System.Console.WriteLine( "OriginalString (string): {0}", parserResult.OriginalString );
			System.Console.WriteLine( "ParsedString (string): {0}", parserResult.ParsedString );
			System.Console.WriteLine( "Title (string): {0}", parserResult.Title );
			System.Console.WriteLine( "EpisodeTitle (string): {0}", parserResult.EpisodeTitle );
			System.Console.WriteLine( "FullTitle (string): {0}", parserResult.FullTitle );
			System.Console.WriteLine( "IsSeries (bool): {0}", parserResult.IsSeries );
			System.Console.WriteLine( "IsMultiEpisode (bool): {0}", parserResult.IsMultiEpisode );
			System.Console.WriteLine( "Season (int): {0}", parserResult.Season );
			System.Console.WriteLine( "Episodes (int list): {0}", string.Join( ", ", parserResult.Episodes ) );
			System.Console.WriteLine( "IDString (string): {0}", parserResult.IDString );
			System.Console.WriteLine( "Resolutions (enum list Resolutions): {0}", string.Join( ", ", parserResult.Resolutions ) );
			System.Console.WriteLine( "Year (int): {0}", parserResult.Year );
			System.Console.WriteLine( "FileExtension (string): {0}", parserResult.FileExtension );
			System.Console.WriteLine( "RemovedTokens (string list): {0}", string.Join( ", ", parserResult.RemovedTokens ) );
			System.Console.WriteLine( "State (enum State): {0}", parserResult.State );
			System.Console.WriteLine( "DetectedOldSpacingChar (char): '{0}'", parserResult.DetectedOldSpacingChar );
			System.Console.WriteLine( "ProcessingDuration (TimeSpan): {0}", parserResult.ProcessingDuration.TotalMilliseconds + " ms" );
			System.Console.WriteLine( "ReleaseGroup (string): {0}", parserResult.ReleaseGroup );
			System.Console.WriteLine( "AudioCodec (string): {0}", parserResult.AudioCodec );
			System.Console.WriteLine( "VideoCodec (string): {0}", parserResult.VideoCodec );

			if (parserResult.Exception != null)
			{
				System.Console.ForegroundColor = ConsoleColor.Red;
				System.Console.WriteLine( parserResult.Exception.Message + Environment.NewLine );
				System.Console.WriteLine( parserResult.Exception.Source + Environment.NewLine );
				System.Console.WriteLine( parserResult.Exception.StackTrace + Environment.NewLine );
				System.Console.ResetColor();
			}

			System.Console.WriteLine( "Done. Hit 'Enter' to exit." );
			System.Console.WriteLine( Environment.NewLine );
			System.Console.ReadLine();
		}
	}
}