using UnityEngine;

public class BlockProgrammingWindow : MonoBehaviour
{
	public void CloseWindow()
	{
		WindowApi.CloseLastWindow();
	}
}