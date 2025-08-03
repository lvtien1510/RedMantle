using System.Collections.Generic;
using UnityEngine;

public class QuestSetup : MonoBehaviour
{
    [SerializeField] private QuestGiver questGiver;

    private void Start()
    {

        List<Quest> quests = new List<Quest>();

        Quest quest1 = new Quest
        {
            questID = 1,
            isActive = false,
            title = "Giết quái",
            descriptions = new List<string> { "Giết 10 Slime" },
            notice = "Chú ý: Slime khá lành tính, tuy nhiên đừng nên chạm vào chúng.",
            goal = new QuestGoal
            {
                goalType = GoalType.Kill,
                requiredAmounts = new List<int> { 10 },
                targetIds = new List<int> { 1 }, // 1: Slime, 2: Chuột
                currentAmounts = new List<int> { 0 } // Đảm bảo khởi tạo currentAmounts
            }
        };
        quests.Add(quest1);

        Quest quest2 = new Quest
        {
            questID = 2,
            isActive = false,
            title = "Giết quái",
            descriptions = new List<string> { "Giết 1 Ma", "Giết 1 Nhi" },
            notice = "Chú ý: Lửa",
            goal = new QuestGoal
            {
                goalType = GoalType.Kill,
                requiredAmounts = new List<int> { 1, 1 },
                targetIds = new List<int> { 1, 1 }, // 3: Slime, 2: Chuột
                currentAmounts = new List<int> { 0, 0 } // Đảm bảo khởi tạo currentAmounts
            }
        };
        quests.Add(quest2);

        Quest quest3 = new Quest
        {
            questID = 3,
            isActive = false,
            title = "Thu thập coins",
            descriptions = new List<string> { "Thu thập 5 coins" },
            notice = "Chú ý: Coins nằm rải rác khắp bản đồ",
            goal = new QuestGoal
            {
                goalType = GoalType.Gathering,
                requiredAmounts = new List<int> { 5 },
                targetIds = new List<int> { 1 }, // ID cho coins
                currentAmounts = new List<int> { 0 } // Đảm bảo khởi tạo currentAmounts
            }
        };
        quests.Add(quest3);

        questGiver.quests = quests;

        // Retrieve stored quest ID
        int storedQuestID = PlayerPrefs.GetInt("CurrentQuestID", -1);

        if (storedQuestID < 1)
        {
            // No quest assigned or quest ID is invalid, assign the first quest
            if (quests.Count > 0)
            {
                questGiver.currentQuest = quests[0];
                questGiver.currentQuest.StartQuest();
                questGiver.UpdateQuestUI();
            }
        }
        else
        {
            // Find and assign the quest with the stored quest ID
            Quest questToAssign = quests.Find(q => q.questID == storedQuestID);
            if (questToAssign != null)
            {
                questGiver.currentQuest = questToAssign;
                questGiver.currentQuest.LoadQuestData(); // Load quest data from PlayerPrefs
                questGiver.UpdateQuestUI();
            }
        }
    }

}
