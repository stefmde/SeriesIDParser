using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Worker;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class ReplaceTokensTests
	{
		[TestMethod]
		public void ReplaceTokensTestDefault()
		{
			ParserSettings ps = new ParserSettings(true);

			// Regular Test
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("EXTENDED", "."));
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() {};

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestDots()
		{
			ParserSettings ps = new ParserSettings(true);

			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>(".EXTENDED.", "."));
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestCase()
		{
			ParserSettings ps = new ParserSettings(true);

			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("extended", "."));
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestNothing()
		{
			ParserSettings ps = new ParserSettings(true);

			string input = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
			string expected = "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestEmptyList()
		{
			ParserSettings ps = new ParserSettings(true);

			ps.ReplaceRegexWithoutListTokens = new List<KeyValuePair<string, string>>();
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestNullList()
		{
			ParserSettings ps = new ParserSettings(true);

			ps.ReplaceRegexWithoutListTokens = null;
			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}


		[TestMethod]
		public void ReplaceTokensTestRegexTest()
		{
			ParserSettings ps = new ParserSettings(true);

			ps.ReplaceRegexWithoutListTokens = new List<KeyValuePair<string, string>>();
			ps.ReplaceRegexWithoutListTokens.Clear();
			ps.ReplaceRegexWithoutListTokens.Add(new KeyValuePair<string, string>("e.tended", "."));

			string input = "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv";
			string expected = "Der.Hobbit.Smaugs.Einoede.2013.German.DL.1080p.BluRay.x264.mkv";
			List<string> expectedOutputList = new List<string>() { };

			List<string> actualRemovedTokens = ParserHelperWorker.ReplaceTokens(ref input, ".", ps.ReplaceRegexWithoutListTokens, false);
			Assert.AreEqual(expected, input, " String compare ");
			CollectionAssert.AreEqual(expectedOutputList, actualRemovedTokens, " List compare ");
		}
	}
}
