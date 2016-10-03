using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public GameObject gameOverPanel;
    public Text scoreText;
    int score = 0;
    public Text bestText;
    public Text currentText;
    public GameObject newAlert;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameOver() {
        Invoke("ShowOverPanel", 2.0f);
    }

    void ShowOverPanel() {
        scoreText.gameObject.SetActive(false);

        if (score > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", score);
            newAlert.SetActive(true);
        }

        bestText.text = "Best Score : " + PlayerPrefs.GetInt("Best", 0).ToString();
        currentText.text = "Current Score : " + score.ToString();

        gameOverPanel.SetActive(true);
    }

    public static void Restart() {
        // Initiate.Fade(Application.loadedLevelName,Color.white,2.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void IncrementScore() {
        score++;
        scoreText.text = score.ToString();
    
    }
}
