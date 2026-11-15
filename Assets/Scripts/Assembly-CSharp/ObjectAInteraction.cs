using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectAInteraction : MonoBehaviour
{
	public GameObject panel;

	public AudioClip audioClick;

	public AudioClip audioClip1;

	public AudioClip audioClip2;

	public AudioSource audioSource;

	public GameObject textF;

	public GameObject textCo;

	private Coroutine textRoutine;

	private void Start()
	{
		if (panel != null)
		{
			panel.SetActive(value: false);
		}
		if (textF != null)
		{
			textF.SetActive(value: false);
		}
		if (textCo != null)
		{
			textCo.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider != null && hitInfo.collider.gameObject == base.gameObject)
			{
				if (audioSource != null && audioClick != null)
				{
					audioSource.PlayOneShot(audioClick);
				}
				if (panel != null)
				{
					panel.SetActive(value: true);
				}
			}
		}
		if (!(panel != null) || !panel.activeSelf)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			if (audioSource != null && audioClip1 != null)
			{
				audioSource.clip = audioClip1;
				audioSource.Play();
			}
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			if (audioSource != null && audioClip2 != null)
			{
				audioSource.clip = audioClip2;
				audioSource.Play();
			}
			if (textRoutine != null)
			{
				StopCoroutine(textRoutine);
			}
			textRoutine = StartCoroutine(HandleTextFToCo());
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			if (audioSource != null)
			{
				audioSource.Stop();
			}
			if (textRoutine != null)
			{
				StopCoroutine(textRoutine);
				textRoutine = null;
			}
			if (textF != null)
			{
				textF.SetActive(value: false);
			}
			if (textCo != null)
			{
				textCo.SetActive(value: false);
			}
			if (panel != null)
			{
				panel.SetActive(value: false);
			}
		}
	}

	private IEnumerator HandleTextFToCo()
	{
		if (textF != null)
		{
			textF.SetActive(value: true);
		}
		yield return new WaitForSeconds(5f);
		if (textF != null)
		{
			textF.SetActive(value: false);
		}
		if (textCo != null)
		{
			textCo.SetActive(value: true);
		}
		yield return new WaitForSeconds(6f);
		if (textCo != null)
		{
			textCo.SetActive(value: false);
		}
	}
}
