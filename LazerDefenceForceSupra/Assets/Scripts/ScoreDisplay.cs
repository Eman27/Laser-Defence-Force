using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text mytext = GetComponent<Text> ();
		mytext.text = "Score: " + ScoreKeeper.score.ToString ();
		ScoreKeeper.Reset ();
	}
}
