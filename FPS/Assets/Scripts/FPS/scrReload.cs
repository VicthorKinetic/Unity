using UnityEngine;
using System.Collections;

public class scrReload : MonoBehaviour {

	public scrFPS_Controler Player;
	public GameObject GunMechanics;
	public scrTotalAmmo TotalAmmo;
	public scrGameAudio GameAudio;
	public GameObject M1911;
	public scrGunFire GunFire;
	public scrGunDamage GunDamage;
	public scrUI UI;
	public bool ReloadActive = false;
	
	// Update is called once per frame
	void Update () {
		if (!UI.paused) {
			if (Input.GetButtonDown ("Fire")) {
				if (TotalAmmo.LoadedAmmo == 0) {
					if (TotalAmmo.CurrentAmmo >= 12) {
						ReloadAction ();
						StartCoroutine (Reactivation());
						TotalAmmo.CurrentAmmo -= 12;
						TotalAmmo.LoadedAmmo = 12;
					} else {
						if (TotalAmmo.CurrentAmmo > 0) {
							ReloadAction ();	
							StartCoroutine (Reactivation());
							TotalAmmo.LoadedAmmo = TotalAmmo.CurrentAmmo;
							TotalAmmo.CurrentAmmo = 0;
						}
					}
				}
			}
			if ((Input.GetKeyDown (KeyCode.R)) && (TotalAmmo.LoadedAmmo != 12)) {
				if (TotalAmmo.CurrentAmmo > 0) {
					ReloadAction ();
					TotalAmmo.CurrentAmmo += TotalAmmo.LoadedAmmo;
					TotalAmmo.CurrentAmmo -= 12;
					TotalAmmo.LoadedAmmo = 12;
					if (TotalAmmo.CurrentAmmo < 0) {
						ReloadAction ();
						TotalAmmo.LoadedAmmo += TotalAmmo.CurrentAmmo;
						TotalAmmo.CurrentAmmo = 0;
					}
				}

				StartCoroutine (Reactivation());
			}
			if (ReloadActive) {
				if (Player.weapon == 1) {
					Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 65f, 0.1f);
				}
			}
			StopCoroutine (Reactivation());
		}
	}

	void ReloadAction()
	{
		ReloadActive = true;
		GunFire.enabled = false;
		GunMechanics.SetActive (false);
		GunDamage.enabled = false;
		UI.Crosshair.SetActive (false);
		GameAudio.Reload.Play ();
		M1911.GetComponent<Animation> ().Play ("Reload");
	}

	IEnumerator Reactivation()
	{
		yield return new WaitForSeconds (2f);
		GunFire.enabled = true;
		GunMechanics.SetActive (true);
		GunDamage.enabled = true;
		UI.Crosshair.SetActive (true);
		ReloadActive = false;
		yield break;
	}
}
	