using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public bool isInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Items.looted = true;
            if (itemData != null && Inventory.Instance != null && isInventory)
            {
                Inventory.Instance.addItemToInventory(itemData);
            }
            Destroy(gameObject); 
        }
    }
}
