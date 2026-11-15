using UnityEngine;

public class CameraFloat : MonoBehaviour
{
	public float floatIntensity = 0.5f;

	public float floatSpeed = 1f;

	private Vector3 originalRotation;

	private void Start()
	{
		originalRotation = base.transform.localEulerAngles;
	}

	private void Update()
	{
		float num = Mathf.PerlinNoise(Time.time * floatSpeed, 0f) - 0.5f;
		float num2 = Mathf.PerlinNoise(0f, Time.time * floatSpeed) - 0.5f;
		float x = originalRotation.x + num * floatIntensity;
		float y = originalRotation.y + num2 * floatIntensity;
		base.transform.localEulerAngles = new Vector3(x, y, originalRotation.z);
	}
}
