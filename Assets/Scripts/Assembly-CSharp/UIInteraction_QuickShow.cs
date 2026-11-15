using UnityEngine;

public class UIInteraction_QuickShow : MonoBehaviour
{
	public Camera cam;

	public GameObject object1;

	public GameObject image1;

	public GameObject image2;

	public GameObject text1;

	public AudioSource audioSource;

	public AudioClip clickAudio;

	public AudioClip hideAudio;

	private bool uiShown;

	private void Start()
	{
		image1.SetActive(value: false);
		image2.SetActive(value: false);
		text1.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && !uiShown && Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider.gameObject == object1)
		{
			ShowUI();
		}
		if (Input.GetKeyDown(KeyCode.Q) && uiShown)
		{
			HideUI();
		}
	}

	private void ShowUI()
	{
		uiShown = true;
		image1.SetActive(value: true);
		image2.SetActive(value: true);
		text1.SetActive(value: true);
		PlaySound(clickAudio);
	}

	private void HideUI()
	{
		uiShown = false;
		image1.SetActive(value: false);
		image2.SetActive(value: false);
		text1.SetActive(value: false);
		PlaySound(hideAudio);
	}

	private void PlaySound(AudioClip clip)
	{
		if (audioSource != null && clip != null)
		{
			audioSource.PlayOneShot(clip);
		}
	}
}
