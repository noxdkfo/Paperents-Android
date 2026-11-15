using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
	public GameObject imageBlack;

	public AudioSource audioKeyUse;

	private bool hasActivated;

	private void OnMouseDown()
	{
		if (base.gameObject.activeInHierarchy && !hasActivated)
		{
			hasActivated = true;
			if (imageBlack != null)
			{
				imageBlack.SetActive(value: true);
			}
			if (audioKeyUse != null)
			{
				audioKeyUse.Play();
			}
			Invoke("LoadEasterScene", 4f);
		}
	}

	private void LoadEasterScene()
	{
		SceneManager.LoadScene("SceneEaster");
	}
}
