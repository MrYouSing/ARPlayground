using TouchScript.Gestures.TransformGestures.Base;
using UnityEngine;

namespace YouSingStudio.AR {
	public class GestureTransform
		:MonoBehaviour
	{
		#region Fields

		public Transform viewer;
		public Transform target;
		public TransformGestureBase gesture;
		public float moveScale=1.0f;
		public float rotateScale=360.0f;
		[System.NonSerialized]protected Pose m_Pose;
		[System.NonSerialized]protected Vector2 m_ScreenPoint;

		#endregion Fields

		#region Unity Messages

		protected virtual void Start() {
			if(viewer==null) {viewer=Camera.main.transform;}
			if(target==null) {target=transform;}
			if(gesture==null) {gesture=FindObjectOfType<TransformGestureBase>();}
			//
			if(gesture!=null) {
				gesture.TransformStarted+=OnTransformStarted;
				gesture.Transformed+=OnTransformed;
				gesture.TransformCompleted+=OnTransformCompleted;
			}
		}

		protected virtual void OnDestroy() {
			if(gesture!=null) {
				gesture.TransformStarted-=OnTransformStarted;
				gesture.Transformed-=OnTransformed;
				gesture.TransformCompleted-=OnTransformCompleted;
			}
		}

		#endregion Unity Messages

		#region Methods
		

		protected virtual void OnTransformStarted(object sender,System.EventArgs e) {
			if(target!=null) {
				m_Pose=new Pose(target.position,target.rotation);
				m_ScreenPoint=gesture.ScreenPosition;
			}
		}

		protected virtual void OnTransformed(object sender,System.EventArgs e) {
			if(target!=null) {
				Vector2 delta=(gesture.ScreenPosition-m_ScreenPoint)*(1.0f/Mathf.Min(Screen.width,Screen.height));
				Quaternion q=Quaternion.AngleAxis(viewer.rotation.eulerAngles.y,Vector3.up);
				if(gesture.NumPointers>=2) {
					if(delta.x*delta.x>=delta.y*delta.y) {
						target.rotation=m_Pose.rotation*Quaternion.AngleAxis(delta.x*rotateScale,Vector3.up);
					}else {
						target.position=m_Pose.position+q*new Vector3(0.0f,delta.y*moveScale,0.0f);
					}
				}else {
					target.position=m_Pose.position+q*new Vector3(delta.x*moveScale,0.0f,delta.y*moveScale);
				}
			}
		}

		protected virtual void OnTransformCompleted(object sender,System.EventArgs e) {
			if(target!=null) {
			}
		}

		#endregion Methods
	}
}
