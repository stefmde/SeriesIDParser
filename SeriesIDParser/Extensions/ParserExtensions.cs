
// MIT License

// Copyright(c) 2016
// Stefan Müller, Stefm, https://gitlab.com/u/Stefm

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SeriesIDParser.Models;

[assembly: InternalsVisibleTo("SeriesIDParser.Test")]
namespace SeriesIDParser.Extensions
{
	public static class ParserExtensions
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

		// ####################

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

		// ####################

		#region DirectoryInfo
		public static IEnumerable<ParserResult> ParseSeriesIDPath(this DirectoryInfo path)
		{
			SeriesID sid = new SeriesID();
			return sid.ParsePath(path);
		}

		public static IEnumerable<ParserResult> ParseSeriesIDPath(this DirectoryInfo path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.ParsePath(path, searchOption);
		}
		#endregion DirectoryInfo

		// ####################

		#region Path
		public static IEnumerable<ParserResult> ParseSeriesIDPath(this string path)
		{
			SeriesID sid = new SeriesID();
			return sid.ParsePath(path);
		}

		public static IEnumerable<ParserResult> ParseSeriesIDPath(this string path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories)
		{
			SeriesID sid = new SeriesID(settings);
			return sid.ParsePath(path, searchOption);
		}
		#endregion
	}
}
