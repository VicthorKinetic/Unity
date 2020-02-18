using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IVCanvas : MonoBehaviour {
	
	#region Variables
	public Image imageHolder;
	#endregion

	#region Activates the Canvas when interacting
	public void Activate(Sprite pic)
	{
		GameManager.ins.currentNode.SetReachableNodes (false);
		GameManager.ins.currentNode.col.enabled = false;
		gameObject.SetActive (true);
		imageHolder.sprite = pic;
	}
	#endregion

	#region Deactivates the Canvas when interacting
	public void Close()
	{
		GameManager.ins.currentNode.SetReachableNodes (true);
		GameManager.ins.currentNode.col.enabled = true;
		gameObject.SetActive (false);
		imageHolder.sprite = null;
	}
	#endregion
}
