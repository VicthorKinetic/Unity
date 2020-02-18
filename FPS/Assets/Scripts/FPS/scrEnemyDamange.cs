using UnityEngine;
using System.Collections;

public class scrEnemyDamange : MonoBehaviour {

	public scrFPS_Controler Player;
	public scrGameAudio GameAudio;
	public scrUI UI;

	void Start()
	{
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();
		GameAudio = GameObject.Find ("GameAudio").GetComponent<scrGameAudio>();
		UI = GameObject.Find ("UI").GetComponent<scrUI> ();

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			if (Player.HP > 0) {
				Player.HP -= 10;
				GameAudio.DamageTaken.Play ();
				UI.Health.transform.localScale = new Vector3 ((Player.HP / Player.TotalHP) * 2, 2f, 1f);
			}
		}
	}

}
