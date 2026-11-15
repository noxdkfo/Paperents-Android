using UnityEngine;
using UnityEngine.SceneManagement;

public class PreStart : MonoBehaviour
{
	public GameObject logo;

	public GameObject nameText;

	public GameObject warmText;

	private void Start()
	{
		if (logo != null)
		{
			logo.SetActive(value: true);
		}
		if (nameText != null)
		{
			nameText.SetActive(value: true);
		}
		if (warmText != null)
		{
			warmText.SetActive(value: false);
		}
		Invoke("ShowWarmText", 2f);
		Invoke("LoadStartScene", 8f);
	}

	private void ShowWarmText()
	{
		if (logo != null)
		{
			logo.SetActive(value: false);
		}
		if (nameText != null)
		{
			nameText.SetActive(value: false);
		}
		if (warmText != null)
		{
			warmText.SetActive(value: true);
		}
	}

	private void LoadStartScene()
	{
		SceneManager.LoadScene("StartScene");
	}
}
