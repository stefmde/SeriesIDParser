using SeriesIDParserCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriesIDParserCore_Console_Demo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("SeriesIDParserCore");
			Console.WriteLine(Environment.NewLine);

			Console.Write("Example: Knight.Rider.S01E07.Die.grosse.Duerre.1982.German.DVDRip.XviD-c0nFuSed.mkv");
			Console.WriteLine(Environment.NewLine);

			Console.Write("Enter string to parse: ");
			string inputLine = Console.ReadLine();

			Console.WriteLine("Try to parse: \"" + inputLine + "\"");
			Console.WriteLine(Environment.NewLine);


			// Use the following three lines as a example for editing the parser settings
			// ParserSettings ps = new ParserSettings();
			// ps.NewSpacingChar = '-';
			// SeriesID sid = new SeriesID(ps);


			// Creating the parser object with default settings(empty ctor)
			SeriesID sid = new SeriesID();
			sid.Parse(inputLine);

			Console.WriteLine("OriginalString (string): {0}", sid.OriginalString);
			Console.WriteLine("ParsedString (string): {0}", sid.ParsedString);
			Console.WriteLine("Title (string): {0}", sid.Title);
			Console.WriteLine("EpisodeTitle (string): {0}", sid.EpisodeTitle);
			Console.WriteLine("FullTitle (string): {0}", sid.FullTitle);
			Console.WriteLine("IsSeries (bool): {0}", sid.IsSeries);
			Console.WriteLine("IsMultiEpisode (bool): {0}", sid.IsMultiEpisode);
			Console.WriteLine("Season (int): {0}", sid.Season);
			Console.WriteLine("Episodes (int list): {0}", string.Join(", ", sid.Episodes));
			Console.WriteLine("IDString (string): {0}", sid.IDString);
			Console.WriteLine("Resolutions (enum list Resolutions): {0}", string.Join(", ", sid.Resolutions));
			Console.WriteLine("Year (int): {0}", sid.Year);
			Console.WriteLine("FileExtension (string): {0}", sid.FileExtension);
			Console.WriteLine("RemovedTokens (string list): {0}", string.Join(", ", sid.RemovedTokens));
			Console.WriteLine("State (enum State): {0}", sid.State);
			Console.WriteLine("DetectedOldSpacingChar (char): '{0}'", sid.DetectedOldSpacingChar);
			Console.WriteLine("ProcessingDuration (TimeSpan): {0}", sid.ProcessingDuration.TotalMilliseconds + " ms");
			Console.WriteLine("ReleaseGroup (string): {0}", sid.ReleaseGroup);
			Console.WriteLine("AudioCodec (string): {0}", sid.AudioCodec);
			Console.WriteLine("VideoCodec (string): {0}", sid.VideoCodec);

			if (sid.Exception != null)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(sid.Exception.Message + Environment.NewLine);
				Console.WriteLine(sid.Exception.Source + Environment.NewLine);
				Console.WriteLine(sid.Exception.StackTrace + Environment.NewLine);
				Console.ResetColor();
			}

			Console.WriteLine("Done. Hit 'Enter' to exit.");
			Console.WriteLine(Environment.NewLine);
			Console.ReadLine();
		}
	}
}
