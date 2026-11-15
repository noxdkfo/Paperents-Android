using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInUIText : MonoBehaviour
{
	public Text uiText;

	public float fadeDuration = 3f;

	private void OnEnable()
	{
		if (uiText == null)
		{
			uiText = GetComponent<Text>();
		}
		Color color = uiText.color;
		color.a = 0f;
		uiText.color = color;
		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		float elapsed = 0f;
		while (elapsed < fadeDuration)
		{
			float a = elapsed / fadeDuration;
			Color color = uiText.color;
			color.a = a;
			uiText.color = color;
			elapsed += Time.deltaTime;
			yield return null;
		}
		Color color2 = uiText.color;
		color2.a = 1f;
		uiText.color = color2;
	}
}
