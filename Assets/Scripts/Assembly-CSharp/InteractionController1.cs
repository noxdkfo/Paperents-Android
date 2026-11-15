using System.Collections;
using UnityEngine;

public class InteractionController1 : MonoBehaviour
{
	public GameObject image2Speaker;

	public GameObject image3;

	public GameObject miniGameUI;

	public GameObject pointer;

	public RectTransform pointerTransform;

	public RectTransform targetZone;

	public GameObject wndo1;

	public GameObject wndo2;

	private bool phoneClicked;

	private bool canClickWindow;

	private bool miniGameActive;

	private bool gameSuccess;

	private float pointerSpeed = 600f;

	private bool movingRight = true;

	private void Update()
	{
		if (miniGameActive)
		{
			MovePointer();
			if (Input.GetKeyDown(KeyCode.E))
			{
				CheckPointerSuccess();
			}
		}
	}

	private void MovePointer()
	{
		float x = pointerSpeed * Time.deltaTime * (float)(movingRight ? 1 : (-1));
		pointerTransform.anchoredPosition += new Vector2(x, 0f);
		if (pointerTransform.anchoredPosition.x > 200f)
		{
			movingRight = false;
		}
		else if (pointerTransform.anchoredPosition.x < -200f)
		{
			movingRight = true;
		}
	}

	private void CheckPointerSuccess()
	{
		float x = pointerTransform.anchoredPosition.x;
		float x2 = targetZone.anchoredPosition.x;
		float num = targetZone.sizeDelta.x / 2f;
		if (Mathf.Abs(x - x2) <= num)
		{
			gameSuccess = true;
			EndMiniGame();
		}
		else
		{
			Debug.Log("Try Again!");
		}
	}

	public void EndMiniGame()
	{
		miniGameUI.SetActive(value: false);
		image2Speaker.SetActive(value: false);
		wndo1.SetActive(value: false);
		wndo2.SetActive(value: true);
		miniGameActive = false;
	}

	public void OnPhoneClicked()
	{
		if (!phoneClicked)
		{
			phoneClicked = true;
			StartCoroutine(PhoneInteractionSequence());
		}
	}

	private IEnumerator PhoneInteractionSequence()
	{
		image2Speaker.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		image3.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		image3.SetActive(value: false);
		canClickWindow = true;
	}

	public void OnWindowClicked()
	{
		if (canClickWindow && !miniGameActive)
		{
			miniGameUI.SetActive(value: true);
			miniGameActive = true;
			pointerTransform.anchoredPosition = new Vector2(-200f, 0f);
			movingRight = true;
		}
	}
}
