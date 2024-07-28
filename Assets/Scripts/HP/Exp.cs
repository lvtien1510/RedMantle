using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Exp : MonoBehaviour
{
    [SerializeField] private int currentExp, maxExp;
    public int currentLevel;

    public delegate void LevelUpHandler(int newLevel);
    public event LevelUpHandler OnLevelUp;

    [SerializeField] private Image expFill;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private TextMeshProUGUI textExpPercent;
    [SerializeField] private float fillSpeed;

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
    }

    private void LevelUp()
    {
        currentExp -= maxExp; // Giữ lại exp dư thừa sau khi lên cấp
        currentLevel++;
        maxExp += 100;

        OnLevelUp?.Invoke(currentLevel);
        UpdateExpBar();
    }

    private void Awake()
    {
        UpdateExpBar();
    }

    private void UpdateExpBar()
    {
        textExp.text = currentLevel.ToString();
        float percentage = (float)currentExp / maxExp * 100;
        string formattedPercentage = percentage.ToString("00.00") + " %";
        textExpPercent.text = formattedPercentage;

        float targetFillAmount = (float)currentExp / maxExp;
        expFill.DOFillAmount(targetFillAmount, fillSpeed);
    }
}
