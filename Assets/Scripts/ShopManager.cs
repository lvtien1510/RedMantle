using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nameItem;
    [SerializeField] private TextMeshProUGUI[] cost;
    [SerializeField] private Image[] image;

    private HealthBar hp;
    private MP mp;
    private PlayerAttack attack;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Bullet2 bullet2;
    private CoinManager coinManager;

    private void Start()
    {
        hp = FindObjectOfType<HealthBar>();
        mp = FindObjectOfType<MP>();
        attack = FindObjectOfType<PlayerAttack>();
        coinManager = FindObjectOfType<CoinManager>();

        nameItem[0].text = "Tăng HP";
        cost[0].text = "$ 2";

        nameItem[1].text = "Tăng MP";
        cost[1].text = "$ 2";

        nameItem[2].text = "Tăng Damage";
        cost[2].text = "$ 2";

        nameItem[3].text = "Tăng Hồi Phục";
        cost[3].text = "$ 2";
    }

    public void Item1()
    {
        if(coinManager.currentCoins >= 2)
        {
            hp.bonus += 20;
            hp.maxHealth += 20;
            hp.UpdateHealth(-hp.bonus);
            coinManager.currentCoins -= 2;
            coinManager.textCoin.text = coinManager.currentCoins.ToString();
            PlayerPrefs.SetFloat("Bonus", hp.bonus);
            PlayerPrefs.SetFloat("MaxHP", hp.maxHealth);
            PlayerPrefs.SetInt("Coin", coinManager.currentCoins);
        }
    }
    public void Item2()
    {
        if (coinManager.currentCoins >= 2)
        {
            mp.bonus += 20;
            mp.maxMP += 20;
            mp.UpdateMP(-mp.bonus);
            coinManager.currentCoins -= 2;
            coinManager.textCoin.text = coinManager.currentCoins.ToString();
            PlayerPrefs.SetFloat("Bonus", mp.bonus);
            PlayerPrefs.SetFloat("MaxMP", mp.maxMP);
            PlayerPrefs.SetInt("Coin", coinManager.currentCoins);
        }
    }
    public void Item3()
    {
        if (coinManager.currentCoins >= 2)
        {
            attack.amountBonus += 5;
            attack.attackDamage += 5;
            bullet.damage += 5;
            bullet2.damage += 5;
            coinManager.currentCoins -= 2;
            coinManager.textCoin.text = coinManager.currentCoins.ToString();
            PlayerPrefs.SetInt("AmountBonus", attack.amountBonus);
            PlayerPrefs.SetInt("Attack", attack.attackDamage);
            PlayerPrefs.SetInt("Bullet", bullet.damage);
            PlayerPrefs.SetInt("Nullet2", bullet2.damage);
            PlayerPrefs.SetInt("Coin", coinManager.currentCoins);
        }
    }
    public void Item4()
    {

        if (coinManager.currentCoins >= 2)
        {
            hp.count += 0.01f;
            mp.count += 0.01f;
            hp.hpIncreaseAmount = (int)(hp.maxHealth * hp.count);
            mp.mpIncreaseAmount = (int)(mp.maxMP * mp.count);

            coinManager.currentCoins -= 2;
            coinManager.textCoin.text = coinManager.currentCoins.ToString();
            PlayerPrefs.SetFloat("Count", hp.count);
            PlayerPrefs.SetFloat("HpIncrease", hp.hpIncreaseAmount);
            PlayerPrefs.SetFloat("MpIncrease", mp.mpIncreaseAmount);
            PlayerPrefs.SetInt("Coin", coinManager.currentCoins);
        }
    }
}
