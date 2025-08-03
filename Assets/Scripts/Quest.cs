using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public List<string> descriptions = new List<string>();
    public string notice;
    public QuestGoal goal;
    public int currentDescriptionIndex = 0;
    public int questID; // ID của quest

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
        PlayerPrefs.SetInt("Quest" + questID + "_IsActive", isActive ? 1 : 0);
        PlayerPrefs.SetInt("Quest" + questID + "_CurrentDescriptionIndex", currentDescriptionIndex);
        goal.SaveProgress(questID);
        PlayerPrefs.Save(); // Lưu lại tất cả các thay đổi
    }

    public void LoadQuestData()
    {
        isActive = PlayerPrefs.GetInt("Quest" + questID + "_IsActive", 0) == 1;
        currentDescriptionIndex = PlayerPrefs.GetInt("Quest" + questID + "_CurrentDescriptionIndex", 0);
        goal.LoadProgress(questID);
    }
}

