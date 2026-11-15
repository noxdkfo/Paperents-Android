using System.Collections;
using UnityEngine;

public class Scene4CamTrans : MonoBehaviour
{
	[Header("Camera References")]
	public Camera mainCamera;

	public Camera knockCamera;

	[Header("Target Object")]
	public GameObject objWindow;

	[Header("Timings")]
	public float activeDelay = 1f;

	public float knockDuration = 3f;

	private float activeTimer;

	private bool isTransitioning;

	private bool hasSwitched;

	private void Start()
	{
		if (mainCamera != null)
		{
			mainCamera.enabled = true;
		}
		if (knockCamera != null)
		{
			knockCamera.enabled = false;
		}
	}

	private void Update()
	{
		if (objWindow == null || isTransitioning || hasSwitched)
		{
			return;
		}
		if (objWindow.activeInHierarchy)
		{
			activeTimer += Time.deltaTime;
			if (activeTimer >= activeDelay)
			{
				StartCoroutine(SwitchToKnockCamera());
			}
		}
		else
		{
			activeTimer = 0f;
		}
	}

	private IEnumerator SwitchToKnockCamera()
	{
		isTransitioning = true;
		if (mainCamera != null)
		{
			mainCamera.enabled = false;
		}
		if (knockCamera != null)
		{
			knockCamera.enabled = true;
		}
		yield return new WaitForSeconds(knockDuration);
		if (mainCamera != null)
		{
			mainCamera.enabled = true;
		}
		if (knockCamera != null)
		{
			knockCamera.enabled = false;
		}
		isTransitioning = false;
		hasSwitched = true;
	}
}
