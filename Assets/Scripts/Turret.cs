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
    [SerializeField] SpriteRenderer showAmmo;
    [SerializeField] Sprite[] ammoIcons;
    [SerializeField] float maxHealth;
    [SerializeField] Image healthBar;

    private AudioSource ad;
    private Transform closestEnemy;
    private Vector2 direction;
    private bool canFire = true;
    private float health;
    private float time;
    private float fireLevel;

    void Start(){
        health = maxHealth;
    }

    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5.25f, enemyMask);
        ad = GetComponent<AudioSource>();

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
            ad.pitch = 1 + Random.Range(-0.2f, 0.2f);
            ad.Play();
            StartCoroutine("CoolDown");
            currentAmmoAmt--;
            ammoText[currentAmmo].text = currentAmmoAmt.ToString();
            canFire = false;
        }

        Selection();

        healthBar.fillAmount = health/maxHealth;

        if(health <= 0){
            FindObjectOfType<EnemyWaves>().enabled = false;
            Enemy[] targ = FindObjectsOfType<Enemy>();
            foreach(Enemy e in targ){
                GameObject.Destroy(e.gameObject);
            }
            FindObjectOfType<Score>().StartCoroutine("ShowScore");
            this.enabled = false;
        }

        fireLevel = FindObjectOfType<Store>().fireLevel;
    }

    private IEnumerator CoolDown(){
        time = coolDownTime - fireLevel/10;
        yield return new WaitForSeconds(time);
        canFire = true;
        StopCoroutine("CoolDown");
    }

    private void Selection(){
        int tempChoice = 0;
        int choice = 0;
        foreach(TMP_Text text in ammoText){
            int temp;
            int.TryParse(text.text, out temp);
            if(temp > 2){
                choice = tempChoice;
            }
            tempChoice++;
        }
        currentAmmo = choice;
        showAmmo.sprite = ammoIcons[currentAmmo];
    }

    public void Damage(float damage){
        health-=damage;
    }
}
