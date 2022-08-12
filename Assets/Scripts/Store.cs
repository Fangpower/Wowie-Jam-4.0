using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private float money = 0;

    private void Start(){
        
    }

    public void UpdateMoney(){
        money+=Random.Range(5, 10);
        text.text = money.ToString();
    }
}
