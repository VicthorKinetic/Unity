using UnityEngine;
using System.Collections;

public class Prop : Node {
	#region Variables
	Interactable inter;

	public Location loc;
	#endregion

	#region Initializing
	void Start()
	{
		inter = GetComponent<Interactable> ();
	}
	#endregion

	#region Overriding the function ArriveAtNode
	public override void ArriveAtNode ()
	{
		if (inter != null && inter.enabled) 
		{
			inter.Interact ();
			return;
		}
			
			base.ArriveAtNode ();

			if (inter != null) {
				col.enabled = true;
				inter.enabled = true;
			}
		}
	#endregion

	#region Overriding the function LeaveNode
	public override void LeaveNode ()
	{
		base.LeaveNode ();

		if (inter != null) 
		{
			inter.enabled = false;
		}
	}
	#endregion
}
