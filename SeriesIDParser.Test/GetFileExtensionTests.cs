﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeriesIDParser.Test
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class GetFileExtensionTests
	{
		[TestMethod]
		public void GetFileExtensionTestDefault()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".mkv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv", ps.FileExtensions), "(1) Should give a extension");
		}

		[TestMethod]
		public void GetFileExtensionTestExtensionAVI()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".avi", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.avi", ps.FileExtensions), "(2) Should give a extension");
		}


		[TestMethod]
		public void GetFileExtensionTestExtensionM4V()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".m4v", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.m4v", ps.FileExtensions), "(3) Should give a extension");
		}


		[TestMethod]
		public void GetFileExtensionTestExtensionWMV()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".wmv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.wmv", ps.FileExtensions), "(4) Should give a extension");
		}


		[TestMethod]
		public void GetFileExtensionTestNoValidExtension()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(String.Empty, Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.xyz", ps.FileExtensions), "(5) Should give no extension");
		}


		[TestMethod]
		public void GetFileExtensionTestCase()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".wmv", Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.WmV", ps.FileExtensions), "(6) Should give a extension");
		}


		[TestMethod]
		public void GetFileExtensionTestSpecialSpacers()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(".mp4", Helper.GetFileExtension("Der,Hobbit,Smaugs,Einoede,2013,EXTENDED,German,DL,1080p,BluRay,x264.mp4", ps.FileExtensions), "(7) Should give a extension");
		}


		[TestMethod]
		public void GetFileExtensionTestNoExtension()
		{
			ParserSettings ps = new ParserSettings(true);
			Assert.AreEqual(String.Empty, Helper.GetFileExtension("Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264", ps.FileExtensions), "(8) Should give no extension");
		}
	}
}