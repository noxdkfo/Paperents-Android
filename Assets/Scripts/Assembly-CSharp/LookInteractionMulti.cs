using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookInteractionMulti : MonoBehaviour
{
	[Header("交互目标物体列表")]
	public List<GameObject> targetObjects = new List<GameObject>();

	[Header("提示 Image UI")]
	public Image tipsImage;

	[Header("检测参数")]
	public float detectionDistance = 10f;

	public LayerMask detectionLayer;

	private bool isLookingAtTarget;

	private void Start()
	{
		if (tipsImage != null)
		{
			tipsImage.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		bool flag = CheckLookAtAnyTarget();
		if (flag && !isLookingAtTarget)
		{
			ShowTip();
		}
		else if (!flag && isLookingAtTarget)
		{
			HideTip();
		}
		isLookingAtTarget = flag;
		if (isLookingAtTarget && Input.GetMouseButtonDown(0))
		{
			HideTip();
		}
	}

	private bool CheckLookAtAnyTarget()
	{
		if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out var hitInfo, detectionDistance, detectionLayer.value))
		{
			foreach (GameObject targetObject in targetObjects)
			{
				if (hitInfo.collider.gameObject == targetObject)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void ShowTip()
	{
		if (tipsImage != null)
		{
			tipsImage.gameObject.SetActive(value: true);
		}
	}

	private void HideTip()
	{
		if (tipsImage != null)
		{
			tipsImage.gameObject.SetActive(value: false);
		}
	}
}
