using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] Tilemap interactiveMap = null;
    [SerializeField] Tilemap pathMap = null;
    [SerializeField] Tile hoverTile = null;
    [SerializeField] Tile pathTile = null;
    [SerializeField] GameObject canvas;
    [SerializeField] LayerMask plantMask;

    private Vector3Int previousMousePos = new Vector3Int();
    private Vector3Int clickedMousePos = new Vector3Int();

    private void Start(){
        grid = GetComponent<Grid>();
    }

    private void Update(){
        Vector3Int mousePos = GetMousePosition();
        Collider2D[] underMouse = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.25f, plantMask);

        //Show highlighter tile
        if(!mousePos.Equals(previousMousePos) && !canvas.activeSelf){
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }

        //Make plant
        if(Input.GetMouseButtonDown(0) && underMouse.Length == 0 && !canvas.activeSelf){
            Time.timeScale = 0.25f;
            clickedMousePos = mousePos;
            canvas.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            canvas.SetActive(true);
        }   

        //Harvest plant
        if(Input.GetMouseButtonDown(1) && underMouse.Length == 1){
            if(underMouse[0].GetComponent<Plant>().done){
                underMouse[0].GetComponent<Plant>().Harvest();
            }
        }
    }

    private Vector3Int GetMousePosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    public void OnClick(GameObject plant){
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
        canvas.SetActive(false);
    }
}
