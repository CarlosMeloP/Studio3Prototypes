using UnityEngine;
using System.Collections;

public class GameState{

	private static GameState instance=null;
	public enum GAMESTATE
	{
		GAMESTART,GAMERESUME,GAMEOVER
	}

	public GAMESTATE gameState;

	private GameState()
	{
	}

	public static GameState Instance
	{
		get
		{
			if (instance==null)
			{
				instance = new GameState();
			}
			return instance;
		}
	}

	public void resumeGame() {
		
	}

	public void gameOver() {
		
	}

	public void startGame() {
		
	}
}