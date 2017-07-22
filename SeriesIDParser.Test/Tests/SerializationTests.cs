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

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.Test.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class SerializationTests
	{
		[TestMethod]
		public void DeSerializationTestDefault()
		{
			// Create and modify object
			ParserSettings expectedParserSettings = new ParserSettings();
			expectedParserSettings.DetectFullHDTokens.Add( "Test" );
			expectedParserSettings.DetectHDTokens.Clear();
			expectedParserSettings.DetectSDTokens.Add( "Test2" );
			expectedParserSettings.DetectUltraHD8kTokens.Clear();
			expectedParserSettings.DetectUltraHDTokens.Add( "Test3" );
			expectedParserSettings.FileExtensions.Add( "AAA" );
			expectedParserSettings.IDStringFormaterSeason = "ABC+#*7543";
			expectedParserSettings.NewSpacingChar = 'x';
			expectedParserSettings.PossibleSpacingChars.Remove( '.' );
			expectedParserSettings.ThrowExceptionInsteadOfErrorFlag = true;
			expectedParserSettings.CacheItemCountLimit = 15;
			expectedParserSettings.CacheMode = CacheMode.Simple;

			// Serialization
			string ps1String = ParserSettings.SerializeToXML( expectedParserSettings );
			ParserSettings actualParserSettings = ParserSettings.DeSerializeFromXML( ps1String );

			// Compare
			CollectionAssert.AreEqual( expectedParserSettings.DetectFullHDTokens, actualParserSettings.DetectFullHDTokens, "DetectFullHDTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.DetectHDTokens, actualParserSettings.DetectHDTokens, "DetectHDTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.DetectSDTokens, actualParserSettings.DetectSDTokens, "DetectSDTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.DetectUltraHD8kTokens, actualParserSettings.DetectUltraHD8kTokens, "DetectUltraHD8kTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.DetectUltraHDTokens, actualParserSettings.DetectUltraHDTokens, "DetectUltraHDTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.FileExtensions, actualParserSettings.FileExtensions, "FileExtensions " );
			Assert.AreEqual( expectedParserSettings.IDStringFormaterSeason, actualParserSettings.IDStringFormaterSeason, "IDStringFormaterSeason " );
			Assert.AreEqual( expectedParserSettings.IDStringFormaterEpisode, actualParserSettings.IDStringFormaterEpisode, "IDStringFormaterEpisode " );
			Assert.AreEqual( expectedParserSettings.NewSpacingChar, actualParserSettings.NewSpacingChar, "NewSpacingChar " );
			CollectionAssert.AreEqual( expectedParserSettings.PossibleSpacingChars, actualParserSettings.PossibleSpacingChars, "PossibleSpacingChars " );
			CollectionAssert.AreEqual( expectedParserSettings.RemoveAndListTokens, actualParserSettings.RemoveAndListTokens, "RemoveAndListTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.RemoveWithoutListTokens, actualParserSettings.RemoveWithoutListTokens,
										"RemoveWithoutListTokens " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringFullHD, actualParserSettings.ResolutionStringFullHD, "ResolutionStringFullHD " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringHD, actualParserSettings.ResolutionStringHD, "ResolutionStringHD " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringSD, actualParserSettings.ResolutionStringSD, "ResolutionStringSD " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringUltraHD, actualParserSettings.ResolutionStringUltraHD, "ResolutionStringUltraHD " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringUltraHD8k, actualParserSettings.ResolutionStringUltraHD8k, "ResolutionStringUltraHD8k " );
			Assert.AreEqual( expectedParserSettings.SeasonAndEpisodeParseRegex, actualParserSettings.SeasonAndEpisodeParseRegex,
							"SeasonAndEpisodeParseRegex " );
			Assert.AreEqual( expectedParserSettings.ThrowExceptionInsteadOfErrorFlag, actualParserSettings.ThrowExceptionInsteadOfErrorFlag,
							"ThrowExceptionInsteadOfErrorFlag " );
			CollectionAssert.AreEqual( expectedParserSettings.ReplaceRegexAndListTokens, actualParserSettings.ReplaceRegexAndListTokens,
										"ReplaceRegexAndListTokens " );
			CollectionAssert.AreEqual( expectedParserSettings.ReplaceRegexWithoutListTokens, actualParserSettings.ReplaceRegexWithoutListTokens,
										"ReplaceRegexWithoutListTokens " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringOutput, actualParserSettings.ResolutionStringOutput, "ResolutionStringOutput " );
			Assert.AreEqual( expectedParserSettings.ResolutionStringUnknown, actualParserSettings.ResolutionStringUnknown, "ResolutionStringUnknown " );
			Assert.AreEqual( expectedParserSettings.CacheItemCountLimit, actualParserSettings.CacheItemCountLimit, "CacheItemCountLimit " );
			Assert.AreEqual( expectedParserSettings.CacheMode, actualParserSettings.CacheMode, "CacheMode " );
		}
	}
}