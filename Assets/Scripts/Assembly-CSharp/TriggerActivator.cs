using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerActivator : MonoBehaviour
{
	public GameObject object2;

	public PlayableDirector timeline;

	public string triggeringTag = "Player";

	private bool hasTriggered;

	private void OnTriggerEnter(Collider other)
	{
		if (!hasTriggered && other.CompareTag(triggeringTag))
		{
			hasTriggered = true;
			StartCoroutine(DelayedActions());
		}
	}

	private IEnumerator DelayedActions()
	{
		yield return new WaitForSeconds(5f);
		if (object2 != null)
		{
			object2.SetActive(value: true);
		}
		yield return new WaitForSeconds(0.1f);
		if (timeline != null)
		{
			timeline.Play();
		}
	}
}
