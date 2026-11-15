using UnityEngine;

public class AudioPause : MonoBehaviour
{
	public AudioSource audioSource1;

	public GameObject targetObjectW;

	private bool hasPaused;

	private void Update()
	{
		if (audioSource1 != null && targetObjectW != null)
		{
			if (audioSource1.isPlaying && !hasPaused)
			{
				PauseAllAudioSources(targetObjectW);
				hasPaused = true;
			}
			if (!audioSource1.isPlaying && hasPaused)
			{
				hasPaused = false;
			}
		}
	}

	private void PauseAllAudioSources(GameObject obj)
	{
		AudioSource[] components = obj.GetComponents<AudioSource>();
		foreach (AudioSource audioSource in components)
		{
			if (audioSource.isPlaying)
			{
				audioSource.Pause();
			}
		}
	}
}
