using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ManaBar : MonoBehaviour
{
    Slider slider;
    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Destroy(this.gameObject);
            return;
        }
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = player.getMana();
    }

    void Update()
    {
        slider.value = player.getMana();
    }
}
