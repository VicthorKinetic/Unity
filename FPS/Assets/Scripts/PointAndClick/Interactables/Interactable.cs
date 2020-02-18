using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Prop))]
public abstract class Interactable : MonoBehaviour {

	#region Initialization
	// Use this for initialization
	void Start () {
		this.enabled = false;
	}
	#endregion

	#region Interacts with an object
	public virtual void Interact()
	{
		Debug.Log ("Interacted with " + name);
	}
	#endregion
}
