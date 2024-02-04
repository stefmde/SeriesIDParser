// MIT License
// 
// Copyright(c) 2016 - 2024
// Stefan (StefmDE) Müller, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class GetResolutionStringTests
{
	[Test]
	public void GetResolutionStringTestLowestDefault()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
		Assert.AreEqual( ps.ResolutionStringFullHD, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p } ),
						"(1) Should give one full HD" );
	}

	[Test]
	public void GetResolutionStringTestLowestSingle()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
		Assert.AreEqual( ps.ResolutionStringFullHD, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p } ), "(2) Should give one full HD" );
	}

	[Test]
	public void GetResolutionStringTestLowestWithUnknown()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
		Assert.AreEqual( ps.ResolutionStringFullHD,
						HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p } ),
						"(3) Should give one full HD" );
	}

	[Test]
	public void GetResolutionStringTestLowestEmpty()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.LowestResolution;
		Assert.AreEqual( ps.ResolutionStringUnknown, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap>() ), "(4) Should give unknown" );
	}

	[Test]
	public void GetResolutionStringTestHighestDefault()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
		Assert.AreEqual( ps.ResolutionStringUltraHD, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p } ),
						"(10) Should give one UHD" );
	}

	[Test]
	public void GetResolutionStringTestHighestSingle()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
		Assert.AreEqual( ps.ResolutionStringFullHD, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p } ), "(11) Should give one full HD" );
	}

	[Test]
	public void GetResolutionStringTestHighestUnknown()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
		Assert.AreEqual( ps.ResolutionStringUltraHD8k,
						HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p } ),
						"(12) Should give one UHD" );
	}

	[Test]
	public void GetResolutionStringTestHighestEmpty()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.HighestResolution;
		Assert.AreEqual( ps.ResolutionStringUnknown, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap>() ), "(13) Should give unknown" );
	}

	[Test]
	public void GetResolutionStringTestAllDefault()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
		Assert.AreEqual( ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD,
						HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p, ResolutionsMap.UltraHD_2160p } ), "(20) Should give HD + UHD" );
	}

	[Test]
	public void GetResolutionStringTestAllSingle()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
		Assert.AreEqual( ps.ResolutionStringFullHD, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.FullHD_1080p } ), "(21) Should give one full HD" );
	}

	[Test]
	public void GetResolutionStringTestAllUnknown()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
		Assert.AreEqual( ps.ResolutionStringFullHD + ps.NewSpacingChar + ps.ResolutionStringUltraHD8k,
						HelperWorker.GetResolutionString( ps, new List<ResolutionsMap> { ResolutionsMap.Unknown, ResolutionsMap.UltraHD8K_4320p, ResolutionsMap.FullHD_1080p } ),
						"(22) Should give UHD HD" );
	}

	[Test]
	public void GetResolutionStringTestAllEmpty()
	{
		ParserSettings ps = new(true);
		ps.ResolutionStringOutput = ResolutionOutputBehavior.AllFoundResolutions;
		Assert.AreEqual( ps.ResolutionStringUnknown, HelperWorker.GetResolutionString( ps, new List<ResolutionsMap>() ), "(23) Should give unknown" );
	}
}