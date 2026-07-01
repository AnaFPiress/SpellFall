using System;
using UnityEngine;

public enum PlatePuzzleSignal
{
    Equals,
    GreaterThan,
    LessThan
}

[System.Serializable]
public struct PuzzleCondition
{
    public PressPlate plate1, plate2;
    public PlatePuzzleSignal signal;
    public GameObject Sinal;
    public Material MaterialOn, MaterialOff;
}

public class PlatesPuzzle : MonoBehaviour
{
    [SerializeField] private PuzzleCondition[] conditions;
    [SerializeField] private GameObject Gate;

    void Update()
    {
        PuzzleSignal();
    }

    public void PuzzleSignal(){
        foreach (PuzzleCondition condition in conditions){
            if (CheckConditions(condition))
            {
                
                condition.Sinal.GetComponent<Renderer>().material = condition.MaterialOn;
                if (Gate != null) Destroy(Gate);
            }
            else
                condition.Sinal.GetComponent<Renderer>().material = condition.MaterialOff;
        }
    }

    private bool CheckConditions(PuzzleCondition condition)
    {
        float weight1 = condition.plate1.getWeight();
        float weight2 = condition.plate2.getWeight();
        if (weight1 == 0 || weight2 == 0) return false;
        switch (condition.signal)
        {
            case PlatePuzzleSignal.Equals:
                if (weight1 != weight2) return false;
                break;
            case PlatePuzzleSignal.GreaterThan:
                if (weight1 <= weight2) return false;
                break;
            case PlatePuzzleSignal.LessThan:
                if (weight1 >= weight2) return false;
                break;
        }
        return true;
    }

    public bool allCondition()
    {
        foreach (PuzzleCondition condition in conditions){
            if (!CheckConditions(condition)) return false;
        }
        return true;
    }
}
