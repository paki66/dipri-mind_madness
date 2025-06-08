using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{

    private Light2D light;
    public float minIntensity = 5f;
    public float maxIntensity = 15f;
    public float cycleSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Mathf.Sin(Time.time * cycleSpeed) + 1f) * 0.5f;

        light.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }


}
