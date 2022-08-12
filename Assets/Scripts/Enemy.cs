using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform centre;
    private Animator anim;
    private bool attacking;

    [SerializeField] float speed;
    [SerializeField] float knockBack;
    [SerializeField] float knockBackTime;
    [SerializeField] float health;
    [SerializeField] float damage;
    
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        centre = GameObject.Find("Turret").transform;
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if(Vector2.Distance(transform.position, centre.position) > 1.15f){
            transform.position = Vector2.MoveTowards(transform.position, centre.position, speed * Time.deltaTime);
            anim.SetBool("Attack", false);
        } else {
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
        rb.AddForce(dir * knockBack, ForceMode2D.Impulse);
        StartCoroutine("EndKnockBack");
        health-=damage;
    }

    private IEnumerator EndKnockBack(){
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
    }

    public void Die(){
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
