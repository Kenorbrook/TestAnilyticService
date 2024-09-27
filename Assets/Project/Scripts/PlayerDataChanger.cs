using System;
using System.Collections.Generic;
using Analytics;
using UnityEngine;

public static class PlayerDataChanger
{
    public static Data data { get; private set; }

    public static void LoadData()
    {
        data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("data", JsonUtility.ToJson(new Data())));
    }

    private static void SaveData()
    {
        PlayerPrefs.SetString("data", JsonUtility.ToJson(data));
    }

    public static void CompleteLevel()
    {
        data.level++;
        SaveData();
    }

    public static void CompleteAchievement(string name)
    {
        if (data.achievement.Contains(name))
            return;

        Analytic.SendEvent("getAchievement", name);
        data.achievement.Add(name);
        SaveData();
    }

    public static void AddMoney(int count)
    {
        data.money += count;
        SaveData();
    }

    public static void RemoveMoney(int count)
    {
        Analytic.SendEvent("spendMoney", $"count:{count}");
        data.money -= count;
        SaveData();
    }


    [Serializable]
    public class Data
    {
        public int level;
        public int money;
        public List<string> achievement;

        public Data()
        {
            level = 0;
            money = 1000;
            achievement = new List<string>();
        }
    }
}