using UnityEngine;
using UnityEngine.Events;

public class MovementActions: MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// fuerza de salto
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// cantidad de movimiento cuando el personaje se agacha 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// suavisado de movimiento

	[SerializeField] private bool m_AirControl = false;							// Si el personaje puede posicionarse 
	[SerializeField] private LayerMask m_WhatIsGround;							// determina que es considerado suelo
	[SerializeField] private Transform m_GroundCheck;							// chequea que es suelo
	[SerializeField] private Transform m_CeilingCheck;							// chequea que es techo
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f;                                         // radio de chequeo de suelo
    const float k_CeilingRadius = .2f;                                          // radio de chequeo de techo

    private bool m_Grounded;													// Whether or not the player is grounded.
	
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;											// determina a que lado esta llendo 
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// El jugador esta en el suelo cuando esto detecta verdadero
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// Verificar si el personaje se puede para despues de agacharse
		if (!crouch)
		{
			// Si hay un  t echo, se mantiene agachado
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//solo permite movimiento cuando el personaje esta en el suelo y tiene el control aereo activado
		if (m_Grounded || m_AirControl)
		{

			// modificador de agachado
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				move *= m_CrouchSpeed;

				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Mueve el personaje segun la velocidad del vector
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// Suavizado de movimiento
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Input derecha y personaje mirando a la izquierda
			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			// Input izquierda y  personaje mirando a la derecha
			else if (move < 0 && m_FacingRight)
			{

				Flip();
			}
		}
		// Caso en el que el personaje salte
		if (m_Grounded && jump)
		{
			// Añade la fuerza al vector de salto
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Reflejo de movimiento
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
