using SeriesIDParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser_Test
{
	public struct TestData
	{
		public string Actual;
		public string FullTitle;
		public string ParsedString;
		public SeriesID Expected;
		public ParserSettings Settings;
		public string Comment;
	}

	public class PrepareTest
	{
		


		public List<TestData> GetTestData()
		{
			List<TestData> testData = new List<TestData>();
			SeriesID id = new SeriesID();
			//TestData td = new TestData();

			// Usual - Test001
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test001"
			});


			// Small s-e - Test002
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test002"
			});


			// three s-e - Test003
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.S001E005.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test003"
			});


			// single s-e - Test004
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.s01e05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test004"
			});



			// different s-e count - Test005
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.S001E5.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test005"
			});



			// token doubble - Test006
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.GERMAN.720p.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai.Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai.Airport.S01E05.Teil5",
				ParsedString = "Dubai.Airport.S01E05.Teil5.720p.DOKU.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test006"
			});



			// Usual with custom spacer - Test007
			id = new SeriesID();
			id.Episode = 5;
			id.EpisodeTitle = "Teil5";
			id.FileExtension = "mkv";
			id.IsSeries = true;
			id.OriginalString = "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "DOKU", "x264" };
			id.Resolution = Resolutions.HD_720p;
			id.Season = 1;
			id.State = State.OK_SUCCESS;
			id.Title = "Dubai-Airport";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Dubai-Airport-S01E05-Teil5",
				ParsedString = "Dubai-Airport-S01E05-Teil5-720p-DOKU-German-x264.mkv",
				Expected = id,
				Settings = new ParserSettings() { NewSpacingChar = '-' },
				Comment = "Test007"
			});



			//Remove Hoster-String - Test008
			id = new SeriesID();
			id.Episode = 10;
			id.EpisodeTitle = null;
			id.FileExtension = null;
			id.IsSeries = true;
			id.OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264-iNFOTv";
			id.RemovedTokens = new List<string>() { "German", "DUBBED", "h264", "WebHD" };
			id.Resolution = Resolutions.FullHD_1080p;
			id.Season = 2;
			id.State = State.OK_SUCCESS;
			id.Title = "Better.Call.Saul";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test008"
			});



			//Remove Hoster-String with spaces - Test009
			id = new SeriesID();
			id.Episode = 10;
			id.EpisodeTitle = null;
			id.FileExtension = null;
			id.IsSeries = true;
			id.OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv";
			id.RemovedTokens = new List<string>() { "German", "DUBBED", "h264", "WebHD" };
			id.Resolution = Resolutions.FullHD_1080p;
			id.Season = 2;
			id.State = State.OK_SUCCESS;
			id.Title = "Better.Call.Saul";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test009"
			});



			//Remove Hoster-String with spaces and extension - Test010
			id = new SeriesID();
			id.Episode = 10;
			id.EpisodeTitle = null;
			id.FileExtension = "avi";
			id.IsSeries = true;
			id.OriginalString = "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv.avi";
			id.RemovedTokens = new List<string>() { "German", "DUBBED", "h264", "WebHD" };
			id.Resolution = Resolutions.FullHD_1080p;
			id.Season = 2;
			id.State = State.OK_SUCCESS;
			id.Title = "Better.Call.Saul";
			id.Year = -1;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Better.Call.Saul.S02E10",
				ParsedString = "Better.Call.Saul.S02E10.1080p.DUBBED.German.h264.WebHD.avi",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test010"
			});



			// Movie with extension - Test011
			id = new SeriesID();
			id.Episode = -1;
			id.EpisodeTitle = null;
			id.FileExtension = "mkv";
			id.IsSeries = false;
			id.OriginalString = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			id.RemovedTokens = new List<string>() { "German", "x264", "EXTENDED", "BluRay" };
			id.Resolution = Resolutions.FullHD_1080p;
			id.Season = -1;
			id.State = State.OK_SUCCESS;
			id.Title = "Der.Hobbit.Smaugs.Einoede";
			id.Year = 2013;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "Der.Hobbit.Smaugs.Einoede",
				ParsedString = "Der.Hobbit.Smaugs.Einoede.2013.1080p.BluRay.EXTENDED.German.x264.mkv",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test011"
			});



			// Movie usual - Test012
			id = new SeriesID();
			id.Episode = -1;
			id.EpisodeTitle = null;
			id.FileExtension = null;
			id.IsSeries = false;
			id.OriginalString = "A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264";
			id.RemovedTokens = new List<string>() { "German", "DTS", "x264" };
			id.Resolution = Resolutions.FullHD_1080p;
			id.Season = -1;
			id.State = State.OK_SUCCESS;
			id.Title = "A.Chinese.Ghost.Story.3";
			id.Year = 1991;

			testData.Add(new TestData()
			{
				Actual = id.OriginalString,
				FullTitle = "A.Chinese.Ghost.Story.3",
				ParsedString = "A.Chinese.Ghost.Story.3.1991.1080p.DTS.German.x264",
				Expected = id,
				Settings = new ParserSettings(),
				Comment = "Test012"
			});


			//_data.Add(new TestData() { actual = "The.Big.Bang.Theory.S09E12.Der.romantische.Asteroid.GERMAN.DL.DUBBED.1080p.WebHD.x264", expected = "x" });     // Usual
			//_data.Add(new TestData() { actual = "House.of.Cards.S04E04.Akt.der.Verzweiflung.German.DD51.Synced.DL.2160p.NetflixUHD.x264", expected = "" });     // Usual
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                // Usual
			//_data.Add(new TestData() { actual = "NCIS.S01E01.Air.Force.One.German.DD20.Dubbed.DL.720p.iTunesHD.AVC-TVS", expected = "" });                      // Resolution and hoster
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.2160p.WebHD.x264", expected = "" });                                // Find 2160p?
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.mkv", expected = "" });                            // Extension
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.MKV", expected = "" });                            // Extension large
			//_data.Add(new TestData() { actual = "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264.avi", expected = "" });                            // Extension
			//_data.Add(new TestData() { actual = "Arrow.s04e03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                // Find of small s-e
			//_data.Add(new TestData() { actual = "ARROW.S04E03.AUFERSTEHUNG.GERMAN.DUBBED.DL.2160P.WEBHD.X264", expected = "" });                                // Large chars converted to Default
			//_data.Add(new TestData() { actual = "Arrow.s04e3102.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                              // Find s-e with four digets
			//_data.Add(new TestData() { actual = "S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264", expected = "" });                                      // No title
			//_data.Add(new TestData() { actual = "059.-.Peter.und.das.Sofamobil", expected = "" });                                                             // Only episode on the beginning; Replace big spacer
			//																																				   // Movies
			//_data.Add(new TestData() { actual = "Black.Mass.2015.German.AC3D.DL.1080p.WEB-DL.h264.mkv", expected = "" });                                       // Usual
			//_data.Add(new TestData() { actual = "Sie nannten ihn Knochenbrecher", expected = "" });

			return testData;
		}

	}
}
