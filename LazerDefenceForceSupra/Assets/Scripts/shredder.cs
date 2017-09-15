using UnityEngine;
using System.Collections;

public class shredder : MonoBehaviour {
	public float damage = 1f;
	void OnTriggerEnter2D(Collider2D col){
		Destroy(col.gameObject);
	}
}
