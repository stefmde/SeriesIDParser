// 
// MIT License
// 
// Copyright(c) 2016 - 2017
// Stefan Müller, Stefm, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
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


using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

// ReSharper disable MissingXmlDoc

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
			Assert.AreEqual('.', HelperWorker.GetSpacingChar("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv", ps), "Should return a '.'");
		}

		[TestMethod]
		public void GetSpacingCharTestWithNoise()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual('.', HelperWorker.GetSpacingChar("Dubai.Airport,S01E05.Teil5.GERMAN.DOKU-HDTV.720p.x264.mkv", ps), "Should return a '.'");
		}

		[TestMethod]
		public void GetSpacingCharTestDashChar()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual('-', HelperWorker.GetSpacingChar("Dubai-Airport-S01E05-Teil5-GERMAN-DOKU-HDTV-720p-x264.mkv", ps), "Should return a '-'");
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
			Assert.AreEqual(new char(), HelperWorker.GetSpacingChar("Dubai Airport S01E05 Teil5 GERMAN-DOKU-HDTV-720p-x264", ps),
							"Should return a empy char");
		}
	}
}