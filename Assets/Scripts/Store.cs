using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [SerializeField] int growCost;
    [SerializeField] TMP_Text growCostText;
    [SerializeField] TMP_Text growLevelText;
    public float growLevel;

    [SerializeField] int harvestCost;
    [SerializeField] TMP_Text harvestCostText;
    [SerializeField] TMP_Text harvestLevelText;
    public float harvestLevel;

    [SerializeField] int fireCost;
    [SerializeField] TMP_Text fireCostText;
    [SerializeField] TMP_Text fireLevelText;
    public float fireLevel;

    [SerializeField] int coinCost;
    [SerializeField] TMP_Text coinCostText;
    [SerializeField] TMP_Text coinLevelText;
    public int coinLevel;

    private float money = 0;
    public float totalMoney = 0;

    private void Start(){
        
    }

    public void UpdateMoney(){
        money += (int)Random.Range(5 + coinLevel, 10 + coinLevel);
        totalMoney += money;
        text.text = "Money: " + money.ToString();
    }

    public void UpgradeGrow(){
        if(money >= growCost){
            money -= growCost;
            text.text = "Money: " + money.ToString();
            growCost = (int)(growCost * 1.5f);
            growCostText.text = "Cost: " + growCost.ToString();
            growLevel++;
            growLevelText.text = "Level: " + growLevel;
        }
    }

    public void UpgradeHarvest(){
        if(money >= harvestCost){
            money -= harvestCost;
            text.text = "Money: " + money.ToString();
            harvestCost = (int)(harvestCost * 1.5f);
            harvestCostText.text = "Cost: " + harvestCost.ToString();
            harvestLevel++;
            harvestLevelText.text = "Level: " + harvestLevel;
        }
    }

    public void UpgradeFire(){
        if(money >= fireCost){
            money -= fireCost;
            text.text = "Money: " + money.ToString();
            fireCost = (int)(fireCost * 1.5f);
            fireCostText.text = "Cost: " + fireCost.ToString();
            fireLevel++;
            fireLevelText.text = "Level: " + fireLevel;
        }
    }

    public void UpgradeCoin(){
        if(money >= coinCost){
            money -= coinCost;
            text.text = "Money: " + money.ToString();
            coinCost = (int)(coinCost * 1.5f);
            coinCostText.text = "Cost: " + coinCost.ToString();
            coinLevel++;
            coinLevelText.text = "Level: " + coinLevel;
        }
    }
}
