using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HeathBar : MonoBehaviour
{
    private Slider slider;
    private Player player;
    
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Destroy(this.gameObject);
            return;
        }
        slider = GetComponent<Slider>();
        slider.maxValue = player.getVida();
        slider.minValue = 0f;
    }

    void Update()
    {
        slider.value = player.getVida();
    }
}
