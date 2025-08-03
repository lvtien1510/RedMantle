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
    public GoalType goalType;
    public List<int> requiredAmounts = new List<int>();
    public List<int> currentAmounts = new List<int>();
    public List<int> targetIds;

    public int CurrentRequiredAmount(int currentDescriptionIndex)
    {
        return requiredAmounts[currentDescriptionIndex];
    }

    public bool IsReached(int currentDescriptionIndex)
    {
        return currentAmounts[currentDescriptionIndex] >= requiredAmounts[currentDescriptionIndex];
    }

    public void EnemyKilled(int enemyID, int questID)
    {
        int index = targetIds.IndexOf(enemyID);
        if (index >= 0 && goalType == GoalType.Kill)
        {
            currentAmounts[index]++;
            SaveProgress(questID); // Truyền questID vào đây
        }
    }

    public void ItemCollected(int itemID, int questID)
    {
        int index = targetIds.IndexOf(itemID);
        if (index >= 0 && goalType == GoalType.Gathering)
        {
            currentAmounts[index]++;
            SaveProgress(questID); // Truyền questID vào đây
        }
    }

    public void ResetProgress()
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            currentAmounts[i] = 0;
        }
    }

    public void SaveProgress(int questID)
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            PlayerPrefs.SetInt("Quest" + questID + "_Goal_CurrentAmount" + i, currentAmounts[i]);
        }
        PlayerPrefs.Save(); // Lưu lại tất cả các thay đổi
    }

    public void LoadProgress(int questID)
    {
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            currentAmounts[i] = PlayerPrefs.GetInt("Quest" + questID + "_Goal_CurrentAmount" + i, 0);
        }
    }
}