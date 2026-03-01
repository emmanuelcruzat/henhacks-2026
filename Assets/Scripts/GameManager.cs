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
    public int water;
    public TextMeshProUGUI waterDisplay;
    public int energy;
    public TextMeshProUGUI energyDisplay;
    public int happiness;
    public TextMeshProUGUI HappinessDisplay;
    public int ore;
    public TextMeshProUGUI oreDisplay;
    public int food;
    public TextMeshProUGUI foodDisplay;
    public int housingOptions;
    public TextMeshProUGUI newHouseDisplay;
    public GameObject newHouseSelect;

    private Building buildingToPlace;
    public GameObject grid;

    public float Health, MaxHealth;

    [SerializeField]
    private sustainabilityBarUI healthBar; 

    public CustomCursor customCursor;

    public Tile[] tiles;

    private void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    private void Update()
    {
        goldDisplay.text = gold.ToString();
        waterDisplay.text = water.ToString();
        energyDisplay.text = energy.ToString();
        HappinessDisplay.text = happiness.ToString();
        oreDisplay.text = ore.ToString();
        foodDisplay.text = food.ToString();

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null) 
        {
            Tile nearestTile = null;
            float shortestDistance = float.MaxValue;
            foreach(Tile tile in tiles)
            {
                //checks cursor's position
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestTile = tile;
                }
            }
            if(nearestTile.isOccupied == false)
            {
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                buildingToPlace = null;
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
        if (housingOptions <= 9)
        {
            newHouseDisplay.fontSize = 32;
        }
        else if (housingOptions <= 99 && housingOptions >= 10)
        {
            newHouseDisplay.fontSize = 24;
        }
        // health test
        if (Input.GetKeyDown("d"))
        {
            SetHealth(-1f);
        }
        if (Input.GetKeyDown("a"))
        {
            SetHealth(1f);
        }

    }

    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
    public void BuyBuilding(Building building)
    {
        if (gold >= building.cost)
        {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            gold -= building.cost;
            buildingToPlace = building;
            grid.SetActive(true);
        }
    }
    public void SelectHouse()
    {
        newHouseSelect.SetActive(newHouseSelect.activeSelf);
    }
}
