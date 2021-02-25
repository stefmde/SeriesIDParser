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


using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SeriesIDParser.Models;

[assembly: InternalsVisibleTo( "SeriesIDParser.Test" )]

namespace SeriesIDParser.Worker
{
	internal static class ParserSettingsWorker
	{
		// ### De/Serialization
		// ############################################################

		#region DeSerialisazion
		/// <summary>
		///     Serializes this object to a xml string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <returns>The xml string representing this object</returns>
		internal static string SerializeToXML( ParserSettings parserSettings )
		{
			string data = string.Empty;
			XmlSerializer x = new XmlSerializer( parserSettings.GetType() );
			using (MemoryStream ms = new MemoryStream())
			{
				x.Serialize( ms, parserSettings );
				ms.Position = 0;
				using (StreamReader sr = new StreamReader( ms, Encoding.UTF8 ))
				{
					data = sr.ReadToEnd();
				}
			}

			return data;
		}

		/// <summary>
		///     Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		internal static string SerializeToJson( ParserSettings parserSettings, JsonSerializerSettings jsonSerializerSettings = null )
		{
			if (jsonSerializerSettings == null)
			{
				jsonSerializerSettings = new JsonSerializerSettings();
			}

			return Newtonsoft.Json.JsonConvert.SerializeObject( parserSettings, jsonSerializerSettings );
		}

		/// <summary>
		///     Deserializes this object from a xml string
		/// </summary>
		/// <param name="xml">The xml string representing this object</param>
		/// <returns>The object generated out of the xml content</returns>
		internal static ParserSettings DeSerializeFromXML( string xml )
		{
			XmlSerializer x = new XmlSerializer( typeof(ParserSettings) );
			byte[] xmlBytes = Encoding.UTF8.GetBytes( xml );
			using (MemoryStream ms = new MemoryStream( xmlBytes ))
			{
				return (ParserSettings) x.Deserialize( ms );
			}
		}

		/// <summary>
		///     Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="json">The json string representing this object</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		internal static ParserSettings DeSerializeFromJson( string json, JsonSerializerSettings jsonSerializerSettings = null )
		{
			if (jsonSerializerSettings == null)
			{
				jsonSerializerSettings = new JsonSerializerSettings();
			}

			return Newtonsoft.Json.JsonConvert.DeserializeObject<ParserSettings>( json, jsonSerializerSettings );
		}
		#endregion DeSerialisazion
	}
}