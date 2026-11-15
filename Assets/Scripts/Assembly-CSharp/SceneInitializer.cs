using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializer : MonoBehaviour
{
	public AudioSource audioSource;

	public AudioClip stormsClip;

	public Image image1;

	private void Start()
	{
		if (audioSource != null && stormsClip != null)
		{
			audioSource.clip = stormsClip;
			audioSource.Play();
		}
		if (image1 != null)
		{
			image1.gameObject.SetActive(value: false);
			StartCoroutine(HandleImageDisplay());
		}
	}

	private IEnumerator HandleImageDisplay()
	{
		yield return new WaitForSeconds(1f);
		image1.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		image1.gameObject.SetActive(value: false);
	}
}
