using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject InteractableLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (InteractableLabel != null) InteractableLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInteractable()
    {
        if (InteractableLabel == null) return;
        InteractableLabel.SetActive(true);
    }

    public void HiddenInteractable()
    {
        if (InteractableLabel == null) return;
        InteractableLabel.SetActive(false);
    }
}
