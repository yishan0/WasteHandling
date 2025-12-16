using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TaskMenu : MonoBehaviour
{
    public static TaskMenu menu;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject endPanel;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 60f) 
        {
            showHint();
            timeElapsed = 0f; 
        }
        
    }
    void Awake()
    {
        if (menu == null)
        {
            menu = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        obj.SetActive(false);
        endPanel.SetActive(false);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("RETURNEDHOME");
    }

    public void restartTask()
    {
        SceneManager.LoadScene("Task1");
        Debug.Log("TASKRESTARTED");
    }

    public void showPrompt()
    {
        obj.SetActive(true);
        Debug.Log("PROMPT SHOWN");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Identify which bottles have no residue or food waste. Place those clean bottles "
        + "into the recycling bin. Dispose the remaining trash in the general waste bin.";
    }

    public void showHint() {
        obj.SetActive(true);
        Debug.Log("TIME HINT SHOWN");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Great effort! Remember you can try other tasks too!";
    }

    public void hidePrompt()
    {
        obj.SetActive(false);
        Debug.Log("PROMPT HIDDEN");
    }

    public void hideEndPanel()
    {
        endPanel.SetActive(false);
        Debug.Log("ENDPANEL HIDDEN");
        returnToMenu();
    }

    public void showResult(bool isAllCorrect)
    {
        if (isAllCorrect)
        {
            endPanel.SetActive(true);
            Debug.Log("RESULT SHOWN - ALL CORRECT");
            endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Great job! You recycled all the bottles correctly.";
            GameObject retryButton = GameObject.Find("Retry");
            retryButton.SetActive(false);
        }
        else
        {
            endPanel.SetActive(true);
            GameObject retryButton = GameObject.Find("Retry");
            retryButton.SetActive(true);
            Debug.Log("RESULT SHOWN - NOT ALL CORRECT");
            endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Hmm… Very close! Remember you should only recycle empty, clean water bottles.";
        }
    }

		
}
