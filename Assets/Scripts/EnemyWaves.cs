using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] Vector4 minMax;

    float deltaTime;

    void Start()
    {
        StartCoroutine("Waves");
    }

    private void Update(){
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //print(Mathf.Ceil(fps));
    }

    private IEnumerator Waves(){
        while(true){
            yield return new WaitForSeconds(2.5f);
            
            int dir = Random.Range(0, 4);
            Vector2 randPos = new Vector2(0,0);

            switch(dir){
                case 0: randPos = new Vector2(-5.5f, Random.Range(-5, 6)); break;
                case 1: randPos = new Vector2(6.5f, Random.Range(-5, 6)); break;
                case 2: randPos = new Vector2(Random.Range(-5, 6), -5.5f); break;
                case 3: randPos = new Vector2(Random.Range(-5, 6), 6.5f); break;
            }

            Instantiate(enemy1, randPos, Quaternion.identity);
        }
    }
}
