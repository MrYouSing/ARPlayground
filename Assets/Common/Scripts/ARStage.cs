using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace YouSingStudio.AR {
	public class ARStage:MonoBehaviour {
		#region Nested Types

		public class Hook:MonoBehaviour {
			public ARStage stage;
			public Transform parent;
			public bool isPlane;
			//
			[System.NonSerialized]public Transform t;

			protected virtual void Start() {
				t=transform;
				t.SetParent(parent,false);
				/*
				t.localPosition=Vector3.zero;
				t.localRotation=Quaternion.identity;
				*/
				t.localScale=Vector3.one;
			}

			protected virtual void Update() {
				if(isPlane) {
					Vector3 pos=t.position;
					if(pos.y<stage.bottom.y) {
						stage.bottom=pos;
					}
				}
			}
		}

		#endregion Nested Types

		#region Fields

		public Transform root;
		public Transform viewer;

		public GameObject pointCloudGo;
		public ARPointCloudManager pointCloudManager;
		public GameObject planeGo;
		public ARPlaneManager planeManager;

		[System.NonSerialized]public Vector3 bottom=Vector3.up*float.MaxValue;
		[System.NonSerialized]protected int m_VisualizerMask;

		#endregion Fields

		#region Unity Messages

		protected virtual void Start() {
			root=transform;
			float f=root.localScale.x;
			root.localScale=Vector3.one*f;
			root.position=Vector3.zero;
			//
			if(viewer==null) {viewer=GetComponentInChildren<Camera>().transform;}
			SetupVisualizer(ref pointCloudGo,ref pointCloudManager,false);
			SetupVisualizer(ref planeGo,ref planeManager,true);
			//
			SetVisualizer(-1);
		}

		protected virtual void Update() {
			if(bottom.y<float.MaxValue) {
				Vector3 v=viewer.position;
				Vector3 v0=v-bottom;
				Vector3 v1=v-root.position;
				//
				v=root.position;
				v.y=(v0-v1).y;
				root.position=v;
				bottom.y=0.0f;
			}
		}

		#endregion Unity Messages

		#region Methods

		public virtual void SetupVisualizer<T>(ref GameObject go,ref T v,bool isPlane) where T:Component {
			if(go==null) {
				go=new GameObject();
				go.transform.SetParent(root,false);
			}
			if(v==null) {
				v=root.GetComponentInChildren<T>();
			}
			if(v!=null) {
				var m=v.GetType().GetMethod("GetPrefab",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
				if(m!=null) {
					GameObject prefab=m.Invoke(v,new object[0]) as GameObject;
					if(prefab!=null) {
						Hook hook=prefab.GetComponent<Hook>();
						if(hook==null) {hook=prefab.AddComponent<Hook>();}
						//
						hook.stage=this;
						hook.parent=go.transform;
						hook.isPlane=isPlane;
					}
				}
			}
		}

		public virtual void SetVisualizer(int mask) {
			m_VisualizerMask=mask;
			//
			if(pointCloudGo!=null) {pointCloudGo.SetActive((mask&0x1)!=0);}
			if(planeGo!=null) {planeGo.SetActive((mask&0x2)!=0);}
		}

		public virtual void ToggleVisualizer() {
			SetVisualizer(m_VisualizerMask==0?-1:0);
		}

		#endregion Methods
	}
}
