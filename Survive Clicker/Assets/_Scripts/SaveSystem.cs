using System;
using UnityEngine;

[Serializable]
public static class SaveSystem
{
    private const string TOTAL_TIME_KEY = "TOTAL_TIME";
    private const string ENDING_NAME_KEY = "ENDING_NAME";
    private const string TOTAL_FOOD_KEY = "TOTAL_FOOD";
    private const string TOTAL_WEAPONS_KEY = "TOTAL_WEAPONS";

    public static void SaveResults(float totalTime, string endingName, int totalFood, int totalWeapons)
    {
        PlayerPrefs.SetFloat(TOTAL_TIME_KEY, totalTime);
        PlayerPrefs.SetString(ENDING_NAME_KEY, endingName);
        PlayerPrefs.SetInt(TOTAL_FOOD_KEY, totalFood);
        PlayerPrefs.SetInt(TOTAL_WEAPONS_KEY, totalWeapons);
        PlayerPrefs.Save();
    }

    public static float LoadResultsTime()
    {
        return PlayerPrefs.GetFloat(TOTAL_TIME_KEY, 0);
    }

    public static string LoadResultsEnding()
    {
        return PlayerPrefs.GetString(ENDING_NAME_KEY, "NONE");
    }

    public static int LoadResultsFood()
    {
        return PlayerPrefs.GetInt(TOTAL_FOOD_KEY, 0);
    }

    public static int LoadResultsWeapons()
    {
        return PlayerPrefs.GetInt(TOTAL_WEAPONS_KEY, 0);
    }
}

