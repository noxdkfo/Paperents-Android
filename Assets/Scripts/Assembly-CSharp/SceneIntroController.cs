using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SceneIntroController : MonoBehaviour
{
	public Image image1;

	public Image image2;

	public Image image3;

	public Image image4;

	public Text text1;

	public AudioSource audio1;

	public AudioSource audio2;

	public AudioSource audio3;

	public PlayableDirector doorTimeline;

	public GameObject objectDoor;

	public GameObject objectDoorDecoy;

	public AudioSource audioClickLight;

	private Camera mainCam;

	public GameObject objectMOM;

	public GameObject objectL1;

	public GameObject objectL2;

	public GameObject objectWHO;

	public GameObject lamp;

	private bool dPressed;

	private bool lookingAtWHO;

	private bool momTriggered;

	private void Start()
	{
		mainCam = Camera.main;
		StartCoroutine(SceneFlow());
	}

	private IEnumerator SceneFlow()
	{
		image1.gameObject.SetActive(value: true);
		audio1.Play();
		yield return new WaitForSeconds(10f);
		image1.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(10f);
		doorTimeline.gameObject.SetActive(value: true);
		doorTimeline.Play();
		audio2.Play();
		yield return new WaitForSeconds(10f);
		text1.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		if (!dPressed && Input.GetKeyDown(KeyCode.E))
		{
			dPressed = true;
			text1.gameObject.SetActive(value: false);
			StartCoroutine(HandleDActions());
		}
		if (Physics.Raycast(mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out var hitInfo))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			if (!momTriggered && gameObject == objectMOM && objectMOM.activeInHierarchy)
			{
				momTriggered = true;
				image4.gameObject.SetActive(value: true);
				objectWHO.SetActive(value: true);
				StartCoroutine(HideAfterSeconds(image4.gameObject, 3f));
			}
			if (gameObject == objectWHO && objectWHO.activeInHierarchy && !lookingAtWHO)
			{
				lookingAtWHO = true;
				objectMOM.SetActive(value: false);
				objectL2.SetActive(value: false);
				lamp.SetActive(value: true);
			}
		}
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hitInfo2) && hitInfo2.collider.gameObject == objectL2 && !objectL1.activeInHierarchy)
		{
			objectL1.SetActive(value: true);
			audioClickLight.Play();
		}
	}

	private IEnumerator HandleDActions()
	{
		yield return new WaitForSeconds(1f);
		image2.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		image2.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		image3.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		image3.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		StartCoroutine(FadeInImage(image1, 1f));
		audio3.Play();
		yield return new WaitForSeconds(9f);
		objectMOM.SetActive(value: true);
		objectL1.SetActive(value: false);
		objectDoor.SetActive(value: false);
		objectDoorDecoy.SetActive(value: true);
		yield return new WaitForSeconds(6f);
		image1.gameObject.SetActive(value: false);
	}

	private void OnMouseDown()
	{
		if (objectMOM.activeInHierarchy)
		{
			image4.gameObject.SetActive(value: true);
			objectWHO.SetActive(value: true);
			StartCoroutine(HideAfterSeconds(image4.gameObject, 2f));
		}
	}

	private IEnumerator HideAfterSeconds(GameObject go, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		go.SetActive(value: false);
	}

	private IEnumerator FadeInImage(Image img, float duration)
	{
		img.gameObject.SetActive(value: true);
		CanvasGroup cg = img.GetComponent<CanvasGroup>();
		if (cg == null)
		{
			cg = img.gameObject.AddComponent<CanvasGroup>();
		}
		cg.alpha = 0f;
		float timer = 0f;
		while (timer < duration)
		{
			cg.alpha = timer / duration;
			timer += Time.deltaTime;
			yield return null;
		}
		cg.alpha = 1f;
		yield return new WaitForSeconds(8f);
	}
}
