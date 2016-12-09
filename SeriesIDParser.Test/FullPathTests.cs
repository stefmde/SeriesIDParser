using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesIDParser.Models;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FullPathTests
	{
		[TestMethod]
		public void FullPathTestAsString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Constants.Path);
			Assert.IsTrue(parserResults.Count() == 2);
		}


		[TestMethod]
		public void FullPathTestAsDirectoryInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = new DirectoryInfo(Constants.Path);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
			Assert.IsTrue(parserResults.Count() == 2);
		}


		[TestMethod]
		public void FullPathTestAsFileInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			FileInfo fileInfo = new FileInfo(Constants.MovieFilePath);
			ParserResult parserResult = seriesIDParser.Parse(fileInfo);
			Assert.IsTrue(parserResult != null);
		}

	}
}
