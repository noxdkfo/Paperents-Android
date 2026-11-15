using UnityEngine;

public class ClickableBedFire : MonoBehaviour
{
	public ItemSelector itemSelector;

	private void OnMouseDown()
	{
		itemSelector?.OnBedFireClicked();
	}
}
