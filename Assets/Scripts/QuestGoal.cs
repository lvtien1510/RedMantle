using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum GoalType
{
    Kill,
    Gathering,
}

public class QuestGoal
{
    // Existing fields
    public GoalType goalType;
    public List<int> requiredAmounts = new List<int>();
    public List<int> currentAmounts = new List<int>();
    public List<int> targetIds;

    // Existing methods

    public int CurrentRequiredAmount(int currentDescriptionIndex)
    {
        return requiredAmounts[currentDescriptionIndex];
    }

    public bool IsReached(int currentDescriptionIndex)
    {
        return (currentAmounts[currentDescriptionIndex] >= requiredAmounts[currentDescriptionIndex]);
    }

    public void EnemyKilled(int enemyID)
    {
        int index = targetIds.IndexOf(enemyID);
        if (index >= 0 && goalType == GoalType.Kill)
        {
            currentAmounts[index]++;
            SaveProgress(); // Lưu dữ liệu tiến trình nhiệm vụ sau khi tiêu diệt kẻ thù
        }
    }

    public void ItemCollected(int itemID)
    {
        int index = targetIds.IndexOf(itemID);
        if (index >= 0 && goalType == GoalType.Gathering)
        {
            currentAmounts[index]++;
            SaveProgress(); // Lưu dữ liệu tiến trình nhiệm vụ sau khi thu thập vật phẩm
        }
    }

    public void ResetProgress()
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            currentAmounts[i] = 0;
        }
    }

    public void SaveProgress()
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            PlayerPrefs.SetInt("QuestGoal_CurrentAmount" + i, currentAmounts[i]);
        }
        PlayerPrefs.Save(); // Lưu lại tất cả các thay đổi
    }

    public void LoadProgress()
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            currentAmounts[i] = PlayerPrefs.GetInt("QuestGoal_CurrentAmount" + i, 0);
        }
    }
}
