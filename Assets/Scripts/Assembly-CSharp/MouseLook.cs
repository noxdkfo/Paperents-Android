using UnityEngine;

[AddComponentMenu("Camera/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	[Header("鼠标灵敏度")]
	public float sensitivityX = 2f;

	public float sensitivityY = 2f;

	[Header("旋转限制")]
	[Tooltip("左右最大旋转角度（总和180度）")]
	[SerializeField]
	private float maxHorizontalAngle = 90f;

	[Tooltip("上下最大旋转角度（总和120度）")]
	[SerializeField]
	private float maxVerticalAngle = 60f;

	private float _currentHorizontalRot;

	private float _currentVerticalRot;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Vector3 eulerAngles = base.transform.eulerAngles;
		_currentHorizontalRot = NormalizeAngle(eulerAngles.y);
		_currentVerticalRot = NormalizeAngle(eulerAngles.x);
		_currentHorizontalRot = Mathf.Clamp(_currentHorizontalRot, 0f - maxHorizontalAngle, maxHorizontalAngle);
		_currentVerticalRot = Mathf.Clamp(_currentVerticalRot, 0f - maxVerticalAngle, maxVerticalAngle);
		ApplyRotation();
	}

	private void Update()
	{
		float num = Input.GetAxis("Mouse X") * sensitivityX;
		float num2 = Input.GetAxis("Mouse Y") * sensitivityY;
		_currentHorizontalRot += num;
		_currentVerticalRot -= num2;
		_currentHorizontalRot = Mathf.Clamp(_currentHorizontalRot, 0f - maxHorizontalAngle, maxHorizontalAngle);
		_currentVerticalRot = Mathf.Clamp(_currentVerticalRot, 0f - maxVerticalAngle, maxVerticalAngle);
		ApplyRotation();
	}

	private void ApplyRotation()
	{
		base.transform.rotation = Quaternion.Euler(_currentVerticalRot, _currentHorizontalRot, 0f);
	}

	private float NormalizeAngle(float angle)
	{
		angle %= 360f;
		if (angle > 180f)
		{
			angle -= 360f;
		}
		if (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	private void OnValidate()
	{
		maxHorizontalAngle = Mathf.Clamp(maxHorizontalAngle, 0f, 180f);
		maxVerticalAngle = Mathf.Clamp(maxVerticalAngle, 0f, 120f);
	}
}
