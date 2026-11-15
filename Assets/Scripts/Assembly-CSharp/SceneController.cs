using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public GameObject blackScreenImage;

	public GameObject dialogBackground;

	public GameObject dialogText1;

	public GameObject dialogText2;

	public AudioSource audioSource;

	public Camera mainCamera;

	public MonoBehaviour playerViewScript;

	private float initialFOV = 10f;

	private float targetFOV = 50f;

	private float fovDuration = 2f;

	private void Start()
	{
		if (playerViewScript != null)
		{
			playerViewScript.enabled = false;
		}
		if (mainCamera != null)
		{
			mainCamera.fieldOfView = initialFOV;
		}
		if (blackScreenImage != null)
		{
			blackScreenImage.SetActive(value: true);
		}
		if (audioSource != null)
		{
			audioSource.Play();
		}
		StartCoroutine(SequenceCoroutine());
	}

	private IEnumerator SequenceCoroutine()
	{
		yield return new WaitForSeconds(5f);
		if (blackScreenImage != null)
		{
			blackScreenImage.SetActive(value: false);
		}
		if (dialogBackground != null)
		{
			dialogBackground.SetActive(value: true);
		}
		if (dialogText1 != null)
		{
			dialogText1.SetActive(value: true);
		}
		if (dialogText2 != null)
		{
			dialogText2.SetActive(value: true);
		}
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
		if (dialogBackground != null)
		{
			dialogBackground.SetActive(value: false);
		}
		if (dialogText1 != null)
		{
			dialogText1.SetActive(value: false);
		}
		if (dialogText2 != null)
		{
			dialogText2.SetActive(value: false);
		}
		float elapsed = 0f;
		if (mainCamera != null)
		{
			while (elapsed < fovDuration)
			{
				elapsed += Time.deltaTime;
				float t = Mathf.Clamp01(elapsed / fovDuration);
				mainCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t);
				yield return null;
			}
			mainCamera.fieldOfView = targetFOV;
		}
		if (playerViewScript != null)
		{
			playerViewScript.enabled = true;
		}
	}
}
