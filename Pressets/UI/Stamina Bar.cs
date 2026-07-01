using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StaminaBar : MonoBehaviour
{
    private Slider slider;
    private Player player;

    void Start(){
        slider = GetComponent<Slider>();
        player = GameObject.FindAnyObjectByType<Player>();
        if (player != null)
        {
            slider.maxValue = player.getStamina();
        }
        else { Destroy(this.gameObject); }
    }

    void Update(){
        slider.value = player.getStamina();
    }
}
