using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End1Controller : MonoBehaviour
{
	public Image blackImage;

	public GameObject fObject;

	public Text endText;

	public Image studioImage;

	private void Start()
	{
		StartCoroutine(EndSequence());
	}

	private IEnumerator EndSequence()
	{
		blackImage.gameObject.SetActive(value: true);
		fObject.SetActive(value: false);
		endText.gameObject.SetActive(value: false);
		studioImage.gameObject.SetActive(value: false);
		SetTextAlpha(endText, 1f);
		yield return new WaitForSeconds(4f);
		blackImage.gameObject.SetActive(value: false);
		fObject.SetActive(value: true);
		yield return new WaitForSeconds(10f);
		blackImage.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		endText.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		yield return FadeText(endText, 2f);
		endText.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		studioImage.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		studioImage.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		PlayerPrefs.SetInt("ShowRetry", 1);
		SceneManager.LoadScene("StartScene");
	}

	private void SetTextAlpha(Text text, float alpha)
	{
		Color color = text.color;
		color.a = alpha;
		text.color = color;
	}

	private IEnumerator FadeText(Text text, float duration)
	{
		float elapsed = 0f;
		Color originalColor = text.color;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float a = Mathf.Lerp(1f, 0f, elapsed / duration);
			text.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
			yield return null;
		}
		text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
	}
}
