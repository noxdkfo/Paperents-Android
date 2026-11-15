using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewItems : MonoBehaviour
{
	[Serializable]
	public class ViewItem
	{
		public GameObject targetObject;

		public string displayText;

		public Image uiImage;
	}

	public List<ViewItem> viewItems = new List<ViewItem>();

	public Text uiText;

	public Image blackBackground;

	private Camera mainCamera;

	private bool isShowing;

	private void Start()
	{
		mainCamera = Camera.main;
		foreach (ViewItem viewItem in viewItems)
		{
			if (viewItem.uiImage != null)
			{
				viewItem.uiImage.gameObject.SetActive(value: false);
			}
		}
		if (uiText != null)
		{
			uiText.text = "";
		}
		if (blackBackground != null)
		{
			blackBackground.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			if (gameObject.CompareTag("ItemsCheck"))
			{
				foreach (ViewItem viewItem in viewItems)
				{
					if (gameObject == viewItem.targetObject)
					{
						ToggleUI(viewItem);
						return;
					}
				}
			}
			if (isShowing)
			{
				HideAll();
			}
		}
		else if (isShowing)
		{
			HideAll();
		}
	}

	private void ToggleUI(ViewItem item)
	{
		if (isShowing)
		{
			HideAll();
			return;
		}
		if (uiText != null)
		{
			uiText.text = item.displayText;
		}
		if (item.uiImage != null)
		{
			item.uiImage.gameObject.SetActive(value: true);
		}
		if (blackBackground != null)
		{
			blackBackground.gameObject.SetActive(value: true);
		}
		isShowing = true;
	}

	private void HideAll()
	{
		if (uiText != null)
		{
			uiText.text = "";
		}
		foreach (ViewItem viewItem in viewItems)
		{
			if (viewItem.uiImage != null)
			{
				viewItem.uiImage.gameObject.SetActive(value: false);
			}
		}
		if (blackBackground != null)
		{
			blackBackground.gameObject.SetActive(value: false);
		}
		isShowing = false;
	}
}
