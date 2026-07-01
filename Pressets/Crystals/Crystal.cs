using UnityEngine;
using UnityEngine.InputSystem;

public class Crystal : MonoBehaviour
{
    [SerializeField] private float weight = 1f;
    [SerializeField] private Key crystalKey = Key.Z;
    [SerializeField] private Canvas canvas;
    Player player;
    public bool isCarried = false;

    void Update()
    {
        if (Keyboard.current[crystalKey].wasPressedThisFrame && player != null && !isCarried){
            player.addCrystal(this);
        }
        if (isCarried)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
        GetComponent<Rigidbody>().isKinematic = isCarried;
    }

    public float getWeight(){
        return weight;
    }

    public void CrystalTransport(){
        isCarried = true;
    }

    public void CrystalDrop(){
        isCarried = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (canvas != null) canvas.gameObject.SetActive(player != null);
        if (player != null)
        {
            this.player = player;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (canvas != null && player != null) canvas.gameObject.SetActive(false);
        if (player != null)
        {
            this.player = null;
        }
    }
}
