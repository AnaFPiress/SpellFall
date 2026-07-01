using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float mouseSense = 10f;
    private float xRotation = 0;
    public static bool Locker = false;

    void Start(){
        CursorLock();
    }

    void Update()
    {
        CameraMovementFunction();
    }

    public static void CursorLock(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void CursorUnLock(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CameraMovementFunction()
    {
        if (Locker || Player.LockPlayer) return;
        if (Mouse.current == null || transform.parent == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSense;
        //float mouseY = mouseDelta.y * mouseSense;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
