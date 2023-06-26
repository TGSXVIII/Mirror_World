using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variable

    public GameObject pauseMenuToggle;
    public GameObject inventoryToggle;
    public GameObject mirrorToggle;
    bool isPaused = false;

    #endregion

    private void Start()
    {
        EventManager.Fire(EventName.Load);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { PauseMenu(); }
    }
    
    public void PauseMenu()
    {
        pauseMenuToggle.SetActive(!pauseMenuToggle.activeSelf);
        inventoryToggle.SetActive(!inventoryToggle.activeSelf);
        mirrorToggle.SetActive(!mirrorToggle.activeSelf);

        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }

        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}
