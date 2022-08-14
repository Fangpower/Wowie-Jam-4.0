using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform centre;
    private Animator anim;
    private bool attacking;
    private AudioSource ad;
    private Vector3 bossOffset;

    private GameObject bossUI;
    private Image bossHealth;
    private TMP_Text bossTitle;
    private float bossMaxHealth;
    private EnemyWaves ew;

    [SerializeField] float speed;
    [SerializeField] float knockBack;
    [SerializeField] float knockBackTime;
    [SerializeField] float health;
    [SerializeField] float damage;
    public bool isBoss;
    [SerializeField] ParticleSystem attackPart;
    [SerializeField] float extraMoney;
    [SerializeField] float extraScore = 1;

    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip hurt;
    
    
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        centre = GameObject.Find("Turret").transform;
        anim = GetComponent<Animator>();
        ad = GetComponent<AudioSource>();
        bossOffset = new Vector3(0, 0.5f);

        ew = FindObjectOfType<EnemyWaves>();

        if(isBoss){
            bossUI = GameObject.Find("BossUI").transform.GetChild(0).gameObject;
            bossUI.SetActive(true);
            bossHealth = GameObject.Find("BossHealthBar").GetComponent<Image>();
            bossTitle = GameObject.Find("BossTitle").GetComponent<TMP_Text>();
            bossMaxHealth = health;
            bossTitle.text = name.TrimEnd(new char[]{'(', 'C', 'l', 'o', 'n', 'e', ')'});
        }
    }

    private void Update(){
        if(Vector2.Distance(transform.position, centre.position + bossOffset) > 1.15f && health > 0){
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

        if(isBoss){
            bossHealth.fillAmount = health/bossMaxHealth;
        }
    }

    public void Hit(Vector2 dir, float damage){
        health-=damage;
        if(health > 0) rb.AddForce(dir * knockBack, ForceMode2D.Impulse);
        Sound(hurt);
        StartCoroutine("EndKnockBack");
    }

    void Sound(AudioClip clip){
        ad.clip = clip;
        ad.pitch = 1 + Random.Range(-0.2f, 0.2f);
        ad.Play();
    }

    private IEnumerator EndKnockBack(){
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
    }

    public void Die(){
        rb.velocity = Vector2.zero;
        GameObject.Find("ScoreText").GetComponent<Score>().UpdateScore(extraScore);
        GameObject.Find("Store").GetComponent<Store>().UpdateMoney(extraMoney);
        
        if(isBoss){
            TMP_Text bossAmmo = null;
            switch(name){
                case "Radish Lord(Clone)": ew.ActivateAm(1); bossAmmo = GameObject.Find("RadishText").GetComponent<TMP_Text>(); break;
                case "Carrot Lord(Clone)": ew.ActivateAm(2); bossAmmo = GameObject.Find("CarrotText").GetComponent<TMP_Text>(); break;
                case "Melon Lord(Clone)": ew.ActivateAm(3); bossAmmo = GameObject.Find("MelonText").GetComponent<TMP_Text>(); break;
            }
            bossAmmo.text = (20).ToString();
            FindObjectOfType<Turret>().RestoreHealth();
            ew.zombieNum++;
            bossUI.SetActive(false);
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

    public void PlayAttack(){
        Sound(attack);
        attackPart.Play();
    }

    public void WinGame(){
        rb.velocity = Vector2.zero;
        GameObject.Find("ScoreText").GetComponent<Score>().UpdateScore(extraScore);
        GameObject.Find("Store").GetComponent<Store>().UpdateMoney(extraMoney);
        FindObjectOfType<Turret>().victoryPart.Play();
        FindObjectOfType<Turret>().enabled = false;
        ew.done = true;
        bossUI.SetActive(false);
        gameObject.layer = 0;
        
        StartCoroutine("EndGame");
    }

    private IEnumerator EndGame(){
        TMP_Text vic = GameObject.Find("GameState").GetComponent<TMP_Text>();
        vic.text = "V";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Vi";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Vic";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Vict";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Victo";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Victor";
        yield return new WaitForSeconds(0.1f);
        vic.text = "Victory";
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(3.3f);
        GameObject.Find("GameState").GetComponent<TMP_Text>().text = "";
        FindObjectOfType<Score>().StartCoroutine("ShowScore");
    }
}
