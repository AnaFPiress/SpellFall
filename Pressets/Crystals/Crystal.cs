using UnityEngine;
using UnityEngine.InputSystem;

public class Crystal : MonoBehaviour
{
    [SerializeField] private float weight = 1f;
    [SerializeField] private Key crystalKey = Key.Z;
    [SerializeField] private Canvas canvas;
    Player player;
    public bool isCarried = false;

    // Handles player interaction and updates the crystal state
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

    // Sets the crystal as being carried by the player
    public void CrystalTransport(){
        isCarried = true;
    }

    // Drops the crystal
    public void CrystalDrop(){
        isCarried = false;
    }

    // Detects when the player enters the interaction area
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (canvas != null) canvas.gameObject.SetActive(player != null);
        if (player != null)
        {
            this.player = player;
        }
    }

    // Detects when the player leaves the interaction area
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
