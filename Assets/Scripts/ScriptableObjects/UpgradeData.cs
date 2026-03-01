using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string name;
    [TextArea(1,3)] public string description;
    public int cost;
}

[CreateAssetMenu(menuName = "Buildings/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public Upgrade[] upgrades;
}