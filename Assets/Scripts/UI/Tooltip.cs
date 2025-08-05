using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;
    public GameObject panel;
    public Text tooltipText;
    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void ShowTooltip(ItemData item, RectTransform sourceRect)
    {

        if (item == null) return;
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        Vector2 pos = sourceRect.anchoredPosition;
        pos.y += sourceRect.sizeDelta.y - 75f;
        pos.x -=  115f;
        panelRect.anchoredPosition = pos;
        panel.SetActive(true);
        tooltipText.text = item.itemName + "\n" + "\n" + item.description;
    }

    public void HideTooltip()
    {
        panel.SetActive(false);
    }
}
