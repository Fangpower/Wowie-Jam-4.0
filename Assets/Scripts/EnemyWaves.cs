using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] GameObject[] zombies;
    [SerializeField] GameObject radishLord;
    [SerializeField] GameObject carrotLord;
    [SerializeField] GameObject melonLord;
    [SerializeField] float speed;
    [SerializeField] float baseTime;

    private float deltaTime;
    private float percentage;

    private bool radishBoss;
    private bool carrotBoss;
    private bool melonBoss;

    public int zombieNum = 0;

    void Start()
    {
        StartCoroutine("Waves");
    }

    private void Update(){
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //print(Mathf.Ceil(fps));
        baseTime -= speed * Time.deltaTime;
        baseTime = Mathf.Clamp(baseTime, 0.5f, 5);
        if(!radishBoss && (baseTime >= 3.999 && baseTime <= 4.001)){
            var temp = Instantiate(radishLord, new Vector2(Random.Range(-5, 6), 6.5f), Quaternion.identity);
            temp.transform.SetParent(transform);
            radishBoss = true;
        }
        if(!carrotBoss && (baseTime >= 2.999 && baseTime <= 3.001)){
            var temp = Instantiate(carrotLord, new Vector2(Random.Range(-5, 6), 6.5f), Quaternion.identity);
            temp.transform.SetParent(transform);
            carrotBoss = true;
        }if(!melonBoss && (baseTime >= 1.999 && baseTime <= 2.001)){
            var temp = Instantiate(melonLord, new Vector2(Random.Range(-5, 6), 6.5f), Quaternion.identity);
            temp.transform.SetParent(transform);
            melonBoss = true;
        }
    }

    private IEnumerator Waves(){
        while(true){
            yield return new WaitForSeconds(baseTime);
            
            int dir = Random.Range(0, 4);
            Vector2 randPos = new Vector2(0,0);

            switch(dir){
                case 0: randPos = new Vector2(-5.5f, Random.Range(-5, 6)); break;
                case 1: randPos = new Vector2(6.5f, Random.Range(-5, 6)); break;
                case 2: randPos = new Vector2(Random.Range(-5, 6), -5.5f); break;
                case 3: randPos = new Vector2(Random.Range(-5, 6), 6.5f); break;
            }

            

            var temp = Instantiate(zombies[zombieNum], randPos, Quaternion.identity);
            temp.transform.SetParent(transform);
        }
    }
}
