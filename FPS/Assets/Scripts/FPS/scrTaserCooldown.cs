using UnityEngine;
using System.Collections;

public class scrTaserCooldown : MonoBehaviour {

	public scrFPS_Controler Player;

	public bool TaserUsed = false;
	public float TaserCharge = 100;
	public float TaserCooldownTime = 1;

	void Update()
	{
		if (TaserUsed) {
			if (Player.weapon == 2) {
				Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 65f, 0.1f);
			}
		}
	}

	public void TaserRechargeStart()
	{
		if (!TaserUsed) {
			TaserUsed = true;
			StartCoroutine (TaserRechargeCoroutine ());
		}
	}

	public void TaserRechargeFinish()
	{
		StopCoroutine (TaserRechargeCoroutine ());
	}

	IEnumerator TaserRechargeCoroutine()
	{
		TaserCharge = -1;
		do 
		{
			TaserCharge += TaserCooldownTime;
			yield return new WaitForSeconds (0.1f);
		} while(TaserCharge != 100);
		TaserUsed = false;
		Player.Taser.GetComponent<Animation> ().Play ("TaserRecharged");
	}
}
