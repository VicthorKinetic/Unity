using UnityEngine;
using System.Collections;

public class scrSpawnerAmmo : MonoBehaviour {

	public float time, spawnTime;
	public GameObject ammo;
	public float posicaoY;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= spawnTime) {
			Instantiate (ammo, new Vector3 (Random.Range (-30, 30), posicaoY, Random.Range (-30, 30)), Quaternion.identity);
			time = 0;
		}
	}
}

