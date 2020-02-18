using UnityEngine;
using System.Collections;

public class scrPentagram : MonoBehaviour {

	public scrPentagramManager PentagramManager;
	public scrGameAudio GameAudio;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Destroy (this.gameObject);
			GameAudio.PentagramPickup.Play ();
			PentagramManager.collected++;
		}
	}
}
