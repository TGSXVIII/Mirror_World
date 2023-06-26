using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private void Start()
    {
        KeepInventory();
    }

    // Define an item struct to hold information about items in the inventory
    [System.Serializable]
    public struct Item
    {
        public string name;
        public int quantity;
        public Sprite icon;

        public Item(string name, int quantity, Sprite icon)
        {
            this.name = name;
            this.quantity = quantity;
            this.icon = icon;
        }
    }

    // The inventory dictionary to hold items
    private Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    // Reference to the UI text component to display item quantities
    public Text[] itemText;

    // Reference to the UI image component to display item icons
    public Image[] itemImage;

    // Add an item to the inventory
    public void AddItem(string itemName, int quantity, Sprite icon)
    {
        if (inventory.ContainsKey(itemName))
        {
            Debug.Log(itemName+" added");
            quantity += inventory[itemName].quantity;
            inventory.Remove(itemName);
            inventory.Add(itemName, new Item(itemName, quantity, icon));
        }

        else
        {
            Debug.Log(itemName + " added");
            inventory.Add(itemName, new Item(itemName, quantity, icon));
        }

        // Update the UI
        UpdateUI();
    }

    // Remove an item from the inventory
    public void RemoveItem(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName))
        {
            quantity -= inventory[itemName].quantity;
            Sprite icon = inventory[itemName].icon;
            inventory.Remove(itemName);
            inventory.Add(itemName, new Item(itemName, quantity, icon));

            // If the item quantity drops to zero, remove it from the inventory
            if (inventory[itemName].quantity <= 0)
            {
                inventory.Remove(itemName);
            }
        }

        // Update the UI
        UpdateUI();
    }

    // Check if an item is in the inventory
    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }

    // Get the quantity of an item in the inventory
    public int GetItemQuantity(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return inventory[itemName].quantity;
        }

        else
        {
            return 0;
        }
    }

    // Update the UI to display the quantity and icon of an item
    private void UpdateUI()
    {
        // Clear the UI 
        for (int x = 0; x < itemImage.Length && x < itemText.Length; x++)
        {
            itemText[x].text = "";
            itemImage[x].sprite = null;
        }

        int i = 0;

        foreach (KeyValuePair<string, Item> item in inventory) 
        {
            itemText[i].text = item.Value.quantity.ToString();
            itemImage[i].sprite = item.Value.icon;
            i++;
        }
    }

    // Save the inventory data to PlayerPrefs
    public void KeepInventory()
    {
        int itemCount = 0;

        foreach (KeyValuePair<string, Item> item in inventory)
        {
            // Save item details using unique keys
            string key = "Item_" + itemCount;

            PlayerPrefs.SetString(key + "_name", item.Value.name);
            PlayerPrefs.SetInt(key + "_quantity", item.Value.quantity);
            PlayerPrefs.SetString(key + "_icon", item.Value.icon.name);

            itemCount++;
        }

        PlayerPrefs.SetInt("ItemCount", itemCount);
    }

    // Load the inventory data from PlayerPrefs
    public void LoadInventory()
    {
        int itemCount = PlayerPrefs.GetInt("ItemCount", 0);

        for (int i = 0; i < itemCount; i++)
        {
            string key = "Item_" + i;

            if (PlayerPrefs.HasKey(key + "_name") && PlayerPrefs.HasKey(key + "_quantity") && PlayerPrefs.HasKey(key + "_icon"))
            {
                string itemName = PlayerPrefs.GetString(key + "_name");
                int itemQuantity = PlayerPrefs.GetInt(key + "_quantity");
                string iconSpriteName = PlayerPrefs.GetString(key + "_icon");

                // Retrieve the sprite from the Resources folder using the sprite name
                Sprite iconSprite = Resources.Load<Sprite>(iconSpriteName);

                // Create the item and add it to the inventory
                Item item = new Item(itemName, itemQuantity, iconSprite);
                inventory[itemName] = item;

                // Update the UI
                UpdateUI();
            }
        }
    }

    public List<Item> GetInventoryData()
    {
        List<Item> inventoryData = new List<Item>();

        foreach (KeyValuePair<string, Item> itemEntry in inventory)
        {
            Item item = itemEntry.Value;

            // Create a new instance of the Item struct with the same data
            Item itemData = new Item(item.name, item.quantity, item.icon);
            inventoryData.Add(itemData);
        }
        return inventoryData;
    }

    public void LoadInventoryData(List<Item> inventoryData)
    {
        inventory.Clear();

        foreach (Item item in inventoryData)
        {
            AddItem(item.name, item.quantity, item.icon);  
        }
    }

    public void updateMirrorUI(Sprite mirrorSprite)
    {
        RemoveItem("Mirror", 1);

        AddItem("Mirror", 1, mirrorSprite);

        UpdateUI();
    }
}


