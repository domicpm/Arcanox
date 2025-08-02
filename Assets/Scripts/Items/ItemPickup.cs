using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Items.looted = true;
            Inventory.Instance.addItemToInventory(itemData); 
            Destroy(gameObject); 
        }
    }
}
