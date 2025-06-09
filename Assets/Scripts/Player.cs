using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationTorque = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem trailEffect;

    private bool isGrounded;
    private Rigidbody2D rb;
    private bool canMove = true;
    private Coroutine fadeOutCoroutine;
    private AudioUtilities audioUtils;
    private AudioSource engineSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Player script started!");
        rb = GetComponent<Rigidbody2D>();
        audioUtils = FindAnyObjectByType<AudioUtilities>();
        engineSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovements();
    }

    void PlayerMovements()
    {
        if (canMove)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            float input = Input.GetAxis("Horizontal");

            if (isGrounded)
            {
                // Apply forward driving force only when grounded
                rb.AddForce(transform.right * input * moveForce);
                rb.AddTorque(-input * rotationTorque / 2);
                if (input != 0)
                {
                    if (!engineSFX.isPlaying)
                    {
                        engineSFX.volume = 1f; // reset in case fade out left it low
                        engineSFX.Play();
                    }
                    if (!trailEffect.isPlaying)
                    {
                        trailEffect.Play();
                    }
                }
                else
                {
                    if (engineSFX.isPlaying)
                    {
                        if (fadeOutCoroutine != null)
                            StopCoroutine(fadeOutCoroutine);

                        fadeOutCoroutine = StartCoroutine(audioUtils.FadeOutAudio(engineSFX));
                    }

                    trailEffect.Stop();
                }
            }
            else
            {
                // allow rotation (for mid-air flips or balance)
                rb.AddTorque(-input * rotationTorque);
            }
        }
    }

    public void DisableControls()
    {
        canMove = false;
        engineSFX.Stop();
        trailEffect.Stop();
    }

}
