using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(TextMeshProUGUI))]
public class EmptySpace : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Word correctWord;
    [SerializeField] private Word prefabWord;
    [SerializeField] private Color CorrectColor = Color.green, IncorrectColor = Color.red;
    private Color defaultColor;
    private TextMeshProUGUI textComponent;
    private bool isCorrect = false;
    private bool isComplete = false;
    private String word;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        defaultColor = textComponent.color;
        textComponent.text = "____";
    }

   public void addWord(Word word)
    {
        if (word.getWord().Equals(correctWord.getWord()))
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        this.word = word.getWord();
        isComplete = true;
        textComponent.text = word.getWord();
    }

    public void removeWord()
    {
        isCorrect = false;
        isComplete = false;
        textComponent.text = "____";
        WordBlank wordBlank = GameObject.FindAnyObjectByType<WordBlank>();
        if (wordBlank == null)
        {
            Debug.LogError("WordBank not found.");
            return;
        }
        Instantiate(prefabWord, wordBlank.transform).GetComponent<Word>().setWord(this.word);

    }

    public bool isCorrectWord()
    {
        return isCorrect;
    }

    public bool isWordComplete()
    {
        return isComplete;

    }

    public void ApplyCheckColor(){
        textComponent.color = (isCorrectWord()) ? CorrectColor : IncorrectColor;
    }

    public void ApplyDefaultColor(){
        textComponent.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicado!");
        
        if (isComplete)
        {
            removeWord(); // exemplo: remove a palavra ao clicar
        }
    }
}
