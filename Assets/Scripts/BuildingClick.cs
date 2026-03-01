using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public string upgradeName;
        public int cost;
        [TextArea(2, 4)] public string description;

        // ✅ What prefab this upgrade turns into (successor)
        public GameObject successorPrefab;
    }

    [System.Serializable]
    public class BuildingInfo
    {
        public string buildingName;
        [TextArea(2, 4)] public string buildingDescription;
        public Sprite icon;
        public Upgrade[] upgrades;

        public int currentTier; // optional, track level
    }

    public BuildingInfo info;

    private UpgradePopupUI popupUI;

    void Awake()
    {
        popupUI = Object.FindFirstObjectByType<UpgradePopupUI>(FindObjectsInactive.Include);
    }

    void OnMouseDown()
    {
        popupUI?.Show(this); // pass the whole BuildingClick so UI can call Upgrade()
    }

    public void ApplyUpgrade(int upgradeIndex)
    {
        if (info == null || info.upgrades == null) return;
        if (upgradeIndex < 0 || upgradeIndex >= info.upgrades.Length) return;

        var upgrade = info.upgrades[upgradeIndex];
        if (upgrade.successorPrefab == null)
        {
            Debug.LogWarning("No successor prefab set for this upgrade.");
            return;
        }

        // Spawn successor at same transform + same parent
        Transform parent = transform.parent;
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        GameObject next = Instantiate(upgrade.successorPrefab, pos, rot, parent);

        // Optional: keep scale identical
        next.transform.localScale = transform.localScale;

        // Optional: carry over tier/state if successor has BuildingClick too
        var nextBC = next.GetComponent<BuildingClick>();
        if (nextBC != null)
        {
            // example: carry tier forward
            nextBC.info.currentTier = info.currentTier + 1;
        }

        // Destroy old
        Destroy(gameObject);

        // Optional: show popup for new building immediately
        if (popupUI != null && nextBC != null)
            popupUI.Show(nextBC);
    }
}