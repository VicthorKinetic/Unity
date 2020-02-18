using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	#region Variables
	public static GameManager ins;

	public IVCanvas ivCanvas;

	public Node startingNode;

	public scrUI UI;

	[HideInInspector] 
	public CameraRig rig;

	public Node currentNode;
		
	#endregion

	#region Initializing
	void Awake()
	{
		ins = this;
		ivCanvas.gameObject.SetActive (false);
	}

	void Start()
	{
		startingNode.ArriveAtNode ();
	}
	#endregion

	#region Update - Zoom's out of current node back to it's location
	void Update()
	{
		if (Input.GetMouseButtonDown (1) && currentNode.GetComponent<Prop> () != null) 
		{
			if (ivCanvas.gameObject.activeInHierarchy) 
			{
				ivCanvas.Close ();
				return;
			}
			currentNode.GetComponent<Prop> ().loc.ArriveAtNode ();
		}
	}
	#endregion
}