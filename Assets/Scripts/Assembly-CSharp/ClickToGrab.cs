using UnityEngine;

public class ClickToGrab : MonoBehaviour
{
	public GameObject objectP;

	public GameObject objectBG;

	public AudioSource grabAudio;

	public Transform player;

	public float interactDistance = 3f;

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0) || !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) || !(hitInfo.collider.gameObject == objectP))
		{
			return;
		}
		if (Vector3.Distance(player.position, objectP.transform.position) <= interactDistance)
		{
			objectP.SetActive(value: false);
			if (grabAudio != null)
			{
				grabAudio.Play();
			}
			if (objectBG != null)
			{
				objectBG.SetActive(value: true);
			}
		}
		else
		{
			Debug.Log("物体太远，无法交互。");
		}
	}
}
