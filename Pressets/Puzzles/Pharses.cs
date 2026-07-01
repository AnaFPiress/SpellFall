using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pharses : MonoBehaviour
{
    [SerializeField] private List<EmptySpace> emptyspace;
    [SerializeField] private Button NextBtn;

    public void addWord(Word word, int index)
    {
        if (index < 0 || index >= emptyspace.Count)
        {
            return;
        }

        emptyspace[index].addWord(word);
    }

    
    void Update()
    {
        if (isPhrasesComplete()){
            foreach(EmptySpace space in emptyspace) space.ApplyCheckColor();
        }
        else{
            foreach(EmptySpace space in emptyspace) space.ApplyDefaultColor();
        }

        if (NextBtn != null) NextBtn.gameObject.SetActive(isPhraseCorrect());
    }


    public bool addWord(Word word)
    {
        for (int i = 0; i < emptyspace.Count; i++){
            if (!emptyspace[i].isWordComplete()){
                emptyspace[i].addWord(word);
                return true;
            }
        }
        return false;
    }

    public bool isPhrasesComplete()
    {
        foreach(EmptySpace space in emptyspace){
            if (!space.isWordComplete()) return false;
        }
        return true;
    }

    public bool isPhraseCorrect()
    {
        foreach(EmptySpace puzzle in emptyspace)
        {
            if (!puzzle.isCorrectWord())
            {
                return false;
            }
        }
        return true;
    }
}
