using UnityEngine;
using System.Collections;

public class scrGunDamage : MonoBehaviour {
	
	//public Transform particle;
	public int damage = 5;
	public float targetDistance;
	public float allowedRange1 = 50;
	public float allowedRange2 = 5;
	public scrTotalAmmo TotalAmmo;
	public scrFPS_Controler Player;
	public scrUI UI;
	public scrTaserCooldown TaserCooldown;
	public scrEnemy Enemy;
	public RaycastHit hit;

	// Update is called once per frame
	void Update () {
		if (!UI.paused) {
			if (Player.weapon == 1) {
				
				Ray ray = new Ray (transform.position, transform.rotation * Vector3.forward);

				Debug.DrawRay (transform.position, (transform.rotation * Vector3.forward) * allowedRange1);

				if (Input.GetButtonDown ("Fire")) {
					if (TotalAmmo.LoadedAmmo > 0) {
						if (Physics.Raycast (ray, out hit, allowedRange1)) {
							hit.transform.SendMessage ("DeductHealth", damage, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			} else {
				Ray ray = new Ray (transform.position, transform.rotation * Vector3.forward);

				Debug.DrawRay (transform.position, (transform.rotation * Vector3.forward) * allowedRange2);

				if (Input.GetButtonDown ("Fire")) {
					if (TaserCooldown.TaserCharge == 100) {
						if (Physics.Raycast (ray, out hit, allowedRange2)) {
							hit.transform.SendMessage ("TaseEnemy", SendMessageOptions.DontRequireReceiver);
						}
					}
				}

			}
		}
	}
}
