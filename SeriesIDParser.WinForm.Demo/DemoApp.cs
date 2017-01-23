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


using SeriesIDParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesIDParser.Extensions;
using SeriesIDParser.Models;

// ReSharper disable MissingXmlDoc

namespace SeriesIDParser.WinForm.Demo
{
	public partial class DemoApp : Form
	{
		public DemoApp()
		{
			InitializeComponent();
			Text += " - SeriesIDParser Assembly v" + typeof(SeriesID).Assembly.GetName().Version;
		}

		private void btnParse_Click(object sender, EventArgs e)
		{
			// Use the following three lines as a example for editing the parser settings
			// ParserSettings ps = new ParserSettings();
			// ps.NewSpacingChar = '-';
			// SeriesID sid = new SeriesID(ps);


			// Creating the parser object with default settings(empty ctor)
			SeriesID sid = new SeriesID();

			// Getting the result methode 1 - function call
			ParserResult parserResult = sid.Parse(tbxInput.Text);

			// Getting the result methode 2 - extension method call
			parserResult = tbxInput.Text.ParseSeriesID();


			dataGridViewResult.Rows.Clear();
			dataGridViewResult.Rows.Add("OriginalString", "string", parserResult.OriginalString);
			dataGridViewResult.Rows.Add("ParsedString", "string", parserResult.ParsedString);
			dataGridViewResult.Rows.Add("Title", "string", parserResult.Title);
			dataGridViewResult.Rows.Add("EpisodeTitle", "string", parserResult.EpisodeTitle);
			dataGridViewResult.Rows.Add("FullTitle", "string", parserResult.FullTitle);
			dataGridViewResult.Rows.Add("IsSeries", "bool", parserResult.IsSeries);
			dataGridViewResult.Rows.Add("IsMultiEpisode", "bool", parserResult.IsMultiEpisode);
			dataGridViewResult.Rows.Add("Season", "int", parserResult.Season);
			dataGridViewResult.Rows.Add("Episodes", "int list", string.Join(", ", parserResult.Episodes));
			dataGridViewResult.Rows.Add("IDString", "string", parserResult.IDString);
			dataGridViewResult.Rows.Add("Resolutions", "enum list Resolutions", string.Join(", ", parserResult.Resolutions));
			dataGridViewResult.Rows.Add("Year", "int", parserResult.Year);
			dataGridViewResult.Rows.Add("FileExtension", "string", parserResult.FileExtension);
			dataGridViewResult.Rows.Add("RemovedTokens", "string list", string.Join(", ", parserResult.RemovedTokens));
			dataGridViewResult.Rows.Add("State", "enum State", parserResult.State);
			dataGridViewResult.Rows.Add("DetectedOldSpacingChar", "char", parserResult.DetectedOldSpacingChar);
			dataGridViewResult.Rows.Add("ProcessingDuration", "TimeSpan", parserResult.ProcessingDuration.TotalMilliseconds + " ms");
			dataGridViewResult.Rows.Add("ReleaseGroup", "string", parserResult.ReleaseGroup);
			dataGridViewResult.Rows.Add("AudioCodec", "string", parserResult.AudioCodec);
			dataGridViewResult.Rows.Add("VideoCodec", "string", parserResult.VideoCodec);
			dataGridViewResult.Rows.Add("Is3D", "bool", parserResult.Is3D);
			dataGridViewResult.Rows.Add("DimensionalType", "enum DimensionalType", parserResult.DimensionalType);

			tbxException.Clear();
			if (parserResult.Exception != null)
			{
				tbxException.AppendText(parserResult.Exception.Message + Environment.NewLine);
				tbxException.AppendText(parserResult.Exception.Source + Environment.NewLine);
				tbxException.AppendText(parserResult.Exception.StackTrace + Environment.NewLine);
			}
		}
	}
}