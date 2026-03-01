using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradePopupUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public Button closeButton;

    void Awake()
    {
        closeButton.onClick.AddListener(Hide);
        Hide();
    }

    public void Show(BuildingData building)
    {
        if (building == null) return;

        panel.SetActive(true);
        titleText.text = building.buildingName + " Upgrades";

        // Build a nice list
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var up in building.upgrades)
        {
            sb.AppendLine($"• {up.name}  (Cost: {up.cost})");
            if (!string.IsNullOrWhiteSpace(up.description))
                sb.AppendLine($"  {up.description}");
            sb.AppendLine();
        }

        bodyText.text = sb.ToString().TrimEnd();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}