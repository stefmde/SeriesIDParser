
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
		static void Main(string[] args)
		{
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Dubai.Airport.S01E05.Teil5.GERMAN.DOKU.HDTV.720p.x264.mkv"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("The.Big.Bang.Theory.S09E12.Der.romantische.Asteroid.GERMAN.DL.DUBBED.1080p.WebHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("House.of.Cards.S04E04.Akt.der.Verzweiflung.German.DD51.Synced.DL.2160p.NetflixUHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Arrow.S04E03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Arrow.s04e03.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Arrow.s4e3.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Arrow.s4e321.Auferstehung.GERMAN.DUBBED.DL.1080p.WebHD.x264"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Sie nannten ihn Knochenbrecher"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("Knight.Rider.S01E07.Die.grosse.Duerre.German.DVDRip.XviD-c0nFuSed"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("059.-.Peter.und.das.Sofamobil"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse("NCIS.S01E01.Air.Force.One.German.DD20.Dubbed.DL.720p.iTunesHD.AVC-TVS"));
			Console.WriteLine(SeriesIDParser.SeriesIDParser.Parse(""));

			Console.ReadLine();
		}
	}
}
