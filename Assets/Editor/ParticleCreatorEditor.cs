//
// Author: Jate Wittayabundit
//
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ParticleCreator))]
public class ParticleCreatorEditor : Editor {
	// Properties Prefabs
	const string kXmlfileName = "XmlfileName";
	
	// Serialized Object
	SerializedProperty _xmlfileName;
	
	ParticleCreator _particleCreator;
	
	public void OnEnable ()
	{
		_xmlfileName = serializedObject.FindProperty (kXmlfileName);
		
		_particleCreator = serializedObject.targetObject as ParticleCreator;
		_particleCreator.Initialize();
	}
	
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		_xmlfileName.stringValue = EditorGUILayout.TextField("XML File Name", _xmlfileName.stringValue);
		
		if (GUILayout.Button("Create Particle")) {
			if (_particleCreator == null) {
				_particleCreator = serializedObject.targetObject as ParticleCreator;
			}
			_particleCreator.CreateParticle();
			Undo.RegisterCompleteObjectUndo(_particleCreator,"Particle Create");
		}

		serializedObject.ApplyModifiedProperties ();
	}
}
