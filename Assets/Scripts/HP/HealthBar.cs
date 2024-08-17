using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public float bonus;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMovement player;
    private Health enemy;
    [SerializeField] private float damageInterval;
    private float lastDamageTime;

    [SerializeField] private float regenInterval; // Thời gian chờ để tăng HP
    public int hpIncreaseAmount; // Lượng HP sẽ tăng mỗi lần
    public float count;
    private float timer; // Bộ đếm thời gian
    [SerializeField] private Exp checkLevel; // Gán Exp script thông qua Inspector

    private SpriteRenderer spriteRend;
    private float iFramesDuration;
    private int numberOfFlashes;
    private Rigidbody2D rb;
    

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        currentHealth = PlayerPrefs.GetFloat("CurrentHP", 100);
        maxHealth = PlayerPrefs.GetFloat("MaxHP", 100);
        count = PlayerPrefs.GetFloat("Count", 0.05f);
        bonus = PlayerPrefs.GetFloat("Bonus", 0);
        hpIncreaseAmount = PlayerPrefs.GetInt("HpIncrease", (int)(maxHealth * count));
        UpdateHealth(0);
        spriteRend = GetComponent<SpriteRenderer>();
        iFramesDuration = 1;
        numberOfFlashes = 7;
        rb = GetComponent<Rigidbody2D>();
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
            maxHealth = (int)(100 * Mathf.Pow(1.2f, checkLevel.currentLevel - 1) + bonus);
            currentHealth = maxHealth;
            UpdateHealth(0);
            PlayerPrefs.SetFloat("MaxHP", maxHealth);
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime; // Giảm bộ đếm thời gian theo thời gian đã trôi qua

        if (timer <= 0)
        {
            UpdateHealth(-hpIncreaseAmount); // Tăng HP hiện tại
            timer = regenInterval; // Đặt lại bộ đếm thời gian
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (Time.time >= lastDamageTime + damageInterval)
            {
                Health enemy = collision.collider.GetComponent<Health>(); // Gán giá trị cho biến enemy
                if (enemy != null) // Kiểm tra null trước khi truy cập thuộc tính
                {
                    UpdateHealth(enemy.damageAmount);
                    StartCoroutine(Invunerability());
                    lastDamageTime = Time.time;
                }
            }
        }
        
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.8f, 0.45f, 0.45f, 0.8f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    public void UpdateHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthText.text = currentHealth + " / " + maxHealth;
        
        UpdateHealthBar();
        if (currentHealth <= 0.01 && !player.isDead)
        {
            player.Die();
            healthBarFill.DOFillAmount(0, fillSpeed);
        }
        PlayerPrefs.SetFloat("CurrentHP", currentHealth);

    }
    private void UpdateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        // healthBarFill.fillAmount = targetFillAmount;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
        // healthBarFill.color = colorGradient.Evaluate(targetFillAmount);
    }
}
