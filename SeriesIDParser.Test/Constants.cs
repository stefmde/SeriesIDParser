using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser.Test
{
	public static class Constants
	{
		//public const string Path = @"D:\Test";
		//public static string RootPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
		public static readonly string TestDataRoot = @"..\..\..\Stuff\TestData";
		public static readonly string TestDataDirectoryRoot = @"..\..\..\Stuff\TestData\Directory";
		public static readonly string TestDataDirectoryCleanRoot = @"..\..\..\Stuff\TestData\Directory\Clean";
		public static readonly string TestDataDirectoryDirtyRoot = @"..\..\..\Stuff\TestData\Directory\Dirty";
		public static readonly string TestDataDirectoryEmptyRoot = @"..\..\..\Stuff\TestData\Directory\Empty";
		public static readonly string TestDataDirectoryRemovedRoot = @"..\..\..\Stuff\TestData\Directory\Removed";

		public static readonly string MovieFilePath = TestDataDirectoryCleanRoot + "\\Der.Regenmacher.1997.German.1080p.BluRay.x264.mkv";
		public static readonly string SeriesFilePath = TestDataDirectoryCleanRoot + "\\Gotham.S02E01.Glueck.oder.Wahrheit.1080p.BluRay.DUBBED.German.x264.mkv";
		public static readonly string MovieFile = "Der.Regenmacher.1997.German.1080p.BluRay.x264.mkv";
		public static readonly string SeriesFile = "Gotham.S02E01.Glueck.oder.Wahrheit.1080p.BluRay.DUBBED.German.x264.mkv";
	}
}
