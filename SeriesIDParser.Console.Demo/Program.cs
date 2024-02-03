using System;
using System.Reflection;

namespace SeriesIDParser.Console.Demo;

class Program
{
	static void Main(string[] args)
	{
		System.Console.WriteLine("SeriesIDParserCore assembly version " + typeof(SeriesIdParser).GetTypeInfo().Assembly.GetName().Version);
		System.Console.WriteLine(Environment.NewLine);

		System.Console.Write("Example: Knight.Rider.S01E07.Die.grosse.Duerre.3D.HOU.1982.German.DVDRip.XviD-c0nFuSed.mkv");
		System.Console.WriteLine(Environment.NewLine);

		System.Console.Write("Enter string to parse: ");
		var inputLine = System.Console.ReadLine();

		System.Console.WriteLine("Try to parse: \"" + inputLine + "\"");
		System.Console.WriteLine(Environment.NewLine);

		// Creating the parser object with default settings(empty ctor)
		SeriesIdParser serieparserResult = new();
		var parserResult = serieparserResult.Parse(inputLine);

		// Use the following three lines as a example for editing the parser settings
		//ParserSettings parserSettings = new ParserSettings();
		//parserSettings.NewSpacingChar = '-';
		//SeriesID seriesId2 = new SeriesID(parserSettings);
		//ParserResult parserResult2 = seriesId2.Parse(inputLine);

		// Use the folowwing line to use the fluent syntax
		//ParserResult parserResult3 = new SeriesID().Parse(inputLine);

		System.Console.WriteLine("OriginalString (string): {0}", parserResult.OriginalString);
		System.Console.WriteLine("ParsedString (string): {0}", parserResult.ParsedString);
		System.Console.WriteLine("Title (string): {0}", parserResult.Title);
		System.Console.WriteLine("EpisodeTitle (string): {0}", parserResult.EpisodeTitle);
		System.Console.WriteLine("FullTitle (string): {0}", parserResult.FullTitle);
		System.Console.WriteLine("IsSeries (bool): {0}", parserResult.IsSeries);
		System.Console.WriteLine("IsMultiEpisode (bool): {0}", parserResult.IsMultiEpisode);
		System.Console.WriteLine("Season (int): {0}", parserResult.Season);
		System.Console.WriteLine("Episodes (int list): {0}", string.Join(", ", parserResult.Episodes));
		System.Console.WriteLine("IDString (string): {0}", parserResult.IDString);
		System.Console.WriteLine("Resolutions (enum list Resolutions): {0}", string.Join(", ", parserResult.Resolutions));
		System.Console.WriteLine("Year (int): {0}", parserResult.Year);
		System.Console.WriteLine("FileExtension (string): {0}", parserResult.FileExtension);
		System.Console.WriteLine("RemovedTokens (string list): {0}", string.Join(", ", parserResult.RemovedTokens));
		System.Console.WriteLine("State (enum State): {0}", parserResult.State);
		System.Console.WriteLine("DetectedOldSpacingChar (char): '{0}'", parserResult.OldSpacingChar);
		System.Console.WriteLine("ProcessingDuration (TimeSpan): {0}", parserResult.ProcessingDuration.TotalMilliseconds + " ms");
		System.Console.WriteLine("ReleaseGroup (string): {0}", parserResult.ReleaseGroup);
		System.Console.WriteLine("AudioCodec (string): {0}", parserResult.AudioCodec);
		System.Console.WriteLine("VideoCodec (string): {0}", parserResult.VideoCodec);
		System.Console.WriteLine("Is3D (string): {0}", parserResult.Is3D);
		System.Console.WriteLine("DimensionalType (string): {0}", parserResult.DimensionalType);

		if (parserResult.Exception != null)
		{
			System.Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine(parserResult.Exception.Message + Environment.NewLine);
			System.Console.WriteLine(parserResult.Exception.Source + Environment.NewLine);
			System.Console.WriteLine(parserResult.Exception.StackTrace + Environment.NewLine);
			System.Console.ResetColor();
		}

		System.Console.WriteLine("Done. Hit 'Enter' to exit.");
		System.Console.WriteLine(Environment.NewLine);
		System.Console.ReadLine();
	}
}