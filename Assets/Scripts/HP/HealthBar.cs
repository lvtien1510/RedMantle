using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private int damageAmount;
    [SerializeField] private float damageInterval;
    private float lastDamageTime;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
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
