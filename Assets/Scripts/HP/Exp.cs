using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Exp : MonoBehaviour
{
    public int currentExp = 0;
    public int maxExp;
    public int currentLevel;

    public delegate void LevelUpHandler(int newLevel);
    public event LevelUpHandler OnLevelUp;

    [SerializeField] private Image expFill;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private TextMeshProUGUI textExpPercent;
    
    
    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void HandleExperienceChange(int newExp)
    {
        currentExp += newExp;
        UpdateExpBar();
        Debug.Log("Current Experience: " + currentExp);
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        PlayerPrefs.SetInt("CurrentExp", currentExp);
        PlayerPrefs.SetInt("MaxExp", maxExp);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }

    private void LevelUp()
    {
        currentExp -= maxExp; // Giữ lại exp dư thừa sau khi lên cấp
        currentLevel++;
        maxExp = (int)(200 * Mathf.Pow(1.5f, currentLevel - 0.5f));
        OnLevelUp?.Invoke(currentLevel);
        UpdateExpBar();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        currentExp = PlayerPrefs.GetInt("CurrentExp", 0);
        maxExp = PlayerPrefs.GetInt("MaxExp", 200);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        UpdateExpBar();
    }

    private void UpdateExpBar()
    {
        textExp.text = currentLevel.ToString();
        float percentage = (float)currentExp / maxExp * 100;
        string formattedPercentage = percentage.ToString("00.00") + " %";
        textExpPercent.text = formattedPercentage;

        float targetFillAmount = (float)currentExp / maxExp;
        expFill.fillAmount = targetFillAmount;
    }
}
