using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Velocidade do projétil
    [SerializeField] private float damage = 20f; // Dano causado pelo projétil
    [SerializeField] private float lifetime = 5f; // Tempo de vida do projétil
    [SerializeField] private bool ApplyDamage_Player = false;
    private bool Lock = false;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public float GetDamage(){
        return damage;
    }

    // Detects collisions and applies damage to the appropriate target.
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Projetil Boss] Apply damage player -> " + ApplyDamage_Player);
        if (other.gameObject.GetComponent<Goblim>() && !Lock && other is CapsuleCollider)
        {   
            other.gameObject.GetComponent<Goblim>().ApplyDamage(damage);
            Lock = true;
        }
        if (ApplyDamage_Player && other.gameObject.GetComponent<Player>() && !Lock)
        {
            Debug.Log("[Projetil Boss] Player detetado");
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            Lock = true;
        }
    }
}
