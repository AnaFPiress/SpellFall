using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AudioSliderLabel : MonoBehaviour
{
    private TextMeshProUGUI _label;
    [SerializeField] private Slider slider;
    private float Interval = 0f;

    void Start()
    {
        _label = GetComponent<TextMeshProUGUI>();
        if (slider == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Interval = Mathf.Abs(slider.minValue) + Mathf.Abs(slider.maxValue);
    }

    void Update()
    {
        _label.text =((int)((((slider.value) < 0 ? slider.value - slider.minValue : slider.value + Mathf.Abs(slider.minValue)) / Interval) * 100f)).ToString() + "%";
    }
}
