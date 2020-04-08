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
		GameObject windowObj = Object.Instantiate(window, window.transform.position, Quaternion.identity);

		RectTransform rect = windowObj.GetComponent<RectTransform>();

		rect.SetParent(Object.FindObjectOfType<Canvas>().transform);
		rect.localPosition = new Vector3(0, 0, 0);
		rect.localScale = new Vector3(1, 1, 1);

		WindowApi.OpenedWindows.Add(windowObj);
	}

	private static void CloseLastWindow(GameObject window)
	{
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