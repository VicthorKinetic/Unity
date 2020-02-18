using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrTotalAmmo : MonoBehaviour {

	public int LoadedAmmo;
	public int InternalLoaded;

	public int CurrentAmmo;
	public int InternalAmmo;

	public scrFPS_Controler Player;

	public scrTaserCooldown TaserCooldown;

	public scrUI UI;

	// Update is called once per frame
	void Update () {
		if (Player.weapon == 1) {
			InternalAmmo = CurrentAmmo;
			UI.AmountAmmo.GetComponent<Text> ().text = "" + InternalAmmo;

			InternalLoaded = LoadedAmmo;
			UI.AmountGun.GetComponent<Text> ().text = "" + InternalLoaded;
		} else {
			UI.TaserCharge.GetComponent<Text> ().text = Mathf.Round(TaserCooldown.TaserCharge) + "%";

		}
	}
}
