using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrPortal : MonoBehaviour {


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			SceneManager.LoadScene ("Boss");
		}
	}
}
