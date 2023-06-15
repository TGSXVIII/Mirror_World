using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
