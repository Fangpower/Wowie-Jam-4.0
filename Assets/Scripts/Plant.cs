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
    [SerializeField] AudioClip audioC;

    private TMP_Text text;
    private Store store;
    private AudioSource audioS;
    private float growLevel;
    public float time;
    
    public int current;
    public bool done;

    private void Start()
    {
        store = FindObjectOfType<Store>();
        growLevel = store.growLevel;
        time = baseTime - growLevel/10;
        StartCoroutine("Grow");

        switch(transform.name){
            case "Potato(Clone)": text = GameObject.Find("PotatoText").GetComponent<TMP_Text>(); break;
            case "Radish(Clone)": text = GameObject.Find("RadishText").GetComponent<TMP_Text>(); break;
            case "Carrot(Clone)": text = GameObject.Find("CarrotText").GetComponent<TMP_Text>(); break;
            case "Melon(Clone)": text = GameObject.Find("MelonText").GetComponent<TMP_Text>(); break;
        }
        int.TryParse(text.text, out current);

        audioS = GameObject.Find("Grid").GetComponent<AudioSource>();
        audioS.clip = audioC;
        audioS.pitch = 1 + Random.Range(-0.1f, 0.1f);
        audioS.Play();
        
    }

    void Update(){
        int.TryParse(text.text, out current);
        growLevel = store.growLevel;
        time = baseTime - growLevel/10;
    }

    private IEnumerator Grow(){
        int x = 0;
        time = baseTime - growLevel/10;
        while(x < cycle.Length){
            yield return new WaitForSeconds(time);
            GetComponent<SpriteRenderer>().sprite = cycle[x];
            x++;
        }
        readyPart.Play();
        done = true;
    }

    public void Harvest(){
        current += Random.Range(2 + (int)store.harvestLevel/2, 4 + (int)store.harvestLevel/2);
        text.text = current.ToString();
        harvestPart.transform.parent = null;
        harvestPart.Play();
        audioS.clip = audioC;
        audioS.pitch = 1 + Random.Range(-0.2f, 0.2f);
        audioS.Play();
        GameObject.Destroy(gameObject);
    }
}
