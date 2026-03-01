using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;

    public int increaseAmount;

    public float timeBtwIncreases;
    private float nextIncreaseTime;

    public enum ResourceType
    {
        Gold,
        Food,
        Ore,
        Water,
        Happiness,
        Energy
    }
    public ResourceType buildingType;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Time.time > nextIncreaseTime)
        {
            nextIncreaseTime = Time.time + timeBtwIncreases;
            ApplyResourceIncrease();
        }
    }
    public void ApplyResourceIncrease()
    {
        switch (buildingType)
        {
            case ResourceType.Gold:
                gm.gold += increaseAmount;
                break;
            case ResourceType.Water:
                gm.water += increaseAmount;
                break;
            case ResourceType.Energy:
                gm.energy += increaseAmount;
                break;
            case ResourceType.Food:
                gm.food += increaseAmount;
                break;
            case ResourceType.Ore:
                gm.ore += increaseAmount;
                break;
            case ResourceType.Happiness:
                gm.happiness += increaseAmount;
                break;
        }
    }
}
