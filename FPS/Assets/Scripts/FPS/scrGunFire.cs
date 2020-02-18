using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrGunFire : MonoBehaviour {

	public scrTotalAmmo TotalAmmo;
	public scrGameAudio GameAudio;
	public scrGunDamage GunDamage;
	public scrUI UI;
	public scrFPS_Controler Player;
	public scrReload Reload;
	public scrGunMechanic GunMechanic;
	public scrTaserCooldown TaserCooldown;
	public scrEnemy Enemy;
	public GameObject MuzzleFlash;
	private GameObject BulletHole;
	public Vector3 aimDownSight;
	// x 0 y -0.22 z 0.51
	public Vector3 hipSight;
	// x 0.16 y -0.26 z 0.51
	public float aimSpeed = 3;
	public int Damage;
	public int DamageToNextRound;

	// Update is called once per frame
	void Start()
	{
		Enemy.EnemyHealth = 10;
		Damage = 0;
		DamageToNextRound = 10;
	}

	void Update ()
	{
		BulletHole = (GameObject)Resources.Load ("BulletHole");
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();

		if (Damage >= DamageToNextRound) {
			Damage = 0;
			DamageToNextRound += 25;
			Enemy.EnemyHealth += 10;
		}

		if (Player.HP > 0) {
			if (Player.weapon == 1) {
				if (!UI.paused) {
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
						if (TotalAmmo.LoadedAmmo > 0) {
							FireM1911 ();
							StartCoroutine (ReactivationM1911 ());
							StopCoroutine (ReactivationM1911 ());
							if (GunDamage.hit.collider.tag != "Enemy") {
								GameObject bhCLone = (GameObject)Instantiate (BulletHole, GunDamage.hit.point, Quaternion.LookRotation (GunDamage.hit.normal));
								GameObject.Destroy (bhCLone, 100f);
							}
							Damage += 1;
						}
					}
				} 
			}
		}
	}

	void FireM1911()
	{
		Reload.enabled = false;
		Player.M1911.GetComponent<scrGunFire>().enabled = false;
		MuzzleFlash.SetActive (true);
		StartCoroutine(MuzzleOff());
		StopCoroutine(MuzzleOff());
		TotalAmmo.LoadedAmmo--;
		GameAudio.GunFire.Play ();
		Player.M1911.GetComponent<Animation> ().Play ("GunFire");
	}

	IEnumerator ReactivationM1911()
	{
		yield return new WaitForSeconds (0.1f);
		Reload.enabled = true;
		Player.M1911.GetComponent<scrGunFire>().enabled = true;
		yield break;
	}

	IEnumerator MuzzleOff()
	{
		yield return new WaitForSeconds (0.17f);
		MuzzleFlash.SetActive (false);
		yield break;
	}
}