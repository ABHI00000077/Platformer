using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    public float deactivateDelay = 2f;       
    public float respawnDelay = 3f;          
    public bool respawn = true;              

    private Renderer platformRenderer;
    private Collider2D platformCollider;
    private bool triggered = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.time);
            triggered = true;
            Invoke(nameof(DeactivatePlatform), deactivateDelay);
        }
    }

    void DeactivatePlatform()
    {
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        if (respawn)
            Invoke(nameof(ReactivatePlatform), respawnDelay);
    }

    void ReactivatePlatform()
    {
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
        triggered = false;
    }
}
