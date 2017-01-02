using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

namespace SeriesIDParser.Test.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class GetSpacingCharTests
	{
		[TestMethod]
		public void GetSpacingCharTestDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual('.', HelperWorker.GetSpacingChar("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", ps), "Should return a .");
		}


		[TestMethod]
		public void GetSpacingCharTestWithNoise()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual('.', HelperWorker.GetSpacingChar("Dubai.Airport,S01E05.Teil5.GERMAN.DOKU-HDTV.720p.x264.mkv", ps), "Should return a .");
		}


		[TestMethod]
		public void GetSpacingCharTestDashChar()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual('-', HelperWorker.GetSpacingChar("Dubai-Airport-S01E05-Teil5-GERMAN-DOKU-HDTV-720p-x264.mkv", ps), "Should return a -");
		}


		[TestMethod]
		public void GetSpacingCharTestCpaceChar()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(' ', HelperWorker.GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN DOKU HDTV 720p x264.mkv", ps), "Should return a space");
		}


		[TestMethod]
		public void GetSpacingCharTestUnknown()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(new char(), HelperWorker.GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN-DOKU-HDTV-720p-x264", ps), "Should return a empy char");
		}
	}
}
