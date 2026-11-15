using UnityEngine;

namespace DigitalRuby.RainMaker
{
	public class DemoScriptStartRainOnSpaceBar : MonoBehaviour
	{
		public BaseRainScript RainScript;

		private void Start()
		{
			if (!(RainScript == null))
			{
				RainScript.EnableWind = false;
			}
		}

		private void Update()
		{
			if (!(RainScript == null) && Input.GetKeyDown(KeyCode.Space))
			{
				RainScript.RainIntensity = ((RainScript.RainIntensity == 0f) ? 1f : 0f);
				RainScript.EnableWind = !RainScript.EnableWind;
			}
		}
	}
}
