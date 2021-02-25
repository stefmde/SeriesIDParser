using SeriesIDParser.Models;
using System;
using System.Reflection;

namespace SeriesIDParser.DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("SeriesIDParserCore assembly version " + typeof(SeriesID).GetTypeInfo().Assembly.GetName().Version);
			Console.WriteLine(Environment.NewLine);

			Console.Write("Example: Knight.Rider.S01E07.Die.grosse.Duerre.3D.HOU.1982.German.DVDRip.XviD-c0nFuSed.mkv");
			Console.WriteLine(Environment.NewLine);

			Console.Write("Enter string to parse: ");
			string inputLine = Console.ReadLine();

			Console.WriteLine("Try to parse: \"" + inputLine + "\"");
			Console.WriteLine(Environment.NewLine);

			// Creating the parser object with default settings(empty ctor)
			SeriesID serieparserResult = new SeriesID();
			ParserResult parserResult = serieparserResult.Parse(inputLine);

			// Use the following three lines as a example for editing the parser settings
			//ParserSettings parserSettings = new ParserSettings();
			//parserSettings.NewSpacingChar = '-';
			//SeriesID seriesId2 = new SeriesID(parserSettings);
			//ParserResult parserResult2 = seriesId2.Parse(inputLine);

			// Use the folowwing line to use the fluent syntax
			//ParserResult parserResult3 = new SeriesID().Parse(inputLine);

			Console.WriteLine("OriginalString (string): {0}", parserResult.OriginalString);
			Console.WriteLine("ParsedString (string): {0}", parserResult.ParsedString);
			Console.WriteLine("Title (string): {0}", parserResult.Title);
			Console.WriteLine("EpisodeTitle (string): {0}", parserResult.EpisodeTitle);
			Console.WriteLine("FullTitle (string): {0}", parserResult.FullTitle);
			Console.WriteLine("IsSeries (bool): {0}", parserResult.IsSeries);
			Console.WriteLine("IsMultiEpisode (bool): {0}", parserResult.IsMultiEpisode);
			Console.WriteLine("Season (int): {0}", parserResult.Season);
			Console.WriteLine("Episodes (int list): {0}", string.Join(", ", parserResult.Episodes));
			Console.WriteLine("IDString (string): {0}", parserResult.IDString);
			Console.WriteLine("Resolutions (enum list Resolutions): {0}", string.Join(", ", parserResult.Resolutions));
			Console.WriteLine("Year (int): {0}", parserResult.Year);
			Console.WriteLine("FileExtension (string): {0}", parserResult.FileExtension);
			Console.WriteLine("RemovedTokens (string list): {0}", string.Join(", ", parserResult.RemovedTokens));
			Console.WriteLine("State (enum State): {0}", parserResult.State);
			Console.WriteLine("DetectedOldSpacingChar (char): '{0}'", parserResult.OldSpacingChar);
			Console.WriteLine("ProcessingDuration (TimeSpan): {0}", parserResult.ProcessingDuration.TotalMilliseconds + " ms");
			Console.WriteLine("ReleaseGroup (string): {0}", parserResult.ReleaseGroup);
			Console.WriteLine("AudioCodec (string): {0}", parserResult.AudioCodec);
			Console.WriteLine("VideoCodec (string): {0}", parserResult.VideoCodec);
			Console.WriteLine("Is3D (string): {0}", parserResult.Is3D);
			Console.WriteLine("DimensionalType (string): {0}", parserResult.DimensionalType);

			if (parserResult.Exception != null)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(parserResult.Exception.Message + Environment.NewLine);
				Console.WriteLine(parserResult.Exception.Source + Environment.NewLine);
				Console.WriteLine(parserResult.Exception.StackTrace + Environment.NewLine);
				Console.ResetColor();
			}

			Console.WriteLine("Done. Hit 'Enter' to exit.");
			Console.WriteLine(Environment.NewLine);
			Console.ReadLine();
		}
    }
}
