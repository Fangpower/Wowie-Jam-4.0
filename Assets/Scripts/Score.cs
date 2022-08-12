using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text text;
    private float score = 0;

    private void Start(){
        text = GetComponent<TMP_Text>();
    }

    public void UpdateScore(){
        score++;
        text.text = score.ToString();
    }
}
