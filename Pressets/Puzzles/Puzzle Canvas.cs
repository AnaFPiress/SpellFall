using UnityEngine;

public class PuzzleCanvas : MonoBehaviour
{
    void Start()
    {
        Player.LockPlayer = true;
    }

    void OnEnable()
    {
        CamMovement.CursorUnLock();
        CamMovement.Locker = true;
    }

    void OnDisable()
    {
        Player.LockPlayer = false;
        CamMovement.CursorLock();
        CamMovement.Locker = false;
    }

    void OnDestroy()
    {
        Player.LockPlayer = false;    
        CamMovement.CursorLock();
        CamMovement.Locker = false;   
    }

    public void ExitButton() { Destroy(this.gameObject); }
    public void ContinueButton() {
        Player.AttackTutorialLock = false;
        Destroy(this.gameObject);
    }
}
