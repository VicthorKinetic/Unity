using UnityEngine;
using System.Collections;

public class scrAI : MonoBehaviour {

	public Transform target;
	public Vector3 enemyPosition;
	public bool In = false;
	private Animator Anim;
	UnityEngine.AI.NavMeshAgent agent;
	public scrEnemy Enemy;
	public int animation;
	public float attackTimer;
	public float coolDown;
	public float attackDuration = 1.5f;
	public float distance;
	private Vector3 dir;
	public float direction;
	public scrFPS_Controler Player;
	public scrUI UI;
	public scrGameAudio GameAudio;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		Anim = GetComponent<Animator>();
		Player = GameObject.Find ("Player").GetComponent<scrFPS_Controler> ();
		GameAudio = GameObject.Find ("GameAudio").GetComponent<scrGameAudio>();
		UI = GameObject.Find ("UI").GetComponent<scrUI> ();

		animation = Random.Range (1, 3);

		attackTimer = 0.0f;
		coolDown = 1.5f;

		Anim.SetInteger ("indexAnim", 0);
	}

	// Update is called once per frame
	void Update ()
	{
		
		distance = Vector3.Distance (target.transform.position, transform.position);
		dir = (target.transform.position - transform.position).normalized;
		direction = Vector3.Dot (dir, transform.forward);

		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		if (attackTimer < 0)
			attackTimer = 0;
		
		if (distance <= 2.5 && direction < 0.5) {
			if (attackTimer == 0) {
				if (!Player.justOnce) {
					if (!Enemy.dead) {
						Player.HP -= 10;
						GameAudio.DamageTaken.Play ();
						UI.Health.transform.localScale = new Vector3 ((Player.HP / Player.TotalHP) * 1, 0.7f, 1f);
						attackTimer = coolDown;
						Anim.SetInteger ("indexAnim", 3);
					}
				}
			} else if ((attackTimer > 0.5) && (distance < 2.5 && direction < 0.5)) {
				Anim.SetInteger ("indexAnim", 0);
			} 
		}

		enemyPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		if (!In) {
			if (distance <= 15) {
				In = true;
			} else {
				In = false;
			}
		}
		if (distance > 2.5 && In ) {
			Anim.SetInteger ("indexAnim", animation);
		}

		if (In) 
		{
				agent.SetDestination (target.position);			
		} 

		if (Enemy.dead) {
				agent.SetDestination (enemyPosition);
				Anim.SetInteger ("indexAnim", 4);
				Destroy (this.gameObject, 10f);
			}
		}

}