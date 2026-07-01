using UnityEngine;

public class EveryLookCamera : MonoBehaviour
{
    Camera camera;

    void Start()
    {
        camera = Camera.main;
    }


    void Update()
    {
        transform.LookAt(camera.transform);
    }
}
