using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string speakerName;
    public Sprite portrait;
    [TextArea(2, 5)]
    public string[] lines;
}