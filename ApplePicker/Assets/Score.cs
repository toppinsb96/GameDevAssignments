using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static Score score;
    public Text highScoreText;

    int curScore = 0;

	void Start () {
        score = this;	
	}

    public void AddScore(int delta) {
        curScore += delta;
        GetComponent<TextMesh>().text = curScore.ToString();
        highScoreText.text = "Your Score: " + curScore.ToString();
    }
}
