using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class VHSPostEffect : MonoBehaviour
{
	public Material vhsMaterial;

	[Range(0f, 2f)]
	public float blurAmount = 0.5f;

	[Range(0f, 5f)]
	public float sharpness = 1f;

	[Range(0f, 1f)]
	public float glitchStrength = 0.1f;

	[Range(0f, 3f)]
	public float contrast = 1f;

	[Range(0f, 3f)]
	public float saturation = 1f;

	[Range(-180f, 180f)]
	public float hueShift;

	[Range(0f, 1f)]
	public float scanlineStrength = 0.2f;

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (vhsMaterial != null)
		{
			vhsMaterial.SetFloat("_BlurAmount", blurAmount);
			vhsMaterial.SetFloat("_Sharpness", sharpness);
			vhsMaterial.SetFloat("_GlitchStrength", glitchStrength);
			vhsMaterial.SetFloat("_Contrast", contrast);
			vhsMaterial.SetFloat("_Saturation", saturation);
			vhsMaterial.SetFloat("_HueShift", hueShift);
			vhsMaterial.SetFloat("_ScanlineStrength", scanlineStrength);
			Graphics.Blit(src, dest, vhsMaterial);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}
}
