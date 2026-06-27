using UnityEngine;

public class FrameLimiter : MonoBehaviour
{

    public int frameCount;
    public int maxFPS = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
