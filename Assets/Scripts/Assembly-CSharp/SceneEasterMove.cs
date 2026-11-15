using UnityEngine;

public class SceneEasterMove : MonoBehaviour
{
	[Header("移动设置")]
	public float walkSpeed = 3f;

	public float runSpeed = 6f;

	private float moveSpeed;

	[Header("鼠标控制")]
	public float mouseSensitivity = 100f;

	public Transform playerCamera;

	private float xRotation;

	[Header("脚步声")]
	public AudioSource audioSource;

	public AudioClip[] stepSounds;

	public float stepInterval = 0.5f;

	private float stepTimer;

	[Header("摄像机摇晃")]
	public float shakeAmount = 0.05f;

	public float shakeSpeed = 5f;

	private Vector3 originalCamPos;

	private CharacterController controller;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		originalCamPos = playerCamera.localPosition;
	}

	private void Update()
	{
		HandleMouseLook();
		HandleMovement();
	}

	private void HandleMouseLook()
	{
		float num = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float num2 = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		xRotation -= num2;
		xRotation = Mathf.Clamp(xRotation, -75f, 75f);
		playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		base.transform.Rotate(Vector3.up * num);
	}

	private void HandleMovement()
	{
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		Vector3 vector = base.transform.right * axis + base.transform.forward * axis2;
		bool num = vector.magnitude > 0.1f;
		moveSpeed = (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
		controller.Move(vector * moveSpeed * Time.deltaTime);
		if (num)
		{
			HandleFootsteps();
			HandleCameraShake();
		}
		else
		{
			playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalCamPos, Time.deltaTime * shakeSpeed);
			stepTimer = 0f;
		}
	}

	private void HandleFootsteps()
	{
		stepTimer += Time.deltaTime;
		if (stepTimer > stepInterval)
		{
			if (stepSounds.Length != 0)
			{
				int num = Random.Range(0, stepSounds.Length);
				audioSource.PlayOneShot(stepSounds[num]);
			}
			stepTimer = 0f;
		}
	}

	private void HandleCameraShake()
	{
		Vector3 vector = Random.insideUnitSphere * shakeAmount;
		vector.y = 0f;
		playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalCamPos + vector, Time.deltaTime * shakeSpeed);
	}
}
