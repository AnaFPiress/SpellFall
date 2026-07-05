using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Goblim : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float health = 20f;
    [SerializeField] private CapsuleCollider capsule;
    [SerializeField] private Slider HealthSlider;
    private Animator _animation;
    private BoxCollider _boxCollider;
    private Player _player;
    private float _nextAttackTime = 0f;
    private bool attack = false;
    private bool DeadAnimatorLock = false;
    private bool Lock = false;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _animation = GetComponent<Animator>();
        if (HealthSlider != null)
        {
            HealthSlider.maxValue = health;
            HealthSlider.value = health;
        }
    }

    void Update()
    {
        if (Lock)
        {
            _animation.SetBool("Run", false);
            _player = null;
        }
        if (_player != null && Vector3.Distance(transform.position, _player.transform.position) < 1.5f)
        {
            attack = true;
        }
        else attack = false;
        if (_player != null && !attack)
        {
            transform.LookAt(_player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (_animation != null)
        {
            _animation.SetBool("Run", _player != null && !attack);
            if (attack 
            && !_animation.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            && Time.time - _nextAttackTime > 1f)
            {
                _animation.SetTrigger("Attack");
                StartCoroutine(PlayerDamage());
                _nextAttackTime = Time.time;
            }
        }
        if (health == 0 && !DeadAnimatorLock)
        {
            if (HealthSlider != null) Destroy(HealthSlider);
            DeadAnimatorLock = true;
            _animation.SetTrigger("Dead");
            Destroy(gameObject,1.7f);
            Lock = true;
        }
    }

     // Applies damage to the player after the attack animation delay.
    private IEnumerator PlayerDamage()
    {
        yield return new WaitForSeconds(1f);
        if (_player != null) _player.TakeDamage(damage);
    }

    // Reduces the enemy's health and updates the health bar.
    public void ApplyDamage(float damage)
    {
        health -= damage;
        health = (health < 0) ? 0 : health;
        if (HealthSlider != null) HealthSlider.value = health;
    }

     // Detects the player or projectile entering the enemy's detection area.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _player = other.gameObject.GetComponent<Player>();   
        }
        if (other.gameObject.GetComponent<Projetil>() != null)
        {
            _player = GameObject.FindFirstObjectByType<Player>();
        }
    }

    // Stops tracking the player when leaving the detection area.
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _player = null;
        }
    }
}
