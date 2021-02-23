using UnityEngine;

public class ManomotionScript:MonoBehaviour {
	public Transform target;

	/*
	protected virtual void Start() {
		ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;
	}

	protected virtual void HandleManoMotionFrameUpdated() {
		GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
		TrackingInfo tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
		Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;
		//
		Vector3 position = ManoUtils.Instance.CalculateWorldPosition(tracking.palm_center,tracking.depth_estimation);
		target.position=position;
	}
	*/

}
