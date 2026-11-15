using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LampEventManager : MonoBehaviour
{
	public GameObject light1;

	public GameObject objectEYE;

	public GameObject objectWHO;

	public GameObject objectBln;

	public GameObject objectBLN1;

	public GameObject objectBlood;

	public GameObject objectWhiteFace;

	public GameObject objectBS;

	public GameObject objectPP;

	public GameObject objectPW;

	public GameObject textLightin;

	public GameObject imageCM;

	public GameObject imageO;

	public GameObject objectBIGEYE;

	public GameObject poster;

	public GameObject objectBLDSTR;

	public PlayableDirector mainCameraTimeline;

	public GameObject glitchLight;

	public Material material1;

	public Material material2;

	public Camera mainCamera;

	public Camera camera1;

	public AudioSource clickSound;

	public AudioSource audioM;

	public AudioSource audioSTP;

	public AudioSource audio41;

	public AudioSource audio42;

	public AudioSource turnOffSound;

	public AudioSource audioExplr1;

	public AudioSource audioExplr2;

	private int clickCount;

	private bool lookingAtPP;

	private bool isLightOn => light1.activeSelf;

	private bool isLampActive => base.gameObject.activeSelf;

	private void Start()
	{
		light1.SetActive(value: false);
		objectEYE.SetActive(value: false);
		camera1.gameObject.SetActive(value: false);
		objectBlood.SetActive(value: false);
		objectWhiteFace?.SetActive(value: false);
		objectBS.SetActive(value: false);
		objectPP.SetActive(value: false);
		objectPW.SetActive(value: false);
		imageCM.SetActive(value: false);
		imageO.SetActive(value: false);
		textLightin.SetActive(value: false);
	}

	private void Update()
	{
		if (objectPW != null && objectPW.activeSelf)
		{
			textLightin.SetActive(value: false);
		}
		else
		{
			textLightin.SetActive(isLampActive && !isLightOn);
		}
		if (objectPP.activeSelf && Physics.Raycast(new Ray(mainCamera.transform.position, mainCamera.transform.forward), out var hitInfo) && hitInfo.collider.gameObject == objectPP)
		{
			if (!lookingAtPP)
			{
				StartCoroutine(HandleLookAtPP());
			}
			lookingAtPP = true;
		}
	}

	private void OnMouseDown()
	{
		if (!isLightOn)
		{
			clickSound?.Play();
			clickCount++;
			switch (clickCount)
			{
			case 1:
				StartCoroutine(FirstClickSequence());
				break;
			case 2:
				StartCoroutine(SecondClickSequence());
				break;
			case 3:
				StartCoroutine(ThirdClickSequence());
				break;
			case 4:
				StartCoroutine(FourthClickSequence());
				break;
			case 5:
				StartCoroutine(FifthClickSequence());
				break;
			}
		}
	}

	private IEnumerator FirstClickSequence()
	{
		light1.SetActive(value: true);
		objectEYE.SetActive(value: true);
		objectWHO?.SetActive(value: false);
		yield return new WaitForSeconds(5f);
		StartCoroutine(SwitchCamera(camera1, 4f));
		yield return new WaitForSeconds(1f);
		audioM?.Play();
		yield return new WaitForSeconds(12f);
		StartCoroutine(ShowUI(imageCM, 2f));
		yield return new WaitForSeconds(1f);
		light1.SetActive(value: false);
		objectEYE.SetActive(value: false);
		audioM?.Stop();
		turnOffSound?.Play();
		audioSTP?.Play();
	}

	private IEnumerator SecondClickSequence()
	{
		light1.SetActive(value: true);
		objectBLN1?.SetActive(value: false);
		audioExplr1?.Play();
		poster.GetComponent<Renderer>().material = material2;
		audioSTP?.Stop();
		audio41?.Play();
		yield return new WaitForSeconds(8f);
		light1.SetActive(value: false);
		audio41?.Stop();
		turnOffSound?.Play();
	}

	private IEnumerator ThirdClickSequence()
	{
		light1.SetActive(value: true);
		objectBIGEYE?.SetActive(value: true);
		audio42?.Play();
		yield return new WaitForSeconds(6.5f);
		objectBln?.SetActive(value: false);
		objectBlood?.SetActive(value: true);
		objectWhiteFace?.SetActive(value: true);
		audioExplr2?.Play();
		if (objectWhiteFace != null)
		{
			StartCoroutine(HideAfterDelay(objectWhiteFace, 0.7f));
		}
		yield return new WaitForSeconds(0.7f);
		light1.SetActive(value: false);
		turnOffSound?.Play();
	}

	private IEnumerator FourthClickSequence()
	{
		light1.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		light1.SetActive(value: false);
		objectBS?.SetActive(value: true);
		turnOffSound?.Play();
	}

	private IEnumerator FifthClickSequence()
	{
		light1.SetActive(value: true);
		objectBS?.SetActive(value: false);
		objectPP?.SetActive(value: true);
		yield return new WaitUntil(() => !objectPP.activeSelf);
		glitchLight?.SetActive(value: true);
		light1?.SetActive(value: false);
		yield return new WaitForSeconds(3f);
		StartCoroutine(ActivatePWAndThenBLDSTR());
	}

	private IEnumerator HandleLookAtPP()
	{
		yield return new WaitForSeconds(2f);
		objectPP?.SetActive(value: false);
		yield return new WaitForSeconds(0f);
		yield return new WaitForSeconds(0f);
		objectPW?.SetActive(value: true);
		yield return new WaitForSeconds(7f);
		StartCoroutine(ShowImageOThenLoadScene());
	}

	private IEnumerator SwitchCamera(Camera toCamera, float duration)
	{
		mainCamera.gameObject.SetActive(value: false);
		toCamera.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(duration);
		toCamera.gameObject.SetActive(value: false);
		mainCamera.gameObject.SetActive(value: true);
	}

	private IEnumerator ShowUI(GameObject uiElement, float duration)
	{
		uiElement?.SetActive(value: true);
		yield return new WaitForSeconds(duration);
		uiElement?.SetActive(value: false);
	}

	private IEnumerator ShowImageOThenLoadScene()
	{
		imageO?.SetActive(value: true);
		objectBlood?.SetActive(value: false);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Scene3");
	}

	private IEnumerator HideAfterDelay(GameObject obj, float delay)
	{
		yield return new WaitForSeconds(delay);
		obj?.SetActive(value: false);
	}

	private IEnumerator ActivatePWAndThenBLDSTR()
	{
		objectPW?.SetActive(value: true);
		mainCameraTimeline?.Play();
		yield return new WaitForSeconds(5f);
		objectBLDSTR?.SetActive(value: true);
	}
}
