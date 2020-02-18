using UnityEngine;
using System.Collections;

public class scrSpawnerEnemy : MonoBehaviour {

	public float time, spawnTime;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	private int whichEnemy;
	public float posicaoY;

	// Use this for initialization
	void Start () 
	{
		time = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		whichEnemy = Random.Range (0, 3);

		time += Time.deltaTime;
		if (time >= spawnTime) {
			if (whichEnemy == 0) {
				Instantiate (enemy1, new Vector3 (Random.Range (-30, 30), posicaoY, Random.Range (-30, 30)), Quaternion.identity);
				time = 0;
			} else {
				if (whichEnemy == 1) {
					Instantiate (enemy2, new Vector3 (Random.Range (-30, 30), posicaoY, Random.Range (-30, 30)), Quaternion.identity);
					time = 0;
				} else {
					if (whichEnemy == 2) {
						Instantiate (enemy3, new Vector3 (Random.Range (-30, 30), posicaoY, Random.Range (-30, 30)), Quaternion.identity);
						time = 0;
					} 
				}
			}
		}
	}
}
