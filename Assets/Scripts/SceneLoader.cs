using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int scene;
    [SerializeField] bool main;
    [SerializeField] GameObject instruct;

    private void Start(){
        if(main){
            StartCoroutine("Close");
        }
    }
    private IEnumerator Close(){
        yield return new WaitForSeconds(4f);
        instruct.SetActive(false);
    }
    public void OnClick(){
        SceneManager.LoadScene(scene);
    }
}
