using UnityEngine;

public class ClickableItem : MonoBehaviour
{
	public ItemSelector itemSelector;

	private void OnMouseDown()
	{
		if (itemSelector != null)
		{
			itemSelector.OnItemsClicked();
		}
		else
		{
			Debug.LogWarning("ItemSelector 未绑定！");
		}
	}
}
