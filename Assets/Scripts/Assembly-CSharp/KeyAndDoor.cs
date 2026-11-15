using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyAndDoor : MonoBehaviour
{
	public static bool wasKeyHidden;

	public GameObject key;

	public GameObject goBED;

	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "Scene3" && key != null)
		{
			wasKeyHidden = !key.activeSelf;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Scene4" && goBED != null)
		{
			if (wasKeyHidden)
			{
				goBED.SetActive(value: false);
				Debug.Log("GoBED 已被强制隐藏，因为玩家离开 Scene3 时 Key 是隐藏的。");
			}
			else
			{
				goBED.SetActive(value: true);
			}
		}
	}
}
