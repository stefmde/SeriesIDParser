
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


using SeriesIDParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser_Test
{
	class Program
	{
		static void Main( string[] args )
		{
			int errorCounter = 0;

			List<string> testList = new List<string>();
			testList.Add( "Better.Call.Saul.S02E10.GERMAN.DL.DUBBED.1080p.WebHD.h264 - iNFOTv" );
			testList.Add( "Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv" );
			testList.Add( "The.Big.Bang.Theory.S09E12.Der.romantische.Asteroid.GERMAN.DL.DUBBED.1080p.WebHD.x264" );
			testList.Add( "House.of.Cards.S04E04.Akt.der.Verzweiflung.German.DD51.Synced.DL.2160p.NetflixUHD.x264" );
			testList.Add( "Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264" );
			testList.Add( "Arrow.s04e03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264" );
			testList.Add( "Arrow.s4e3.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264" );
			testList.Add( "Arrow.s4e321.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264" );
			testList.Add( "Sie nannten ihn Knochenbrecher" );
			testList.Add( "Knight.Rider.S01E07.Die.grosse.Duerre.German.DVDRip.XviD-c0nFuSed" );
			testList.Add( "059.-.Peter.und.das.Sofamobil" );
			testList.Add( "NCIS.S01E01.Air.Force.One.German.DD20.Dubbed.DL.720p.iTunesHD.AVC-TVS" );
			testList.Add( "" );
			testList.Add( "." );
			testList.Add( "Der.Hobbit.Smaugs.Einoede.2013.EXTENDED.German.DL.1080p.BluRay.x264.mkv" );
			testList.Add( "A.Chinese.Ghost.Story.3.1991.German.DTS.1080p.BD9.x264.mkv" );
			testList.Add( "Black.Mass.2015.German.AC3D.DL.1080p.WEB-DL.h264.mkv" );


			Console.WriteLine();

			foreach (string item in testList)
			{
				try
				{
					SeriesID id = null;
					id = SeriesIDParser.SeriesIDParser.Parse( item );

					Console.WriteLine( "Test: " + item );
					if (id.State == State.OK_SUCCESS)
					{
						Console.WriteLine( "Result: " + id.FullTitle );
					}
					else
					{
						Console.WriteLine( "ERROR: " + id.State.ToString() );
					}
					//Console.WriteLine( "--------------------------------------------------" );
					Console.WriteLine();
				}
				catch (Exception ex)
				{
					errorCounter++;
				}
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine( "##################################################" );
			if (errorCounter == 0)
			{
				Console.WriteLine( " Test Completed With No Exceptions." );
			}
			else
			{
				Console.WriteLine( " Test FAILED. There were " + errorCounter + " Exceptions." );
			}
			Console.WriteLine( "##################################################" );



			Console.ReadLine();
		}
	}
}
