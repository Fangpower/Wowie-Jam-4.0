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

    public void UpdateScore(float newS){
        score += newS;
        text.text = "Score: " + score.ToString();
    }

    public IEnumerator ShowScore(){
        scoreMenu.SetActive(true);
        enemiesScore.gameObject.SetActive(true);
        float curScore = 0;
        while(curScore < score){
            curScore = Mathf.Clamp(curScore += 76, 0, score);
            enemiesScore.text = "Score: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        yield return new WaitForSeconds(0.5f);

        curScore = 0;
        float money = FindObjectOfType<Store>().totalMoney;
        print(money);
        moneyScore.gameObject.SetActive(true);
        while(curScore < money){
            curScore = Mathf.Clamp(curScore += 76, 0, money);
            moneyScore.text = "Money: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        yield return new WaitForSeconds(0.5f);

        curScore = 0;
        float totalScore = score + money;
        finalScore.gameObject.SetActive(true);
        while(curScore < totalScore){
            curScore = Mathf.Clamp(curScore += 76, 0, totalScore);
            finalScore.text = "Total Score: " + curScore.ToString();
            yield return new WaitForSeconds(0.00001f);
        }
        yield return new WaitForSeconds(0.5f);
        menuButton.SetActive(true);
        yield return null;
    }
}
