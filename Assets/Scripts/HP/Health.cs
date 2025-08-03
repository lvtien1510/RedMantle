using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private GameObject[] components;
    //private bool invulnerable;

    [Header("ItemDrops")]
    [SerializeField] private GameObject[] itemDrops;
    [Range(0f, 1f)]
    [SerializeField] private float dropRate;

    [SerializeField] private int expAmount;
    [SerializeField] private int enemyID; // ID của quái vật
    public int damageAmount;

    private QuestGiver questGiver;

    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        iFramesDuration = 1;
        numberOfFlashes = 7;
        questGiver = FindObjectOfType<QuestGiver>(); // Tìm và gán QuestGiver
        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
            ExperienceManager.Instance.AddExperience(expAmount);
            ItemDrop();

            if (questGiver != null)
            {
                questGiver.EnemyKilled(enemyID); // Gọi phương thức khi quái vật chết
            }

            //Deactivate all attached component classes
            foreach (GameObject component in components)
            {
                Destroy(component, 0.5f);
            }
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

    private IEnumerator Invunerability()
    {
        //invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.8f, 0.45f, 0.45f, 0.95f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        //invulnerable = false;
    }
}
