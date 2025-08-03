using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public delegate void ExperienceChangeHandler(int amout);
    public event ExperienceChangeHandler OnExperienceChange;
    // Singleton check
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
