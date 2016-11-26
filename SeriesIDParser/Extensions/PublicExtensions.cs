using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser.Extensions
{
	public static class PublicExtensions
	{
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
	}
}
