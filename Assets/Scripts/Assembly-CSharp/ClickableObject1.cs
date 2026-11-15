using UnityEngine;

public class ClickableObject1 : MonoBehaviour
{
	private InteractionController1 controller;

	private void Start()
	{
		controller = Object.FindObjectOfType<InteractionController1>();
	}

	private void OnMouseDown()
	{
		if (CompareTag("Phone"))
		{
			controller.OnPhoneClicked();
		}
		else if (CompareTag("Window"))
		{
			controller.OnWindowClicked();
		}
	}
}
