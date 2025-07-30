using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;
    private bool isGrounded;
    private bool respawn;
    private Vector3 spawnpoint = new Vector3(-44, -13, 0);
    public LayerMask deadend;
    private float movedir = 0f;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void OnMoveLeft(bool pressed) => movedir = pressed ? -1 : (movedir < 0 ? 0 : movedir);
    public void OnMoveRight(bool pressed) => movedir = pressed ? 1 : (movedir > 0 ? 0 : movedir);
    void Update()
    {
        float keyboardInput = Input.GetAxisRaw("Horizontal"); // -1 (left), 0, +1 (right)
        float inputDirection = Mathf.Abs(keyboardInput) > 0.1f ? keyboardInput : movedir;

        rb.linearVelocity = new Vector2(inputDirection * moveSpeed, rb.linearVelocity.y);
        if (inputDirection != 0)
            transform.localScale = new Vector3(Mathf.Sign(inputDirection), 1f, 1f);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        respawn = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, deadend);
        if (respawn)
        {
            transform.position = spawnpoint;
        }
        anim.SetFloat("Speed", Mathf.Abs(inputDirection));
        anim.SetBool("IsJumping", !isGrounded);
        if (Input.GetKeyDown(KeyCode.Space))
            OnJump();
    }
    public void OnJump() 
    {
        if (isGrounded)
        {
            audioManager.PlaySFX(audioManager.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    public void OnInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var c in hits)
        {
            if (c.CompareTag("Shift"))
            {
                c.GetComponent<Shiftvalue>()?.Interact();
                return;
            }
            if (c.CompareTag("Interactable"))
            {
                c.GetComponent<BoxInteractable>()?.Interact();
                return;
            }
            if (c.CompareTag("Door"))
            {
                c.GetComponent<DoorInteractable>()?.OpenDoor();
                return;
            }
        }
    } 
}
