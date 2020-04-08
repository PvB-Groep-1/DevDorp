using UnityEditor;
using UnityEngine;

/// <summary>
/// Adds an inspector button that will generate the window array in InitializeWindows.
/// </summary>
[CustomEditor(typeof(InitializeWindows))]
public class InitializeWindowsEditor : Editor
{
	/// <summary>
	/// This method will add an inspector button to generate the window array in InitializeWindows.
	/// </summary>
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		InitializeWindows initializeWindows = (InitializeWindows)target;

		EditorGUILayout.Space();

		if (GUILayout.Button("Generate Window Array"))
			initializeWindows.GenerateWindowArray();
	}
}