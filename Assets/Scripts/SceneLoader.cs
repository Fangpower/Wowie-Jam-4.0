using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int scene;
    [SerializeField] bool main;
    [SerializeField] GameObject instruct;

    public void Close(){
        instruct.SetActive(false);
    }
    
    public void OnClick(){
        SceneManager.LoadScene(scene);
    }
}
