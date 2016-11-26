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
			ParserResult pr = sid.Parse(input.Name);
			pr.FileInfo = input;
			return pr;
		}

		public static ParserResult ParseSeriesID(this FileInfo input, ParserSettings settings)
		{
			SeriesID sid = new SeriesID(settings);
			ParserResult pr = sid.Parse(input.Name);
			pr.FileInfo = input;
			return pr;
		}
		#endregion
	}
}
