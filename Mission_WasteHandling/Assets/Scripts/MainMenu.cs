using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		Game.instance.setMode(1);
		SceneManager.LoadScene("Task1");
		Debug.Log("TASK1LOADED");
	}
	
	public void PlayGame2() {
		Game.instance.setMode(2);
		SceneManager.LoadScene("Task2");
		Debug.Log("TASK2LOADED");
	}
	
	public void quitGame() {
		Debug.Log("GAMEENDED");
		Application.Quit();
	}
		
}
