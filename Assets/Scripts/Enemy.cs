using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform centre;
    private Animator anim;
    private bool attacking;
    private AudioSource ad;

    [SerializeField] float speed;
    [SerializeField] float knockBack;
    [SerializeField] float knockBackTime;
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] bool isBoss;
    
    
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        centre = GameObject.Find("Turret").transform;
        anim = GetComponent<Animator>();
        ad = GetComponent<AudioSource>();
    }

    private void Update(){
        if(Vector2.Distance(transform.position, centre.position) > 1.15f && health > 0){
            transform.position = Vector2.MoveTowards(transform.position, centre.position, speed * Time.deltaTime);
            anim.SetBool("Attack", false);
        } else if(health > 0) {
            anim.SetBool("Attack", true);
            if(!attacking) StartCoroutine("Attack");
        }

        if(health <= 0){
            anim.SetTrigger("Die");
        }

        if(transform.position.x < centre.position.x){
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Hit(Vector2 dir, float damage){
        health-=damage;
        if(health > 0) rb.AddForce(dir * knockBack, ForceMode2D.Impulse);
        ad.pitch = 1 + Random.Range(-0.2f, 0.2f);
        ad.Play();
        StartCoroutine("EndKnockBack");
    }

    private IEnumerator EndKnockBack(){
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
    }

    public void Die(){
        rb.velocity = Vector2.zero;
        GameObject.Find("ScoreText").GetComponent<Score>().UpdateScore();
        GameObject.Find("Store").GetComponent<Store>().UpdateMoney();
        
        if(isBoss){
            TMP_Text bossAmmo = null;
            switch(name){
                case "Radish Lord(Clone)": bossAmmo = GameObject.Find("RadishText").GetComponent<TMP_Text>(); break;
                case "Carrot Lord(Clone)": bossAmmo = GameObject.Find("CarrotText").GetComponent<TMP_Text>(); break;
            }
            bossAmmo.text = (5).ToString();
            FindObjectOfType<Turret>().RestoreHealth();
        }
        GameObject.Destroy(gameObject);
    }

    private IEnumerator Attack(){
        attacking = true;
        yield return new WaitForSeconds(1f);
        while(anim.GetBool("Attack")){
            centre.gameObject.GetComponent<Turret>().Damage(damage);
            yield return new WaitForSeconds(5f);
        }
        attacking = false;
        StopCoroutine("Attack");
    }
}
