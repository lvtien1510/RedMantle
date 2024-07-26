using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Exp : MonoBehaviour
{
    private float maxMP = 100;
    private float currentMP = 100;
    [SerializeField] private Image mPFill;
    [SerializeField] private TextMeshProUGUI textMP;
    [SerializeField] private float fillSpeed;
    // [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerAttack player;
    [SerializeField] private int reduceMP;


    private void Awake()
    {
        currentMP = maxMP;
        textMP.text = currentMP + " / " + maxMP;
    }
    private void FixedUpdate()
    {
        if (player.isShooting)
        {
            UpdateMP(reduceMP);
        }
    }
    public void UpdateMP(float amount)
    {
        currentMP += amount;
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
