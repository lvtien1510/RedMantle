using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MP : MonoBehaviour
{
    private float maxMP = 100;
    public float currentMP = 100;
    [SerializeField] private Image mPFill;
    [SerializeField] private TextMeshProUGUI textMP;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerAttack player;
    [SerializeField] private int reduceMP;
    private int countShoot;


    private void Awake()
    {
        currentMP = maxMP;
        textMP.text = currentMP + " / " + maxMP;
        countShoot = 0;
        
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
