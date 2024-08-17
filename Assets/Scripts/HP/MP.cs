using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MP : MonoBehaviour
{

    public float maxMP;
    public float currentMP;
    public float bonus;
    [SerializeField] private Image mPFill;
    [SerializeField] private TextMeshProUGUI textMP;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerAttack player;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Bullet2 bullet2;

    [SerializeField] private float regenInterval; // Thời gian chờ để tăng MP
    public int mpIncreaseAmount; // Lượng MP sẽ tăng mỗi lần
    public float count;
    private float timer; // Bộ đếm thời gian
    private int countShoot;
    [SerializeField] private Exp checkLevel; // Gán Exp script thông qua Inspector


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        currentMP = PlayerPrefs.GetFloat("CurrentMP", 100);
        maxMP = PlayerPrefs.GetFloat("MaxMP", 100);
        bonus = PlayerPrefs.GetFloat("Bonus", 0);
        count = PlayerPrefs.GetFloat("Count", 0.05f);
        mpIncreaseAmount = PlayerPrefs.GetInt("MpIncrease", (int)(maxMP * count));
        player.amountBonus = PlayerPrefs.GetInt("AmountBonus", 0);
        player.attackDamage = PlayerPrefs.GetInt("Attack", 40);
        bullet.damage = PlayerPrefs.GetInt("Bullet", 20);
        bullet2.damage = PlayerPrefs.GetInt("Bullet2", 10);
        UpdateMP(0);
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
            maxMP = (int)(100 * Mathf.Pow(1.1f, checkLevel.currentLevel - 1) + bonus);
            currentMP = maxMP;
            player.attackDamage += 40 + player.amountBonus;
            bullet.damage += 20 + player.amountBonus;
            bullet2.damage += 15 + player.amountBonus;
            UpdateMP(0);
            PlayerPrefs.SetFloat("MaxMP", maxMP);
            PlayerPrefs.SetInt("Attack", player.attackDamage);
            PlayerPrefs.SetInt("Bullet", bullet.damage);
            PlayerPrefs.SetInt("Bullet2", bullet2.damage);
        }
    }
    private void Update()
    {
        if (player.isShooting && countShoot == 0)
        {
            UpdateMP(player.reduceMP);
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
        PlayerPrefs.SetFloat("CurrentMP", currentMP);

    }
    private void UpdateMPBar()
    {
        float targetFillAmount = currentMP / maxMP;
        // healthBarFill.fillAmount = targetFillAmount;
        mPFill.DOFillAmount(targetFillAmount, fillSpeed);
        // healthBarFill.color = colorGradient.Evaluate(targetFillAmount);
    }
}
