using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static int currentScore;
    public static int highScore;
           
    public static int currentLevel;
    public static int unlockLevel;
        
	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        currentLevel = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public static void CompleteLevel()
    {
        if(currentLevel<SceneManager.sceneCount)
        {
            currentLevel += 1;
        SceneManager.LoadScene(currentLevel);
        }
        else 
        {
            currentLevel = 0; ;
            SceneManager.LoadScene(0);
        }
    }
}
