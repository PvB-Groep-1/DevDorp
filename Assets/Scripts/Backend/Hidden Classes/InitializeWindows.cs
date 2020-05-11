using UnityEngine;
using System;

/// <summary>
/// Sends all windows as type GameObject to the WindowApi class.
/// </summary>
public class InitializeWindows : MonoBehaviour
{
	[SerializeField] private Window[] _windows;

	private void Start()
	{
		bool undefinedFields = false;
		bool containsDuplicateWindowTypes = false;

		WindowApi.windows = new GameObject[Enum.GetNames(typeof(WindowTypes)).Length];

		// Windows array check
		for (int i = 0; i < _windows.Length; i++)
		{
			// Undefined Fields Check
			if (!undefinedFields && !_windows[i].model)
				undefinedFields = true;

			// Contains Duplicate WindowTypes Check
			if (!containsDuplicateWindowTypes && WindowApi.windows[(int)_windows[i].type])
				containsDuplicateWindowTypes = true;

			WindowApi.windows[(int)_windows[i].type] = _windows[i].model;
		}

		// Undefined Fields Warning
		if (undefinedFields)
			Debug.LogWarning("You have undefined fields in " + this);

		// Contains Duplicate WindowTypes Warning
		if (containsDuplicateWindowTypes)
			Debug.LogWarning("You have duplicate WindowTypes in " + this);
	}

	/// <summary>
	/// Generates a window array with the size of the WindowTypes enum.
	/// Automatically sets a different type for each window.
	/// </summary>
	public void GenerateWindowArray()
	{
		int windowTypesLength = Enum.GetNames(typeof(WindowTypes)).Length;

		_windows = new Window[windowTypesLength];

		for (int i = 0; i < windowTypesLength; i++)
			_windows[i].type = (WindowTypes)i;
	}

	[Serializable]
	private struct Window
	{
		/// <summary>
		/// The specific type of this window.
		/// </summary>
		public WindowTypes type;

		/// <summary>
		/// The model as type GameObject of this window.
		/// </summary>
		public GameObject model;
	}
}