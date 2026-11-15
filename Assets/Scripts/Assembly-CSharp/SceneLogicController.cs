using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogicController : MonoBehaviour
{
	[Header("调试选项")]
	[SerializeField]
	private bool debugStartFromThisScene = true;

	[Header("摄像机")]
	[SerializeField]
	private Camera camera1;

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private Camera camera2;

	[Header("UI 黑屏")]
	[SerializeField]
	private Image blackScreen;

	[Header("场景对象")]
	[SerializeField]
	private GameObject npcM1;

	[SerializeField]
	private GameObject npcD1;

	[SerializeField]
	private GameObject arm1;

	[SerializeField]
	private GameObject npcD2;

	[SerializeField]
	private GameObject arm2;

	[SerializeField]
	private GameObject arm3;

	[SerializeField]
	private GameObject npcM2;

	[SerializeField]
	private GameObject eyeball;

	[SerializeField]
	private GameObject blvdStnd;

	[SerializeField]
	private GameObject door1;

	[SerializeField]
	private GameObject door2;

	[SerializeField]
	private GameObject npcB;

	[Header("音效")]
	[SerializeField]
	private AudioSource audioScream;

	[SerializeField]
	private AudioSource audioThunder;

	[SerializeField]
	private AudioSource audioKnock;

	[Header("灯光")]
	[SerializeField]
	private Light spotLight1;

	private Camera currentCam;

	private bool npcM1Clicked;

	private bool npcD2Clickable;

	private bool npcD2Clicked;

	private float originalSpotAngle;

	private void Start()
	{
		Debug.Log("\ud83c\udfac SceneLogicController 启动");
		camera1.gameObject.SetActive(value: true);
		mainCamera.gameObject.SetActive(value: false);
		camera2.gameObject.SetActive(value: false);
		currentCam = camera1;
		if (spotLight1 != null)
		{
			originalSpotAngle = spotLight1.spotAngle;
		}
		if (debugStartFromThisScene)
		{
			npcM1?.SetActive(value: true);
			npcD1?.SetActive(value: true);
			arm1?.SetActive(value: true);
			npcD2?.SetActive(value: false);
			arm2?.SetActive(value: false);
			arm3?.SetActive(value: false);
			npcM2?.SetActive(value: false);
			eyeball?.SetActive(value: false);
			blvdStnd?.SetActive(value: false);
			door1?.SetActive(value: true);
			door2?.SetActive(value: false);
			npcB?.SetActive(value: false);
		}
		if (blackScreen != null)
		{
			blackScreen.color = new Color(0f, 0f, 0f, 1f);
			StartCoroutine(FadeFromBlack());
		}
		Invoke("SwitchToMainCamera", 4f);
	}

	private IEnumerator FadeFromBlack()
	{
		float duration = 2f;
		float t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			float a = Mathf.Lerp(1f, 0f, t / duration);
			Color color = blackScreen.color;
			blackScreen.color = new Color(color.r, color.g, color.b, a);
			yield return null;
		}
		blackScreen.gameObject.SetActive(value: false);
	}

	private void SwitchToMainCamera()
	{
		camera1.gameObject.SetActive(value: false);
		mainCamera.gameObject.SetActive(value: true);
		currentCam = mainCamera;
	}

	private void Update()
	{
		if (!(currentCam == null) && Input.GetMouseButtonDown(0) && Physics.Raycast(currentCam.ScreenPointToRay(Input.mousePosition), out var hitInfo))
		{
			string text = hitInfo.collider.gameObject.name;
			if (text == npcM1.name && !npcM1Clicked)
			{
				npcM1Clicked = true;
				Debug.Log("\ud83d\uddb1\ufe0f 点击了 NPC-M-1");
				Invoke("OnNPCM1ClickedAfterDelay", 8f);
			}
			if (text == npcD2.name && npcD2Clickable && !npcD2Clicked)
			{
				npcD2Clicked = true;
				Debug.Log("\ud83d\uddb1\ufe0f 点击了 NPC-D-2");
				OnNPCD2Clicked();
			}
		}
	}

	private void OnNPCM1ClickedAfterDelay()
	{
		npcD1?.SetActive(value: false);
		arm1?.SetActive(value: false);
		if (npcD2 != null)
		{
			npcD2.SetActive(value: true);
			Debug.Log("✅ 激活 NPC-D-2");
		}
		if (arm2 != null)
		{
			arm2.SetActive(value: true);
			Debug.Log("✅ 激活 Arm-2");
		}
		npcD2Clickable = false;
		Invoke("EnableNPCD2Click", 6f);
	}

	private void EnableNPCD2Click()
	{
		npcD2Clickable = true;
		Debug.Log("\ud83d\udfe2 允许点击 NPC-D-2");
	}

	private void OnNPCD2Clicked()
	{
		arm2?.SetActive(value: false);
		arm3?.SetActive(value: true);
		audioScream?.Play();
		Debug.Log("\ud83c\udfac NPC-D-2 事件触发，Arm-3 出现");
		Invoke("Arm3EventAfter2s", 2f);
		Invoke("Arm3EventAfter6s", 6f);
		Invoke("Arm3EventAfter7s", 7f);
		Invoke("Arm3EventAfter10s", 10f);
	}

	private void Arm3EventAfter2s()
	{
		npcM1?.SetActive(value: false);
		npcM2?.SetActive(value: true);
		eyeball?.SetActive(value: true);
		Debug.Log("\ud83c\udfad NPC-M 替换 + Eyeball 出现");
	}

	private void Arm3EventAfter6s()
	{
		camera2.gameObject.SetActive(value: true);
		mainCamera.gameObject.SetActive(value: false);
		currentCam = camera2;
		Debug.Log("\ud83c\udfa5 切换到 Camera 2");
		Invoke("BackToMainCamera", 3f);
	}

	private void BackToMainCamera()
	{
		mainCamera.gameObject.SetActive(value: true);
		camera2.gameObject.SetActive(value: false);
		currentCam = mainCamera;
		Debug.Log("\ud83c\udfa5 切换回 Main Camera");
	}

	private void Arm3EventAfter7s()
	{
		arm1?.SetActive(value: false);
		arm2?.SetActive(value: false);
		Debug.Log("\ud83d\udd3d Arm-1 和 Arm-2 隐藏");
		npcM2?.SetActive(value: false);
		npcD2?.SetActive(value: false);
		blvdStnd?.SetActive(value: true);
		if (spotLight1 != null)
		{
			spotLight1.spotAngle = 179f;
			Invoke("RestoreSpotAngle", 0.8f);
		}
		audioThunder?.Play();
		door1?.SetActive(value: false);
		door2?.SetActive(value: true);
		Debug.Log("⚡ 场景变化 + 雷声播放");
	}

	private void RestoreSpotAngle()
	{
		if (spotLight1 != null)
		{
			spotLight1.spotAngle = originalSpotAngle;
			Debug.Log("\ud83d\udd01 恢复 SpotLight 原始角度");
		}
	}

	private void Arm3EventAfter10s()
	{
		audioKnock?.Play();
		npcB?.SetActive(value: true);
		Debug.Log("\ud83d\udeaa 敲门声 + NPC-B 出现");
	}
}
