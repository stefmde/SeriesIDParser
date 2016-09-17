using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser;
using System.Collections.Generic;
using System.Linq;


namespace SeriesIDParser_Test
{
	[TestClass]
	public class UnitTest1
	{

		[TestMethod]
		public void MainFunctionTest()
		{
			PrepareTest pt = new PrepareTest();
			List<TestData> testData = pt.GetTestData();

			foreach (TestData td in testData)
			{
				SeriesID idp = new SeriesID(td.Settings);
				SeriesID id = idp.Parse(td.Actual);
				Assert.AreEqual(td.FullTitle, id.FullTitle, td.Comment + "-FullTitle ");
				Assert.AreEqual(td.ParsedString, id.ParsedString, td.Comment + "-ParsedString ");
				Assert.AreEqual(td.Expected.Episode, id.Episode, td.Comment + "-Episode ");
				Assert.AreEqual(td.Expected.EpisodeTitle, id.EpisodeTitle, td.Comment + "-EpisodeTitle ");
				Assert.AreEqual(td.Expected.FileExtension, id.FileExtension, td.Comment + "-FileExtension ");
				Assert.AreEqual(td.Expected.IsSeries, id.IsSeries, td.Comment + "-IsSeries ");
				Assert.AreEqual(td.Actual, id.OriginalString, td.Comment + "-OriginalString manipulated ");
				CollectionAssert.AreEqual(td.Expected.RemovedTokens.OrderBy(x => x).ToList(), id.RemovedTokens.OrderBy(x => x).ToList(), td.Comment + "-RemovedTokens ");
				CollectionAssert.AreEqual(td.Expected.Resolutions.OrderBy(x => x).ToList(), id.Resolutions.OrderBy(x => x).ToList(), td.Comment + "-Resolution ");
				Assert.AreEqual(td.Expected.Season, id.Season, td.Comment + "-Season ");
				Assert.AreEqual(td.Expected.State, id.State, td.Comment + "-State ");
				Assert.AreEqual(td.Expected.Title, id.Title, td.Comment + "-Title ");
				Assert.AreEqual(td.Expected.Year, id.Year, td.Comment + "-Year ");
				Assert.AreEqual(td.Expected.DetectedOldSpacingChar, id.DetectedOldSpacingChar, td.Comment + "-DetectedOldSpacingChar ");
			}
		}


