using System.Collections.Generic;
using UnityEngine;

internal static class OpenCloseWindow
{
	static OpenCloseWindow()
	{
		WindowApi.OnWindowOpen += OpenWindow;
		WindowApi.OnCloseLastWindow += CloseLastWindow;
		WindowApi.OnCloseAllWindows += CloseAllWindows;
	}

	private static void OpenWindow(GameObject window)
	{
		Transform canvas = Object.FindObjectOfType<Canvas>().transform;
		Transform[] transformsInCanvas = canvas.GetComponentsInChildren<Transform>(true);

		foreach (Transform transform in transformsInCanvas)
		{
			if (transform.name == window.name)
			{
				transform.gameObject.SetActive(true);
				WindowApi.OpenedWindows.Add(transform.gameObject);
				return;
			}
		}

		GameObject windowObj = Object.Instantiate(window, window.transform.position, Quaternion.identity, canvas);

		RectTransform rect = windowObj.GetComponent<RectTransform>();

		rect.localPosition = new Vector3(0, 0, 0);
		rect.localScale = new Vector3(1, 1, 1);

		WindowApi.OpenedWindows.Add(windowObj);
	}

	private static void CloseLastWindow(GameObject window)
	{
		Transform canvas = Object.FindObjectOfType<Canvas>().transform;
		Transform[] transformsInCanvas = canvas.GetComponentsInChildren<Transform>(true);

		foreach (Transform transform in transformsInCanvas)
		{
			if (transform.name == window.name)
			{
				transform.gameObject.SetActive(false);
				WindowApi.OpenedWindows.RemoveAt(WindowApi.OpenedWindows.Count - 1);
				return;
			}
		}

		Object.Destroy(window);
		WindowApi.OpenedWindows.RemoveAt(WindowApi.OpenedWindows.Count - 1);
	}

	private static void CloseAllWindows(List<GameObject> windows)
	{
		for (int i = 0; i < windows.Count; i++)
		{
			Object.Destroy(windows[i]);
		}

		WindowApi.OpenedWindows.Clear();
	}
}