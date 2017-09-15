using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 5f;
	private float padding = 8.5f;
	private float speed = 5f;
	private Vector3 pos;
	private float xMax;
	private float xMin;
	private bool forward = true;
	private float spawnDelay = 0.5f;
	// Use this for initialization
	void Start () {
		
		SpawnTillFull ();
		//Get xMin and xMax from the camera
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftSide = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightSide = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftSide.x + padding;
		xMax = rightSide.x - padding;
	}

	void SpawnEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnTillFull(){
		Transform nextPos = NextFreePos ();
		if(nextPos){
			GameObject enemy = Instantiate (enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = nextPos;
		}
		if(NextFreePos()){
			Invoke ("SpawnTillFull",spawnDelay);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	bool AllMembersDead(){
		foreach (Transform childPos in transform){
			if (childPos.childCount > 0) {
				return false;
			}
		}
		return true; 
	}

	Transform NextFreePos(){
		foreach (Transform childPos in transform) {
			if(childPos.childCount == 0){
				return childPos;
			}
		}
		return null;
	}

	// Update is called once per frame
	void Update () {

		if(forward == true){
			this.transform.position += new Vector3(speed*Time.deltaTime, 0);
		}

		if(forward == false){
			this.transform.position -= new Vector3(speed*Time.deltaTime, 0);
		}
		//Restrict Player to gamespace
		float newX = Mathf.Clamp (this.transform.position.x, xMin,xMax);
		this.transform.position = new Vector3 (newX, this.transform.position.y, this.transform.position.z);
		if((this.transform.position.x == xMax) || (this.transform.position.x == xMin)){
			forward = !forward;
		}
		if(AllMembersDead()){
			Debug.Log ("All are dead");

			SpawnTillFull ();
		}
	}
}
