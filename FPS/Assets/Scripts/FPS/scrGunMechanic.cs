using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrGunMechanic : MonoBehaviour {

	public scrTotalAmmo TotalAmmo;
	public scrGameAudio GameAudio;
	public scrGunDamage GunDamage;
	public scrUI UI;
	public scrFPS_Controler Player;
	public scrTaserCooldown TaserCooldown;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();
	}
	
	// Update is called once per frame
	void Update () {
		SwitchWeapons ();

		if (TaserCooldown.TaserCharge == 100) {
			Player.Taser.GetComponent<scrTaserFire> ().enabled = true;
		}
	}

	public void SwitchWeapons()
	{
		float gunSwitch = Input.GetAxisRaw ("ChangeWeapons");

		if ((gunSwitch > 0) || (gunSwitch < 0)) {
			if (Player.weapon == 1) {
				Player.weapon = 2;
				Player.M1911.SetActive (false);
				Player.Taser.SetActive (true);
				UI.AmountAmmo.SetActive (false);
				UI.AmountGun.SetActive (false);
				UI.Dash.SetActive (false);
				UI.TaserCharge.SetActive (true);
				UI.CurrentGun.GetComponent<Text> ().text = "Taser";

			} else {
				Player.weapon = 1;
				Player.M1911.SetActive (true);
				Player.Taser.SetActive (false);
				UI.AmountAmmo.SetActive (true);
				UI.AmountGun.SetActive (true);
				UI.Dash.SetActive (true);
				UI.TaserCharge.SetActive (false);
				UI.CurrentGun.GetComponent<Text> ().text = "M1911";
			}
		}
	}
}
