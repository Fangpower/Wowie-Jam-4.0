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

    private float money = 0;

    private void Start(){
        
    }

    public void UpdateMoney(){
        money+=Random.Range(5, 10);
        text.text = money.ToString();
    }

    public void UpgradeGrow(){
        if(money >= growCost){
            money -= growCost;
            text.text = money.ToString();
            growCost = (int)(growCost * 1.5f);
            growCostText.text = "Cost: " + growCost.ToString();
            growLevel++;
            growLevelText.text = "Level: " + growLevel;
        }
    }

    public void UpgradeHarvest(){
        if(money >= harvestCost){
            money -= harvestCost;
            text.text = money.ToString();
            harvestCost = (int)(harvestCost * 1.5f);
            harvestCostText.text = "Cost: " + harvestCost.ToString();
            harvestLevel++;
            harvestLevelText.text = "Level: " + harvestLevel;
        }
    }
}
