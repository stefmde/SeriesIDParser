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
	}
}
