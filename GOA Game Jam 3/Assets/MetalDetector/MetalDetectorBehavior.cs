using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MetalDetectorBehavior : MonoBehaviour
{
    public LayerMask detectMask;
    public float scanRadius;
    public float maxBeepRadius;
    private AudioSource source;
    private Light2D light;
    public float minIntensity;
    public float maxIntensity;
    private float flashIntensity;
    private float pingDelay;
    public float minPingDelay;
    public float maxPingDelay;
    public bool pinging;
    public float distance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        light = GetComponent<Light2D>();
        IEnumerator coroutine = Ping();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        distance = -1;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, scanRadius, detectMask);
        foreach(Collider2D col in cols)
        {
            float newDis = Vector2.Distance(transform.position, col.transform.position);
            if (distance == -1 || newDis < distance) distance = newDis;
        }
        if (distance != -1)
        {
            pinging = true;
            float x = Mathf.InverseLerp(scanRadius, 0f, distance);
            pingDelay = Mathf.Lerp(maxPingDelay, minPingDelay, x);
            source.volume = Mathf.Lerp(0.1f, 1f, x);
            flashIntensity = Mathf.Lerp(minIntensity, maxIntensity, x);
        }
        else
        {
            pinging = false;
        }
    }


    public IEnumerator Ping()
    {
        while (true)
        {
            if (pinging)
            {
                float time = Time.realtimeSinceStartup;
                float timeInSeconds = 0f;
                source.Play();
                while (timeInSeconds < source.clip.length)
                {
                    timeInSeconds = Time.realtimeSinceStartup - time;
                    float x = Mathf.InverseLerp(0f, source.clip.length, timeInSeconds);
                    if(x<0.5f) light.intensity = Mathf.Lerp(0f, flashIntensity, x * 2); else light.intensity = Mathf.Lerp(flashIntensity, 0, x/2);
                    yield return null;
                }
                light.intensity = 0f;
                source.Stop();
            }
            yield return new WaitForSeconds(pingDelay);
        }

    }
}
