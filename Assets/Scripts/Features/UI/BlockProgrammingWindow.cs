﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents all functionality for the block programming UI.
/// </summary>
public class BlockProgrammingWindow : MonoBehaviour
{
	/// <summary>
	/// A delegate with a BuildingTypes parameter.
	/// </summary>
	/// <param name="buildingType">The type of a building.</param>
	public delegate void BuildingEvent(BuildingTypes buildingType);

	/// <summary>
	/// An event for when a building gets destroyed.
	/// </summary>
	public static event BuildingEvent OnDestroyBuilding;

	/// <summary>
	/// An event for when a building gets build.
	/// </summary>
	public static event BuildingEvent OnBuildBuilding;

	[SerializeField]
	private Button _backButton;

	/// <summary>
	/// Calls the OnDestroyBuilding event.
	/// </summary>
	/// <param name="buildingType">The type of the building.</param>
	public static void DestroyBuilding(BuildingTypes buildingType)
	{
		OnDestroyBuilding?.Invoke(buildingType);
	}

	/// <summary>
	/// Calls the OnBuildBuilding event.
	/// </summary>
	/// <param name="buildingType">The type of the building.</param>
	public static void BuildBuilding(BuildingTypes buildingType)
	{
		OnBuildBuilding?.Invoke(buildingType);
	}

	/// <summary>
	/// Closes the block programming window.
	/// </summary>
	public void CloseWindow()
	{
		Game.MainCamera.Dragging.EnableDragging();
		Game.MainCamera.Zooming.EnableZooming();
		WindowApi.CloseLastWindow();
	}

	/// <summary>
	/// Enables the back button.
	/// </summary>
	public void EnableBackButton()
	{
		_backButton.interactable = true;
	}

	/// <summary>
	/// Disables the back button.
	/// </summary>
	public void DisableBackButton()
	{
		_backButton.interactable = false;
	}
}