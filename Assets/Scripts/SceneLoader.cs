using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int scene;
    public void OnClick(){
        SceneManager.LoadScene(scene);
    }
}
