using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
	public Button startButton;

	public Button quitButton;

	public Button retryButton;

	public Image blackImage;

	public AudioSource audioSource;

	public AudioClip touchClip;

	public AudioClip pushClip;

	private Dictionary<Button, Vector3> originalScales = new Dictionary<Button, Vector3>();

	private void Start()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		originalScales[startButton] = startButton.transform.localScale;
		originalScales[quitButton] = quitButton.transform.localScale;
		originalScales[retryButton] = retryButton.transform.localScale;
		AddButtonEvents(startButton, OnStartGame);
		AddButtonEvents(quitButton, OnQuitGame);
		AddButtonEvents(retryButton, OnRetryGame);
		if (PlayerPrefs.GetInt("ShowRetry", 0) == 1)
		{
			retryButton.gameObject.SetActive(value: true);
		}
		else
		{
			retryButton.gameObject.SetActive(value: false);
		}
		PlayerPrefs.DeleteKey("ShowRetry");
	}

	private void AddButtonEvents(Button button, UnityAction onClickAction)
	{
		button.onClick.AddListener(() =>
		{
			audioSource.PlayOneShot(pushClip);
			button.transform.localScale = originalScales[button] * 0.95f;
			onClickAction();
		});
		EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>();
		if (eventTrigger == null)
		{
			eventTrigger = button.gameObject.AddComponent<EventTrigger>();
		}
		EventTrigger.Entry entry = new EventTrigger.Entry
		{
			eventID = EventTriggerType.PointerEnter
		};
		entry.callback.AddListener((BaseEventData data) =>
		{
			audioSource.PlayOneShot(touchClip);
			button.transform.localScale = originalScales[button] * 1.05f;
		});
		EventTrigger.Entry entry2 = new EventTrigger.Entry
		{
			eventID = EventTriggerType.PointerExit
		};
		entry2.callback.AddListener((BaseEventData data) =>
		{
			button.transform.localScale = originalScales[button];
		});
		eventTrigger.triggers.Add(entry);
		eventTrigger.triggers.Add(entry2);
	}

	public void OnStartGame()
	{
		StartCoroutine(FadeAndLoadScene("Scene1"));
	}

	public void OnRetryGame()
	{
		StartCoroutine(FadeAndLoadScene("Scene3"));
	}

	public void OnQuitGame()
	{
		Application.Quit();
	}

	private IEnumerator FadeAndLoadScene(string sceneName)
	{
		blackImage.gameObject.SetActive(value: true);
		blackImage.color = new Color(0f, 0f, 0f, 0f);
		float timer = 0f;
		while (timer < 1f)
		{
			timer += Time.deltaTime;
			blackImage.color = new Color(0f, 0f, 0f, timer);
			yield return null;
		}
		SceneManager.LoadScene(sceneName);
	}
}
