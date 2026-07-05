using UnityEngine;

public class Fight : MonoBehaviour
{
    public float tempoEntreAtaques = 15f;
    private float startTime = 0f;
    private bool PlayerRound = true;
    private bool inCombat = false;
    private float tempo_restante = 0f;

    void Start()
    {
        startTime = Time.time;       
    }

    // Switches the turn between the player and the boss and resets the timer.
    public void changeRound()
    {
        PlayerRound = !PlayerRound;
        startTime = Time.time;
    }


     // Returns true if it is currently the player's turn.
    public bool IsPlayerRound()
    {
        return PlayerRound;
    }


    void Update()
    {
        if (!inCombat) return;
        tempo_restante = tempoEntreAtaques - (Time.time - startTime);
        if(Time.time - startTime >= tempoEntreAtaques)
        {
            changeRound(); 
        }
    }

    // Starts the combat and initializes the turn timer.
    public void StartCombat()
    {
        inCombat = true;
        startTime = Time.time;
    }

    // Returns the remaining time of the current turn.
    public float getTempoRestante()
    {
        return tempo_restante;
    }
}
