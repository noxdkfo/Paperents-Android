using UnityEngine;

public class ClickableWetStained : MonoBehaviour
{
	public ItemSelector itemSelector;

	private void OnMouseDown()
	{
		if (itemSelector != null)
		{
			itemSelector.OnWetStainedClicked();
		}
		else
		{
			Debug.LogWarning("ItemSelector 未绑定！");
		}
	}
}