		[TestMethod]
		public void GetSpacingCharTest()
		{
			Assert.AreEqual('.', new SeriesID().GetSpacingChar("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv"), "Should return a .");
			Assert.AreEqual('.', new SeriesID().GetSpacingChar("Dubai.Airport,S01E05.Teil5.GERMAN.DOKU-HDTV.720p.x264.mkv"), "Should return a .");
			Assert.AreEqual('-', new SeriesID().GetSpacingChar("Dubai-Airport-S01E05-Teil5-GERMAN-DOKU-HDTV-720p-x264.mkv"), "Should return a -");
			Assert.AreEqual(' ', new SeriesID().GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN DOKU HDTV 720p x264.mkv"), "Should return a space");
			Assert.AreEqual(new char(), new SeriesID().GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN-DOKU-HDTV-720p-x264"), "Should return a empy char");
		}


		[TestMethod]
		public void DeSerializationTest()
		{
			// Create and modify object
			ParserSettings ps1 = new ParserSettings();
			ps1.DetectFullHDTokens.Add("Test");
			ps1.DetectHDTokens.Clear();
			ps1.DetectSDTokens.Add("Test2");
			ps1.DetectUltraHD8kTokens.Clear();
			ps1.DetectUltraHDTokens.Add("Test3");
			ps1.FileExtensions.Add("AAA");
			ps1.IDStringFormater = "ABC+#*7543";
			ps1.NewSpacingChar = 'x';
			ps1.PossibleSpacingChars.Remove('.');
			ps1.ThrowExceptionInsteadOfErrorFlag = true;

			// Serialization
			string ps1String = ParserSettings.SerializeToXML(ps1);
			ParserSettings ps2 = ParserSettings.DeSerializeFromXML(ps1String);

			// Compare
			CollectionAssert.AreEqual(ps1.DetectFullHDTokens, ps2.DetectFullHDTokens, "DetectFullHDTokens ");
			CollectionAssert.AreEqual(ps1.DetectHDTokens, ps2.DetectHDTokens, "DetectHDTokens ");
			CollectionAssert.AreEqual(ps1.DetectSDTokens, ps2.DetectSDTokens, "DetectSDTokens ");
			CollectionAssert.AreEqual(ps1.DetectUltraHD8kTokens, ps2.DetectUltraHD8kTokens, "DetectUltraHD8kTokens ");
			CollectionAssert.AreEqual(ps1.DetectUltraHDTokens, ps2.DetectUltraHDTokens, "DetectUltraHDTokens ");
			CollectionAssert.AreEqual(ps1.FileExtensions, ps2.FileExtensions, "FileExtensions ");
			Assert.AreEqual(ps1.IDStringFormater, ps2.IDStringFormater, "IDStringFormater ");
			Assert.AreEqual(ps1.NewSpacingChar, ps2.NewSpacingChar, "NewSpacingChar ");
			CollectionAssert.AreEqual(ps1.PossibleSpacingChars, ps2.PossibleSpacingChars, "PossibleSpacingChars ");
			CollectionAssert.AreEqual(ps1.RemoveAndListTokens, ps2.RemoveAndListTokens, "RemoveAndListTokens ");
			CollectionAssert.AreEqual(ps1.RemoveWithoutListTokens, ps2.RemoveWithoutListTokens, "RemoveWithoutListTokens ");
			Assert.AreEqual(ps1.ResolutionStringFullHD, ps2.ResolutionStringFullHD, "ResolutionStringFullHD ");
			Assert.AreEqual(ps1.ResolutionStringHD, ps2.ResolutionStringHD, "ResolutionStringHD ");
			Assert.AreEqual(ps1.ResolutionStringSD, ps2.ResolutionStringSD, "ResolutionStringSD ");
			Assert.AreEqual(ps1.ResolutionStringUltraHD, ps2.ResolutionStringUltraHD, "ResolutionStringUltraHD ");
			Assert.AreEqual(ps1.ResolutionStringUltraHD8k, ps2.ResolutionStringUltraHD8k, "ResolutionStringUltraHD8k ");
			Assert.AreEqual(ps1.SeasonAndEpisodeParseRegex, ps2.SeasonAndEpisodeParseRegex, "SeasonAndEpisodeParseRegex ");
			Assert.AreEqual(ps1.ThrowExceptionInsteadOfErrorFlag, ps2.ThrowExceptionInsteadOfErrorFlag, "ThrowExceptionInsteadOfErrorFlag ");
		}


		[TestMethod]
		public void GetYearTest()
		{
			Assert.AreEqual(2013, new SeriesID().GetYear("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give a year");
			Assert.AreEqual(-1, new SeriesID().GetYear("Der.Hobbit.Smaugs.Einoede.1899.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give no year");
			Assert.AreEqual(1900, new SeriesID().GetYear("Der.Hobbit.Smaugs.Einoede.1900.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give a year");
			Assert.AreEqual(-1, new SeriesID().GetYear("Der.Hobbit.Smaugs.Einoede." + DateTime.Now.AddYears(1) + ".EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give no year");
			Assert.AreEqual(2013, new SeriesID().GetYear("Der,Hobbit-Smaugs;Einoedex2013?EXTENDED(German)DL/1080p*BluRay+x264#mkv"), "Should give a year");
		}


		[TestMethod]
		public void GetFileExtensionTest()
		{
			ParserSettings ps = new ParserSettings(true);

			Assert.AreEqual("mkv", new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.FileExtensions), "(1) Should give a extension");
			Assert.AreEqual("avi", new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.avi", ps.FileExtensions), "(2) Should give a extension");
			Assert.AreEqual("m4v", new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.m4v", ps.FileExtensions), "(3) Should give a extension");
			Assert.AreEqual("wmv", new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.wmv", ps.FileExtensions), "(4) Should give a extension");
			Assert.AreEqual(null, new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.xyz", ps.FileExtensions), "(5) Should give no extension");
			Assert.AreEqual("wmv", new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.WmV", ps.FileExtensions), "(6) Should give a extension");
			Assert.AreEqual("mp4", new SeriesID().GetFileExtension("Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mp4", ps.FileExtensions), "(7) Should give a extension");
			Assert.AreEqual(null, new SeriesID().GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264", ps.FileExtensions), "(8) Should give no extension");
		}


		[TestMethod]
		public void RemoveTokensTest()
		{
			ParserSettings ps = new ParserSettings(true);

			// Regular Test
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
				new SeriesID().RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
				"(1) Should give a string");
			Assert.AreEqual(null,
				new SeriesID().RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", null, ps.RemoveWithoutListTokens, false),
				"(2) Should give null");
			Assert.AreEqual(null,
				new SeriesID().RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", new List<string>(), false),
				"(3) Should give null");
			Assert.AreEqual(null,
				new SeriesID().RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", null, false),
				"(4) Should give null");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
				new SeriesID().RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.MIRROR.WEB.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
				"(5) Should give a string");

			// Regex Test
			ps.RemoveWithoutListTokens.Clear();
			ps.RemoveWithoutListTokens.Add("");
			// TODO implement regex test
		}


		[TestMethod]
		public void ReplaceTokensTest()
		{
			ParserSettings ps = new ParserSettings(true);

			// Regular Test
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("EXTENDED", "."));

			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv",
				new SeriesID().ReplaceTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.ReplaceRegexWithoutListTokens, false),
				"(1) Should give a string");

			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>(".EXTENDED.", "."));
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv",
				new SeriesID().ReplaceTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.ReplaceRegexWithoutListTokens, false),
				"(2) Should give a string");

			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("extended", "."));
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv",
				new SeriesID().ReplaceTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.ReplaceRegexWithoutListTokens, false),
				"(3) Should give a string");
			Assert.AreEqual("Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv",
				new SeriesID().ReplaceTokens("Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv", ".", ps.ReplaceRegexWithoutListTokens, false),
				"(4) Should give a string");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv",
				new SeriesID().ReplaceTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", new List<KeyValuePair<string, string>>(), false),
				"(5) Should give a string");
			Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv",
				new SeriesID().ReplaceTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", null, false),
				"(6) Should give a pass throw");


			// Regex Test
			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("extended", "xxXxx"));
			// TODO implement regex test
		}


		[TestMethod]
		public void GetResolutionStringTest()
		{
			ParserSettings ps = new ParserSettings(true);

			// Lowest
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(1) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(2) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(3) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap>()),
				"(4) Should give unknown");

			// Highest
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringUltraHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(10) Should give one UHD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(11) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringUltraHD8k,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(12) Should give one UHD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap>()),
				"(13) Should give unknown");

			// All
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(20) Should give HD + UHD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(21) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD  + ps.NewSpacingChar + ps.ResolutionStringUltraHD8k,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(22) Should give UHD HD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				new SeriesID().GetResolutionString(ps, new List<ResolutionsMap>()),
				"(23) Should give unknown");
		}

	}
}
