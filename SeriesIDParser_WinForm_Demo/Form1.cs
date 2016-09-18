
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesIDParser_WinForm_Demo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnParse_Click(object sender, EventArgs e)
		{
			// Use the following three lines as a example for editing the parser settings
			//ParserSettings ps = new ParserSettings();
			//ps.NewSpacingChar = '-';
			//SeriesID sid = new SeriesID(ps);

			// Creating the parser object with default settings(empty ctor)
			SeriesID sid = new SeriesID();

			sid.Parse(tbxInput.Text);

			dataGridViewResult.Rows.Clear();
			dataGridViewResult.Rows.Add("OriginalString", "string", sid.OriginalString);
			dataGridViewResult.Rows.Add("ParsedString", "string", sid.ParsedString);
			dataGridViewResult.Rows.Add("Title", "string", sid.Title);
			dataGridViewResult.Rows.Add("EpisodeTitle", "string", sid.EpisodeTitle);
			dataGridViewResult.Rows.Add("FullTitle", "string", sid.FullTitle);
			dataGridViewResult.Rows.Add("IsSeries", "bool", sid.IsSeries);
			dataGridViewResult.Rows.Add("Season", "int", sid.Season);
			dataGridViewResult.Rows.Add("Episode", "int", sid.Episode);
			dataGridViewResult.Rows.Add("IDString", "string", sid.IDString);
			dataGridViewResult.Rows.Add("Resolutions", "enum list Resolutions", string.Join(", ", sid.Resolutions));
			dataGridViewResult.Rows.Add("Year", "int", sid.Year);
			dataGridViewResult.Rows.Add("FileExtension", "string", sid.FileExtension);
			dataGridViewResult.Rows.Add("RemovedTokens", "string list", string.Join(", ", sid.RemovedTokens));
			dataGridViewResult.Rows.Add("State", "enum State", sid.State);
			dataGridViewResult.Rows.Add("DetectedOldSpacingChar", "char", sid.DetectedOldSpacingChar);
			dataGridViewResult.Rows.Add("ProcessingDuration", "TimeSpan", sid.ProcessingDuration.TotalMilliseconds + " ms");
			dataGridViewResult.Rows.Add("ReleaseGroup", "string", sid.ReleaseGroup);

			tbxException.Clear();
			if (sid.Exception != null)
			{
				tbxException.AppendText(sid.Exception.Message + Environment.NewLine);
				tbxException.AppendText(sid.Exception.Source + Environment.NewLine);
				tbxException.AppendText(sid.Exception.StackTrace + Environment.NewLine);
			}
		}
	}
}
