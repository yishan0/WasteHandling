using UnityEngine;

public class BadgeCanvas : MonoBehaviour
{
    [SerializeField] private BadgeUI badgeUI;
    public static BadgeCanvas badges;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (badges == null)
        {
            badges = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addBadges(int amount)
    {
        badgeUI.AddBadges(amount);
    }
}
