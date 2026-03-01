using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class UpgradePopupUI : MonoBehaviour
{
    [Header("Root")]
    public GameObject panel;

    public GameManager gameManager; // to check resources for upgrades (optional)

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

    private int tempCost = 1000000; // placeholder, replace with actual cost from upgrade data

    void Awake()
    {
        if (closeButton != null) closeButton.onClick.AddListener(Hide);
        if (upgradeButton != null) upgradeButton.onClick.AddListener(() => UpgradePressed());

        Hide();
    }

    public bool IsOpen => panel != null && panel.activeSelf;

    public void Show(BuildingClick building)
    {
        if (building == null || building.info == null) return;

        currentBuilding = building;
        if(building.info.upgrades != null && building.info.upgrades.Length > 0)
        {
            tempCost = building.info.upgrades[0].cost; // get cost of first upgrade (index 0)
        }
        else
        {
            tempCost = 1000000; // no upgrades, set to very high cost to disable button
        }

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

private void Update() {
    if (gameManager.gold >= tempCost)
    {        upgradeButton.interactable = true;
    }
    else
    {
        upgradeButton.interactable = false;
    }
}
    public void Hide()
    {
        if (panel != null) panel.SetActive(false);
        if (blocker != null) blocker.SetActive(false);
        currentBuilding = null;
    }

    public void UpgradePressed()
    {
        if (currentBuilding == null) return;
        gameManager.gold -= currentBuilding.info.upgrades[0].cost; // deduct cost
        currentBuilding.ApplyUpgrade(0);
        
    }
}