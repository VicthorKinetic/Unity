using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrTaserFire : MonoBehaviour {

	public scrTotalAmmo TotalAmmo;
	public scrGameAudio GameAudio;
	public scrGunDamage GunDamage;
	public scrUI UI;
	public scrFPS_Controler Player;
	public scrTaserCooldown TaserCooldown;
	public scrGunMechanic GunMechanic;
	public GameObject TaserFlash;
	private GameObject BulletHole;
	public Vector3 aimDownSight;
	// x 0 y -0.22 z 0.51
	public Vector3 hipSight;
	// x 0.16 y -0.26 z 0.51
	public float aimSpeed = 3;

	// Update is called once per frame
	void Update () 
	{
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();
		if (Player.HP > 0) {
			if (Player.weapon == 2) {
				if (!UI.paused) {

					if (TaserCooldown.TaserCharge == 100) {
						Player.Taser.GetComponent<scrTaserFire>().enabled = true;
					}

					if (Input.GetButton ("Aim")) {
						UI.Crosshair.SetActive (false);
						Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 45f, 0.1f);
						transform.localPosition = Vector3.Slerp (transform.localPosition, aimDownSight, aimSpeed * Time.deltaTime);
					} else {
						UI.Crosshair.SetActive (true);
						Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 65f, 0.1f);
						transform.localPosition = Vector3.Slerp (transform.localPosition, hipSight, aimSpeed * Time.deltaTime);
					}
					if (Input.GetButtonDown ("Fire")) {
						if (TaserCooldown.TaserCharge > 0) {
							FireTaser ();
							StartCoroutine (ReactivationTaser());
							StopCoroutine (ReactivationTaser());
							if (TaserCooldown.TaserCharge == 100) {
								TaserCooldown.TaserRechargeFinish ();
							}
						}
					}
				}
			}
		}
	}


	void FireTaser()
	{
		Player.Taser.GetComponent<scrTaserFire>().enabled = false;
		TaserFlash.SetActive (true);
		TaserCooldown.TaserRechargeStart ();
		StartCoroutine(TaserFlashOff());
		StopCoroutine(TaserFlashOff());
		GameAudio.TaserFire.Play ();
		Player.Taser.GetComponent<Animation> ().Play ("TaserFire");
	}

	IEnumerator ReactivationTaser()
	{
		yield return new WaitForSeconds (11.5f);
		Player.Taser.GetComponent<scrTaserFire>().enabled = true;
		yield break;
	}

	IEnumerator TaserFlashOff()
	{
		yield return new WaitForSeconds (0.17f);
		TaserFlash.SetActive (false);
		yield break;
	}
}