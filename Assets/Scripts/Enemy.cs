using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform centre;

    [SerializeField] float speed;
    [SerializeField] float knockBack;
    [SerializeField] float knockBackTime;
    [SerializeField] float health;
    
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        centre = GameObject.Find("Turret").transform;
    }

    private void Update(){
        if(Vector2.Distance(transform.position, centre.position) > 1.15f){
            transform.position = Vector2.MoveTowards(transform.position, centre.position, speed * Time.deltaTime);
        }

        if(health <= 0){
            GameObject.Destroy(gameObject);
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
}
