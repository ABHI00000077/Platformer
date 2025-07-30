using UnityEngine;

public class Shiftvalue : MonoBehaviour
{
    public static Shiftvalue Instance;
    private string message;
    private bool playerInRange = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
    public void DisplayEncodedPassword(int encoded)
    {
        message = "Ceaser Cypher Shift Value: " + encoded;
    }
    void Update()
    {
        if (playerInRange) UIManager.Instance.ShowInteractButton(true);
    }

    public void Interact()
    {
        audioManager.PlaySFX(audioManager.openbox);
        UIManager.Instance.ShowInteractButton(false);
        UIManager.Instance.ShowMessage(message);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance.ShowInteractButton(false);
        }
    }
}
