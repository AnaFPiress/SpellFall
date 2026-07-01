using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bosses : MonoBehaviour
{
[SerializeField] private float damage = 10f;
[SerializeField] private float AttackInterval = 2f, health = 50;
[SerializeField] private Slider _health_slider;
[SerializeField] private ProjetilSpawner[] spawners;
private bool Dead = false;
private Animator animator;
private Fight fight;
private FinalBoss finalboss;
private Player player;
private bool lockAttack = false;

    void Start()
    {
        fight = GameObject.FindAnyObjectByType<Fight>();
        player = GameObject.FindAnyObjectByType<Player>();
        animator = GetComponent<Animator>();
        if (_health_slider != null) _health_slider.maxValue = health;
    }

 
    void Update()
    {
        if (fight == null) return;
        if (player == null) return;
        if (!fight.IsPlayerRound() && !lockAttack)
        {
            lockAttack = true;
            StartCoroutine(AttackDelay());
        }
        if (_health_slider != null) _health_slider.value = health;
    }


    private IEnumerator AttackDelay()
    {
        if (animator != null) animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        player.TakeDamage(damage);
        yield return new WaitForSeconds(AttackInterval / 2f);
        if (spawners.Length != 0){
            foreach(ProjetilSpawner s in spawners){
                s.SpawnProjetil();
            }
        }
        yield return new WaitForSeconds(AttackInterval / 2f);
        fight.changeRound();
        lockAttack = false;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage,0,health);
        if (health == 0) Dead = true;
        if (animator != null)
        {
            if (Dead)
            {
                animator.SetTrigger("Dead");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else animator.SetTrigger("Damage");
        }
    }

    public bool isDead(){ return Dead; }
}
