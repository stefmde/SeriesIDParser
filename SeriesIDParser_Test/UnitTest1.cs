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
		public void TestMainFunction()
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
				Assert.AreEqual(td.Expected.Resolution, id.Resolution, td.Comment + "-Resolution ");
				Assert.AreEqual(td.Expected.Season, id.Season, td.Comment + "-Season ");
				Assert.AreEqual(td.Expected.State, id.State, td.Comment + "-State ");
				Assert.AreEqual(td.Expected.Title, id.Title, td.Comment + "-Title ");
				Assert.AreEqual(td.Expected.Year, id.Year, td.Comment + "-Year ");
			}
		}

		[TestMethod]
		public void TestGetSpacingChar()
		{
			SeriesID idp = new SeriesID();
			string actual = idp.GetSpacingChar("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv");
			Assert.AreEqual(".", actual);
		}


		//[TestMethod]
		//public void DeSerialization()
		//{
		//	// Create and modify object
		//	ParserSettings ps1 = new ParserSettings();
		//	ps1.DetectFullHDTokens.Add("Test");
		//	ps1.DetectHDTokens.Clear();
		//	ps1.DetectSDTokens.Add("Test2");
		//	ps1.DetectUltraHD8kTokens.Clear();
		//	ps1.DetectUltraHDTokens.Add("Test3");
		//	ps1.FileExtensions.Add("AAA");
		//	ps1.IDStringFormater = "ABC+#*7543";
		//	ps1.NewSpacingChar = 'x';
		//	ps1.PossibleSpacingChars.Remove('.');
		//	ps1.ThrowExceptionInsteadOfErrorFlag = true;

		//	// Serialization
		//	string ps1String = ParserSettings.SerializeToXML(ps1);
		//	ParserSettings ps2 = ParserSettings.DeSerializeFromXML(ps1String);

		//	// Compare
		//	CollectionAssert.AreEqual(ps1.DetectFullHDTokens, ps2.DetectFullHDTokens, "DetectFullHDTokens ");
		//	CollectionAssert.AreEqual(ps1.DetectHDTokens, ps2.DetectHDTokens, "DetectHDTokens ");
		//	CollectionAssert.AreEqual(ps1.DetectSDTokens, ps2.DetectSDTokens, "DetectSDTokens ");
		//	CollectionAssert.AreEqual(ps1.DetectUltraHD8kTokens, ps2.DetectUltraHD8kTokens, "DetectUltraHD8kTokens ");
		//	CollectionAssert.AreEqual(ps1.DetectUltraHDTokens, ps2.DetectUltraHDTokens, "DetectUltraHDTokens ");
		//	CollectionAssert.AreEqual(ps1.FileExtensions, ps2.FileExtensions, "FileExtensions ");
		//	Assert.AreEqual(ps1.IDStringFormater, ps2.IDStringFormater, "IDStringFormater ");
		//	Assert.AreEqual(ps1.NewSpacingChar, ps2.NewSpacingChar, "NewSpacingChar ");
		//	CollectionAssert.AreEqual(ps1.PossibleSpacingChars, ps2.PossibleSpacingChars, "PossibleSpacingChars ");
		//	CollectionAssert.AreEqual(ps1.RemoveAndListTokens, ps2.RemoveAndListTokens, "RemoveAndListTokens ");
		//	CollectionAssert.AreEqual(ps1.RemoveWithoutListTokens, ps2.RemoveWithoutListTokens, "RemoveWithoutListTokens ");
		//	Assert.AreEqual(ps1.ResolutionStringFullHD, ps2.ResolutionStringFullHD, "ResolutionStringFullHD ");
		//	Assert.AreEqual(ps1.ResolutionStringHD, ps2.ResolutionStringHD, "ResolutionStringHD ");
		//	Assert.AreEqual(ps1.ResolutionStringSD, ps2.ResolutionStringSD, "ResolutionStringSD ");
		//	Assert.AreEqual(ps1.ResolutionStringUltraHD, ps2.ResolutionStringUltraHD, "ResolutionStringUltraHD ");
		//	Assert.AreEqual(ps1.ResolutionStringUltraHD8k, ps2.ResolutionStringUltraHD8k, "ResolutionStringUltraHD8k ");
		//	Assert.AreEqual(ps1.SeasonAndEpisodeParseRegex, ps2.SeasonAndEpisodeParseRegex, "SeasonAndEpisodeParseRegex ");
		//	Assert.AreEqual(ps1.ThrowExceptionInsteadOfErrorFlag, ps2.ThrowExceptionInsteadOfErrorFlag, "ThrowExceptionInsteadOfErrorFlag ");
		//}
	}
}
