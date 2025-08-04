using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public ItemDrops id;
    public bool isInventory;
    public bool isSpell;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CompareTag("ItemWithEffect")){
                Items.looted = true;
            }
            if (itemData != null && Inventory.Instance != null && isInventory && !isSpell)
            {
                Inventory.Instance.addItemToInventory(itemData);
            }
            else if(itemData != null && Inventory.Instance != null && isInventory && isSpell)
            {
                Inventory.Instance.addSpellToInventory(itemData);
                id.applyItemWithEffects();
            }
            Destroy(gameObject); 
        }
    }
}
