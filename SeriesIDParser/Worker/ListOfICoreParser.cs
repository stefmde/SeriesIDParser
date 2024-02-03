// MIT License
// 
// Copyright(c) 2016 - 2024
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using SeriesIDParser.Models;

namespace SeriesIDParser.Worker;

public class ListOfICoreParser : List<ICoreParser>, IXmlSerializable
{
	private Type _type = typeof(ICoreParser);

	public ListOfICoreParser() : base()
	{
	}

	/// <inheritdoc />
	public XmlSchema GetSchema()
	{
		return null;
	}

	/// <inheritdoc />
	public void ReadXml( XmlReader reader )
	{
		reader.ReadStartElement( "ListOfICoreParser" );
		while (reader.IsStartElement( _type.Name ))
		{
			var type = Type.GetType( reader.GetAttribute( "AssemblyQualifiedName" ) );
			XmlSerializer serial = new(_type);

			reader.ReadStartElement( _type.Name );
			Add( (ICoreParser)serial.Deserialize( reader ) );
			reader.ReadEndElement(); //ICoreParser
		}

		reader.ReadEndElement(); //ListOfICoreParser
	}

	/// <inheritdoc />
	public void WriteXml( XmlWriter writer )
	{
		foreach (var coreParser in this)
		{
			writer.WriteStartElement( _type.Name );
			writer.WriteAttributeString( "AssemblyQualifiedName", coreParser.GetType().AssemblyQualifiedName );
			XmlSerializer xmlSerializer = new(coreParser.GetType());
			xmlSerializer.Serialize( writer, coreParser );
			writer.WriteEndElement();
		}
	}
}