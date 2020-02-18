using UnityEngine;
using System.Collections;

public class scrEnemy : MonoBehaviour {

	public int EnemyHealth;
	public bool dead = false;

	// Update is called once per frame
	void LateUpdate () {

		if (EnemyHealth <= 0) {
			dead = true;
		}
	}

	void DeductHealth (int damage)
	{
		EnemyHealth -= damage;
	}

	void TaseEnemy ()
	{
		EnemyHealth -= EnemyHealth;
	}
}
