using System;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
public class DownSample : UnityStandardAssets.ImageEffects.PostEffectsBase
{

	public int widthResolution = 360;
	public int heightResolution = 240;
    public Shader DownSampleShader = null;
    private Material downSampleMaterial = null;


    public override bool CheckResources () {
        CheckSupport (false);

		downSampleMaterial = CheckShaderAndCreateMaterial (DownSampleShader, downSampleMaterial);

    	if (!isSupported)
            ReportAutoDisable ();
        return isSupported;
    }

    public void OnDisable () {
		if (downSampleMaterial)
			DestroyImmediate (downSampleMaterial);
	}

    public void OnRenderImage (RenderTexture source, RenderTexture destination) {
        if (CheckResources() == false) {
            Graphics.Blit (source, destination);
            return;
        }

       
		source.filterMode = FilterMode.Point;

		int rtW = widthResolution;
		int rtH = heightResolution;

        // downsample
        RenderTexture rt = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);

		rt.filterMode = FilterMode.Point;
		Graphics.Blit (source, rt, downSampleMaterial, 0);
        Graphics.Blit (rt, destination);

        RenderTexture.ReleaseTemporary (rt);
    }
}

