using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	private Text healthText;
	public int healthTotal = 5;
	private string healthBar = "";
	// Use this for initialization
	void Start () {
		healthText = GetComponent<Text>();
		UpdateHealthBar();
	}

	private void UpdateHealthBar(){
		healthBar = "Health:";
		for (int i = 0; i < healthTotal; i++) {
			healthBar = healthBar + " *";
		}
		healthText.text = healthBar;
	}

	public void HealthDamaged(int damage){
		healthTotal -= damage;
		UpdateHealthBar ();
	}

}
