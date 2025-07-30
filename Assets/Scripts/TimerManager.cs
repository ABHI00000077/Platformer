using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float levelStartTime;
    private float totalTime;
    private bool isRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        levelStartTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            totalTime += Time.time - levelStartTime;
            isRunning = false;
        }
    }
    public float GetCurrentLevelTime()
    {
        if (!isRunning) return totalTime;
        return Time.time - levelStartTime;
    }
    public float GetTotalTime() => totalTime;
    public void OnLevelCompleted()
    {
        StopTimer();
        float total = TimerManager.Instance.GetTotalTime();
        #if UNITY_WEBGL && !UNITY_EDITOR
        string js = $"window.submitTime({total.ToString("F2")});";
        Application.ExternalEval(js);
        #else
        Debug.Log($"[SubmitTime] {total:F2}s");
        #endif
    }
}
