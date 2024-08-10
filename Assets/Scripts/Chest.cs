using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    [Header("Components")]
    [SerializeField] private GameObject[] components;
    //private bool invulnerable;

    [Header("ItemDrops")]
    [SerializeField] private GameObject[] itemDrops;
    [Range(0f, 1f)]
    [SerializeField] private float dropRate;

    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();

    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
            ItemDrop();

            //Deactivate all attached component classes
            foreach (GameObject component in components)
            {
                Destroy(component, 0.2f);
            }
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            float dropChance = Random.Range(0f, 1f); // Sinh số ngẫu nhiên từ 0 đến 1

            if (dropChance <= dropRate) // Kiểm tra nếu tỷ lệ ngẫu nhiên nhỏ hơn hoặc bằng tỷ lệ rơi
            {
                Debug.Log("Item");
                Instantiate(itemDrops[i], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("No item");
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }


}
