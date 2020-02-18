using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraRig : MonoBehaviour {

	#region Variables
	public Transform y_axis;
	public Transform x_axis;
	public float moveTime = 0.75f;
	#endregion

	#region Does the smooth animation when clicking on a node
	public void AllignTo(Transform target)
	{
		Sequence seq = DOTween.Sequence ();
		seq.Append (y_axis.DOMove (target.position, moveTime));
		seq.Join (y_axis.DORotate(new Vector3(0f, target.rotation.eulerAngles.y, 0f), moveTime));
		seq.Join (x_axis.DOLocalRotate(new Vector3(target.rotation.eulerAngles.x, 0f, 0f), moveTime));
	}
	#endregion
}
