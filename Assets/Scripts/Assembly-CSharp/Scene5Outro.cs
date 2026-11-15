using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene5Outro : MonoBehaviour
{
	public Image imageBlackOutro;

	public Image imageHB;

	public Image imageEND;

	public Image imageStudio;

	public AudioSource audioOpenDoor;

	public string sceneToLoad = "StartScene";

	private bool triggered;

	private void OnTriggerEnter(Collider other)
	{
		if (!triggered && other.CompareTag("Player"))
		{
			triggered = true;
			StartCoroutine(OutroSequence());
		}
	}

	private IEnumerator OutroSequence()
	{
		yield return StartCoroutine(FadeImage(imageBlackOutro, 0f, 1f, 3f));
		yield return new WaitForSeconds(1f);
		if (audioOpenDoor != null)
		{
			audioOpenDoor.Play();
		}
		yield return new WaitForSeconds(3f);
		imageHB.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageHB.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		imageEND.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageEND.gameObject.SetActive(value: false);
		imageStudio.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		imageStudio.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(3f);
		PlayerPrefs.SetInt("ShowRetry", 1);
		SceneManager.LoadScene(sceneToLoad);
	}

	private IEnumerator FadeImage(Image image, float fromAlpha, float toAlpha, float duration)
	{
		image.gameObject.SetActive(value: true);
		Color color = image.color;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float a = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
			image.color = new Color(color.r, color.g, color.b, a);
			yield return null;
		}
		image.color = new Color(color.r, color.g, color.b, toAlpha);
	}
}
