using UnityEngine;
using System.Collections;

public class FPS_Controller : MonoBehaviour {

	public GameObject cam;
	public float sensitivity, moveSpeed, jumpSpeed, damping, airDamping;

	private CharacterController charCtrl;

	private Vector3 acc;

	private GameObject BulletHole, weapon;

	private Animator weaponAnimator;

	// Use this for initialization
	void Start () {

		BulletHole = (GameObject)Resources.Load ("BulletHole",typeof(GameObject));

		weapon = GameObject.Find ("weapon");

		weaponAnimator = weapon.GetComponent<Animator> ();

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.lockState = CursorLockMode.Locked;

		charCtrl = GetComponent<CharacterController> ();
		acc = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
//		this.transform.Rotate (0, Input.GetAxis("Mouse X") * sensitivity, 0); // Mouse X controla rotação Y do jogador
//		cam.transform.Rotate (-Input.GetAxis ("Mouse Y") * sensitivity, 0, 0); // Mouse Y controla rotação X da camera

		#region Input

		float moveHor = Input.GetAxisRaw("Horizontal");
		float moveVer = Input.GetAxisRaw("Vertical");
		float jump = Input.GetAxis ("Jump");
		float attack1 = Input.GetAxis ("attack1");
		float attack2 = Input.GetAxis ("attack2");

		#endregion

		#region Camera

		Quaternion rotX = Quaternion.Euler (-Input.GetAxis ("Mouse Y") * sensitivity, 0, 0); // Nova rotação com base no movimento do mouse
		Quaternion rotY = Quaternion.Euler (0, Input.GetAxis ("Mouse X") * sensitivity, 0);

		Quaternion targetRot_char = transform.localRotation * rotY; // rotações alvo
		Quaternion targetRot_cam = cam.transform.localRotation * rotX;

		transform.localRotation = targetRot_char; // rotação alvo do personagem para ele direto
		cam.transform.localRotation = ClampRotationX (targetRot_cam); // "capar" rotação da câmera, e então aplicar.

		#endregion

		#region Movement

		Vector3 moveDirection = new Vector3 (moveHor, 0, moveVer); // vetor para direção do movimento
		moveDirection.Normalize(); // normalizar vetor de movimento
		moveDirection = transform.TransformDirection(moveDirection); // movimento global para local
		moveDirection *= moveSpeed; // multiplicar pela velocidade

		Vector3 gravity = new Vector3 (0, -9.8f, 0);

		float d = charCtrl.isGrounded ? damping : airDamping;
		acc.x = Mathf.Lerp (acc.x, moveDirection.x, d);
		acc.z = Mathf.Lerp (acc.z, moveDirection.z, d);
		acc.y = charCtrl.isGrounded ? -0.1f : acc.y + gravity.y * Time.deltaTime;
		acc.y += charCtrl.isGrounded ? jump * jumpSpeed : 0;

		//		charCtrl.SimpleMove (acc); // aplicar movimento
		charCtrl.Move (acc *  Time.deltaTime); // aplicar movimento

		#endregion

		#region Tiro

		weaponAnimator.ResetTrigger("attack1");
		weaponAnimator.ResetTrigger("attack2");

		if(attack1 > 0)
		{
			RaycastHit r = new RaycastHit();
			Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out r, 50f);
			GameObject bhCLone = (GameObject)Instantiate(BulletHole, r.point, Quaternion.identity);

			GameObject.Destroy(bhCLone,1f);

			weaponAnimator.SetTrigger("attack1");
		}
		else
		{
			if (attack2 > 0)
			{
				RaycastHit r = new RaycastHit();
				Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out r, 50f);
				GameObject bhCLone = (GameObject)Instantiate(BulletHole, r.point, Quaternion.identity);

				GameObject.Destroy(bhCLone,1f);

				weaponAnimator.SetTrigger("attack2");
			}
		}

		#endregion
	}

	Quaternion ClampRotationX(Quaternion q){
		
		// divisões para "nornalizar" a rotação
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f; // trava o "w" em 1

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x); // conversão de radianos para angulos

		angleX = Mathf.Clamp (angleX, -90f, 90f); // travar a rotação conforme minimo e maximo

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX); // re-converter de angulos para radianos

		return q;

	}
}
