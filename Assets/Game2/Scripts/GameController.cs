using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Game controller controls the behaviour of the ga,e
/// </summary>
public class GameController : MonoBehaviour {  

    // start menu camvas
public GameObject startPanel;


	//private GameObject handler;
	// events to handle game start actions
	public delegate void OnGamestartAction();
	public static event OnGamestartAction OnGameStart;
	// events to handle events when save me is clicked
	public delegate void OnGameReviveAction();
	public static event OnGameReviveAction OnGameRevive;

	// Use this for initialization
	void Start () {		
		Player.OnGameOver += OnGameOver;
		//Player.OnIncreaseScore += OnScoreIncrement;
		Player.distanceTraveled = 0;
		QualitySettings.antiAliasing = 4;
		iTween.CameraFadeAdd ();
		iTween.CameraFadeFrom (1.5f,1.5f);
		//handler = GameObject.FindGameObjectWithTag ("handler");
		GameState.Instance.gameState = GameState.GAMESTATE.GAMESTART;
        
    }
	

	public IEnumerator startEnemy() {
		while(true) {
			yield return new WaitForSeconds (1);
			Debug.Log ("One");
			yield return new WaitForSeconds (1);
			Debug.Log ("two");
		}
	}

	// when play button is hit
	public void startGame() {
		GameState.Instance.gameState = GameState.GAMESTATE.GAMERESUME;
		if(OnGameStart!=null) {
			OnGameStart ();
		}

	}
	// when game is over
	void OnGameOver() {
        restartGame();
	}

	// Restart Game is hit
	public void restartGame() {
		Player.OnGameOver -= OnGameOver;
		SceneManager.LoadScene ("GameScene");
	}

}
