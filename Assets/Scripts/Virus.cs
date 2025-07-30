using UnityEngine;
using System.Collections;
using System.Threading;
using System.Linq.Expressions;
public class VirusPatrol : MonoBehaviour
{
    public Transform pointA, pointB;
    private Vector3 spawnpoint = new Vector3(-44, -13, 0);
    public float speed = 2f;
    private Vector3 target;
    public GameObject Player;
    public GameObject youdied;
    SpriteRenderer spriteRenderer;
    PlayerController moveScript;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start() => target = pointB.position;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == pointB.position) target = pointA.position;
        if (transform.position == pointA.position) target = pointB.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveScript = collision.gameObject.GetComponent<PlayerController>();
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
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
