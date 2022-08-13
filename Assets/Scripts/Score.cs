using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text text;
    private float score = 0;

    [SerializeField] GameObject scoreMenu;
    [SerializeField] TMP_Text enemiesScore;
    [SerializeField] TMP_Text moneyScore;
    [SerializeField] TMP_Text finalScore;
    [SerializeField] GameObject menuButton;

    private void Start(){
        text = GetComponent<TMP_Text>();
    }

    public void UpdateScore(){
        score++;
        text.text = "Score: " + score.ToString();
    }

    public IEnumerator ShowScore(){
        scoreMenu.SetActive(true);
        enemiesScore.gameObject.SetActive(true);
        int curScore = 0;
        while(curScore < score){
            curScore++;
            enemiesScore.text = "Score: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        curScore = 0;
        float money = FindObjectOfType<Store>().totalMoney;
        moneyScore.gameObject.SetActive(true);
        while(curScore < money){
            curScore++;
            moneyScore.text = "Money: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        curScore = 0;
        float totalScore = score * money;
        finalScore.gameObject.SetActive(true);
        while(curScore < totalScore){
            curScore++;
            finalScore.text = "Total Score: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        menuButton.SetActive(true);
        yield return null;
    }
}
