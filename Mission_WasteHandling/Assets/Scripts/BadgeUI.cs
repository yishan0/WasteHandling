using UnityEngine;

public class BadgeUI : MonoBehaviour
{
    [SerializeField] private Transform badgeContainer;
    [SerializeField] private GameObject badgePrefab;

    private int badgeCount = 0;

    public void AddBadges(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(badgePrefab, badgeContainer);
            badgeCount++;
        }
    }

    void Start()
    {
        // Initialize with zero badges
        badgeCount = 3;
    }
}