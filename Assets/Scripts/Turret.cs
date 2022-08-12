using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turret : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject[] bullet;
    [SerializeField] Transform barrel;
    [SerializeField] float coolDownTime = 1f;
    [SerializeField] float force;

    [SerializeField] TMP_Text[] ammoText;
    [SerializeField] int currentAmmo;

    [SerializeField] Image[] boxes;
    [SerializeField] Sprite[] boxLooks;

    private Transform closestEnemy;
    private Vector2 direction;
    private bool canFire = true;

    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 7f, enemyMask);

        foreach(Collider2D coll in enemies){
            if(closestEnemy == null){
                closestEnemy = coll.transform;
            } else if(Vector3.Distance(coll.transform.position, transform.position) < Vector3.Distance(closestEnemy.position, transform.position)){
                closestEnemy = coll.transform;
            } 
        }

        if(closestEnemy) direction = ((Vector2)closestEnemy.position - (Vector2)transform.position).normalized;
        transform.up = direction;

        int currentAmmoAmt;
        int.TryParse(ammoText[currentAmmo].text, out currentAmmoAmt);

        if(canFire && closestEnemy != null && currentAmmoAmt > 0){
            var temp = Instantiate(bullet[currentAmmo], barrel.position, Quaternion.identity);
            temp.transform.up = direction;
            temp.GetComponent<Bullet>().dir = direction;
            StartCoroutine("CoolDown");
            currentAmmoAmt--;
            ammoText[currentAmmo].text = currentAmmoAmt.ToString();
            canFire = false;
        }
    }

    private IEnumerator CoolDown(){
        yield return new WaitForSeconds(coolDownTime);
        canFire = true;
        StopCoroutine("CoolDown");
    }

    public void Selection(int choice){
        boxes[currentAmmo].sprite = boxLooks[0];
        currentAmmo = choice;
        boxes[currentAmmo].sprite = boxLooks[1];
    }
}
