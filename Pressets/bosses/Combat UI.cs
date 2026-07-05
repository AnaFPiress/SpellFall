using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Atacante, TimerText;
    [SerializeField] private Slider slider;
    [SerializeField] private Button AttackButton;
    private Player player;
    private Fight fight;
    private bool LastRound;

    void Start()
    {
        fight = GameObject.FindAnyObjectByType<Fight>();
        if (fight == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (slider != null) slider.maxValue = fight.tempoEntreAtaques;
        player = GameObject.FindAnyObjectByType<Player>();
    }

    void Update(){
        if (Atacante != null) Atacante.text = fight.IsPlayerRound() ? "Player" : "Boss";
        if (slider != null) slider.value = fight.getTempoRestante();
        if (TimerText != null) TimerText.text = ((int) fight.getTempoRestante()).ToString();
        if (AttackButton != null)
        {
            if (LastRound != fight.IsPlayerRound())
            {
                LastRound = fight.IsPlayerRound();
                AttackButton.interactable = LastRound;
            }
        }
    }

    // Starts the player's attack sequence when the attack button is pressed.
    public void PlayerAttack()
    {
        if (AttackButton != null) AttackButton.interactable = false;
        Bosses bosses = GameObject.FindAnyObjectByType<Bosses>();
        if (bosses == null) return;
        
        StartCoroutine(DelayAnimator(bosses));
    }

     // Plays the attack animation, applies damage to the boss and ends the player's turn.
    private IEnumerator DelayAnimator(Bosses bosses)
    {
        StartCoroutine(player.Attack());
        yield return new WaitForSeconds(1.5f);
        bosses.TakeDamage(5);
        yield return new WaitForSeconds(1.5f);
        fight.changeRound();
    }
}
