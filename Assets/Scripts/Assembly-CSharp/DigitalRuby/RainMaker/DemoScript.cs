using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.RainMaker
{
	public class DemoScript : MonoBehaviour
	{
		private enum RotationAxes
		{
			MouseXAndY = 0,
			MouseX = 1,
			MouseY = 2
		}

		public RainScript RainScript;

		public Toggle MouseLookToggle;

		public Toggle FlashlightToggle;

		public Slider RainSlider;

		public Light Flashlight;

		public GameObject Sun;

		private RotationAxes axes;

		private float sensitivityX = 15f;

		private float sensitivityY = 15f;

		private float minimumX = -360f;

		private float maximumX = 360f;

		private float minimumY = -60f;

		private float maximumY = 60f;

		private float rotationX;

		private float rotationY;

		private Quaternion originalRotation;

		private void UpdateRain()
		{
			if (RainScript != null)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					RainScript.RainIntensity = 0f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					RainScript.RainIntensity = 0.2f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					RainScript.RainIntensity = 0.5f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					RainScript.RainIntensity = 0.8f;
				}
			}
		}

		private void UpdateMovement()
		{
			float num = 5f * Time.deltaTime;
			if (Input.GetKey(KeyCode.W))
			{
				Camera.main.transform.Translate(0f, 0f, num);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				Camera.main.transform.Translate(0f, 0f, 0f - num);
			}
			if (Input.GetKey(KeyCode.A))
			{
				Camera.main.transform.Translate(0f - num, 0f, 0f);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				Camera.main.transform.Translate(num, 0f, 0f);
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				FlashlightToggle.isOn = !FlashlightToggle.isOn;
			}
		}

		private void UpdateMouseLook()
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.M))
			{
				MouseLookToggle.isOn = !MouseLookToggle.isOn;
			}
			if (MouseLookToggle.isOn)
			{
				if (axes == RotationAxes.MouseXAndY)
				{
					rotationX += Input.GetAxis("Mouse X") * sensitivityX;
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					rotationX = ClampAngle(rotationX, minimumX, maximumX);
					rotationY = ClampAngle(rotationY, minimumY, maximumY);
					Quaternion quaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
					Quaternion quaternion2 = Quaternion.AngleAxis(rotationY, -Vector3.right);
					base.transform.localRotation = originalRotation * quaternion * quaternion2;
				}
				else if (axes == RotationAxes.MouseX)
				{
					rotationX += Input.GetAxis("Mouse X") * sensitivityX;
					rotationX = ClampAngle(rotationX, minimumX, maximumX);
					Quaternion quaternion3 = Quaternion.AngleAxis(rotationX, Vector3.up);
					base.transform.localRotation = originalRotation * quaternion3;
				}
				else
				{
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					rotationY = ClampAngle(rotationY, minimumY, maximumY);
					Quaternion quaternion4 = Quaternion.AngleAxis(0f - rotationY, Vector3.right);
					base.transform.localRotation = originalRotation * quaternion4;
				}
			}
		}

		public void RainSliderChanged(float val)
		{
			RainScript.RainIntensity = val;
		}

		public void MouseLookChanged(bool val)
		{
			MouseLookToggle.isOn = val;
		}

		public void FlashlightChanged(bool val)
		{
			FlashlightToggle.isOn = val;
			Flashlight.enabled = val;
		}

		public void DawnDuskSliderChanged(float val)
		{
			Sun.transform.rotation = Quaternion.Euler(val, 0f, 0f);
		}

		public void FollowCameraChanged(bool val)
		{
			RainScript.FollowCamera = val;
		}

		private void Start()
		{
			originalRotation = base.transform.localRotation;
			RainScript rainScript = RainScript;
			float rainIntensity = (RainSlider.value = 0.5f);
			rainScript.RainIntensity = rainIntensity;
			RainScript.EnableWind = true;
		}

		private void Update()
		{
			UpdateRain();
			UpdateMovement();
			UpdateMouseLook();
		}

		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}
	}
}
