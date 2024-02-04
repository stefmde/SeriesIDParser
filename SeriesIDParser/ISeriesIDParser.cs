// MIT License
// 
// Copyright(c) 2016 - 2024
// Stefan (StefmDE) Müller, https://Stefm.de, https://github.com/stefmde/SeriesIDParser
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


using System.Collections.Generic;
using System.IO;
using SeriesIDParser.Models;

namespace SeriesIDParser;

public interface ISeriesIDParser
{
	/// <summary>
	///     The primary parsing function
	/// </summary>
	/// <param name="input">The series or movie string who get parsed. Must be at least five chars</param>
	/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
	IParserResult Parse( string input );

	/// <summary>
	///     The primary parsing function
	/// </summary>
	/// <param name="input">The series or movie FileInfo who get parsed.</param>
	/// <returns>The SeriesIDResult object that represents the series or movie string</returns>
	ParserResult Parse( FileInfo input );

	/// <summary>
	///     Get all files in a path parsed
	/// </summary>
	/// <param name="path"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	List<IParserResult> ParsePath( DirectoryInfo path, SearchOption searchOption = SearchOption.AllDirectories );

	/// <summary>
	///     Get all files in a path parsed
	/// </summary>
	/// <param name="path"></param>
	/// <param name="searchOption"></param>
	/// <returns></returns>
	List<IParserResult> ParsePath( string path, SearchOption searchOption = SearchOption.AllDirectories );
}