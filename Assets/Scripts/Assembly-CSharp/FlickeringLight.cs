using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
	[Header("Flicker Settings")]
	public float minFlickerInterval = 0.03f;

	public float maxFlickerInterval = 0.08f;

	[Header("Light Intensity Settings")]
	public bool randomIntensity = true;

	public float minIntensity = 0.2f;

	public float maxIntensity = 1f;

	private Light targetLight;

	private float originalIntensity;

	private void OnEnable()
	{
		targetLight = GetComponent<Light>();
		originalIntensity = targetLight.intensity;
		StartCoroutine(FlickerLoop());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		if (targetLight != null)
		{
			targetLight.enabled = true;
			targetLight.intensity = originalIntensity;
		}
	}

	private IEnumerator FlickerLoop()
	{
		while (true)
		{
			targetLight.enabled = !targetLight.enabled;
			if (targetLight.enabled && randomIntensity)
			{
				targetLight.intensity = Random.Range(minIntensity, maxIntensity);
			}
			float seconds = Random.Range(minFlickerInterval, maxFlickerInterval);
			yield return new WaitForSeconds(seconds);
		}
	}
}
