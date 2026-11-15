using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSequence : MonoBehaviour
{
	public GameObject objectA;

	public GameObject objectB;

	public GameObject objectC;

	public GameObject objectD;

	public GameObject objectE;

	public Image image1;

	public Text text1;

	public Text text2;

	public Text text3;

	public string[] text1Contents = new string[5];

	public AudioClip[] voiceClips = new AudioClip[5];

	public AudioSource audioSource;

	private bool hasTriggered;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger entered by: " + other.gameObject.name);
		if (!hasTriggered && other.gameObject == objectA)
		{
			hasTriggered = true;
			StartCoroutine(Sequence());
		}
	}

	private void SetTextAndPlayAudio(int index)
	{
		if (index >= 0 && index < text1Contents.Length)
		{
			text1.text = text1Contents[index];
			if (voiceClips[index] != null && audioSource != null)
			{
				audioSource.Stop();
				audioSource.clip = voiceClips[index];
				audioSource.Play();
			}
		}
	}

	private IEnumerator Sequence()
	{
		objectA.SetActive(value: false);
		objectB.SetActive(value: true);
		objectC.SetActive(value: true);
		objectD.SetActive(value: true);
		objectE.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		image1.gameObject.SetActive(value: true);
		text1.gameObject.SetActive(value: true);
		text2.gameObject.SetActive(value: true);
		text3.gameObject.SetActive(value: false);
		SetTextAndPlayAudio(0);
		yield return new WaitForSeconds(3f);
		SetTextAndPlayAudio(1);
		text2.gameObject.SetActive(value: true);
		text3.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(3f);
		SetTextAndPlayAudio(2);
		text2.gameObject.SetActive(value: false);
		text3.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		SetTextAndPlayAudio(3);
		text3.gameObject.SetActive(value: false);
		text2.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(5f);
		SetTextAndPlayAudio(4);
		yield return new WaitForSeconds(4f);
		image1.gameObject.SetActive(value: false);
		text1.gameObject.SetActive(value: false);
		text2.gameObject.SetActive(value: false);
		text3.gameObject.SetActive(value: false);
	}
}
