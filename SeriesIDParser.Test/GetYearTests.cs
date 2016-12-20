using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class GetYearTests
	{
		[TestMethod]
		public void GetYearTestDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(2013, HelperWorker.GetYear("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex), "Should give a year");
		}

		[TestMethod]
		public void GetYearTestToOld()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(-1, HelperWorker.GetYear("Der.Hobbit.Smaugs.Einoede.1899.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex), "Should give no year");
		}

		[TestMethod]
		public void GetYearTestOldest()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(1900, HelperWorker.GetYear("Der.Hobbit.Smaugs.Einoede.1900.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex), "Should give a year");
		}

		[TestMethod]
		public void GetYearTestFuture()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(-1, HelperWorker.GetYear("Der.Hobbit.Smaugs.Einoede." + DateTime.Now.AddYears(1) + ".EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.YearParseRegex), "Should give no year");
		}

		[TestMethod]
		public void GetYearTestSpecialCharNoise()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(2013, HelperWorker.GetYear("Der,Hobbit-Smaugs;Einoedex2013?EXTENDED(German)DL/1080p*BluRay+x264#mkv", ps.YearParseRegex), "Should give a year");
		}
	}
}
