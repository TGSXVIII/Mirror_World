using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Misc")]
    public GameManager gameManager;

    public void ResumeGame()
    {
        gameManager.PauseMenu();
    }

    public void LoadGame()
    {
        SaveGame saveGame = GetComponent<SaveGame>();
        saveGame.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}