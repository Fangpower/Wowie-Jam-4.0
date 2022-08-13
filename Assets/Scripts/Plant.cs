using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    [SerializeField] float baseTime;
    [SerializeField] Sprite[] cycle;
    [SerializeField] ParticleSystem readyPart;
    [SerializeField] ParticleSystem harvestPart;

    private TMP_Text text;
    private Store store;
    private float growLevel;
    public float time;
    
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

        store = FindObjectOfType<Store>();
    }

    void Update(){
        int.TryParse(text.text, out current);
        growLevel = store.growLevel;
        time = baseTime - growLevel/10;
    }

    private IEnumerator Grow(){
        int x = 0;
        while(x < cycle.Length){
            yield return new WaitForSeconds(time);
            GetComponent<SpriteRenderer>().sprite = cycle[x];
            x++;
        }
        readyPart.Play();
        done = true;
    }

    public void Harvest(){
        current += Random.Range(2 + (int)store.harvestLevel/4, 4 + (int)store.harvestLevel/4);
        text.text = current.ToString();
        harvestPart.transform.parent = null;
        harvestPart.Play();
        GameObject.Destroy(gameObject);
    }
}
