using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public Transform inventoryPanel; 
    public GameObject itemPrefab;
    //Spells:
    public ItemData BlackBullet;
    public ItemData BlueSpell;
    public ItemData SpellShield;
    public ItemData Chest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        addItemToInventory(BlackBullet);
        addItemToInventory(BlueSpell);
        addItemToInventory(SpellShield);
    }

    public void addItemToInventory(ItemData itemData)
    {
        GameObject newItem = Instantiate(itemPrefab, inventoryPanel);
        Image image = newItem.GetComponent<Image>();
        if (image != null && itemData != null)
        {
            image.sprite = itemData.icon;
            image.color = itemData.color;
        }
        
    }
}
