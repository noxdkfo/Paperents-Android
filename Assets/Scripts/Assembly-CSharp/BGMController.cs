using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
	public AudioSource disturbAudio;

	public GameObject window3;

	public GameObject boyD;

	private bool isPlaying;

	private bool isFading;

	private static BGMController instance;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Update()
	{
		if (!isPlaying && SceneManager.GetActiveScene().name == "Scene4" && ((window3 != null && window3.activeInHierarchy) || (boyD != null && boyD.activeInHierarchy)))
		{
			PlayDisturb();
		}
		if (Input.GetKeyDown(KeyCode.K) && disturbAudio.isPlaying && !isFading)
		{
			StartCoroutine(FadeOut(5f));
		}
	}

	private void PlayDisturb()
	{
		disturbAudio.Play();
		isPlaying = true;
	}

	private IEnumerator FadeOut(float duration)
	{
		isFading = true;
		float startVolume = disturbAudio.volume;
		float time = 0f;
		while (time < duration)
		{
			disturbAudio.volume = Mathf.Lerp(startVolume, 0f, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		disturbAudio.volume = 0f;
		disturbAudio.Stop();
		isPlaying = false;
		isFading = false;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "EndScene1" && disturbAudio.isPlaying && !isFading)
		{
			StartCoroutine(FadeOut(5f));
		}
		if (scene.name == "EndScene2" && disturbAudio.isPlaying && !isFading)
		{
			StartCoroutine(DelayedFadeOut(20f, 5f));
		}
	}

	private IEnumerator DelayedFadeOut(float delay, float fadeDuration)
	{
		yield return new WaitForSeconds(delay);
		if (disturbAudio.isPlaying && !isFading)
		{
			StartCoroutine(FadeOut(fadeDuration));
		}
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
