using UnityEngine;

public class LookTrigger : MonoBehaviour
{
	public string triggerTag = "Trigger1";

	public GameObject object1;

	public Material materialA;

	public Material materialB;

	public float rayDistance = 100f;

	private Renderer objectRenderer;

	private bool isLookingAtTrigger;

	private void Start()
	{
		if (object1 != null)
		{
			objectRenderer = object1.GetComponent<Renderer>();
		}
	}

	private void Update()
	{
		if (Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out var hitInfo, rayDistance) && hitInfo.collider.CompareTag(triggerTag))
		{
			if (!isLookingAtTrigger)
			{
				SetMaterial(materialA);
				isLookingAtTrigger = true;
			}
		}
		else if (isLookingAtTrigger)
		{
			SetMaterial(materialB);
			isLookingAtTrigger = false;
		}
	}

	private void SetMaterial(Material mat)
	{
		if (objectRenderer != null && mat != null)
		{
			objectRenderer.material = mat;
		}
	}
}
