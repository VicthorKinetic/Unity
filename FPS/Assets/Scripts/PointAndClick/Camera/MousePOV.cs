using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraRig))]
public class MousePOV : MonoBehaviour {

	#region Variables
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;
	public bool lockCursor = true;
	private Quaternion yAxis;
	private Quaternion xAxis;
	private CameraRig rig;
	#endregion

	#region Initializing
	void Start()
	{
		rig = GetComponent<CameraRig>();
	}
	#endregion

	#region Update - Checks if mouse is moving
	void Update()
	{
		if (Input.GetMouseButton (0) && (Input.GetAxis ("Mouse X") != 0 || (Input.GetAxis ("Mouse Y") != 0))) 
		{
			if (GameManager.ins.ivCanvas.gameObject.activeInHierarchy)
				return;
			yAxis = rig.y_axis.localRotation;
			xAxis = rig.x_axis.localRotation;
			LookRotation ();
		}
	}
	#endregion

	#region Determines the amount of rotation using Axis via Mouse Movement
	public void LookRotation()
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		yAxis *= Quaternion.Euler (0f, yRot, 0f);
		xAxis *= Quaternion.Euler (-xRot, 0f, 0f);

		if(clampVerticalRotation)
			xAxis = ClampRotationAroundXAxis (xAxis);

		if(smooth)
		{
			rig.y_axis.localRotation = Quaternion.Slerp (rig.y_axis.localRotation, yAxis,
				smoothTime * Time.deltaTime);
			rig.x_axis.localRotation = Quaternion.Slerp (rig.x_axis.localRotation, xAxis,
				smoothTime * Time.deltaTime);
		}
		else
		{
			rig.y_axis.localRotation = yAxis;
			rig.x_axis.localRotation = xAxis;
		}
	}
	#endregion

	#region 
	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
	#endregion
}