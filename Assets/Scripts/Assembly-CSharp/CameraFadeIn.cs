using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeIn : MonoBehaviour
{
	[Header("黑屏UI Image组件（命名为Image Black）")]
	public Image blackImage;

	[Header("淡出时长（秒）")]
	public float fadeDuration = 2f;

	private void Start()
	{
		if (blackImage == null)
		{
			GameObject gameObject = GameObject.Find("Image Black");
			if (gameObject != null)
			{
				blackImage = gameObject.GetComponent<Image>();
			}
		}
		if (blackImage != null)
		{
			StartCoroutine(FadeOut());
		}
		else
		{
			Debug.LogWarning("未找到名为 Image Black 的UI Image对象！");
		}
	}

	private IEnumerator FadeOut()
	{
		blackImage.gameObject.SetActive(value: true);
		Color color = blackImage.color;
		color.a = 1f;
		blackImage.color = color;
		float timer = 0f;
		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			float a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
			blackImage.color = new Color(color.r, color.g, color.b, a);
			yield return null;
		}
		blackImage.color = new Color(color.r, color.g, color.b, 0f);
		blackImage.gameObject.SetActive(value: false);
	}
}
