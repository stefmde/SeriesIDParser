
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
		ERR_EMPTY_OR_TO_SHORT_ARGUMENT,
		ERR_ID_NOT_FOUND
	}

	public class SeriesID
	{
		public SeriesID( State state, bool isSeries = false, string title = null, string episodeTitle = null, int year = -1, int season = -1, int episode = -1, Resolutions resolution = Resolutions.UNKNOWN )
		{
			this._state = state;
			this._isSeries = isSeries;
			this._title = title;
			this._episodeTitle = episodeTitle;
			this._year = year;
			this._season = season;
			this._episode = episode;
			this._resolution = resolution;
		}

		/// <summary>
		/// Returns the full series string if object state IsSeries otherwise it returns Title
		/// </summary>
		/// <exception cref="InvalidOperationException">While object state is not OK_SUCCESS</exception>
		public string FullTitle
		{
			get
			{
				if (_state == State.OK_SUCCESS)
				{
					if (_isSeries)
					{
						return _title + "." + IDString + "." + _episodeTitle;
					}
					else
					{
						return _title;
					}
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while object state is not OK_SUCCESS" );
				}
			}
		}


		private string _title;
		/// <summary>
		/// Contains the series title or the movie name, depends on IsSeries
		/// </summary>
		public string Title
		{
			get
			{
				return _title;
			}
		}


		private State _state;
		/// <summary>
		/// Contains the state of the object e.g. OK_SUCCESS
		/// </summary>
		public State State
		{
			get
			{
				return _state;
			}
		}


		private string _episodeTitle;
		/// <summary>
		/// Contains the eposide title if object state is series
		/// </summary>
		/// <exception cref="InvalidOperationException">While IsSeries is false and state is not OK_SUCCESS</exception>
		public string EpisodeTitle
		{
			get
			{
				if (_state == State.OK_SUCCESS && _isSeries)
				{
					return _episodeTitle;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while IsSeries is false and state is not OK_SUCCESS" );
				}
			}
		}


		private int _season;
		/// <summary>
		/// Contains the season id if object state is series
		/// </summary>
		/// <exception cref="InvalidOperationException">While IsSeries is false and state is not OK_SUCCESS</exception>
		public int Season
		{
			get
			{
				if (_state == State.OK_SUCCESS && _isSeries)
				{
					return _season;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while IsSeries is false and state is not OK_SUCCESS" );
				}
			}
		}


		private int _episode;
		/// <summary>
		/// Contains the eposide id if object state is series
		/// </summary>
		/// <exception cref="InvalidOperationException">While IsSeries is false and state is not OK_SUCCESS</exception>
		public int Episode
		{
			get
			{
				if (_state == State.OK_SUCCESS && _isSeries)
				{
					return _episode;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while IsSeries is false and state is not OK_SUCCESS" );
				}
			}
		}


		private bool _isSeries;
		/// <summary>
		/// Specifies if the object contains a series or a movie
		/// </summary>
		/// <exception cref="InvalidOperationException">While object state is not OK_SUCCESS</exception>
		public bool IsSeries
		{
			get
			{
				if (_state == State.OK_SUCCESS)
				{

					return _isSeries;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while object state is not OK_SUCCESS" );
				}
			}
		}


		private int _year;
		/// <summary>
		/// Returns the year of the episode or movie if contained, otherwise -1
		/// </summary>
		/// <exception cref="InvalidOperationException">While object state is not OK_SUCCESS</exception>
		public int Year
		{
			get
			{
				if (_state == State.OK_SUCCESS)
				{

					return _year;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while object state is not OK_SUCCESS" );
				}
			}
		}


		/// <summary>
		/// Contains the ID-String of a series e.g. S01E05.
		/// </summary>
		/// <exception cref="InvalidOperationException">While IsSeries is false and state is not OK_SUCCESS</exception>
		public string IDString
		{
			get
			{
				if (_state == State.OK_SUCCESS && _isSeries)
				{
					return "S" + _season.ToString( "D2" ) + "E" + _episode.ToString( "D2" );
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while IsSeries is false and state is not OK_SUCCESS" );
				}
			}
		}


		private Resolutions _resolution;
		/// <summary>
		/// Returns the resolution as enum
		/// </summary>
		/// <exception cref="InvalidOperationException">While object state is not OK_SUCCESS</exception>
		public Resolutions Resolution
		{
			get
			{
				if (_state == State.OK_SUCCESS)
				{
					return _resolution;
				}
				else
				{
					throw new InvalidOperationException( "Invalid operation while object state is not OK_SUCCESS" );
				}
			}
		}

		/// <summary>
		/// </summary>
		/// <returns>FullTitle and resolution</returns>
		/// /// <exception cref="InvalidOperationException">While object state is not OK_SUCCESS</exception>
		public override string ToString()
		{
			if (_state == State.OK_SUCCESS)
			{
				return FullTitle + " -- " + _resolution.ToString();
			}
			else
			{
				throw new InvalidOperationException( "Invalid operation while object state is not OK_SUCCESS" );
			}
		}
	}
}
