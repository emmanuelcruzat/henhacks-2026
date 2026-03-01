using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class UpgradePopupUI : MonoBehaviour
{
    [Header("Root")]
    public GameObject panel;

    // Optional fullscreen blocker behind popup to stop clicks
    public GameObject blocker;

    [Header("Text")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI bodyText;
    public Image iconImage;

    [Header("Buttons")]
    public Button closeButton;
    public Button upgradeButton; // single upgrade button (upgrade index 0)

    private BuildingClick currentBuilding;

    void Awake()
    {
        if (closeButton != null) closeButton.onClick.AddListener(Hide);
        if (upgradeButton != null) upgradeButton.onClick.AddListener(() => UpgradePressed(0));

        Hide();
    }

    public bool IsOpen => panel != null && panel.activeSelf;

    public void Show(BuildingClick building)
    {
        if (building == null || building.info == null) return;

        currentBuilding = building;

        if (blocker != null) blocker.SetActive(true);

        panel.SetActive(true);
        panel.transform.SetAsLastSibling(); // ensure on top

        // Fill UI
        var data = building.info;

        if (titleText != null) titleText.text = data.buildingName;
        if (descText != null) descText.text = data.buildingDescription;
        if (iconImage != null) iconImage.sprite = data.icon;

        if (bodyText != null)
        {
            var sb = new StringBuilder();

            if (data.upgrades == null || data.upgrades.Length == 0)
            {
                sb.AppendLine("No upgrades available.");
            }
            else
            {
                for (int i = 0; i < data.upgrades.Length; i++)
                {
                    var up = data.upgrades[i];
                    sb.AppendLine($"{i+1}. {up.upgradeName}  (Cost: {up.cost})");
                    if (!string.IsNullOrWhiteSpace(up.description))
                        sb.AppendLine($"   {up.description}");
                    if (up.successorPrefab == null)
                        sb.AppendLine("   [Missing successor prefab]");
                    sb.AppendLine();
                }
            }

            bodyText.text = sb.ToString().TrimEnd();
        }

        // Enable/disable upgrade button (index 0)
        if (upgradeButton != null)
        {
            bool canUpgrade = data.upgrades != null &&
                              data.upgrades.Length > 0 &&
                              data.upgrades[0].successorPrefab != null;

            upgradeButton.interactable = canUpgrade;
        }
    }

    public void Hide()
    {
        if (panel != null) panel.SetActive(false);
        if (blocker != null) blocker.SetActive(false);
        currentBuilding = null;
    }

    public void UpgradePressed(int upgradeIndex)
    {
        if (currentBuilding == null) return;
        currentBuilding.ApplyUpgrade(upgradeIndex);
    }
}