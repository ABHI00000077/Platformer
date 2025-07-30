using UnityEngine;
using System.Collections;
public class TrapTrigger : MonoBehaviour
{
    private Animator trapAnimator;
    private Vector3 spawnpoint = new Vector3(-44, -13, 0);
    public GameObject Player;
    SpriteRenderer spriteRenderer;
    PlayerController moveScript;
    public GameObject youdied;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        trapAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trapAnimator.SetTrigger("ActivateTrap");
            spriteRenderer = other.GetComponent<SpriteRenderer>();
            moveScript = other.gameObject.GetComponent<PlayerController>();
            Die();
        }
    }
    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }
    IEnumerator Respawn(float duration)
    {
        audioManager.PlaySFX(audioManager.die);
        youdied.SetActive(true);
        spriteRenderer.enabled = false;
        moveScript.moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        Player.transform.position = spawnpoint;
        youdied.SetActive(false);
        moveScript.moveSpeed = 20;
        spriteRenderer.enabled = true;
    }
}   
