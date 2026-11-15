using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTriggerUISequence : MonoBehaviour
{
	public Camera mainCamera;

	public Camera cameraB;

	public GameObject boyObject;

	[Header("UI 对话配置（共8组）")]
	public List<GameObject> uiGroups;

	public List<AudioClip> audioClips;

	public List<float> displayDurations;

	public AudioSource audioSource;

	private bool triggered;

	private void Start()
	{
		if (cameraB != null)
		{
			cameraB.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (!triggered && boyObject.activeInHierarchy && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider.gameObject == boyObject)
		{
			triggered = true;
			StartCoroutine(SequenceRoutine());
		}
	}

	private IEnumerator SequenceRoutine()
	{
		mainCamera.gameObject.SetActive(value: false);
		cameraB.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < uiGroups.Count; i++)
		{
			if (i < uiGroups.Count)
			{
				uiGroups[i].SetActive(value: true);
			}
			if (i < audioClips.Count && audioClips[i] != null)
			{
				audioSource.clip = audioClips[i];
				audioSource.Play();
			}
			float seconds = ((i < displayDurations.Count) ? displayDurations[i] : 6f);
			yield return new WaitForSeconds(seconds);
			if (i < uiGroups.Count)
			{
				uiGroups[i].SetActive(value: false);
			}
		}
		yield return new WaitForSeconds(1f);
		cameraB.gameObject.SetActive(value: false);
		mainCamera.gameObject.SetActive(value: true);
	}
}
