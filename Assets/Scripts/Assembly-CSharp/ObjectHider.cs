using UnityEngine;

public class ObjectHider : MonoBehaviour
{
	public GameObject objectA;

	public GameObject objectB;

	public GameObject objectE;

	private bool timerStarted;

	private float timer;

	public float delay = 40f;

	private void Update()
	{
		if (objectB.activeSelf && !timerStarted)
		{
			timerStarted = true;
			timer = 0f;
		}
		if (!timerStarted)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer >= delay)
		{
			if (objectA != null)
			{
				objectA.SetActive(value: false);
			}
			if (objectE != null)
			{
				objectE.SetActive(value: false);
			}
			timerStarted = false;
		}
	}
}
