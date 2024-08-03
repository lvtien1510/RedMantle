using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    // Existing fields
    public bool isActive;
    public string title;
    public List<string> descriptions = new List<string>();
    public string notice;
    public QuestGoal goal;
    public int currentDescriptionIndex = 0;
    public int questID; // ID của quest

    // Existing methods

    public void StartQuest()
    {
        isActive = true;
        goal.ResetProgress();
        currentDescriptionIndex = 0; // Bắt đầu từ mô tả đầu tiên
    }

    public void CompleteCurrentDescription()
    {
        if (goal.IsReached(currentDescriptionIndex))
        {
            if (currentDescriptionIndex < descriptions.Count - 1)
            {
                currentDescriptionIndex++;
                goal.ResetProgress();
                SaveQuestData(); // Lưu dữ liệu nhiệm vụ sau khi hoàn thành mô tả hiện tại
            }
            else
            {
                CompleteQuest();
            }
        }
    }

    public void CompleteQuest()
    {
        isActive = false;
        Debug.Log("Completed quest");
    }

    public void SaveQuestData()
    {
        PlayerPrefs.SetInt("CurrentQuestID", questID);
        PlayerPrefs.SetString("IsActive", isActive.ToString());
        PlayerPrefs.SetInt("CurrentDescriptionIndex", currentDescriptionIndex);
        for (int i = 0; i < goal.currentAmounts.Count; i++)
        {
            PlayerPrefs.SetInt("Quest" + questID + "_CurrentAmount" + i, goal.currentAmounts[i]);
        }
        PlayerPrefs.Save(); // Lưu lại tất cả các thay đổi
    }

    public void LoadQuestData()
    {
        currentDescriptionIndex = PlayerPrefs.GetInt("CurrentDescriptionIndex", 0);
        isActive = bool.Parse(PlayerPrefs.GetString("IsActive", "true"));
        for (int i = 0; i < goal.currentAmounts.Count; i++)
        {
            goal.currentAmounts[i] = PlayerPrefs.GetInt("Quest" + questID + "_CurrentAmount" + i, 0);
        }
    }
}
