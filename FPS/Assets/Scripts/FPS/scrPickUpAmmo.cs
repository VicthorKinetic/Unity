using UnityEngine;
using System.Collections;

public class scrPickUpAmmo : MonoBehaviour {

	public scrTotalAmmo TotalAmmo;
	public scrGameAudio GameAudio;
	public scrFPS_Controler Player;

	public float rotation;

	void Start()
	{
		TotalAmmo = GameObject.Find ("AmmoObject").GetComponent<scrTotalAmmo>();
		GameAudio = GameObject.Find ("GameAudio").GetComponent<scrGameAudio>();
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
				if (TotalAmmo.CurrentAmmo < 72) {
					if (TotalAmmo.LoadedAmmo == 0) {
						GameAudio.AmmoPickup.Play ();
						TotalAmmo.LoadedAmmo += 12;
						Destroy (this.gameObject);
					} else {
						GameAudio.AmmoPickup.Play ();
						TotalAmmo.CurrentAmmo += 12;
						Destroy (this.gameObject);
					}
				}
		} else {
		}
			
	}

	void Update()
	{
		if (TotalAmmo.CurrentAmmo > 72) {
			TotalAmmo.CurrentAmmo = 72;
		}
	}
}
