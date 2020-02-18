using UnityEngine;
using System.Collections;

public class ImageViewer : Interactable {
	
	#region Variables
	public Sprite pic;
	#endregion

	#region Overriding the interaction to show 
	public override void Interact ()
	{
		GameManager.ins.ivCanvas.Activate (pic);
	}
	#endregion

}
