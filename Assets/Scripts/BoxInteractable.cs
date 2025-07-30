using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class BoxInteractable : MonoBehaviour
{
    public static BoxInteractable Instance;
    private string message;
    public void DisplayEncodedPassword(string encoded)
    {
        message = "Encoded Password: " + encoded;
    }
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
