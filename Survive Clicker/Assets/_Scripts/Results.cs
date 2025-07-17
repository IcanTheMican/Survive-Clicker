using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{
    [SerializeField] private TMP_Text lastResultsText;

    float time;
    string ending;
    int food;
    int weapons;

    private void Start()
    {
        ChangeResultsText();
    }

    public void ChangeResultsText()
    {
        time = Mathf.Round(SaveSystem.LoadResultsTime() * 100.0f) * 0.01f; 
        ending = SaveSystem.LoadResultsEnding();
        food = SaveSystem.LoadResultsFood();
        weapons = SaveSystem.LoadResultsWeapons();
        lastResultsText.text = $"Time: {time} secs" +
            $"\nEnding: {ending}\nFood: {food}\nWeapons: {weapons}";
    }
}
