using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public abstract class Node : MonoBehaviour 
{
	#region Variables
	public Transform cameraPosition;

	public List<Node> reachableNodes = new List<Node>();

	[HideInInspector]
	public Collider col;
	#endregion

	#region Calling function when clicking node
	void OnMouseDown()
	{
		ArriveAtNode();
	}
	#endregion


	#region Initializing
	// Use this for initialization
	void Awake() 
	{
		col = GetComponent<Collider>();
		col.enabled = false;
	}
	#endregion

	#region Action for when click on node is registered
	public virtual void ArriveAtNode()
	{
		if (GameManager.ins.currentNode != null)
			GameManager.ins.currentNode.LeaveNode ();

		GameManager.ins.currentNode = this;

		//Camera.main.transform.position = cameraPosition.position;
		//Camera.main.transform.rotation = cameraPosition.rotation;

		GameManager.ins.rig.AllignTo(cameraPosition);

		if (col != null)
			col.enabled = false;

		SetReachableNodes (true);

	}
	#endregion

	#region Action for when leaving a previously clicked node
	public virtual void LeaveNode()
	{
		SetReachableNodes (false);
	}
	#endregion

	#region Changes the node's collider to be set to true or false
	public void SetReachableNodes(bool set)
	{
		foreach (Node node in reachableNodes) 
		{
			if (node.col != null)
				node.col.enabled = set;
		}
	}
	#endregion
}
