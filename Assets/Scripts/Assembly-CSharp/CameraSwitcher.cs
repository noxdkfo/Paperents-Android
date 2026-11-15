using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	public Camera cameraFall;

	public Camera cameraFloor;

	public float switchDelay = 10f;

	private void Start()
	{
		if (cameraFall != null && cameraFloor != null)
		{
			cameraFall.enabled = true;
			cameraFloor.enabled = false;
			StartCoroutine(SwitchCameraAfterDelay());
		}
		else
		{
			Debug.LogWarning("Camera references are not set in the inspector.");
		}
	}

	private IEnumerator SwitchCameraAfterDelay()
	{
		yield return new WaitForSeconds(switchDelay);
		cameraFall.enabled = false;
		cameraFloor.enabled = true;
	}
}
