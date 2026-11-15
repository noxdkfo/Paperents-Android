using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
	public enum AudioSourceType
	{
		None = 0,
		SourceA = 1,
		SourceB = 2
	}

	[Serializable]
	public class DialogueStep
	{
		[TextArea(2, 5)]
		public string textContent;

		public float displayDuration = 2f;

		public bool showText2;

		public bool showText3;

		public AudioClip audioClip;

		public AudioSourceType audioSource;
	}

	[Header("场景物体")]
	public GameObject object1;

	public GameObject object2;

	public GameObject object3;

	[Header("UI组件")]
	public GameObject image1;

	public Text text1;

	public GameObject text2;

	public GameObject text3;

	[Header("音效源")]
	public AudioSource clickAudio;

	public AudioSource audioSourceA;

	public AudioSource audioSourceB;

	[Header("对话内容设置")]
	public DialogueStep[] dialogueSteps;

	private bool interactionStarted;

	private void Update()
	{
		if (!interactionStarted && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider.gameObject == object1)
		{
			StartInteraction();
		}
	}

	private void StartInteraction()
	{
		interactionStarted = true;
		if (clickAudio != null)
		{
			clickAudio.Play();
		}
		image1.SetActive(value: true);
		text1.gameObject.SetActive(value: true);
		text2.SetActive(value: false);
		text3.SetActive(value: false);
		StartCoroutine(PlayDialogueSequence());
	}

	private IEnumerator PlayDialogueSequence()
	{
		for (int i = 0; i < dialogueSteps.Length; i++)
		{
			DialogueStep dialogueStep = dialogueSteps[i];
			if (dialogueStep.audioClip != null)
			{
				switch (dialogueStep.audioSource)
				{
				case AudioSourceType.SourceA:
					if (audioSourceA != null)
					{
						audioSourceA.clip = dialogueStep.audioClip;
						audioSourceA.Play();
					}
					break;
				case AudioSourceType.SourceB:
					if (audioSourceB != null)
					{
						audioSourceB.clip = dialogueStep.audioClip;
						audioSourceB.Play();
					}
					break;
				}
			}
			text1.text = dialogueStep.textContent;
			text2.SetActive(dialogueStep.showText2);
			text3.SetActive(dialogueStep.showText3);
			switch (i)
			{
			case 3:
				object1.SetActive(value: false);
				object2.SetActive(value: true);
				break;
			case 6:
				object2.SetActive(value: false);
				object3.SetActive(value: true);
				break;
			}
			yield return new WaitForSeconds(dialogueStep.displayDuration);
		}
		image1.SetActive(value: false);
		text1.gameObject.SetActive(value: false);
		text2.SetActive(value: false);
		text3.SetActive(value: false);
	}
}
