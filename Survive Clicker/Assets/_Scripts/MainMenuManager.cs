using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuPanel.activeSelf && !optionsPanel.activeSelf)
        {
            PauseGame();
        }

    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameManager.InitializeGame();
    }

    public void OptionsMenu()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu() 
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

}
