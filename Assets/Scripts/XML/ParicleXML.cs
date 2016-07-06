//
// Author: Jate Wittayabundit
//
using UnityEngine;
using System.Xml.Serialization;

public class ParticleXML {

	[XmlAttribute("name")]
	public string Name;

	public float Duration;
	public float StartDelay;
	public float StartLifeTime;
	public float StartSpeed;
	public float StartSizeMax;
	public float StartSizeMin;
	public float StartRotation;
	public string StartColor;
	public string StartColorMin;
	public string StartColorMax;
	public float EmissionRate;
	public int MaxParticle;
	public int ShapeType;
	public string MaterialName;
	public bool Loop;
	
	public static Color HexToColor(string hex)
	{
		hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
		hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
		byte a = 255;//assume fully visible unless specified in hex
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		//Only use alpha if the string has enough characters
		if(hex.Length == 8){
			a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r,g,b,a);
	}

	public static uint ColorToUInt(Color32 color)
	{
		return (uint)((color.a << 24) | (color.r << 16) | (color.g << 8) | (color.b << 0));
	}
}