using UnityEngine;

public class LampSwitch : MonoBehaviour
{
	public Light light1;

	public Light light2;

	public Light light3;

	public AudioClip clickSound;

	private AudioSource audioSource;

	private bool isOn;

	private void Start()
	{
		if (light1 != null)
		{
			light1.enabled = false;
		}
		if (light2 != null)
		{
			light2.enabled = false;
		}
		if (light3 != null)
		{
			light3.enabled = false;
		}
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		audioSource.clip = clickSound;
		audioSource.playOnAwake = false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider.gameObject == base.gameObject)
		{
			isOn = !isOn;
			if (light1 != null)
			{
				light1.enabled = isOn;
			}
			if (light2 != null)
			{
				light2.enabled = isOn;
			}
			if (light3 != null)
			{
				light3.enabled = isOn;
			}
			if (clickSound != null)
			{
				audioSource.Play();
			}
		}
	}
}
