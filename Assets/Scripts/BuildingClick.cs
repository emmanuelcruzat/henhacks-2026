using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public string upgradeName;
        public int cost;
        [TextArea(2, 4)] public string description;
    }

    [System.Serializable]
    public class BuildingInfo
    {
        public string buildingName;
        [TextArea(2, 4)] public string buildingDescription;
        public Sprite icon;
        public Upgrade[] upgrades;
    }

    public BuildingInfo info; // editable in Inspector

    private UpgradePopupUI popupUI;

    void Awake()
    {
        popupUI = Object.FindFirstObjectByType<UpgradePopupUI>(FindObjectsInactive.Include);
        if (popupUI == null)
            Debug.LogError("No UpgradePopupUI found in the scene!");
    }

    void OnMouseDown()
    {
        if (popupUI != null && info != null)
        {
            popupUI.Show(info);   // ✅ CALL the method
        }
    }
}