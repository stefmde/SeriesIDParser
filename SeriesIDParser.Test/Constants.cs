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


using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace SeriesIDParser.Test;

[ExcludeFromCodeCoverage]
internal static class Constants
{
	public static readonly string TestDataRoot = @"TestData";
	
	public static readonly string TestDataDirectoryCleanRoot = $"{TestDataRoot}{Path.DirectorySeparatorChar}Clean";
	public static readonly string TestDataDirectoryDirtyRoot =$"{TestDataRoot}{Path.DirectorySeparatorChar}Dirty";
	public static readonly string TestDataDirectoryEmptyRoot = $"{TestDataRoot}{Path.DirectorySeparatorChar}Empty";
	public static readonly string TestDataDirectoryRemovedRoot = $"{TestDataRoot}{Path.DirectorySeparatorChar}Removed";

	public static readonly string MovieFilePath = $"{TestDataDirectoryCleanRoot}{Path.DirectorySeparatorChar}Der.Regenmacher.1997.German.1080p.BluRay.x264.mkv";
	public static readonly string SeriesFilePath = $"{TestDataDirectoryCleanRoot}{Path.DirectorySeparatorChar}Gotham.S02E01.Glueck.oder.Wahrheit.1080p.BluRay.DUBBED.German.x264.mkv";

	public static readonly string MovieFile = "Der.Regenmacher.1997.German.1080p.BluRay.x264.mkv";
	public static readonly string SeriesFile = "Gotham.S02E01.Glueck.oder.Wahrheit.1080p.BluRay.DUBBED.German.x264.mkv";
}