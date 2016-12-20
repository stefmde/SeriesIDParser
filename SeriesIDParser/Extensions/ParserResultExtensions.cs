
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SeriesIDParser.Models;
using SeriesIDParser.Worker;

[assembly: InternalsVisibleTo("SeriesIDParser.Test")]
namespace SeriesIDParser.Extensions
{
	public static class ParserResultExtensions
	{
		#region DeSerialisazion
		/// <summary>
		/// Serializes this object to a xml string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <returns>The xml string representing this object</returns>
		public static string SerializeToXML(this ParserSettings parserSettings)
		{
			return ParserSettingsWorker.SerializeToXML(parserSettings);
		}


		/// <summary>
		/// Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="parserSettings">The object that should be converted to an xml string</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		public static string SerializeToJson(this ParserSettings parserSettings, JsonSerializerSettings jsonSerializerSettings = null)
		{
			return ParserSettingsWorker.SerializeToJson(parserSettings, jsonSerializerSettings);
		}


		/// <summary>
		/// Deserializes this object from a xml string
		/// </summary>
		/// <param name="xml">The xml string representing this object</param>
		/// <returns>The object generated out of the xml content</returns>
		public static ParserSettings DeSerializeParserSettingsFromXML(this string xml)
		{
			return ParserSettingsWorker.DeSerializeFromXML(xml);
		}


		/// <summary>
		/// Serializes this object to a json string that could be stored in a file or somewhere else
		/// </summary>
		/// <param name="json">The json string representing this object</param>
		/// <param name="jsonSerializerSettings">JsonSerializerSettings for the Newtonsoft JsonConvert</param>
		/// <returns>The json string representing this object</returns>
		public static ParserSettings DeSerializeParserSettingsFromJson(this string json, JsonSerializerSettings jsonSerializerSettings = null)
		{
			return ParserSettingsWorker.DeSerializeFromJson(json, jsonSerializerSettings);
		}
		#endregion DeSerialisazion
	}
}
