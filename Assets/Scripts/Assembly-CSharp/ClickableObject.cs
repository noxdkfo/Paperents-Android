using UnityEngine;

public class ClickableObject : MonoBehaviour
{
	public InteractionManager manager;

	private void OnMouseDown()
	{
		if (manager != null)
		{
			manager.OnObject1Clicked();
		}
	}
}
