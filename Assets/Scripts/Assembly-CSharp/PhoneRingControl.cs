using UnityEngine;

public class PhoneRingControl : MonoBehaviour
{
	public GameObject arm2;

	public GameObject arm3;

	public GameObject ringObject;

	private AudioSource ringAudio;

	private bool isRinging;

	private bool isHidden;

	private void Start()
	{
		if (ringObject != null)
		{
			ringAudio = ringObject.GetComponent<AudioSource>();
		}
	}

	private void Update()
	{
		if (arm2.activeSelf && !isRinging && !isHidden && ringAudio != null)
		{
			ringAudio.Play();
			isRinging = true;
		}
		if (!isHidden && (Input.GetKeyDown(KeyCode.Alpha1) || arm3.activeSelf))
		{
			if (ringAudio != null && ringAudio.isPlaying)
			{
				ringAudio.Stop();
			}
			if (ringObject != null)
			{
				ringObject.SetActive(value: false);
			}
			isHidden = true;
			isRinging = false;
		}
	}
}
