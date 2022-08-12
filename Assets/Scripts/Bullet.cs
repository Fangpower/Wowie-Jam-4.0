using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float force;
    [SerializeField] float damage;
    [SerializeField] bool perice;
    [SerializeField] bool bomb;
    [SerializeField] float bombSize;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] ParticleSystem breakPart;

    public Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir * force, ForceMode2D.Impulse);
        StartCoroutine("Death");
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy")){
            if(!bomb) col.GetComponent<Enemy>().Hit(dir, damage);
            else{
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombSize, enemyMask);
                foreach(Collider2D coll in enemies){
                    coll.GetComponent<Enemy>().Hit(dir, damage);
                }
            }
            if(!perice) Die();
        }
    }

    private IEnumerator Death(){
        yield return new WaitForSeconds(5f);
        Die();
    }

    private void Die(){
        breakPart.transform.parent = null;
        breakPart.Play();
        GameObject.Destroy(gameObject);
    }
}
