using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    public float flashSpeed = 0.5f;
    TextMeshProUGUI text;
    float timer;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        text.enabled = Mathf.FloorToInt(timer / flashSpeed) % 2 == 0;
    }
}
