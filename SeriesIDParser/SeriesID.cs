using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesIDParser
{
	public enum Resolutions
	{
		UNKNOWN,
		UltraHD8K_4320p,
		UltraHD_2160p,
		FullHD_1080p,
		HD_720p,
		SD_Any
	}

	public enum State
	{
		UNKNOWN,
		OK_SUCCESS,
		ERR_EMPTY_ARGUMENT,
		ERR_ID_NOT_FOUND
	}

	public class SeriesID
	{
		public SeriesID(State state, string seriesTitle = null, string episodeTitle = null, int season = -1, int episode = -1, Resolutions resolution = Resolutions.UNKNOWN )
		{
			this._seriesTitle = seriesTitle;
			this._episodeTitle = episodeTitle;
			this._season = season;
			this._episode = episode;
			this._resolution = resolution;
			this._state = state;
		}

		public string FullTitle
		{
			get { return _seriesTitle + "." + IDString + "." + _episodeTitle; }
		}

		private string _seriesTitle;
		public string SeriesTitle
		{
			get { return _seriesTitle; }
		}

		private State _state;
		public State State
		{
			get { return _state; }
		}

		private string _episodeTitle;
		public string EpisodeTitle
		{
			get { return _episodeTitle; }
		}

		private int _season;
		public int Season
		{
			get { return _season; }
		}

		private int _episode;
		public int Episode
		{
			get { return _episode; }
		}

		public string IDString
		{
			get { return "S" + _season.ToString("D2") + "E" + _episode.ToString("D2"); }
		}

		private Resolutions _resolution;
		public Resolutions Resolution
		{
			get { return _resolution; }
		}

		public override string ToString()
		{
			if (_state == State.OK_SUCCESS)
			{
				return FullTitle + " -- " + _resolution.ToString();
			}
			else
			{
				return _state.ToString();
			}
		}
	}
}
