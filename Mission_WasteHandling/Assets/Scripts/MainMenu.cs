using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
		SceneManager.LoadScene("Task1");
		Debug.Log("TASK1LOADED");
	}
	
	public void PlayGame2() {
		SceneManager.LoadScene("Task2");
		Debug.Log("TASK2LOADED");
	}
	
	public void quitGame() {
		Debug.Log("GAMEENDED");
		Application.Quit();
	}
		
}
