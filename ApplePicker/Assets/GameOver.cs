using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public static GameOver gameOver;
    public bool isGameOver = false;

	void Start () {
        gameOver = this;	
	}

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit() {
        Application.Quit();
    }

    public void BeginGameOver() {
        if (!isGameOver)
        {
            isGameOver = true;
            Invoke("ShowGameOver", 1);
        }
    }

    void ShowGameOver() {
        GetComponent<Animator>().enabled = true;
    }
	
	void Update () {
		
	}
}
