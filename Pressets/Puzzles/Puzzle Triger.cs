using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleTriger : MonoBehaviour
{
    [SerializeField] private Canvas InteractiveLabel;
    [SerializeField] private Key key;
    [SerializeField] private Canvas PuzzleCanvas;
    private bool PlayerIN = false;
    private Canvas PuzzleCanvasInstantiate;

    void Start(){
        if (InteractiveLabel != null) InteractiveLabel.gameObject.SetActive(false);
    }

    void Update(){
        if (Keyboard.current[key].wasPressedThisFrame)
            if (PuzzleCanvasInstantiate == null && PlayerIN) PuzzleCanvasInstantiate = Instantiate(PuzzleCanvas,Vector3.zero,Quaternion.identity);
        
    }

    public void ShowInteractiveLabel(){
        if (InteractiveLabel == null) return;
        InteractiveLabel.gameObject.SetActive(true);
    }

    public void HiddenInteractiveLabel(){
        if (InteractiveLabel == null) return;
        InteractiveLabel.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other){
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            PlayerIN = true;
            ShowInteractiveLabel();
        }
    }

    public void OnTriggerExit(Collider other){
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            PlayerIN = false;
            HiddenInteractiveLabel();
        }
    }
}
