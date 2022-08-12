using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] Tilemap interactiveMap = null;
    [SerializeField] Tile hoverTile = null;
    [SerializeField] GameObject canvas;
    [SerializeField] LayerMask plantMask;
    [SerializeField] LayerMask dirtMask;
    [SerializeField] GameObject plant;

    private Vector3Int previousMousePos = new Vector3Int();

    private void Start(){
        grid = GetComponent<Grid>();
    }

    private void Update(){
        Vector3Int mousePos = GetMousePosition();
        Collider2D[] underMouse = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.25f, plantMask);
        Collider2D[] dirtUnderMouse = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f, dirtMask);

        //Show highlighter tile
        if(!mousePos.Equals(previousMousePos) && !canvas.activeSelf){
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }

        //Make plant
        if(Input.GetMouseButtonDown(0) && underMouse.Length == 0 && !canvas.activeSelf && dirtUnderMouse.Length == 1){
            OnPlant();
        } else if(Input.GetMouseButtonDown(0) && underMouse.Length == 1){
            if(underMouse[0].GetComponent<Plant>().done){
                underMouse[0].GetComponent<Plant>().Harvest();
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    private Vector3Int GetMousePosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    public void OnClick(GameObject _plant){
        plant = _plant;
        canvas.SetActive(false);
    }

    void OnPlant(){
        TMP_Text text = null;
        int current;
        switch(plant.name){
            case "Potato": text = GameObject.Find("PotatoText").GetComponent<TMP_Text>(); break;
            case "Radish": text = GameObject.Find("RadishText").GetComponent<TMP_Text>(); break;
            case "Carrot": text = GameObject.Find("CarrotText").GetComponent<TMP_Text>(); break;
            case "Melon": text = GameObject.Find("MelonText").GetComponent<TMP_Text>(); break;
        }
        int.TryParse(text.text, out current);
        if(current >= 1){
            Instantiate(plant, previousMousePos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            current--;
            text.text = current.ToString();
        }
    }
}
