using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Vector3 pos;
	public GameObject lazer;
	public float lazerSpeed = 4f;
	private float lazerRate = .2f;
	public float speed = 1.0f;
	public float padding = 0.5f;
	private float xMin;
	private float xMax;
	public float health = 5f;
	public AudioClip lazerSFX;
	public AudioClip playerDestroyed;
	private HealthDisplay healthdisplay;
	private LevelManager level_manager;

	// Use this for initialization
	void Start () {
		//Get xMin and xMax from the camera
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftSide = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightSide = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftSide.x + padding;
		xMax = rightSide.x - padding;

		level_manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		healthdisplay = GameObject.Find ("Health").GetComponent<HealthDisplay>();
	}

	void Fire(){
		Vector3 offset = transform.position + new Vector3 (0f,0.8f,0f);
		GameObject beam = Instantiate (lazer, offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,lazerSpeed);
		AudioSource.PlayClipAtPoint (lazerSFX,transform.position);
	}

	// Update is called once per frame
	void Update () {

		//Firing lazer
		if(Input.GetKeyDown("space")){
			InvokeRepeating("Fire", 0.00001f,lazerRate);
		}
		if(Input.GetKeyUp("space")){
			CancelInvoke ("Fire");
		}

		//Horizontal Movement
		if(Input.GetKey("left")){
			pos = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
			this.transform.position += pos * speed * Time.deltaTime;
		}
		if (Input.GetKey ("right")) {
			pos = new Vector3 (Input.GetAxis("Horizontal"), 0, 0);
			this.transform.position += pos * speed * Time.deltaTime;
		}

		//Restrict Player to gamespace
		float newX = Mathf.Clamp (this.transform.position.x, xMin,xMax);
		this.transform.position = new Vector3 (newX, this.transform.position.y, this.transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D col){
		laser beam = col.gameObject.GetComponent<laser>();
		if(beam){
			health -= beam.GetDamage();
			healthdisplay.HealthDamaged ((int)beam.GetDamage());
			if(health <= 0){
				AudioSource.PlayClipAtPoint (playerDestroyed,transform.position);
				Destroy(this.gameObject);	
				level_manager.LoadLevel ("Lose");
			}
			beam.Hit ();
		}
	}
}
