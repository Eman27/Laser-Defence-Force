using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {

	public float damage = 1;

	public float GetDamage(){
		return damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
