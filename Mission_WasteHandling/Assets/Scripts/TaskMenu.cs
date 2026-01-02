using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TaskMenu : MonoBehaviour
{
    public static TaskMenu menu;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private GameObject panelBig;


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
        if(Game.instance.getMode() == 1)
            SceneManager.LoadScene("Task1");
        if(Game.instance.getMode() == 2)
            SceneManager.LoadScene("Task2");
        Debug.Log("TASKRESTARTED");
    }

    public void showPrompt()
    {
        if(Game.instance.getMode() == 1)
            obj.SetActive(true);
        if(Game.instance.getMode() == 2)
            panelBig.SetActive(true);
        Debug.Log("PROMPT SHOWN");
        if(Game.instance.getMode() == 2)
        {
            panelBig.GetComponentInChildren<TextMeshProUGUI>().text = "Recycle all containers correctly: Use your mouse to select a container and"
+ " determine whether it is completely clean and empty before placing it in the recycling bin."
+ " You may perform any of the following actions: (1) drag the container directly into the"
+ " recycling bin, (2) press “E” to empty any remaining residue into the food-waste bin, or"
+ " (3) press “R” to rinse the container.";
        }
        else
        {
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "Identify which bottles have no residue or food waste. Place those clean bottles "
            + "into the recycling bin. Dispose the remaining trash in the general waste bin.";
        }
    }

    public void warnRinse()
    {
        obj.SetActive(true);
        Debug.Log("RINSE WARNING SHOWN");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Hint: Remember to rinse the bottle before emptying!";
    }

    public void warnEmpty()
    {
        obj.SetActive(true);
        Debug.Log("RINSE ALREADY WARNING SHOWN");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Hint: This bottle has already been rinsed, cannot empty!";
    }
    public void showHint()
    {
        obj.SetActive(true);
        Debug.Log("TIME HINT SHOWN");
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Great effort! Remember you can try other tasks too!";
    }

    public void hidePrompt()
    {
        obj.SetActive(false);
        panelBig.SetActive(false);
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
            Game.instance.ResetScore();
            endPanel.SetActive(true);
            Debug.Log("RESULT SHOWN - ALL CORRECT");
            endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Great job! You recycled all the bottles correctly.";
            GameObject retryButton = GameObject.Find("Retry");
            retryButton.SetActive(false);
            if (Game.instance.getMode() == 1)
            {
                BadgeCanvas.badges.addBadges(1);
            }
            else if (Game.instance.getMode() == 2)
            {
                BadgeCanvas.badges.addBadges(2);
            }
        }
        else
        {
            Game.instance.ResetScore();
            endPanel.SetActive(true);
            GameObject retryButton = GameObject.Find("Retry");
            retryButton.SetActive(true);
            Debug.Log("RESULT SHOWN - NOT ALL CORRECT");
            endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Hmm… Very close! Remember you should only recycle empty, clean water bottles.";
        }
    }

		
}
