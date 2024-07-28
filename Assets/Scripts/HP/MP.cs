using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MP : MonoBehaviour
{
    public float maxMP;
    public float currentMP;
    [SerializeField] private Image mPFill;
    [SerializeField] private TextMeshProUGUI textMP;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerAttack player;
    [SerializeField] private int reduceMP;

    [SerializeField] private float regenInterval; // Thời gian chờ để tăng MP
    [SerializeField] private int mpIncreaseAmount; // Lượng MP sẽ tăng mỗi lần
    private float timer; // Bộ đếm thời gian
    private int countShoot;
    [SerializeField] private Exp checkLevel; // Gán Exp script thông qua Inspector


    private void Awake()
    {
        currentMP = maxMP;
        textMP.text = currentMP + " / " + maxMP;
        countShoot = 0;
        timer = regenInterval;
    }
    private void OnEnable()
    {
        checkLevel.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        checkLevel.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        if (newLevel == checkLevel.currentLevel)
        {
            maxMP += 100;
            currentMP = maxMP;
            UpdateMP(0);
        }
    }
    private void Update()
    {
        if (player.isShooting && countShoot == 0)
        {
            UpdateMP(reduceMP);
            countShoot = 1;
        } else if (!player.isShooting)
        {
            countShoot = 0;
        }
        timer -= Time.deltaTime; // Giảm bộ đếm thời gian theo thời gian đã trôi qua

        if (timer <= 0)
        {
            UpdateMP(-mpIncreaseAmount); // Tăng MP hiện tại
            timer = regenInterval; // Đặt lại bộ đếm thời gian
        }
        
    }
    public void UpdateMP(float amount)
    {
        currentMP -= amount;
        currentMP = Mathf.Clamp(currentMP, 0, maxMP);
        textMP.text = currentMP + " / " + maxMP;
        UpdateMPBar();


    }
    private void UpdateMPBar()
    {
        float targetFillAmount = currentMP / maxMP;
        // healthBarFill.fillAmount = targetFillAmount;
        mPFill.DOFillAmount(targetFillAmount, fillSpeed);
        // healthBarFill.color = colorGradient.Evaluate(targetFillAmount);
    }
}
