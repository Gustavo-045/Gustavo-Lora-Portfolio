using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ItemPickup : InteractableObject
{
    private InventoryManager inventoryManager; // Reference to the inventory manager
    public ItemData itemToAdd; // The item to add to the inventory

    public override void Interact()
    {
        Debug.Log("Chocke con un item ");
        inventoryManager.AddItemToInventory(itemToAdd); // Add item to inventory
        Destroy(gameObject); // Destroy the pickup after it's collected
    }

    private void Start()
    {
        inventoryManager = Object.FindFirstObjectByType<InventoryManager>(); // Store reference to InventoryManager
    }
}
