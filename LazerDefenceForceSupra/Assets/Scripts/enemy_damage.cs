using UnityEngine;
using System.Collections;

public class enemy_damage : MonoBehaviour {

	float health = 2;
	public float lazerSpeed = -4f;
	public GameObject lazer;
	private ScoreKeeper scoreKeeper;
	public float shotFreq = 0.5f;
	public int points = 1;
	public AudioClip laserSFX;
	public AudioClip destroyedSFX;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D col){
		laser beam = col.gameObject.GetComponent<laser>();
		if(beam){
			health -= beam.GetDamage();
			if(health <= 0){
				scoreKeeper.Score (points);
				AudioSource.PlayClipAtPoint (destroyedSFX,transform.position);
				Destroy(this.gameObject);		
			}
			beam.Hit ();
		}
	}

	void Fire(){
		Vector3 offset = transform.position + new Vector3 (0f,-0.8f,0f);
		GameObject beam = Instantiate (lazer, offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,lazerSpeed);
	}

	void Update(){
		float probability = shotFreq * Time.deltaTime;
		if(Random.value < probability){
			Fire ();
			AudioSource.PlayClipAtPoint (laserSFX,transform.position);
		}
	}
}