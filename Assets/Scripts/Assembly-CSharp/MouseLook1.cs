using UnityEngine;

public class MouseLook1 : MonoBehaviour
{
	public float mouseSensitivity = 100f;

	public float pitchMin = -90f;

	public float pitchMax = 90f;

	private float yaw = 180f;

	private float pitch;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		base.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
	}

	private void Update()
	{
		float num = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float num2 = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		yaw += num;
		pitch -= num2;
		pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
		base.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
	}
}
