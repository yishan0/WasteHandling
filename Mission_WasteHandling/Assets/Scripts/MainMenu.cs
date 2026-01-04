using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		AudioManager.Instance.PlaySFX("Click");
		Game.instance.setMode(1);
		SceneManager.LoadScene("Task1");
		Debug.Log("TASK1LOADED");
	}
	
	public void PlayGame2() {
		AudioManager.Instance.PlaySFX("Click");
		Game.instance.setMode(2);
		SceneManager.LoadScene("Task2");
		Debug.Log("TASK2LOADED");
	}
	
	public void quitGame() {
		AudioManager.Instance.PlaySFX("Click");
		Debug.Log("GAMEENDED");
		Application.Quit();
	}
		
}
