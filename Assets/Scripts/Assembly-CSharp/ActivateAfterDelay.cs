using System.Collections;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
	public GameObject objectToActivate;

	public float delaySeconds = 15f;

	public bool useTrigger = true;

	private bool hasActivated;

	private void OnTriggerEnter(Collider other)
	{
		if (useTrigger && !hasActivated && other.CompareTag("Activator"))
		{
			StartCoroutine(ActivateWithDelay());
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!useTrigger && !hasActivated && collision.gameObject.CompareTag("Activator"))
		{
			StartCoroutine(ActivateWithDelay());
		}
	}

	private IEnumerator ActivateWithDelay()
	{
		hasActivated = true;
		yield return new WaitForSeconds(delaySeconds);
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(value: true);
		}
	}
}
