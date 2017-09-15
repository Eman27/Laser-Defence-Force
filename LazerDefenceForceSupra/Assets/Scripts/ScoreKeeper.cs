using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private Text mytext;
	public static int score = 0;
	public int goal = 100;
	private LevelManager level_manager;

	void Start(){
		mytext = GetComponent<Text> ();
		level_manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	public void Score(int points){
		score += points;
		mytext.text = "Score: " + score.ToString();

		if (score >= goal) {
			level_manager.LoadLevel ("Win Screen");
		}
	}
	
	public static void Reset(){
		score = 0;
	}
}
