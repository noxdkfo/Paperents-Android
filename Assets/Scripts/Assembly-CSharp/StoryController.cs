using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
	[Serializable]
	public class AutoStep
	{
		public float delayTime = 3f;

		public string textN;

		public string textD;
	}

	[Header("NPCs & Objects")]
	public GameObject npc1_1;

	[Header("NPCs & Objects")]
	public GameObject npc1_2;

	[Header("NPCs & Objects")]
	public GameObject npc1_3;

	public GameObject npc2_1;

	public GameObject npc2_2;

	public GameObject npc2_3;

	public GameObject objectA;

	public GameObject objectB;

	public GameObject objectC;

	public GameObject objectD;

	public GameObject objectP;

	[Header("Cameras")]
	public Camera mainCamera;

	[Header("Cameras")]
	public Camera camera1;

	[Header("Cameras")]
	public Camera camera2;

	[Header("UI Elements")]
	public GameObject image1;

	[Header("UI Elements")]
	public GameObject image2;

	[Header("UI Elements")]
	public GameObject image3;

	[Header("UI Elements")]
	public GameObject image4;

	public Text textW;

	public Text textN;

	public Text textD;

	public Text textS;

	[Header("Audio")]
	public AudioSource audioSource;

	public AudioClip[] spaceClips;

	[Header("Auto Step Config")]
	public List<AutoStep> autoSteps;

	private bool sequenceStarted;

	private bool wPressed;

	private int spaceStep;

	private void Start()
	{
		npc1_1.SetActive(value: false);
		npc1_2.SetActive(value: false);
		npc1_3.SetActive(value: false);
		npc2_1.SetActive(value: false);
		npc2_2.SetActive(value: false);
		npc2_3.SetActive(value: false);
		objectP.SetActive(value: false);
		image1.SetActive(value: false);
		image2.SetActive(value: false);
		image3.SetActive(value: false);
		image4.SetActive(value: false);
		textW.gameObject.SetActive(value: false);
		textS.gameObject.SetActive(value: false);
		camera1.gameObject.SetActive(value: false);
		camera2.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			if (hitInfo.collider.CompareTag("PosterRoll"))
			{
				npc1_1.SetActive(value: true);
			}
			if (!sequenceStarted && hitInfo.collider.CompareTag("NPC1-1"))
			{
				sequenceStarted = true;
				StartCoroutine(MainSequence());
			}
		}
		if (sequenceStarted && !wPressed && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)))
		{
			wPressed = true;
			textW.gameObject.SetActive(value: false);
			image3.SetActive(value: true);
			textN.text = "MOM";
			textD.text = "Isn't that the gift from our neighbor?";
			spaceStep = 0;
			StartCoroutine(AutoAdvanceSteps());
		}
	}

	private IEnumerator MainSequence()
	{
		mainCamera.gameObject.SetActive(value: false);
		camera1.gameObject.SetActive(value: true);
		image1.SetActive(value: true);
		objectA.SetActive(value: false);
		objectB.SetActive(value: false);
		objectC.SetActive(value: false);
		objectD.SetActive(value: false);
		npc2_1.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		camera1.gameObject.SetActive(value: false);
		camera2.gameObject.SetActive(value: true);
		image1.SetActive(value: false);
		image2.SetActive(value: true);
		yield return new WaitForSeconds(5f);
		camera2.gameObject.SetActive(value: false);
		mainCamera.gameObject.SetActive(value: true);
		image2.SetActive(value: false);
		yield return new WaitForSeconds(1f);
		textW.gameObject.SetActive(value: true);
	}

	private IEnumerator AutoAdvanceSteps()
	{
		while (spaceStep < autoSteps.Count)
		{
			AutoStep step = autoSteps[spaceStep];
			yield return new WaitForSeconds(step.delayTime);
			UpdateStepByCase(spaceStep);
			textN.text = step.textN;
			textD.text = step.textD;
			PlaySpaceClip(spaceStep);
			spaceStep++;
		}
		yield return new WaitForSeconds(5f);
		image3.SetActive(value: false);
		textW.gameObject.SetActive(value: false);
		StartCoroutine(FadeInImage4());
	}

	private void UpdateStepByCase(int step)
	{
		switch (step)
		{
		case 0:
			npc1_1.SetActive(value: false);
			npc1_2.SetActive(value: true);
			npc2_1.SetActive(value: false);
			npc2_2.SetActive(value: true);
			break;
		case 1:
			npc2_2.SetActive(value: false);
			npc2_3.SetActive(value: true);
			break;
		case 3:
			npc1_2.SetActive(value: false);
			npc1_3.SetActive(value: true);
			npc2_2.SetActive(value: false);
			npc2_3.SetActive(value: true);
			objectP.SetActive(value: true);
			break;
		case 2:
		case 4:
		case 5:
		case 6:
		case 7:
			break;
		}
	}

	private void PlaySpaceClip(int index)
	{
		if (audioSource != null && spaceClips != null && index < spaceClips.Length && spaceClips[index] != null)
		{
			audioSource.PlayOneShot(spaceClips[index]);
		}
	}

	private IEnumerator FadeInImage4()
	{
		CanvasGroup canvas = image4.GetComponent<CanvasGroup>();
		if (canvas == null)
		{
			canvas = image4.AddComponent<CanvasGroup>();
		}
		image4.SetActive(value: true);
		canvas.alpha = 0f;
		float timer = 0f;
		while (timer < 5f)
		{
			canvas.alpha = timer / 3f;
			timer += Time.deltaTime;
			yield return null;
		}
		canvas.alpha = 1f;
		yield return new WaitForSeconds(2f);
		StartCoroutine(FadeTextS());
	}

	private IEnumerator FadeTextS()
	{
		textS.gameObject.SetActive(value: true);
		CanvasGroup group = textS.GetComponent<CanvasGroup>();
		if (group == null)
		{
			group = textS.gameObject.AddComponent<CanvasGroup>();
		}
		group.alpha = 0f;
		float timer = 0f;
		while (timer < 1f)
		{
			group.alpha = timer / 1f;
			timer += Time.deltaTime;
			yield return null;
		}
		group.alpha = 1f;
		yield return new WaitForSeconds(3f);
		timer = 0f;
		while (timer < 1f)
		{
			group.alpha = 1f - timer / 1f;
			timer += Time.deltaTime;
			yield return null;
		}
		group.alpha = 0f;
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Scene2");
	}
}
