using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneInteraction : MonoBehaviour
{
	[Header("Scene Objects")]
	public GameObject phone;

	public GameObject boy;

	public GameObject theKey;

	[Header("UI Elements")]
	public GameObject imageP;

	public GameObject imageWish;

	public GameObject imageB1;

	public GameObject imageB2;

	public GameObject imageB3;

	public GameObject imageBlack;

	[Header("Audio")]
	public AudioSource audioSource;

	public AudioClip audioPickUp;

	public AudioClip audioNoise;

	public AudioClip audioRun;

	public AudioClip audioLiam;

	private bool imagePActive;

	private bool hasPressed1;

	private bool hasPressed2;

	private bool oneThenTwoTriggerB;

	private bool hasPressed1_BoyHidden;

	private float boyActiveTime = -1f;

	private bool isBoyHidden => !boy.activeSelf;

	private void Start()
	{
		imageP?.SetActive(value: false);
		imageWish?.SetActive(value: false);
		imageB1?.SetActive(value: false);
		imageB2?.SetActive(value: false);
		imageB3?.SetActive(value: false);
		imageBlack?.SetActive(value: false);
		theKey?.SetActive(value: false);
	}

	private void Update()
	{
		if (boy.activeSelf && boyActiveTime < 0f)
		{
			boyActiveTime = Time.time;
		}
		HandleClick();
		if (!imagePActive)
		{
			return;
		}
		if (isBoyHidden)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				hasPressed1_BoyHidden = true;
				audioSource?.PlayOneShot(audioRun);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				audioSource?.PlayOneShot(audioNoise);
			}
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				imageP?.SetActive(value: false);
				imagePActive = false;
				if (hasPressed1_BoyHidden)
				{
					StartCoroutine(ShowWishSequence());
				}
				hasPressed1_BoyHidden = false;
			}
		}
		else
		{
			if (!(Time.time - boyActiveTime >= 30f))
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				imageP?.SetActive(value: false);
				imagePActive = false;
				if (hasPressed1 && !hasPressed2)
				{
					StartCoroutine(PlaySequence());
				}
				hasPressed1 = false;
				hasPressed2 = false;
				oneThenTwoTriggerB = false;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				hasPressed1 = true;
				audioSource?.PlayOneShot(audioNoise);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) && !hasPressed2)
			{
				hasPressed2 = true;
				if (hasPressed1)
				{
					oneThenTwoTriggerB = true;
				}
				StartCoroutine(HandleKey2());
			}
		}
	}

	private void HandleClick()
	{
		if (!(phone == null) && !(Camera.main == null) && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider != null && hitInfo.collider.gameObject == phone)
		{
			audioSource?.PlayOneShot(audioPickUp);
			imageP?.SetActive(value: true);
			imagePActive = true;
			hasPressed1 = false;
			hasPressed2 = false;
			oneThenTwoTriggerB = false;
			hasPressed1_BoyHidden = false;
		}
	}

	private IEnumerator HandleKey2()
	{
		audioSource?.PlayOneShot(audioLiam);
		yield return new WaitForSeconds(2f);
		imageP?.SetActive(value: false);
		imagePActive = false;
		theKey?.SetActive(value: true);
		if (oneThenTwoTriggerB)
		{
			yield return new WaitForSeconds(1f);
			StartCoroutine(PlaySequence());
		}
		hasPressed1 = false;
		hasPressed2 = false;
		oneThenTwoTriggerB = false;
	}

	private IEnumerator ShowWishSequence()
	{
		yield return new WaitForSeconds(1f);
		imageWish?.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageWish?.SetActive(value: false);
	}

	private IEnumerator PlaySequence()
	{
		yield return new WaitForSeconds(1f);
		imageB1?.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageB1?.SetActive(value: false);
		imageB2?.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageB2?.SetActive(value: false);
		imageB3?.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		imageB3?.SetActive(value: false);
		yield return new WaitForSeconds(1f);
		StartCoroutine(FadeInAndTransition());
	}

	private IEnumerator FadeInAndTransition()
	{
		if (imageBlack == null)
		{
			Debug.LogError("⚠\ufe0f imageBlack 未绑定！");
			yield break;
		}
		imageBlack.SetActive(value: true);
		CanvasGroup cg = imageBlack.GetComponent<CanvasGroup>();
		if (cg == null)
		{
			cg = imageBlack.AddComponent<CanvasGroup>();
		}
		float t = 0f;
		while (t < 2f)
		{
			t += Time.deltaTime;
			cg.alpha = Mathf.Clamp01(t / 2f);
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("Scene4");
	}
}
