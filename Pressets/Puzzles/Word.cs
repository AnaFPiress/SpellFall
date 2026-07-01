using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class Word : MonoBehaviour
{
    [SerializeField] private string word;
    [SerializeField] private TextMeshProUGUI textComponent;
    private Button button;
    void Start()
    {
        if (textComponent !=null) textComponent.text = word;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("Click");
            Pharses pharses = GameObject.FindAnyObjectByType<Pharses>();
            if (pharses == null)
            {
                return;
            }
            pharses.addWord(this);
            Destroy(gameObject);

        });
    }

    void Update()
    {
        
    }

    public void setWord(string newWord)
    {
        word = newWord;
        if (textComponent != null) textComponent.text = word;
    }

     public string getWord(){ return word; }
}
