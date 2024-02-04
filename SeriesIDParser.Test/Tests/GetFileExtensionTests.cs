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
using NUnit.Framework;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SeriesIDParser.Test.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
[SuppressMessage( "ReSharper", "StringLiteralTypo" )]
public class GetFileExtensionTests
{
	[Test]
	public void GetFileExtensionTestDefault()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".mkv", HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.FileExtensions ), "(1) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestExtensionAvi()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".avi", HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.avi", ps.FileExtensions ), "(2) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestExtensionM4V()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".m4v", HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.m4v", ps.FileExtensions ), "(3) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestExtensionWmv()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".wmv", HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.wmv", ps.FileExtensions ), "(4) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestNoValidExtension()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( string.Empty, HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.xyz", ps.FileExtensions ), "(5) Should give no extension" );
	}

	[Test]
	public void GetFileExtensionTestCase()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".wmv", HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.WmV", ps.FileExtensions ), "(6) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestSpecialSpacers()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( ".mp4", HelperWorker.GetFileExtension( "Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mp4", ps.FileExtensions ), "(7) Should give a extension" );
	}

	[Test]
	public void GetFileExtensionTestNoExtension()
	{
		var ps = new ParserSettings();
		Assert.AreEqual( string.Empty, HelperWorker.GetFileExtension( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264", ps.FileExtensions ), "(8) Should give no extension" );
	}
}