
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
			ParserSettings ps = new ParserSettings(true);

			Assert.AreEqual('.', Helper.GetSpacingChar("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", ps), "Should return a .");
			Assert.AreEqual('.', Helper.GetSpacingChar("Dubai.Airport,S01E05.Teil5.GERMAN.DOKU-HDTV.720p.x264.mkv", ps), "Should return a .");
			Assert.AreEqual('-', Helper.GetSpacingChar("Dubai-Airport-S01E05-Teil5-GERMAN-DOKU-HDTV-720p-x264.mkv", ps), "Should return a -");
			Assert.AreEqual(' ', Helper.GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN DOKU HDTV 720p x264.mkv", ps), "Should return a space");
			Assert.AreEqual(new char(), Helper.GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN-DOKU-HDTV-720p-x264", ps), "Should return a empy char");
		}


		[TestMethod]
		public void DeSerializationTest()
		{
			// Create and modify object
			ParserSettings expectedParserSettings = new ParserSettings();
			expectedParserSettings.DetectFullHDTokens.Add("Test");
			expectedParserSettings.DetectHDTokens.Clear();
			expectedParserSettings.DetectSDTokens.Add("Test2");
			expectedParserSettings.DetectUltraHD8kTokens.Clear();
			expectedParserSettings.DetectUltraHDTokens.Add("Test3");
			expectedParserSettings.FileExtensions.Add("AAA");
			expectedParserSettings.IDStringFormater = "ABC+#*7543";
			expectedParserSettings.NewSpacingChar = 'x';
			expectedParserSettings.PossibleSpacingChars.Remove('.');
			expectedParserSettings.ThrowExceptionInsteadOfErrorFlag = true;

			// Serialization
			string ps1String = ParserSettings.SerializeToXML(expectedParserSettings);
			ParserSettings actualParserSettings = ParserSettings.DeSerializeFromXML(ps1String);

			// Compare
			DeSerializationTestHelper(expectedParserSettings, actualParserSettings);
		}

		private void DeSerializationTestHelper(ParserSettings expectedParserSettings, ParserSettings actualParserSettings)
		{
			CollectionAssert.AreEqual(expectedParserSettings.DetectFullHDTokens, actualParserSettings.DetectFullHDTokens, "DetectFullHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectHDTokens, actualParserSettings.DetectHDTokens, "DetectHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectSDTokens, actualParserSettings.DetectSDTokens, "DetectSDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectUltraHD8kTokens, actualParserSettings.DetectUltraHD8kTokens, "DetectUltraHD8kTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectUltraHDTokens, actualParserSettings.DetectUltraHDTokens, "DetectUltraHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.FileExtensions, actualParserSettings.FileExtensions, "FileExtensions ");
			Assert.AreEqual(expectedParserSettings.IDStringFormater, actualParserSettings.IDStringFormater, "IDStringFormater ");
			Assert.AreEqual(expectedParserSettings.NewSpacingChar, actualParserSettings.NewSpacingChar, "NewSpacingChar ");
			CollectionAssert.AreEqual(expectedParserSettings.PossibleSpacingChars, actualParserSettings.PossibleSpacingChars, "PossibleSpacingChars ");
			CollectionAssert.AreEqual(expectedParserSettings.RemoveAndListTokens, actualParserSettings.RemoveAndListTokens, "RemoveAndListTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.RemoveWithoutListTokens, actualParserSettings.RemoveWithoutListTokens, "RemoveWithoutListTokens ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringFullHD, actualParserSettings.ResolutionStringFullHD, "ResolutionStringFullHD ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringHD, actualParserSettings.ResolutionStringHD, "ResolutionStringHD ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringSD, actualParserSettings.ResolutionStringSD, "ResolutionStringSD ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringUltraHD, actualParserSettings.ResolutionStringUltraHD, "ResolutionStringUltraHD ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringUltraHD8k, actualParserSettings.ResolutionStringUltraHD8k, "ResolutionStringUltraHD8k ");
			Assert.AreEqual(expectedParserSettings.SeasonAndEpisodeParseRegex, actualParserSettings.SeasonAndEpisodeParseRegex, "SeasonAndEpisodeParseRegex ");
			Assert.AreEqual(expectedParserSettings.ThrowExceptionInsteadOfErrorFlag, actualParserSettings.ThrowExceptionInsteadOfErrorFlag, "ThrowExceptionInsteadOfErrorFlag ");
			CollectionAssert.AreEqual(expectedParserSettings.ReplaceRegexAndListTokens, actualParserSettings.ReplaceRegexAndListTokens, "ReplaceRegexAndListTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.ReplaceRegexWithoutListTokens, actualParserSettings.ReplaceRegexWithoutListTokens, "ReplaceRegexWithoutListTokens ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringOutput, actualParserSettings.ResolutionStringOutput, "ResolutionStringOutput ");
			Assert.AreEqual(expectedParserSettings.ResolutionStringUnknown, actualParserSettings.ResolutionStringUnknown, "ResolutionStringUnknown ");
		}




		[TestMethod]
		public void GetYearTest()
		{
			Assert.AreEqual(2013, Helper.GetYear("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give a year");
			Assert.AreEqual(-1, Helper.GetYear("Der.Hobbit.Smaugs.Einoede.1899.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give no year");
			Assert.AreEqual(1900, Helper.GetYear("Der.Hobbit.Smaugs.Einoede.1900.EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give a year");
			Assert.AreEqual(-1, Helper.GetYear("Der.Hobbit.Smaugs.Einoede." + DateTime.Now.AddYears(1) + ".EXTENDED.German.DL.1080p.BluRay.x264.mkv"), "Should give no year");
			Assert.AreEqual(2013, Helper.GetYear("Der,Hobbit-Smaugs;Einoedex2013?EXTENDED(German)DL/1080p*BluRay+x264#mkv"), "Should give a year");
		}


		[TestMethod]
		public void GetFileExtensionTest()
		{
			ParserSettings ps = new ParserSettings(true);

			Assert.AreEqual("mkv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.FileExtensions), "(1) Should give a extension");
			Assert.AreEqual("avi", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.avi", ps.FileExtensions), "(2) Should give a extension");
			Assert.AreEqual("m4v", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.m4v", ps.FileExtensions), "(3) Should give a extension");
			Assert.AreEqual("wmv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.wmv", ps.FileExtensions), "(4) Should give a extension");
			Assert.AreEqual(null, Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.xyz", ps.FileExtensions), "(5) Should give no extension");
			Assert.AreEqual("wmv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.WmV", ps.FileExtensions), "(6) Should give a extension");
			Assert.AreEqual("mp4", Helper.GetFileExtension("Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mp4", ps.FileExtensions), "(7) Should give a extension");
			Assert.AreEqual(null, Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264", ps.FileExtensions), "(8) Should give no extension");
		}


		[TestMethod]
		public void RemoveTokensTest()
		{
			ParserSettings ps = new ParserSettings(true);
			//TODO Implement me
			// Regular Test
			//Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
			//	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
			//	"(1) Should give a string");
			//Assert.AreEqual(null,
			//	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", null, ps.RemoveWithoutListTokens, false),
			//	"(2) Should give null");
			//Assert.AreEqual(null,
			//	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", new List<string>(), false),
			//	"(3) Should give null");
			//Assert.AreEqual(null,
			//	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", null, false),
			//	"(4) Should give null");
			//Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
			//	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.MIRROR.WEB.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
			//	"(5) Should give a string");

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
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 1);


			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>(".EXTENDED.", "."));
			input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 2);


			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("extended", "."));
			input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 3);

			input = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
			expected = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 4);


			ps.ReplaceRegexWithoutListTokens = new List<KeyValuePair<string, string>>();
			input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 5);


			ps.ReplaceRegexWithoutListTokens = null;
			input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 5);


			// Regex Test
			ps.ReplaceRegexWithoutListTokens = new List<KeyValuePair<string, string>>();
			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("e.tended", "."));

			input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			expectedOutputList = new List<string>() { };
			ReplaceTokensTestHelper(ps, input, expected, expectedOutputList, 10);
		}


		private static void ReplaceTokensTestHelper(ParserSettings ps, string inputString, string expectedString, List<string> expectedRemovedTokens, int testID)
		{
			List<string> actualRemovedTokens = Helper.ReplaceTokens(ref inputString, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expectedString, inputString, "(" + testID + ") String compare ");
			CollectionAssert.AreEqual(expectedRemovedTokens, actualRemovedTokens, "(" + testID + ") List compare ");
		}




		[TestMethod]
		public void GetResolutionStringTest()
		{
			ParserSettings ps = new ParserSettings(true);

			// Lowest
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(1) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(2) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(3) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				Helper.GetResolutionString(ps, new List<ResolutionsMap>()),
				"(4) Should give unknown");

			// Highest
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringUltraHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(10) Should give one UHD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(11) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringUltraHD8k,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(12) Should give one UHD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				Helper.GetResolutionString(ps, new List<ResolutionsMap>()),
				"(13) Should give unknown");

			// All
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(20) Should give HD + UHD");
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(21) Should give one full HD");
			Assert.AreEqual(ps.ResolutionStringFullHD  + ps.NewSpacingChar + ps.ResolutionStringUltraHD8k,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.UNKNOWN, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(22) Should give UHD HD");
			Assert.AreEqual(ps.ResolutionStringUnknown,
				Helper.GetResolutionString(ps, new List<ResolutionsMap>()),
				"(23) Should give unknown");
		}


		[TestMethod]
		public void GetResolutionByResMapTest()
		{
			ParserSettings ps = new ParserSettings(true);
			// TODO implement me
			string inputTitle = "";
			string expectedTitle = "";
			GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 1);

			inputTitle = "";
			expectedTitle = "";
			GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 2);

			inputTitle = "";
			expectedTitle = "";
			GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 3);

			inputTitle = "";
			expectedTitle = "";
			GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 4);

			inputTitle = "";
			expectedTitle = "";
			GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 5);
		}


		private static void GetResolutionByResMapTestHelper(ParserSettings ps, char seperator, string actualTitle, string expectedTitle, ResolutionsMap expectedResolution, int id)
		{
			List<ResolutionsMap> actualResolutions = new List<ResolutionsMap>();

			// Try get 8K
			actualResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, seperator, ref actualTitle));

			// Try get 4K
			actualResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, seperator, ref actualTitle));

			// Try get FullHD
			actualResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, seperator, ref actualTitle));

			// Try get HD
			actualResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectHDTokens, ResolutionsMap.HD_720p, seperator, ref actualTitle));

			// Try get SD
			actualResolutions.AddRange(Helper.GetResolutionByResMap(ps.DetectSDTokens, ResolutionsMap.SD_Any, seperator, ref actualTitle));

			actualResolutions = actualResolutions.Distinct().ToList();

			if (actualResolutions.Count == 1)
			{
				Assert.AreEqual(expectedResolution, actualResolutions.LastOrDefault(), "(" + id + ") Collections");
				Assert.AreEqual(expectedTitle, actualTitle, "(+" + id + ") Title");
			}
			else
			{
				Assert.Fail("(+" + id + ") Collection Error: More than one resolution found for input: \"" + actualTitle + "\"");
			}
		}


	}
}
