using UnityEngine;
using System.Collections;

public class scrFPS_Controler : MonoBehaviour {

	public GameObject mainCamera;
	public float sensitivityX;
	public float sensitivityY;
	public float moveSpeed;
	public float jumpSpeed;
	public float damping;
	private CharacterController charCtrl;
	private Vector3 acc;
	public bool justOnce = false;

	public float TotalHP = 100;
	public float HP = 100;

	public scrGameAudio GameAudio;
	public scrUI UI;
	public scrTotalAmmo TotalAmmo;

	public GameObject M1911;
	public GameObject Taser;
	public int weapon = 1;

	// Use this for initialization
	void Start () {
		
		GameAudio = GameObject.Find ("GameAudio").GetComponent<scrGameAudio>();
		UI = GameObject.Find ("UI").GetComponent<scrUI> ();

		HP = TotalHP;
		Cursor.lockState = CursorLockMode.Locked;
		charCtrl = GetComponent <CharacterController> ();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		float mouseX = Input.GetAxis ("Mouse X");
		float mouseY = Input.GetAxis ("Mouse Y");

//		transform.Rotate (0, mouseX * sensitivityX * Time.deltaTime, 0);
//		mainCamera.transform.Rotate (-mouseY * sensitivityY * Time.deltaTime, 0, 0);

		//Movimento do mouse transformado em rotacao
		Quaternion rotX = Quaternion.Euler (-mouseY * sensitivityY * Time.deltaTime, 0, 0);
		Quaternion rotY = Quaternion.Euler (0, mouseX * sensitivityX * Time.deltaTime, 0);

		//Rotacao calculada do personagem
		Quaternion targetRot_char = transform.localRotation * rotY;
		Quaternion targetRot_cam = mainCamera.transform.localRotation * rotX;

		//Aplicação da rotação
		transform.localRotation = targetRot_char;
		mainCamera.transform.localRotation = ClampRotationX (targetRot_cam);

		//Movimentação
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveZ = Input.GetAxisRaw("Vertical");
		float jump = Input.GetAxis ("Jump");
		float running = Input.GetAxisRaw ("Run");

		if (running > 0) {
			moveSpeed = 10f;
		} else {
			moveSpeed = 5f;
		}
		Vector3 moveDirection = new Vector3(moveX,0,moveZ);
		moveDirection.Normalize ();
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= moveSpeed;

		//acc = Vector3.Lerp(acc, moveDirection, damping);
		acc.x = Mathf.Lerp (acc.x, moveDirection.x, damping);
		acc.z = Mathf.Lerp (acc.z, moveDirection.z, damping);

		acc.y = charCtrl.isGrounded ? -0.1f : acc.y - 9.8f * Time.deltaTime;
		acc.y += charCtrl.isGrounded ? jump * jumpSpeed : 0;
		damping = charCtrl.isGrounded ? 0.1f : 0.01f;

		//charCtrl.SimpleMove(transform.TransformDirection(moveDirection * 6));
		charCtrl.Move(acc * Time.deltaTime);

		if (!justOnce) 
		{
			if (HP == 0) 
			{
				GameOver ();
				this.GetComponent<scrFPS_Controler>().enabled = false;
				GameObject.FindGameObjectWithTag ("MainCamera").SetActive (false);
				justOnce = true;
			}
		}

		if(Input.GetButtonDown("ESC"))
		{
			if (!UI.paused)
				UI.PauseGame ();
				else
				UI.ResumeGame ();
		}
	}

	void GameOver()
	{
		int Song = Random.Range (0, 4);
		Cursor.visible = true;
		GameAudio.HordeMode.Stop ();
		UI.GameOver.SetActive (true);
		UI.Camera.SetActive (true);
		UI.AmmoCounter.SetActive (false);
		UI.Crosshair.SetActive (false);
		UI.HealthBar.SetActive (false);
		StartCoroutine (FadeInMenu());
		if (Song == 0)
			GameAudio.Dead.Play ();
		else if (Song == 1)
			GameAudio.Roundabout.Play ();
		else if (Song == 2)
			GameAudio.Rain.Play ();
		else if (Song == 3)
			GameAudio.SoundOfSilence.Play ();
	}

	IEnumerator FadeInMenu()
	{
		yield return new WaitForSeconds (3f);
		UI.Pause.SetActive (true);
		UI.Resume.SetActive (false);
		yield break;
	}

	Quaternion ClampRotationX(Quaternion q){
	
	//divisões para normalizar a rotação
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f; //trava o "w" em 1

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x); // conversão de radianos para angulos

		angleX = Mathf.Clamp (angleX, -90f, 90f); // travar a rotação conforme minimo e maximo

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX); //reconverter angulos para radianos

		return q;

	}

}
