using System.Collections;
using UnityEngine;

public class NPCClickUITrigger : MonoBehaviour
{
	public Camera playerCamera;

	public float interactionDistance = 100f;

	public GameObject imageA;

	public GameObject imageB;

	public AudioSource audioSource;

	public AudioClip soundA;

	public AudioClip soundB;

	private bool isProcessing;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !isProcessing && Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo, interactionDistance) && hitInfo.collider.CompareTag("NPC"))
		{
			StartCoroutine(ShowUISequence());
		}
	}

	private IEnumerator ShowUISequence()
	{
		isProcessing = true;
		imageA.SetActive(value: true);
		if (soundA != null && audioSource != null)
		{
			audioSource.PlayOneShot(soundA);
		}
		yield return new WaitForSeconds(1.5f);
		imageA.SetActive(value: false);
		imageB.SetActive(value: true);
		if (soundB != null && audioSource != null)
		{
			audioSource.PlayOneShot(soundB);
		}
		yield return new WaitForSeconds(2f);
		imageB.SetActive(value: false);
		isProcessing = false;
	}
}
