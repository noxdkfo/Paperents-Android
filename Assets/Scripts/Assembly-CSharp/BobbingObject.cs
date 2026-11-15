using UnityEngine;

public class BobbingObject : MonoBehaviour
{
	public float amplitude = 0.1f;

	public float frequency = 1f;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		float y = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
		base.transform.position = new Vector3(startPos.x, y, startPos.z);
	}
}
