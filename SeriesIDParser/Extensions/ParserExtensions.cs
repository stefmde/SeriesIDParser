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


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SeriesIDParser.Models;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Extensions;

/// <summary>
///     Provides some extensions to use the SeriesIDParser
/// </summary>
public static class ParserExtensions
{
	#region String
	/// <summary>
	///     SeriesIDParser extension to parse a string
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static ParserResult ParseSeriesID( this string input )
	{
		SeriesID sid = new();
		return sid.Parse( input );
	}

	/// <summary>
	///     SeriesIDParser extension to parse a string with custom ParserSettings
	/// </summary>
	/// <param name="input"></param>
	/// <param name="settings"></param>
	/// <returns></returns>
	public static ParserResult ParseSeriesID( this string input, ParserSettings settings )
	{
		SeriesID sid = new( settings );
		return sid.Parse( input );
	}
	#endregion String

	// ####################

	#region FileInfo
	/// <summary>
	///     SeriesIDParser extension to parse a FileInfo
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static ParserResult ParseSeriesID( this FileInfo input )
	{
		SeriesID sid = new();
		return sid.Parse( input );
	}

	/// <summary>
	///     SeriesIDParser extension to parse a FileInfo with custom ParserSettings
	/// </summary>
	/// <param name="input"></param>
	/// <param name="settings"></param>
	/// <returns></returns>
	public static ParserResult ParseSeriesID( this FileInfo input, ParserSettings settings )
	{
		SeriesID sid = new( settings );
		return sid.Parse( input );
	}
	#endregion FileInfo

	// ####################

	#region DirectoryInfo
	/// <summary>
	///     SeriesIDParser extension to parse an entire directory by DirectoryInfo
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static IEnumerable<ParserResult> ParseSeriesIDPath( this DirectoryInfo path )
	{
		SeriesID sid = new();
		return sid.ParsePath( path );
	}

	/// <summary>
	///     SeriesIDParser extension to parse an entire directory by DirectoryInfo with custom ParserSettings and SearchOption
	/// </summary>
	/// <param name="path"></param>
	/// <param name="settings"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	public static IEnumerable<ParserResult> ParseSeriesIDPath( this DirectoryInfo path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories )
	{
		SeriesID sid = new( settings );
		return sid.ParsePath( path, searchOption );
	}
	#endregion DirectoryInfo

	// ####################

	#region Path
	/// <summary>
	///     SeriesIDParser extension to parse an entire directory by string path
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static IEnumerable<ParserResult> ParseSeriesIDPath( this string path )
	{
		SeriesID sid = new();
		return sid.ParsePath( path );
	}

	/// <summary>
	///     SeriesIDParser extension to parse an entire directory by string path with custom ParserSettings and SearchOption
	/// </summary>
	/// <param name="path"></param>
	/// <param name="settings"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	public static IEnumerable<ParserResult> ParseSeriesIDPath( this string path, ParserSettings settings = null, SearchOption searchOption = SearchOption.AllDirectories )
	{
		SeriesID sid = new( settings );
		return sid.ParsePath( path, searchOption );
	}
	#endregion
}