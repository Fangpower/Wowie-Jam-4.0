using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Sprite[] cycle;

    private TMP_Text text;
    
    public int current;
    public bool done;

    private void Start()
    {
        StartCoroutine("Grow");

        switch(transform.name){
            case "Potato(Clone)": text = GameObject.Find("PotatoText").GetComponent<TMP_Text>(); break;
            case "Radish(Clone)": text = GameObject.Find("RadishText").GetComponent<TMP_Text>(); break;
            case "Carrot(Clone)": text = GameObject.Find("CarrotText").GetComponent<TMP_Text>(); break;
            case "Melon(Clone)": text = GameObject.Find("MelonText").GetComponent<TMP_Text>(); break;
        }
        int.TryParse(text.text, out current);
    }

    void Update(){
        int.TryParse(text.text, out current);
    }

    private IEnumerator Grow(){
        int x = 0;
        while(x < cycle.Length){
            yield return new WaitForSeconds(time);
            GetComponent<SpriteRenderer>().sprite = cycle[x];
            x++;
        }
        done = true;
    }

    public void Harvest(){
        current+=2;
        text.text = current.ToString();
        GameObject.Destroy(gameObject);
    }
}
