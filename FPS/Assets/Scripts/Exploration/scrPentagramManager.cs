using UnityEngine;
using System.Collections;

public class scrPentagramManager : MonoBehaviour {

	public GameObject Portal;

	public int collected = 0;

	// Update is called once per frame
	void Update () {
	
		if (collected == 4) {
			Portal.gameObject.SetActive (true);
		}

	}
}
