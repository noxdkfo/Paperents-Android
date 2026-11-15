using UnityEngine;
using UnityEngine.Playables;

public class ClickToActivate : MonoBehaviour
{
	[Header("Timeline播放控制器")]
	public PlayableDirector timeline;

	[Header("需要激活的物体")]
	public GameObject itemsToActivate;

	private bool hasActivated;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && hitInfo.collider.gameObject == base.gameObject && !hasActivated)
		{
			if (timeline != null)
			{
				timeline.Play();
			}
			if (itemsToActivate != null)
			{
				itemsToActivate.SetActive(value: true);
			}
			hasActivated = true;
		}
	}
}
