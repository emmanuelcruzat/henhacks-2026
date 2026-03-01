using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    public BuildingData buildingData;

    private UpgradePopupUI popupUI;

    void Awake()
    {
        // Unity 6+: FindFirstObjectByType is the replacement
        popupUI = Object.FindFirstObjectByType<UpgradePopupUI>();

        if (popupUI == null)
            Debug.LogError("No UpgradePopupUI found in the scene!");
    }

    void OnMouseDown()
    {
        if (popupUI != null && buildingData != null)
            popupUI.Show(buildingData);
    }
}