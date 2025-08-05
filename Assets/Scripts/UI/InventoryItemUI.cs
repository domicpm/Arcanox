using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData itemData; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Tooltip.Instance != null)
        {
            Tooltip.Instance.ShowTooltip(itemData, GetComponent<RectTransform>());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.HideTooltip();
    }
}
