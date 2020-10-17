using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Save Data", menuName = "Character/Data", order = 1)]
public class CharacterSaveData_SO : ScriptableObject
{
    [Header("Stats")]

    [SerializeField]
    int currentHealth;

    [Header("Leveling")]

    [SerializeField]
    int currentLevel = 1;
    [SerializeField]
    int maxLevel = 30;
    [SerializeField]
    int basisPoints = 200;
    [SerializeField]
    int pointsTillNextLevel;

    [SerializeField]
    float levelBuff = 0.1f; //10 %

    [Header("Save Data")]

    [SerializeField]
    string key;

    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public void AggregateAttackPoints(int points) //connected to the attack system to determine when should we level up
    {
        pointsTillNextLevel -= points;

        if (pointsTillNextLevel <= 0)
        {
            currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);

            pointsTillNextLevel += (int)(basisPoints * LevelMultiplier);

            Debug.Log("LEVEL UP! New Level: " + currentLevel);
        }
    }

    void OnEnable()
    {
        if (pointsTillNextLevel == 0)
        {
            pointsTillNextLevel = (int)(basisPoints * LevelMultiplier);
        }

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    void OnDisable()
    {
        if (key == "")
        {
            key = name;
        }

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
}
