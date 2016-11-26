using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FullPathTests
	{
		private const string Path = @"D:\Test";
		private const string MovieFile = "D:\\Test\\Der.Regenmacher.1997.German.1080p.BluRay.x264.mkv";
		private const string SeriesFile = "D:\\Test\\Gotham.S02E01.Glueck.oder.Wahrheit.1080p.BluRay.DUBBED.German.x264.mkv";

		[TestMethod]
		public void FullPathTestAsString()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(Path);
		}


		[TestMethod]
		public void FullPathTestAsDirectoryInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			DirectoryInfo directoryInfo = new DirectoryInfo(Path);
			IEnumerable<ParserResult> parserResults = seriesIDParser.ParsePath(directoryInfo);
		}


		[TestMethod]
		public void FullPathTestAsFileInfo()
		{
			ParserSettings parserSettings = new ParserSettings(true);
			SeriesID seriesIDParser = new SeriesID(parserSettings);
			FileInfo fileInfo = new FileInfo(MovieFile);
			ParserResult parserResult = seriesIDParser.Parse(fileInfo);
		}

	}
}
