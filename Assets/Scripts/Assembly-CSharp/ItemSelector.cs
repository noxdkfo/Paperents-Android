using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
	public GameObject itemsA;

	public GameObject itemsB;

	public GameObject bedFire;

	public GameObject wetStained;

	public GameObject fire;

	public GameObject imageUSE1;

	public GameObject imageUSE2;

	public GameObject imageOutro;

	public AudioSource audioPickup;

	private bool firstItemUsed;

	private bool bedFireClicked;

	private bool secondItemUsed;

	public void OnItemsClicked()
	{
		if (!firstItemUsed)
		{
			itemsA.SetActive(value: false);
			imageUSE1.SetActive(value: true);
			bedFire.SetActive(value: true);
			audioPickup?.Play();
			firstItemUsed = true;
		}
		else if (bedFireClicked && !secondItemUsed)
		{
			itemsB.SetActive(value: false);
			imageUSE2.SetActive(value: true);
			audioPickup?.Play();
			secondItemUsed = true;
		}
	}

	public void OnBedFireClicked()
	{
		if (firstItemUsed)
		{
			wetStained.SetActive(value: true);
			imageUSE1.SetActive(value: false);
			bedFire.SetActive(value: false);
			bedFireClicked = true;
		}
	}

	public void OnWetStainedClicked()
	{
		if (secondItemUsed)
		{
			fire.SetActive(value: true);
			imageUSE2.SetActive(value: false);
			StartCoroutine(ShowOutroSequence());
		}
	}

	private IEnumerator ShowOutroSequence()
	{
		yield return new WaitForSeconds(5f);
		yield return StartCoroutine(FadeInImage(imageOutro.GetComponent<Image>()));
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("EndScene2");
	}

	private IEnumerator FadeInImage(Image img)
	{
		img.gameObject.SetActive(value: true);
		Color color = img.color;
		color.a = 0f;
		img.color = color;
		while (color.a < 1f)
		{
			color.a += Time.deltaTime;
			img.color = color;
			yield return null;
		}
	}
}
