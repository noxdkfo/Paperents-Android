using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Scene4Controller : MonoBehaviour
{
	[Header("Interactive Objects")]
	public GameObject WNDO2;

	public GameObject WNDO3;

	public GameObject BoyN;

	public GameObject BoyD;

	public GameObject BoyE1;

	public GameObject BoyE2;

	public GameObject BoyE3;

	public GameObject BloodOnWall;

	public GameObject ParentStandUp;

	public GameObject Escape;

	[Header("UI Elements")]
	public GameObject option1UI;

	public GameObject option2UI;

	public GameObject videoUI;

	[Header("Audio - Main")]
	public AudioSource audioSource;

	public AudioClip audioKnockingClip;

	[Header("Audio - Video Background")]
	public AudioSource musicSource;

	public AudioClip squeezClip;

	[Header("Video")]
	public VideoPlayer videoPlayer;

	public VideoClip videoClip1;

	[Header("Materials")]
	public GameObject Poster;

	public Material material1;

	public Material material2;

	[Header("Cameras")]
	public Camera mainCamera;

	private bool waitingForChoice;

	private Coroutine deathTimerCoroutine;

	private bool hasClickedWNDO2;

	private void Start()
	{
		if ((bool)option1UI)
		{
			option1UI.SetActive(value: false);
		}
		if ((bool)option2UI)
		{
			option2UI.SetActive(value: false);
		}
		if ((bool)videoUI)
		{
			videoUI.SetActive(value: false);
		}
		if ((bool)BloodOnWall)
		{
			BloodOnWall.SetActive(value: false);
		}
		if ((bool)ParentStandUp)
		{
			ParentStandUp.SetActive(value: false);
		}
		if ((bool)WNDO3)
		{
			WNDO3.SetActive(value: false);
		}
		if ((bool)BoyD)
		{
			BoyD.SetActive(value: false);
		}
		if ((bool)BoyE1)
		{
			BoyE1.SetActive(value: false);
		}
		if ((bool)BoyE2)
		{
			BoyE2.SetActive(value: false);
		}
		if ((bool)BoyE3)
		{
			BoyE3.SetActive(value: false);
		}
		if ((bool)mainCamera)
		{
			mainCamera.gameObject.SetActive(value: true);
		}
		if ((bool)videoPlayer)
		{
			videoPlayer.isLooping = false;
		}
	}

	private void Update()
	{
		if (!hasClickedWNDO2 && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider != null && hitInfo.collider.gameObject == WNDO2)
		{
			hasClickedWNDO2 = true;
			waitingForChoice = true;
			if ((bool)option1UI)
			{
				option1UI.SetActive(value: true);
			}
		}
		if (waitingForChoice)
		{
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				waitingForChoice = false;
				StartCoroutine(HandleBranchL());
			}
			else if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				waitingForChoice = false;
				StartCoroutine(HandleBranchW());
			}
		}
	}

	private IEnumerator HandleBranchL()
	{
		PlayKnockEvent();
		if ((bool)BoyN)
		{
			BoyN.SetActive(value: false);
		}
		if ((bool)BoyD)
		{
			BoyD.SetActive(value: true);
		}
		yield return new WaitForSeconds(4f);
		yield return StartCoroutine(PlayVideo());
		yield return new WaitForSeconds(1f);
		if ((bool)ParentStandUp)
		{
			ParentStandUp.SetActive(value: true);
		}
		deathTimerCoroutine = StartCoroutine(DeathCounting());
		yield return new WaitForSeconds(1f);
		if ((bool)option2UI)
		{
			option2UI.SetActive(value: true);
		}
		if ((bool)Escape)
		{
			Escape.SetActive(value: true);
		}
	}

	private IEnumerator HandleBranchW()
	{
		PlayKnockEvent();
		if ((bool)WNDO2)
		{
			WNDO2.SetActive(value: false);
		}
		if ((bool)WNDO3)
		{
			WNDO3.SetActive(value: true);
		}
		yield return new WaitForSeconds(1f);
		if ((bool)BoyN)
		{
			BoyN.SetActive(value: false);
		}
		if ((bool)BoyE1)
		{
			BoyE1.SetActive(value: true);
		}
		yield return new WaitForSeconds(2f);
		if ((bool)BoyE1)
		{
			BoyE1.SetActive(value: false);
		}
		if ((bool)BoyE2)
		{
			BoyE2.SetActive(value: true);
		}
		yield return new WaitForSeconds(4f);
		if ((bool)Poster)
		{
			Poster.SetActive(value: false);
		}
		yield return new WaitForSeconds(6f);
		yield return StartCoroutine(PlayVideo());
		yield return new WaitForSeconds(2f);
		if ((bool)ParentStandUp)
		{
			ParentStandUp.SetActive(value: true);
		}
		yield return new WaitForSeconds(5f);
		if ((bool)BoyE2)
		{
			BoyE2.SetActive(value: false);
		}
		if ((bool)BoyE3)
		{
			BoyE3.SetActive(value: true);
		}
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("EndScene1");
	}

	private void PlayKnockEvent()
	{
		if ((bool)audioSource && (bool)audioKnockingClip)
		{
			audioSource.PlayOneShot(audioKnockingClip);
		}
		if ((bool)option1UI)
		{
			option1UI.SetActive(value: false);
		}
		if ((bool)Poster && (bool)material2)
		{
			Renderer component = Poster.GetComponent<Renderer>();
			if ((bool)component)
			{
				component.material = material2;
			}
		}
		if ((bool)BloodOnWall)
		{
			BloodOnWall.SetActive(value: true);
		}
	}

	private IEnumerator PlayVideo()
	{
		if ((bool)videoPlayer && (bool)videoClip1 && (bool)videoUI)
		{
			videoUI.SetActive(value: true);
			videoPlayer.clip = videoClip1;
			videoPlayer.Play();
			if ((bool)musicSource && (bool)squeezClip)
			{
				musicSource.PlayOneShot(squeezClip);
			}
			yield return new WaitUntil(() => !videoPlayer.isPlaying);
			videoUI.SetActive(value: false);
		}
	}

	private IEnumerator DeathCounting()
	{
		yield return new WaitForSeconds(30f);
		SceneManager.LoadScene("EndScene1");
	}
}
