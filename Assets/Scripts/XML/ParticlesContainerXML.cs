//
// Author: Jate Wittayabundit
//
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("ParticlesContainerXML")]
public class ParticlesContainerXML {

	[XmlArray("Particles"),XmlArrayItem("Particle")]
	public List<ParticleXML> Particles = new List<ParticleXML>();

	#region Save & Load using file stream
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(ParticlesContainerXML));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	// Load from resource folder
	public static ParticlesContainerXML Load(string path)
	{
		var serializer = new XmlSerializer(typeof(ParticlesContainerXML));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as ParticlesContainerXML;
		}
	}
	#endregion
	
	// Load directly from string
	public static ParticlesContainerXML LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(ParticlesContainerXML));
		return serializer.Deserialize(new StringReader(text)) as ParticlesContainerXML;
	}
}
