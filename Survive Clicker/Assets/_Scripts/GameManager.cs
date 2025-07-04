using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameRunning;

    [Header("Resources")]
    [SerializeField] private int days;
    [SerializeField] private int workers;
    [SerializeField] private int unemployed;
    [SerializeField] private int food;
    [SerializeField] private int wood;
    [SerializeField] private int stone;
    [SerializeField] private int iron;
    [SerializeField] private int tools;

    [Header("Buildings")]
    [SerializeField] private int house;
    [SerializeField] private int farm;
    [SerializeField] private int woodcutter;
    [SerializeField] private int blacksmith;
    [SerializeField] private int quarry;
    [SerializeField] private int ironMine;
    [SerializeField] private int goldMine;

    [Header("Resources Text")]
    [SerializeField] private TMP_Text daysText;
    [SerializeField] private TMP_Text poopulationText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text ironText;
    [SerializeField] private TMP_Text toolsText;

    [Header("Buildings Text")]
    [SerializeField] private TMP_Text houseText;
    [SerializeField] private TMP_Text farmText;
    [SerializeField] private TMP_Text woodcutterText;
    [SerializeField] private TMP_Text blacksmithText;
    [SerializeField] private TMP_Text quarryText;
    [SerializeField] private TMP_Text ironMineText;
    //[SerializeField] private TMP_Text goldMineText;

    private float timer;

    private void Start()
    {
        IncreasePoopulation();
        UpdateText();
    }

    private void Update()
    {
        TimePassage();
    }

    public void InitializeGame() 
    {
        isGameRunning = true;
        UpdateText();
    }

    void TimePassage() 
    {
        if (!isGameRunning)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            days++;
            timer = 0;
            FoodGathering();
            FoodProduction();
            WoodProduction();
            StoneProduction();
            FoodConsumption(1);
            IncreasePoopulation();
            UpdateText();
        }
    }

    void FoodConsumption(int foodConsumed) 
    {
        food -= foodConsumed * Poopulation();
    }

    int GetMaxPoopulation()
    {
        int maxPoopulation = house * 4;
        return maxPoopulation;
    }

    void IncreasePoopulation()
    {
        if (days % 2 == 0) 
        {
            if (Poopulation() < GetMaxPoopulation()) 
            { 
                unemployed += house;
                if (Poopulation() > GetMaxPoopulation()) { unemployed = GetMaxPoopulation() - workers; }
            }
        }
    }

    int Poopulation() 
    {
        return workers + unemployed;
    }

    public void BuildFarm()
    {
        if (wood >= 10 && unemployed >= 1) 
        { 
            farm++;
            unemployed--;
            workers++;
            wood -= 10;
            UpdateText();
        }
    }

    public void BuildWoodcutter()
    {
        if (wood >= 5 && iron >= 1 && unemployed >= 1)
        {
            woodcutter++;
            unemployed--;
            workers++;
            wood -= 5;
            iron--;
            UpdateText();
        }
    }

    public void BuildHouse()
    {
        if (wood >= 2 && unemployed >= 1)
        {
            house++;
            unemployed--;
            workers++;
            wood -= 2;
            UpdateText();
        }
    }

    public void BuildQuarry()
    {
        if (wood >= 10 && unemployed >= 1)
        {
            quarry++;
            unemployed--;
            workers++;
            wood -= 10;
            UpdateText();
        }
    }

    void WoodProduction()
    {
        wood += woodcutter * 2;
    }

    void StoneProduction()
    {
        stone += quarry * 2;
    }

    void FoodGathering() 
    {
        food += unemployed / 2;
    }

    void FoodProduction() 
    {
        food += farm * 4;
    }

    void UpdateText() 
    {
        daysText.text = $"Days: {days}";
        //Resources
        poopulationText.text = $"Poopulation : {Poopulation()}/{GetMaxPoopulation()}\n" +
            $"   Workers: {workers}\n   Unemployed: {unemployed}";
        foodText.text = $"Food: {food}";
        woodText.text = $"Wood: {wood}";
        stoneText.text = $"Stone: {stone}";
        ironText.text = $"Iron: {iron}";
        toolsText.text = $"Tools: {tools}";

        //Buildings
        houseText.text = $"BUILD HOUSE\n\nHouses: {house}";
        farmText.text = $"BUILD FARM\n\nFarms: {farm}";
        quarryText.text = $"BUILD QUARRY\n\nQuarry: {quarry}";
        //blacksmithText.text = $"\nFood: {blacksmith}";
        woodcutterText.text = $"BUILD WOODCUTTER\n\nWoodcutters: {woodcutter}";
        //ironMineText.text = $"\nFood: {ironMine}";
    }

}
