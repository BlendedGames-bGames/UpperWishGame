using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KuroAnimation : MonoBehaviour {
    private PlayerMovement mov;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("Custom Events")]
    public UnityEvent kuroEvents;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    private ParticleSystem _jumpParticle;
    private ParticleSystem _landParticle;

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }
    public bool startedDashing { private get; set; }
    public bool turnAround { private get; set; }
    public bool touchedWall { private get; set; }
    public bool walking { private get; set; }

    public float currentVelY;
    public float currentVelX;

    private Vector2 _moveInput;

    private void Start()
        {
        mov = GetComponent<PlayerMovement>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        anim = spriteRend.GetComponent<Animator>();

        _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
        _landParticle = landFX.GetComponent<ParticleSystem>();

        }
    

    private void Update()
        {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        if (_moveInput.x != 0)
            {
            mov.CheckDirectionToFace(_moveInput.x > 0);
            if (mov.LastOnGroundTime > 0)
                {
                anim.SetFloat("Vel X", Mathf.Abs(mov.RB.velocity.x));
                }
            }
        }

    private void LateUpdate()
        {
        if (!mov.IsSliding)
            {
            anim.SetBool("WallSlide", false);
            }
        else if (mov.IsSliding)
            {
            anim.SetBool("WallSlide", true);
            }
        CheckAnimationState();
        }

    private void CheckAnimationState()
        {
        if (startedJumping)
            {
            anim.SetTrigger("Jump");
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
            }

        if (justLanded)
            {
            anim.SetTrigger("Recovery");
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;
            return;
            }

        if (turnAround)
            {
            anim.SetTrigger("Recovery");
            turnAround = false;
            return;
            }

        anim.SetFloat("Vel Y", mov.RB.velocity.y);
        }
    }

