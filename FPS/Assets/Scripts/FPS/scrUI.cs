using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrUI : MonoBehaviour {

	public GameObject Crosshair;
	public GameObject Gun;
	public GameObject GameOver;
	public GameObject AmmoCounter;
	public GameObject HealthBar;
	public GameObject Health;
	public GameObject Camera;
	public GameObject Pause;
	public GameObject Resume;
	public GameObject AmountAmmo;
	public GameObject AmountGun;
	public GameObject CurrentGun;
	public GameObject TaserCharge;
	public GameObject Dash;
	public GameObject Options;
	public GameObject btnOptions;

	public scrGameAudio GameAudio;

	public bool paused = false;
	public bool optionActive = false;

	void Awake()
	{
		Time.timeScale = 1;
	}

	public void PauseGame()
	{
		Crosshair.SetActive (false);
		Pause.SetActive (true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;
		paused = true;
	}

	public void ResumeGame()
	{
		Crosshair.SetActive (true);
		Pause.SetActive (false);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
		paused = false;
	}

	public void Investigation()
	{
		SceneManager.LoadScene ("PointAndClick");
	}

	public void TitleScreen()
	{
		SceneManager.LoadScene ("TitleScreen");
	}

	public void FPS()
	{
		SceneManager.LoadScene ("FPS");	
	}

	public void Exploration()
	{
		SceneManager.LoadScene ("Exploration");	
	}

	public void Option()
	{
		if (!optionActive) {
			optionActive = true;
			Options.SetActive (true);
			btnOptions.GetComponent<Text> ().text = "Voltar";
		} else {
			optionActive = false;
			Options.SetActive (false);
			btnOptions.GetComponent<Text> ().text = "Opções";
		}
	}

}
