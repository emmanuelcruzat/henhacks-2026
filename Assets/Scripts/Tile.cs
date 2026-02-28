using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    public Color greenColor;
    public Color redColor;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(isOccupied == true)
        {
            sr.color = redColor;
        }
        else
        {
            sr.color = greenColor;
        }
    }
}
