using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    // Reference to the player game object
    public GameObject player;
    public InventoryManager inventoryManager;

    [System.Serializable]
    public class PlayerData
    {
        public string level;
        public Vector3 position;
        public List<InventoryManager.Item> inventory;
    }

    // Save the player's progress
    public void Save()
    {
        // Create a data model to hold the relevant game data
        PlayerData data = new PlayerData();
        data.level = SceneManager.GetActiveScene().name;
        data.position = player.transform.position;
        data.inventory = inventoryManager.GetInventoryData();

        // Serialize the data to JSON
        string json = JsonUtility.ToJson(data);

        // Save the JSON to a local file
        string savePath = Path.Combine(Application.persistentDataPath, "save.json");
        File.WriteAllText(savePath, json);

        Debug.Log("Game saved.");
    }

    // Load the player's progress
    public void LoadGame()
    {
        // Load the JSON data from the local file
        string savePath = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(savePath))
        {
            
            string json = File.ReadAllText(savePath);

            // Deserialize the JSON to retrieve the saved data
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            
            EventManager.StartListening(EventName.Load, LoadProgress);

            // Load the specified level
            SceneManager.LoadScene(data.level);
            
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }
    
    public void LoadProgress()
    {
        Debug.Log("start of load");
        string savePath = Path.Combine(Application.persistentDataPath, "save.json");

        string json = File.ReadAllText(savePath);

        // Deserialize the JSON to retrieve the saved data
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);   
        
        EventManager.StopListening(EventName.Load, LoadProgress);

        // Find the player object in the new scene
        //player = GameObject.FindGameObjectWithTag("Player");

        // Find the player object in the new scene
        inventoryManager = FindObjectOfType<InventoryManager>();

        // Apply the saved data to the player
        player.transform.position = data.position;
        inventoryManager.LoadInventoryData(data.inventory);

        Debug.Log("Game loaded.");
        
    }
}
