using System;
using UnityEngine;


    [ExecuteInEditMode]
    [RequireComponent (typeof(Camera))]
public class DownSample : UnityStandardAssets.ImageEffects.PostEffectsBase
    {

        [Range(0, 5)]
        public int downsample = 1;




 
        public Shader blurShader = null;
        private Material blurMaterial = null;


        public override bool CheckResources () {
            CheckSupport (false);

            blurMaterial = CheckShaderAndCreateMaterial (blurShader, blurMaterial);

            if (!isSupported)
                ReportAutoDisable ();
            return isSupported;
        }

        public void OnDisable () {
            if (blurMaterial)
                DestroyImmediate (blurMaterial);
        }

        public void OnRenderImage (RenderTexture source, RenderTexture destination) {
            if (CheckResources() == false) {
                Graphics.Blit (source, destination);
                return;
            }

           
			source.filterMode = FilterMode.Point;

            int rtW = source.width >> downsample;
            int rtH = source.height >> downsample;

            // downsample
            RenderTexture rt = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);

			rt.filterMode = FilterMode.Point;
            Graphics.Blit (source, rt, blurMaterial, 0);

     
            Graphics.Blit (rt, destination);

            RenderTexture.ReleaseTemporary (rt);
        }
    }

