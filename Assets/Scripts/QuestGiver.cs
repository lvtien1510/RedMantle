using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public List<Quest> quests;
    [SerializeField] private CoinManager player;

    [SerializeField] private GameObject questWindow;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI[] descriptionTexts;
    [SerializeField] private TextMeshProUGUI noticeText;

    public Quest currentQuest;
    public int canReceiveNewQuest;

    private void Start()
    {
        LoadQuestData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (questWindow.activeSelf)
            {
                CloseQuestWindow();
            }
            else
            {
                OpenQuestWindow();
                Debug.Log(currentQuest.currentDescriptionIndex);
            }
        }
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        UpdateQuestUI();
        Time.timeScale = 0;
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateQuestUI()
    {
        if (currentQuest != null)
        {
            titleText.text = currentQuest.title;
            for (int i = 0; i < descriptionTexts.Length; i++)
            {
                if (i < currentQuest.descriptions.Count)
                {
                    if (currentQuest.currentDescriptionIndex == i)
                        descriptionTexts[i].text = $"- {currentQuest.descriptions[i]} {currentQuest.goal.currentAmounts[i]} / {currentQuest.goal.CurrentRequiredAmount(i)}";
                    else if (currentQuest.currentDescriptionIndex > i)
                        descriptionTexts[i].text = $"- {currentQuest.descriptions[i]} {currentQuest.goal.CurrentRequiredAmount(i)} / {currentQuest.goal.CurrentRequiredAmount(i)}";
                    else
                        descriptionTexts[i].text = $"- {currentQuest.descriptions[i]} 0 / {currentQuest.goal.CurrentRequiredAmount(i)}";

                    descriptionTexts[i].color = (currentQuest.currentDescriptionIndex >= i) ? Color.white : new Color(1, 1, 1, 0.4f);
                }
                else
                {
                    descriptionTexts[i].text = "";
                    descriptionTexts[i].color = new Color(1, 1, 1, 1);
                }
            }
            noticeText.text = currentQuest.notice;
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest != null)
        {
            currentQuest.CompleteCurrentDescription();
            if (!currentQuest.isActive)
            {
                canReceiveNewQuest = 1;
            }
            UpdateQuestUI();
            SaveQuestData();
        }
    }

    public void EnemyKilled(int enemyID)
    {
        if (currentQuest != null && currentQuest.isActive && currentQuest.goal.goalType == GoalType.Kill)
        {
            currentQuest.goal.EnemyKilled(enemyID);
            UpdateQuestUI();
            SaveQuestData();

            if (currentQuest.goal.IsReached(currentQuest.currentDescriptionIndex))
            {
                CompleteCurrentQuest();
            }
        }
    }

    public void ItemCollected(int itemID)
    {
        if (currentQuest != null && currentQuest.isActive && currentQuest.goal.goalType == GoalType.Gathering)
        {
            currentQuest.goal.ItemCollected(itemID);
            UpdateQuestUI();
            SaveQuestData();

            if (currentQuest.goal.IsReached(currentQuest.currentDescriptionIndex))
            {
                CompleteCurrentQuest();
            }
        }
    }

    public void ReceiveNewQuest()
    {
        if (currentQuest == null || !currentQuest.isActive)
        {
            int currentQuestIndex = quests.IndexOf(currentQuest);
            if (currentQuestIndex < quests.Count - 1)
            {
                currentQuest = quests[currentQuestIndex + 1];
                currentQuest.StartQuest();
                canReceiveNewQuest = 0;
                UpdateQuestUI();
                SaveQuestData();
            }
            else
            {
                currentQuest = null;
            }
        }
    }

    public void SaveQuestData()
    {
        if (currentQuest != null)
        {
            PlayerPrefs.SetInt("CurrentQuestID", currentQuest.questID);
            PlayerPrefs.SetInt("CanReceiveNewQuest", canReceiveNewQuest);
            currentQuest.SaveQuestData();
        }
    }

    public void LoadQuestData()
    {
        int currentQuestID = PlayerPrefs.GetInt("CurrentQuestID", -1);
        canReceiveNewQuest = PlayerPrefs.GetInt("CanReceiveNewQuest", 0);
        if (currentQuestID != -1)
        {
            currentQuest = quests.Find(q => q.questID == currentQuestID);
            if (currentQuest != null)
            {
                currentQuest.LoadQuestData();
                UpdateQuestUI();
            }
        }
    }
}
