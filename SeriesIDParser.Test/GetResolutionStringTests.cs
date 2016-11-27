using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class GetResolutionStringTests
	{
		[TestMethod]
		public void GetResolutionStringTestLowestDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(1) Should give one full HD");
		}


		[TestMethod]
		public void GetResolutionStringTestLowestSingle()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD, Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(2) Should give one full HD");
		}


		[TestMethod]
		public void GetResolutionStringTestLowestWithUnknown()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(3) Should give one full HD");
		}


		[TestMethod]
		public void GetResolutionStringTestLowestEmpty()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
			Assert.AreEqual(ps.ResolutionStringUnknown, Helper.GetResolutionString(ps, new List<ResolutionsMap>()), "(4) Should give unknown");
		}


		[TestMethod]
		public void GetResolutionStringTestHighestDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringUltraHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(10) Should give one UHD");
		}


		[TestMethod]
		public void GetResolutionStringTestHighestSingle()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(11) Should give one full HD");
		}


		[TestMethod]
		public void GetResolutionStringTestHighestUnknown()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringUltraHD8k,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(12) Should give one UHD");
		}


		[TestMethod]
		public void GetResolutionStringTestHighestEmpty()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
			Assert.AreEqual(ps.ResolutionStringUnknown,
				Helper.GetResolutionString(ps, new List<ResolutionsMap>()),
				"(13) Should give unknown");
		}


		[TestMethod]
		public void GetResolutionStringTestAllDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p }),
				"(20) Should give HD + UHD");
		}


		[TestMethod]
		public void GetResolutionStringTestAllSingle()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringFullHD,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p }),
				"(21) Should give one full HD");
		}


		[TestMethod]
		public void GetResolutionStringTestAllUnknown()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD8k,
				Helper.GetResolutionString(ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p }),
				"(22) Should give UHD HD");
		}


		[TestMethod]
		public void GetResolutionStringTestAllEmpty()
		{
			ParserSettings ps = new ParserSettings(true);
			ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
			Assert.AreEqual(ps.ResolutionStringUnknown,
				Helper.GetResolutionString(ps, new List<ResolutionsMap>()),
				"(23) Should give unknown");
		}
	}
}
