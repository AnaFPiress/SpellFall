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

    public void changeRound()
    {
        PlayerRound = !PlayerRound;
        startTime = Time.time;
    }



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

    public void StartCombat()
    {
        inCombat = true;
        startTime = Time.time;
    }

    public float getTempoRestante()
    {
        return tempo_restante;
    }
}
