using UnityEngine;
using UnityEngine.SceneManagement;

public class SelfRescue : MonoBehaviour
{
	public Transform playerCamera;

	public float detectionDistance = 5f;

	public LayerMask interactLayer;

	public GameObject goBed;

	public GameObject goFloor;

	public GameObject imageBed;

	public GameObject imageFloor;

	public GameObject beiZi;

	public GameObject bedDoor;

	private GameObject currentTarget;

	private void Update()
	{
		CheckPlayerView();
		HandleInteractionInput();
	}

	private void CheckPlayerView()
	{
		Ray ray = new Ray(playerCamera.position, playerCamera.forward);
		if ((bool)imageBed)
		{
			imageBed.SetActive(value: false);
		}
		if ((bool)imageFloor)
		{
			imageFloor.SetActive(value: false);
		}
		currentTarget = null;
		if (!Physics.Raycast(ray, out var hitInfo, detectionDistance, interactLayer))
		{
			return;
		}
		GameObject gameObject = hitInfo.collider.gameObject;
		if (gameObject == goBed)
		{
			if ((bool)imageBed)
			{
				imageBed.SetActive(value: true);
			}
			currentTarget = goBed;
		}
		else if (gameObject == goFloor)
		{
			if ((bool)imageFloor)
			{
				imageFloor.SetActive(value: true);
			}
			currentTarget = goFloor;
		}
	}

	private void HandleInteractionInput()
	{
		if (!(currentTarget == null) && Input.GetMouseButtonDown(0))
		{
			if (currentTarget == goBed)
			{
				OnGoBedTriggered();
			}
			else if (currentTarget == goFloor)
			{
				OnGoFloorTriggered();
			}
		}
	}

	private void OnGoBedTriggered()
	{
		if ((bool)beiZi)
		{
			beiZi.SetActive(value: false);
		}
		if ((bool)bedDoor)
		{
			bedDoor.SetActive(value: true);
		}
	}

	private void OnGoFloorTriggered()
	{
		SceneManager.LoadScene("Scene5");
	}
}
