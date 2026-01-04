using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    private int score = 0;
    private int mode = 0;

    private int bottlesCollected = 0;

    [SerializeField] private int bottlesToCollect;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bottlesCollected >= bottlesToCollect)
        {
            if (score >= bottlesToCollect)
            {
                Debug.Log("ALL CORRECT!");
                TaskMenu.menu.showResult(true);
            }
            else
            {
                Debug.Log("NOT ALL CORRECT!");
                TaskMenu.menu.showResult(false);
            }

            bottlesCollected = 0;
            Debug.Log("Task completed");
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void ResetScore()
    {
        score = 0;
        bottlesCollected = 0;
        Debug.Log("Score reset to 0");
    }

    public void IncrementBottlesCollected()
    {
        bottlesCollected++;
        Debug.Log("Bottles Collected: " + bottlesCollected);
    }

    public void setMode(int newMode)
    {
        mode = newMode;
        bottlesCollected = 0;
        Debug.Log("Mode set to: " + mode);
    }
    
    public int getMode()
    {
        return mode;
    }

}
