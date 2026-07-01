using UnityEngine;
using UnityEngine.InputSystem;

public class settingsMenu : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame) gameObject.SetActive(false);
    }
}
