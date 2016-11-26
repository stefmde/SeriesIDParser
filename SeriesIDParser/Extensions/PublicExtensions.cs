using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser.Extensions
{
	public static class PublicExtensions
	{
		#region String
		public static ParserResult ParseSeriesID(this string input)
		{
			SeriesID sid = new SeriesID();
			return sid.Parse(input);
		}

		public static ParserResult ParseSeriesID(this string input, ParserSettings settings)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.Parse(input);
		}
		#endregion String

		#region FileInfo
		public static ParserResult ParseSeriesID(this FileInfo input)
		{
			SeriesID sid = new SeriesID();
			return sid.Parse(input);
		}

		public static ParserResult ParseSeriesID(this FileInfo input, ParserSettings settings)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.Parse(input);
		}
		#endregion FileInfo

		#region DirectoryInfo
		public static IEnumerable<ParserResult> ParseSeriesID(this DirectoryInfo path, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID();
			return sid.ParsePath(path, searchOption);
		}

		public static IEnumerable<ParserResult> ParseSeriesID(this DirectoryInfo path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.ParsePath(path, searchOption);
		}
		#endregion DirectoryInfo

		#region Path
		public static IEnumerable<ParserResult> ParseSeriesIDPath(this string path, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID();
			return sid.ParsePath(path, searchOption);
		}

		public static IEnumerable<ParserResult> ParseSeriesIDPath(this string path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.ParsePath(path, searchOption);
		}
		#endregion
	}
}
