using UnityEngine;

public enum PoisonType
{
    Health,
    Mana,
}

public class Poison : MonoBehaviour
{
    [SerializeField] private PoisonType poisonType;
    [SerializeField] private float increment = 5f;


    public void OnTriggerEnter(Collider other){
        Player player = other.GetComponent<Player>();
        if (player != null){
            if (poisonType == PoisonType.Health && player.getPercentVida() < 1f)
            {
                player.addVida(increment);
                Destroy(this.gameObject);
            }
            else if (poisonType == PoisonType.Mana && player.getPercentMana() < 1f)
            {
                player.addMana(increment);
                Destroy(this.gameObject);
            }
        }
    }
}
