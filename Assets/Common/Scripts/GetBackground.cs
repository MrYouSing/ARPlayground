using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace YouSingStudio.AR {
	public class GetBackground:MonoBehaviour {
		#region Fields

		public ARCameraManager arManager;
		public ARCameraBackground arBackground;
		public Camera cameraBackground;
		public ARFoundationBackgroundRenderer rendererBackground;
		public Camera cameraFinal;
		public RenderTexture renderTexture;
		public Material material;
		public Material[] materials;

		public RawImage rawImage;

		#endregion Fields

		#region Unity Messages

		protected virtual void Awake() {
			if(arManager==null) {
				arManager=GetComponent<ARCameraManager>();
			}
			if(arBackground==null) {
				arBackground=FindObjectOfType<ARCameraBackground>();
			}
			if(cameraFinal==null) {
			if(arManager!=null) {
				cameraFinal=arManager.GetComponent<Camera>();
			}}
		}

		protected virtual void OnEnable() {
			arManager.frameReceived+=OnCameraFrameReceived;
		}

		protected virtual void OnDisable() {
			arManager.frameReceived-=OnCameraFrameReceived;
		}


		protected unsafe virtual void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs) {
			if(true) {
				int w=Screen.width,h=Screen.height;
				if(renderTexture==null||renderTexture.width!=w||renderTexture.height!=h) {
					if(renderTexture!=null) {RenderTexture.ReleaseTemporary(renderTexture);}
					renderTexture=RenderTexture.GetTemporary(w,h,0,RenderTextureFormat.ARGB32);
				}
				cameraBackground.targetTexture=renderTexture;
				if(eventArgs.projectionMatrix.HasValue) {
					cameraBackground.projectionMatrix=eventArgs.projectionMatrix.Value;
				}
				if(rendererBackground==null) {
					rendererBackground=new ARFoundationBackgroundRenderer();
					rendererBackground.mode=ARRenderMode.MaterialAsBackground;
					rendererBackground.backgroundMaterial=arBackground.material;
					rendererBackground.camera=cameraBackground;
				}
				//
				Texture texture=renderTexture;
				if(true) {
					Material mat;
					for(int i=0,imax=materials?.Length??0;i<imax;++i) {
						mat=materials[i];
						if(mat!=null) {
							mat.mainTexture=texture;
						}
					}
					//
					if(rawImage!=null) {
						rawImage.texture=texture;
					}
				}
			}
		}

		#endregion Unity Messages
	}
}
