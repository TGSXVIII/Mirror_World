using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class gameOverScreen : MonoBehaviour
{
    public Button restartButton;

    void Start()
    {
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(restartGame);
    }

    private void restartGame()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex);
    }

    private void quitGame()
    {
        
    }
}
