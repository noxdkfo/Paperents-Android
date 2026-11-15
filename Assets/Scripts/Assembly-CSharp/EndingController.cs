using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingController : MonoBehaviour
{
	public GameObject newspaper;

	public GameObject end2VidUI;

	public VideoPlayer videoPlayer;

	public CanvasGroup videoCanvasGroup;

	public GameObject imageEND;

	public GameObject imageStudio;

	public string nextSceneName = "StartScene";

	public float timeBeforeNewspaper = 6f;

	public float newspaperDisplayDuration = 3f;

	public float videoFadeInDuration = 1f;

	public float imageEndDisplayDuration = 4f;

	public float imageStudioDisplayDuration = 4f;

	public float finalWaitBeforeSceneLoad = 2f;

	private void Start()
	{
		if (newspaper != null)
		{
			newspaper.SetActive(value: false);
		}
		if (end2VidUI != null)
		{
			end2VidUI.SetActive(value: false);
		}
		if (videoCanvasGroup != null)
		{
			videoCanvasGroup.alpha = 0f;
		}
		if (imageEND != null)
		{
			imageEND.SetActive(value: false);
		}
		if (imageStudio != null)
		{
			imageStudio.SetActive(value: false);
		}
		StartCoroutine(PlayEndingSequence());
	}

	private IEnumerator PlayEndingSequence()
	{
		yield return new WaitForSeconds(timeBeforeNewspaper);
		if (newspaper != null)
		{
			newspaper.SetActive(value: true);
		}
		yield return new WaitForSeconds(newspaperDisplayDuration);
		if (end2VidUI != null)
		{
			end2VidUI.SetActive(value: true);
		}
		if (videoCanvasGroup != null)
		{
			float elapsed = 0f;
			while (elapsed < videoFadeInDuration)
			{
				elapsed += Time.deltaTime;
				float alpha = Mathf.Clamp01(elapsed / videoFadeInDuration);
				videoCanvasGroup.alpha = alpha;
				yield return null;
			}
			videoCanvasGroup.alpha = 1f;
		}
		if (videoPlayer != null)
		{
			videoPlayer.Play();
			while (!videoPlayer.isPlaying)
			{
				yield return null;
			}
			while (videoPlayer.isPlaying)
			{
				yield return null;
			}
		}
		if (end2VidUI != null)
		{
			end2VidUI.SetActive(value: false);
		}
		if (imageEND != null)
		{
			imageEND.SetActive(value: true);
		}
		yield return new WaitForSeconds(imageEndDisplayDuration);
		if (imageEND != null)
		{
			imageEND.SetActive(value: false);
		}
		if (imageStudio != null)
		{
			imageStudio.SetActive(value: true);
		}
		yield return new WaitForSeconds(imageStudioDisplayDuration);
		if (imageStudio != null)
		{
			imageStudio.SetActive(value: false);
		}
		yield return new WaitForSeconds(finalWaitBeforeSceneLoad);
		PlayerPrefs.SetInt("ShowRetry", 1);
		SceneManager.LoadScene(nextSceneName);
	}
}
