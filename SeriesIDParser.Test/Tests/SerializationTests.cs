using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;

namespace SeriesIDParser.Test
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
			expectedParserSettings.DetectFullHDTokens.Add("Test");
			expectedParserSettings.DetectHDTokens.Clear();
			expectedParserSettings.DetectSDTokens.Add("Test2");
			expectedParserSettings.DetectUltraHD8kTokens.Clear();
			expectedParserSettings.DetectUltraHDTokens.Add("Test3");
			expectedParserSettings.FileExtensions.Add("AAA");
			expectedParserSettings.IDStringFormaterSeason = "ABC+#*7543";
			expectedParserSettings.NewSpacingChar = 'x';
			expectedParserSettings.PossibleSpacingChars.Remove('.');
			expectedParserSettings.ThrowExceptionInsteadOfErrorFlag = true;

			// Serialization
			string ps1String = ParserSettings. SerializeToXML(expectedParserSettings);
			ParserSettings actualParserSettings = ParserSettings.DeSerializeFromXML(ps1String);

			// Compare
			CollectionAssert.AreEqual(expectedParserSettings.DetectFullHDTokens, actualParserSettings.DetectFullHDTokens, "DetectFullHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectHDTokens, actualParserSettings.DetectHDTokens, "DetectHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectSDTokens, actualParserSettings.DetectSDTokens, "DetectSDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectUltraHD8kTokens, actualParserSettings.DetectUltraHD8kTokens, "DetectUltraHD8kTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.DetectUltraHDTokens, actualParserSettings.DetectUltraHDTokens, "DetectUltraHDTokens ");
			CollectionAssert.AreEqual(expectedParserSettings.FileExtensions, actualParserSettings.FileExtensions, "FileExtensions ");
			Assert.AreEqual(expectedParserSettings.IDStringFormaterSeason, actualParserSettings.IDStringFormaterSeason, "IDStringFormaterSeason ");
			Assert.AreEqual(expectedParserSettings.IDStringFormaterEpisode, actualParserSettings.IDStringFormaterEpisode, "IDStringFormaterEpisode ");
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
	}
}
