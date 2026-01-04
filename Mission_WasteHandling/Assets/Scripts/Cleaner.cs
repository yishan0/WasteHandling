using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private bool correctlyRecycled = false;
    private bool isRinsed = false;
    private bool isEmptied = false;
    [SerializeField] private bool requiresRinsing;
    [SerializeField] private bool requiresEmptying;
    [SerializeField] private bool requiresEither;

    [SerializeField] private TaskMenu taskMenu;

    private Transform rinsedVersion;
    private Transform normalVersion;

    private Transform emptyedVersion;

    public void rinse()
    {
        if (requiresEither)
        {
            AudioManager.Instance.PlaySFX("Rinse");
            isRinsed = true;
            Debug.Log("Container rinsed");
            rinsedVersion.gameObject.SetActive(true);
            normalVersion.gameObject.SetActive(false);
            emptyedVersion.gameObject.SetActive(false);
        }
        else if (requiresEmptying && isEmptied)
        {
            AudioManager.Instance.PlaySFX("Rinse");
            isRinsed = true;
            Debug.Log("Container rinsed");
            rinsedVersion.gameObject.SetActive(true);
            normalVersion.gameObject.SetActive(false);
            emptyedVersion.gameObject.SetActive(false);
        }
        else if (!requiresEmptying)
        {
            AudioManager.Instance.PlaySFX("Rinse");
            isRinsed = true;
            Debug.Log("Container rinsed");
            rinsedVersion.gameObject.SetActive(true);
            normalVersion.gameObject.SetActive(false);
            emptyedVersion.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Container must be emptied before rinsing");
            taskMenu.warnRinse();
        }
    }
    public void empty()
    {
        isEmptied = true;
        if (requiresEither)
        {
            AudioManager.Instance.PlaySFX("Empty");
            isEmptied = true;
            Debug.Log("Container emptied");
            emptyedVersion.gameObject.SetActive(true);
            normalVersion.gameObject.SetActive(false);
            rinsedVersion.gameObject.SetActive(false);
        }
        if (!isRinsed && requiresEmptying)
        {
            AudioManager.Instance.PlaySFX("Empty");
            emptyedVersion.gameObject.SetActive(true);
            normalVersion.gameObject.SetActive(false);
            rinsedVersion.gameObject.SetActive(false);
            Debug.Log("Container emptied");
        }
        else if (requiresEmptying)
        {
            Debug.Log("Container already rinsed");
            taskMenu.warnEmpty();
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Game.instance.setMode(2);
        isRinsed = requiresRinsing ? false : true;
        isEmptied = requiresEmptying ? false : true;

        rinsedVersion = transform.Find("Rinsed");
        normalVersion = transform.Find("Default");
        emptyedVersion = transform.Find("Empty");

        rinsedVersion.gameObject.SetActive(false);
        normalVersion.gameObject.SetActive(true);
        emptyedVersion.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (requiresEither)
        {
            if (isRinsed || isEmptied)
            {
                correctlyRecycled = true;
            }
            else
            {
                correctlyRecycled = false;
            }
        }
        else if ((requiresRinsing ? isRinsed : true) && (requiresEmptying ? isEmptied : true))
        {
            correctlyRecycled = true;
            if (gameObject.name.Contains("Milk"))
            {

                GameObject noteObj = GameObject.Find("Note");

                if (noteObj != null)
                {
                    LockSpriteRotation noteScript =
                        noteObj.GetComponent<LockSpriteRotation>();

                    if (noteScript != null)
                    {
                        noteScript.rinseMilk();
                    }
                }
            }
        }
        else
        {
            correctlyRecycled = false;
        }
    }

    public bool isCorrectlyRecycled()
    {
        return correctlyRecycled;
    }

    void start()
    {
        correctlyRecycled = false;
    }

}
