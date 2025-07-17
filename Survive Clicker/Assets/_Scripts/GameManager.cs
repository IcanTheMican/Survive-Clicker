using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Save system se koristi u metodama Starve i Ending na kraju
//Zao mi je ak morate gledat ovaj kod :(

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool isGameRunning;
    bool introPassed;

    [Header("Resources")]
    [SerializeField] private Image dayCounter;
    [SerializeField] private Button daySkipButton;
    private int days;
    private int workers;
    private int unemployed;
    private int food;
    private int wood;
    private int stone;
    private int iron;
    private int tools;

    [Header("Buildings")]
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject costBox;
    [SerializeField] private TMP_Text costText;

    private int house;
    [SerializeField] GameObject housePrefab;
    private int farm;
    [SerializeField] GameObject farmPrefab;
    private int woodcutter;
    [SerializeField] GameObject woodcutterPrefab;
    private int blacksmith;
    [SerializeField] GameObject blacksmithPrefab;
    private int quarry;
    [SerializeField] GameObject quarryPrefab;
    private int ironMine;
    [SerializeField] GameObject ironMinePrefab;

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

    [Header("Building Buttons")]
    [SerializeField] private Button houseButton;
    [SerializeField] private Button farmButton;
    [SerializeField] private Button woodcutterButton;
    [SerializeField] private Button blacksmithButton;
    [SerializeField] private Button quarryButton;
    [SerializeField] private Button ironMineButton;

    [Header("Scenes")]
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameOver2Panel;
    [SerializeField] GameObject badEndingPanel;
    [SerializeField] GameObject goodEndingPanel;

    private float timer;
    private float timeElapsed;
    private int farmCost;
    private int blacksmithCost;
    public bool isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (quarry == 1 && woodcutter == 1 && !introPassed)
        {
            MakeButtonsInteractable();
            introPassed = true;
        }
        if (introPassed) { TimePassage(); }
    }

    public void InitializeGame() 
    {
        ResetVariables();
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
        timeElapsed += Time.deltaTime; 
        dayCounter.fillAmount = 1-timer/12;
        if (timer >= 12)
        {
            days++;
            timer = 0;
            FoodProduction();
            ToolsProduction();
            WoodProduction();
            StoneProduction();
            IronProduction();
            FoodConsumption();
            UpdateText();
            if (food < 0) 
            {
                isGameOver = true;
                Starve(); 
            }
            else if (days == 20)
            {
                isGameOver = true;
                Ending();
            }
        }
    }

    void FoodConsumption() 
    {
        food -= house;
    }

    int Poopulation() 
    {
        return workers + unemployed;
    }

    public void BuildFarm()
    {
        if (wood >= farmCost && stone >= farmCost && iron >= farmCost && unemployed >= farmCost && farm < 4) 
        {
            GameObject farmInstance = Instantiate(farmPrefab, parent);
            farmInstance.transform.position += Vector3.right * (208 * farm);
            farm++;
            unemployed -= farmCost;
            workers += farmCost;
            wood -= farmCost;
            stone -= farmCost;
            iron -= farmCost;
            farmCost *= 2;
            if (farm == 4) 
            {
                food += 80;
                farmButton.interactable = false;
                HideCost();
            }
            costText.text = $"COST: {farmCost} WOOD,\n{farmCost} STONE, {farmCost} IRON\n{farmCost} WORKERS";
            costBox.transform.SetAsLastSibling();
            UpdateText();
        }
    }

    public void BuildBlacksmith()
    {
        if (wood >= blacksmithCost && stone >= blacksmithCost && iron >= blacksmithCost && unemployed >= 3 && blacksmith < 4)
        {
            GameObject blacksmithInstance = Instantiate(blacksmithPrefab, parent);
            blacksmithInstance.transform.position += Vector3.right * (208 * blacksmith);
            blacksmith++;
            unemployed -= 3;
            workers += 3;
            wood -= blacksmithCost;
            stone -= blacksmithCost;
            iron -= blacksmithCost;
            blacksmithCost *= 2;
            if (blacksmith == 4) 
            {
                tools += 70;
                blacksmithButton.interactable = false;
                HideCost();
            }
            costText.text = $"COST: {blacksmithCost} WOOD,\n{blacksmithCost} STONE, {blacksmithCost} IRON\n3 WORKERS";
            costBox.transform.SetAsLastSibling();
            UpdateText();
        }
    }

    public void BuildWoodcutter()
    {
        if (wood >= 1 && stone >= 3 && unemployed >= 2 && woodcutter < 4)
        {
            GameObject woodcutterInstance = Instantiate(woodcutterPrefab, parent);
            if (woodcutter < 2) { woodcutterInstance.transform.position += Vector3.right * (214 * woodcutter); }
            else
            {
                woodcutterInstance.transform.position += Vector3.right * (214 * (woodcutter - 2));
                woodcutterInstance.transform.position += Vector3.down * 111;
            }
            woodcutter++;
            unemployed -= 2;
            workers += 2;
            wood -= 1;
            stone -= 3;
            UpdateText();
            if (woodcutter == 4) 
            { 
                woodcutterButton.interactable = false;
                HideCost();
            }
        }
    }

    public void BuildHouse()
    {
        if (wood >= 2 && stone >= 2 && house<12)
        {
            GameObject houseInstance = Instantiate(housePrefab, parent);
            if (house < 6) { houseInstance.transform.position += Vector3.right * (138 * house); }
            else 
            {
                houseInstance.transform.position += Vector3.right * (138 * (house - 6));
                houseInstance.transform.position += Vector3.down * 130; 
            }
            house++;
            unemployed+=4;
            wood -= 2;
            stone -= 2;
            costBox.transform.SetAsLastSibling();
            UpdateText();
            if(house == 12) 
            { 
                houseButton.interactable = false;
                HideCost();
            }
        }
    }

    public void BuildQuarry()
    {
        if (wood >= 3 && stone >= 1 && unemployed >= 2 && quarry < 4)
        {
            GameObject quarryInstance = Instantiate(quarryPrefab, parent);
            if (quarry < 2) { quarryInstance.transform.position += Vector3.right * (214 * quarry); }
            else
            {
                quarryInstance.transform.position += Vector3.right * (214 * (quarry - 2));
                quarryInstance.transform.position += Vector3.down * 111;
            }
            quarry++;
            unemployed -= 2;
            workers += 2;
            wood -= 3;
            stone -= 1;
            costBox.transform.SetAsLastSibling();
            UpdateText();
            if (quarry == 4) 
            { 
                quarryButton.interactable = false;
                HideCost();
            }
        }
    }

    public void BuildIronMine()
    {
        if (wood >= 4 && stone >= 4 && unemployed >= 2 && ironMine < 4)
        {
            GameObject ironMineInstance = Instantiate(ironMinePrefab, parent);
            if (ironMine < 2) { ironMineInstance.transform.position += Vector3.right * (214 * ironMine); }
            else
            {
                ironMineInstance.transform.position += Vector3.right * (214 * ironMine + 34);
            }
            ironMine++;
            unemployed-=2;
            workers+=2;
            wood -= 4;
            stone -= 4;
            costBox.transform.SetAsLastSibling();
            UpdateText();
            if (ironMine == 4) { 
                ironMineButton.interactable = false;
                HideCost();
            }
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

    void IronProduction()
    {
        iron += ironMine * 2;
    }

    void FoodProduction() 
    {
        food += farm * 6;
    }

    void ToolsProduction()
    {
        tools += blacksmith * 3;
    }

    void UpdateText() 
    {
        daysText.text = $"DAY: {days}";
        //Resources
        poopulationText.text = $"Population : {Poopulation()}/48\n" +
            $"   Workers: {workers}\n   Unemployed: {unemployed}";
        foodText.text = $"Food: {food} (200)";
        woodText.text = $"Wood: {wood}";
        stoneText.text = $"Stone: {stone}";
        ironText.text = $"Iron: {iron}";
        toolsText.text = $"Weapons: {tools} (100)";

        //Buildings
        houseText.text = $"BUILD HOUSE\nHouses: {house}/12";
        farmText.text = $"BUILD FARM\nFarms: {farm}/4";
        quarryText.text = $"BUILD QUARRY\nQuarries: {quarry}/4";
        blacksmithText.text = $"BUILD SMITHY\nSmithies: {blacksmith}/4";
        woodcutterText.text = $"BUILD SAWMILL\nSawmills: {woodcutter}/4";
        ironMineText.text = $"BUILD MINE\nMines: {ironMine}/4";
    }

    public void ShowHouseCost() 
    {
        if (houseButton.interactable == false) { return; }
        costBox.SetActive(true);
        costText.text = "COST: 2 WOOD,\n2 STONE";
        costBox.transform.position = new Vector3(costBox.transform.position.x, 950, costBox.transform.position.z);
        costBox.transform.SetAsLastSibling();
    }

    public void ShowFarmCost()
    {
        if (farmButton.interactable == false) { return; }
        costBox.SetActive(true);
        costText.text = $"COST: {farmCost} WOOD,\n{farmCost} STONE, {farmCost} IRON\n{farmCost} WORKERS";
        costBox.transform.position = new Vector3(costBox.transform.position.x, 785, costBox.transform.position.z);
        costBox.transform.SetAsLastSibling();
    }

    public void ShowBlacksmithCost()
    {
        if (blacksmithButton.interactable == false) { return; }
        costBox.SetActive(true);
        costText.text = $"COST: {blacksmithCost} WOOD,\n{blacksmithCost} STONE, {blacksmithCost} IRON\n3 WORKERS";
        costBox.transform.position = new Vector3(costBox.transform.position.x, 620, costBox.transform.position.z);
        costBox.transform.SetAsLastSibling();
    }

    public void ShowWoodcutterCost()
    {
        if (woodcutterButton.interactable == false) { return; }
        costText.text = "COST: 1 WOOD,\n3 STONE, 2 WORKERS";
        costBox.SetActive(true);
        costBox.transform.position = new Vector3(costBox.transform.position.x, 455, costBox.transform.position.z);
    }

    public void ShowQuarryCost()
    {
        if (quarryButton.interactable == false) { return; }
        costText.text = "COST: 3 WOOD,\n1 STONE, 2 WORKERS";
        costBox.SetActive(true);
        costBox.transform.position = new Vector3(costBox.transform.position.x, 290, costBox.transform.position.z);
        costBox.transform.SetAsLastSibling();
    }

    public void ShowIronMineCost()
    {
        if (ironMineButton.interactable == false) { return; }
        costText.text = "COST: 4 WOOD,\n4 STONE, 2 WORKERS";
        costBox.SetActive(true);
        costBox.transform.position = new Vector3(costBox.transform.position.x, 125, costBox.transform.position.z);
        costBox.transform.SetAsLastSibling();
    }

    public void HideCost()
    {
        costBox.SetActive(false);
    }

    public void MakeButtonsInteractable() 
    {
        houseButton.interactable = true;
        farmButton.interactable = true;
        blacksmithButton.interactable = true;
        ironMineButton.interactable = true;
        daySkipButton.interactable = true;
    }

    public void SkipDay() { timer = 11.9f; }

    void ResetVariables() 
    {
        days = 0;
        workers = 0;
        unemployed = 0;
        food = 16;
        wood = 6;
        stone = 6;
        iron = 1;
        tools = 0;
        introPassed = false;
        timer = 0;
        farmCost = 1;
        blacksmithCost = 2;
        isGameOver = false;
        woodcutter = 0;
        quarry = 0;
        farm = 0;
        blacksmith = 0;
        ironMine = 0;
        house = 0;
        houseButton.interactable = false;
        farmButton.interactable = false;
        blacksmithButton.interactable = false;
        ironMineButton.interactable = false;
        daySkipButton.interactable = false;
        dayCounter.fillAmount = 1;
        BuildHouse();
    }

    //Endings
    void Starve() 
    {
        SaveSystem.SaveResults(timeElapsed, "GAME OVER (STARVE)", food, tools);
        gamePanel.SetActive(false);
        gameOver2Panel.SetActive(true);
    }

    void Ending() 
    {
        if(tools >= 100) 
        {
            SaveSystem.SaveResults(timeElapsed, "WAR", food, tools);
            gamePanel.SetActive(false);
            badEndingPanel.SetActive(true);
        }
        else if(food >= 200) 
        {
            SaveSystem.SaveResults(timeElapsed, "FRIENDSHIP", food, tools);
            gamePanel.SetActive(false);
            goodEndingPanel.SetActive(true);
        }
        else
        {
            SaveSystem.SaveResults(timeElapsed, "GAME OVER", food, tools);
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

}
