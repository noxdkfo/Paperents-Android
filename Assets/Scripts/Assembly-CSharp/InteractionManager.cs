using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
	[Header("UI Elements")]
	public GameObject image1;

	public Text text1;

	public Text text2;

	public Text text3;

	[Header("Scene Objects")]
	public GameObject object1;

	public GameObject object2;

	[Header("Audio")]
	public AudioSource clickSound;

	public AudioSource finalAudio;

	public AudioSource dialogueAudioSource;

	public AudioClip[] dialogueClips;

	[Header("Content Settings")]
	public string[] dialogueContents;

	public float[] dialogueDurations;

	private bool isInteracting;

	private void Update()
	{
	}

	public void OnObject1Clicked()
	{
		if (!isInteracting)
		{
			isInteracting = true;
			image1.SetActive(value: true);
			text1.gameObject.SetActive(value: true);
			text2.gameObject.SetActive(value: true);
			text3.gameObject.SetActive(value: false);
			if (clickSound != null)
			{
				clickSound.Play();
			}
			StartCoroutine(PlayDialogueSequence());
		}
	}

	private IEnumerator PlayDialogueSequence()
	{
		int totalSteps = Mathf.Min(dialogueContents.Length, dialogueDurations.Length);
		for (int i = 0; i < totalSteps; i++)
		{
			text1.text = dialogueContents[i];
			bool flag = i % 2 == 0;
			text2.gameObject.SetActive(flag);
			text3.gameObject.SetActive(!flag);
			if (i < dialogueClips.Length && dialogueClips[i] != null)
			{
				dialogueAudioSource.clip = dialogueClips[i];
				dialogueAudioSource.Play();
			}
			yield return new WaitForSeconds(dialogueDurations[i]);
		}
		object1.SetActive(value: false);
		object2.SetActive(value: true);
		if (finalAudio != null)
		{
			finalAudio.Play();
		}
		yield return new WaitForSeconds(5f);
		image1.SetActive(value: false);
		text1.gameObject.SetActive(value: false);
		text2.gameObject.SetActive(value: false);
		text3.gameObject.SetActive(value: false);
	}
}
