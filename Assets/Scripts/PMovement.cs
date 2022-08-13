using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float speed = 5;
    [SerializeField] Animator anim;

    public Vector3 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position != targetPos){
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            anim.SetBool("Walk", true);
        } else {
            anim.SetBool("Walk", false);
        }
    }
}
