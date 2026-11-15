using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFadeIn : MonoBehaviour
{
	public float typingSpeed = 0.05f;

	private Text uiText;

	private string fullText;

	private Coroutine typingCoroutine;

	private void Awake()
	{
		uiText = GetComponent<Text>();
		fullText = uiText.text;
		uiText.text = "";
	}

	private void OnEnable()
	{
		if (typingCoroutine != null)
		{
			StopCoroutine(typingCoroutine);
		}
		typingCoroutine = StartCoroutine(TypeText());
	}

	private IEnumerator TypeText()
	{
		uiText.text = "";
		Color baseColor = uiText.color;
		baseColor.a = 0f;
		uiText.color = baseColor;
		for (int i = 0; i <= fullText.Length; i++)
		{
			uiText.text = fullText.Substring(0, i);
			float a = Mathf.Clamp01((float)i / (float)fullText.Length);
			uiText.color = new Color(baseColor.r, baseColor.g, baseColor.b, a);
			yield return new WaitForSeconds(typingSpeed);
		}
	}
}
