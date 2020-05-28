using UnityEditor;
using UnityEngine;

/// <summary>
/// Adds an inspector button that will generate the building array in InitializeBuildings.
/// </summary>
[CustomEditor(typeof(InitializeBuildings))]
public class InitializeBuildingsEditor : Editor
{
	/// <summary>
	/// This method will add an inspector button to generate the building array in InitializeBuildings.
	/// </summary>
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		InitializeBuildings initializeBuildings = (InitializeBuildings)target;

		EditorGUILayout.Space();

		if (GUILayout.Button("Generate Building Array"))
			initializeBuildings.GenerateBuildingArray();
	}
}