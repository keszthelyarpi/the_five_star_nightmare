using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pálya beállítások")]
    public string firstLevelName = "DefaultScene"; // Ide írd az első jeleneted nevét

    [Header("Panelek")]
    public GameObject mainPanel;    
    public GameObject settingsPanel; 

    void Start()
    {
        // Alapértelmezetten a főmenü látszik, a beállítások nem
        ShowMainPanel();
    }

    public void StartNewGame()
    {
        if (GameManager.Milestones != null)
        {
            GameManager.Milestones.Clear();
        }

        SceneManager.LoadScene(firstLevelName);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Kilépés a játékból...");
        Application.Quit();
    }
}