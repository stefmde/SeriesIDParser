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
			dataGridViewResult.Rows.Add("Resolution", "enum Resolutions", sid.Resolution);
			dataGridViewResult.Rows.Add("Year", "int", sid.Year);
			dataGridViewResult.Rows.Add("FileExtension", "string", sid.FileExtension);
			dataGridViewResult.Rows.Add("RemovedTokens", "string list", string.Join(", ", sid.RemovedTokens));
			dataGridViewResult.Rows.Add("State", "enum State", sid.State);

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
