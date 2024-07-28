using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private int damageAmount;
    [SerializeField] private float damageInterval;
    private float lastDamageTime;

    [SerializeField] private float regenInterval; // Thời gian chờ để tăng HP
    [SerializeField] private int hpIncreaseAmount; // Lượng HP sẽ tăng mỗi lần
    private float timer; // Bộ đếm thời gian
    [SerializeField] private Exp checkLevel; // Gán Exp script thông qua Inspector

    private void Awake()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
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
            maxHealth += 100;
            currentHealth = maxHealth;
            UpdateHealth(0);
        }
    }
    private void Update()
    {
        
        timer -= Time.deltaTime; // Giảm bộ đếm thời gian theo thời gian đã trôi qua

        if (timer <= 0)
        {
            UpdateHealth(-hpIncreaseAmount); // Tăng MP hiện tại
            timer = regenInterval; // Đặt lại bộ đếm thời gian
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (Time.time >= lastDamageTime + damageInterval)
            {
                Debug.Log("damage");
                UpdateHealth(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }
    public void UpdateHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthText.text = currentHealth + " / " + maxHealth;
        if (currentHealth <= 0.01)
        {
            player.Die();
        }
        UpdateHealthBar();

        
    }
    private void UpdateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        // healthBarFill.fillAmount = targetFillAmount;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        // healthBarFill.color = colorGradient.Evaluate(targetFillAmount);
    }
}
