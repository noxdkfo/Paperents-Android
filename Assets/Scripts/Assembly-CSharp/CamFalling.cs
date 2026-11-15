using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamFalling : MonoBehaviour
{
	public Camera cameraFalling;

	public Camera playerMainCamera;

	public string targetSceneName = "SceneEaster";

	public float switchDelay = 5f;

	private void Start()
	{
		if (SceneManager.GetActiveScene().name == targetSceneName)
		{
			StartCoroutine(SwitchCameraAfterDelay());
		}
	}

	private IEnumerator SwitchCameraAfterDelay()
	{
		if (cameraFalling != null)
		{
			cameraFalling.enabled = true;
		}
		if (playerMainCamera != null)
		{
			playerMainCamera.enabled = false;
		}
		yield return new WaitForSeconds(switchDelay);
		if (cameraFalling != null)
		{
			cameraFalling.enabled = false;
		}
		if (playerMainCamera != null)
		{
			playerMainCamera.enabled = true;
		}
	}
}
