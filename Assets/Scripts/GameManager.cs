using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldDisplay;

    private void Update()
    {
        goldDisplay.text = gold.ToString();
    }
}
