
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class UnitTest1
	{
		//[TestMethod]
		//public void GetResolutionByResMapTest()
		//{
		//    ParserSettings ps = new ParserSettings(true);
		//    // TODO implement me
		//    string inputTitle = "";
		//    string expectedTitle = "";
		//    GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 1);

		//    //inputTitle = "";
		//    //expectedTitle = "";
		//    //GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 2);

		//    //inputTitle = "";
		//    //expectedTitle = "";
		//    //GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 3);

		//    //inputTitle = "";
		//    //expectedTitle = "";
		//    //GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 4);

		//    //inputTitle = "";
		//    //expectedTitle = "";
		//    //GetResolutionByResMapTestHelper(ps, '.', inputTitle, expectedTitle, ResolutionsMap.FullHD_1080p, 5);
		//}


		//[TestMethod]
		//public void RemoveTokensTest()
		//{
		//    ParserSettings ps = new ParserSettings(true);
		//    //TODO Implement me
		//    // Regular Test
		//    //Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
		//    //	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
		//    //	"(1) Should give a string");
		//    //Assert.AreEqual(null,
		//    //	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", null, ps.RemoveWithoutListTokens, false),
		//    //	"(2) Should give null");
		//    //Assert.AreEqual(null,
		//    //	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", new List<string>(), false),
		//    //	"(3) Should give null");
		//    //Assert.AreEqual(null,
		//    //	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ".", null, false),
		//    //	"(4) Should give null");
		//    //Assert.AreEqual("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.1080p.BluRay.x264.mkv",
		//    //	Helper.RemoveTokens("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.MIRROR.WEB.1080p.BluRay.x264.mkv", ".", ps.RemoveWithoutListTokens, false),
		//    //	"(5) Should give a string");

		//    // Regex Test
		//    ps.RemoveWithoutListTokens.Clear();
		//    ps.RemoveWithoutListTokens.Add("");
		//    // TODO implement regex test
		//}




		#region Helper

		private static void GetResolutionByResMapTestHelper(ParserSettings ps, char seperator, string actualTitle, string expectedTitle, ResolutionsMap expectedResolution, int id)
		{
			List<ResolutionsMap> actualResolutions = new List<ResolutionsMap>();

			// Try get 8K
			actualResolutions.AddRange(ParserHelperWorker.GetResolutionByResMap(ps.DetectUltraHD8kTokens, ResolutionsMap.UltraHD8K_4320p, seperator, ref actualTitle));

			// Try get 4K
			actualResolutions.AddRange(ParserHelperWorker.GetResolutionByResMap(ps.DetectUltraHDTokens, ResolutionsMap.UltraHD_2160p, seperator, ref actualTitle));

			// Try get FullHD
			actualResolutions.AddRange(ParserHelperWorker.GetResolutionByResMap(ps.DetectFullHDTokens, ResolutionsMap.FullHD_1080p, seperator, ref actualTitle));

			// Try get HD
			actualResolutions.AddRange(ParserHelperWorker.GetResolutionByResMap(ps.DetectHDTokens, ResolutionsMap.HD_720p, seperator, ref actualTitle));

			// Try get SD
			actualResolutions.AddRange(ParserHelperWorker.GetResolutionByResMap(ps.DetectSDTokens, ResolutionsMap.SD_Any, seperator, ref actualTitle));

			actualResolutions = actualResolutions.Distinct().ToList();

			if (actualResolutions.Count == 1)
			{
				Assert.AreEqual(expectedResolution, actualResolutions.LastOrDefault(), "(" + id + ") Collections");
				Assert.AreEqual(expectedTitle, actualTitle, "(" + id + ") Title");
			}
			else
			{
				Assert.Fail("(+" + id + ") Collection Error: More than one resolution found for input: \"" + actualTitle + "\"");
			}
		}

		#endregion Helper

	}
}
