//
// Author: Jate Wittayabundit
//
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.IO;

public class ParticleCreator : MonoBehaviour {
	public string XmlfileName = "ParticleController.xml";

	[SerializeField, HideInInspector]
	ParticlesContainerXML _particleXML;
	[SerializeField, HideInInspector]
	List<ParticleSystem> _particles;

	public void Initialize() {
		try {
			_particleXML = ParticlesContainerXML.Load(Path.Combine(Application.dataPath + "/Resources", XmlfileName));
			_particles = new List<ParticleSystem>();
		} catch (UnityException e) {
			Debug.LogError("Error = " + e.Message);
		}
	}

	public void CreateParticle() {
		if (_particles != null) {
			for (int i = 0; i < _particleXML.Particles.Count; ++i) {
				GameObject go = new GameObject(_particleXML.Particles[i].Name);
				ParticleSystem ps = go.AddComponent<ParticleSystem>();
				ps.startDelay = _particleXML.Particles[i].StartDelay;
				ps.startLifetime = _particleXML.Particles[i].StartLifeTime;
				ps.startSpeed = _particleXML.Particles[i].StartSpeed;
				ps.startRotation = _particleXML.Particles[i].StartRotation;
				ps.startColor = ParticleXML.HexToColor(_particleXML.Particles[i].StartColor);
				ps.maxParticles = _particleXML.Particles[i].MaxParticle;
				ParticleSystem.EmissionModule emission = ps.emission;
				emission.rate = new ParticleSystem.MinMaxCurve(_particleXML.Particles[i].EmissionRate);
				ps.loop = _particleXML.Particles[i].Loop;

				if (_particleXML.Particles[i].MaterialName != "default") {
					Material mat = Resources.Load ("Materials/Star") as Material;
					ParticleSystemRenderer pr = (ParticleSystemRenderer)ps.GetComponent<Renderer>();
					pr.sharedMaterial = mat;
				}
				
				#if UNITY_EDITOR
				SerializedObject so = new SerializedObject(ps);
				so.FindProperty("InitialModule.startSize.minMaxState").intValue = 2; //Random between two constants
				so.FindProperty("InitialModule.startSize.maxCurve.m_Curve.Array.data[0].value").floatValue = _particleXML.Particles[i].StartSizeMax;
				so.FindProperty("InitialModule.startSize.minCurve.m_Curve.Array.data[0].value").floatValue = _particleXML.Particles[i].StartSizeMin;
				so.FindProperty("InitialModule.startSize.maxCurve.m_Curve.Array.data[1].value").floatValue = _particleXML.Particles[i].StartSizeMax;
				so.FindProperty("InitialModule.startSize.minCurve.m_Curve.Array.data[1].value").floatValue = _particleXML.Particles[i].StartSizeMin;
				so.FindProperty("ShapeModule.enabled").boolValue = true;

				switch (_particleXML.Particles[i].ShapeType) {
				case 0: // Sphere
					so.FindProperty("ShapeModule.type").intValue = 0; // Sphere
					so.FindProperty("ShapeModule.radius").floatValue = 1;
					break;
				case 1: // Box
					so.FindProperty("ShapeModule.type").intValue = 5; // Box
					so.FindProperty("ShapeModule.boxX").floatValue = 28;
					so.FindProperty("ShapeModule.boxY").floatValue = 14;
					so.FindProperty("ShapeModule.boxZ").floatValue = 1;
					break;
				}
				so.FindProperty("ColorModule.enabled").boolValue = true;
				so.FindProperty("ColorModule.gradient.maxGradient.key0.rgba").longValue = ParticleXML.ColorToUInt(ParticleXML.HexToColor(_particleXML.Particles[i].StartColorMax));
				so.FindProperty("ColorModule.gradient.minGradient.key0.rgba").longValue = ParticleXML.ColorToUInt(ParticleXML.HexToColor(_particleXML.Particles[i].StartColorMax));
				so.FindProperty("ColorModule.gradient.maxGradient.key1.rgba").longValue = ParticleXML.ColorToUInt(ParticleXML.HexToColor(_particleXML.Particles[i].StartColorMin));
				so.FindProperty("ColorModule.gradient.minGradient.key1.rgba").longValue = ParticleXML.ColorToUInt(ParticleXML.HexToColor(_particleXML.Particles[i].StartColorMin));
				so.ApplyModifiedProperties();

				// Checking all properties
				/*SerializedProperty it = so.GetIterator();
				while (it.Next(true))
					Debug.Log (it.propertyPath);*/
				#endif
				
				_particles.Add(ps);
			}
		} else {
			Debug.LogError("Error: something wrong with intialize");
		}
	}

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		Initialize();
		CreateParticle();
#else
		Debug.LogWarning("Please click on the ParticleCreator Game object and go to the inspector and click "Create" to create the particle before building");
#endif
	}
}
