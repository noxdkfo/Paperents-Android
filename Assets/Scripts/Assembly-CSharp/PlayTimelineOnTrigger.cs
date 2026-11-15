using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineOnTrigger : MonoBehaviour
{
	public PlayableDirector director;

	private bool hasPlayed;

	private void Start()
	{
		if (director == null)
		{
			director = GetComponent<PlayableDirector>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hasPlayed && other.CompareTag("NPC"))
		{
			director.Play();
		}
	}
}
