
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//Objeto scriptable que contiene todos los datos de movimiento del jugador
	public PlayerMovementData Data;
	public Transform ChildObject;
	public KuroAnimation KuroAnim { get; private set; }

    #region COMPONENTS
    public Rigidbody2D RB { get; private set; }
	#endregion

	#region STATE_PARAMETERS

	//Variables de control de los diferentes movimientos que puede hacer el jugador.
	//Campos de lectura publica pero de escritura privada para otros metodos.
	public bool IsFacingRight { get; private set; }
	public bool IsJumping { get; private set; }
	public bool IsWallJumping { get; private set; }
	public bool IsDashing { get; private set; }
	public bool IsSliding { get; private set; }

	//Contadores de Tiempo
	public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }

	//Salto
	private bool _isJumpCut;
	private bool _isJumpFalling;

	//Salto en Muro
	private float _wallJumpStartTime;
	private int _lastWallJumpDir;

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;

	#endregion

	#region INPUT_PARAMETERS

	private Vector2 _moveInput;

	public float LastPressedJumpTime { get; private set; }
	public float LastPressedDashTime { get; private set; }

	#endregion

	#region CHECK_PARAMETERS

	//Asignar todos estos valores en el inspector
	[Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f); // Tamaño del groundcheck depende del tamaño del personaje, generalmente un poco mas chico de ancho y alto para los checkeos de piso y muralla
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);

	[SerializeField] private AudioSource footstepSFX; 

    #endregion

    #region LAYERS & TAGS

    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;

	#endregion

    private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
		KuroAnim = GetComponent<KuroAnimation>();
	}

	private void Start()
	{
		SetGravityScale(Data.gravityScale);
		IsFacingRight = true;
	}

	private void Update()
	{
        #region TIMERS

        LastOnGroundTime -= Time.deltaTime;
		LastOnWallTime -= Time.deltaTime;
		LastOnWallRightTime -= Time.deltaTime;
		LastOnWallLeftTime -= Time.deltaTime;

		LastPressedJumpTime -= Time.deltaTime;
		LastPressedDashTime -= Time.deltaTime;

		#endregion

		#region INPUT_HANDLER

		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		if (_moveInput.x != 0)
		{
			CheckDirectionToFace(_moveInput.x > 0);
            if (LastOnGroundTime > 0)
	        {
                footstepSFX.enabled = true;
            }
        }
         else footstepSFX.enabled = false;
 

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
        {
			OnJumpInput();    
            }

		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
		{
			OnJumpUpInput();
		}

		if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(1) ) 
		{
			OnDashInput();
		}

        

        #endregion

        #region COLLISION_CHECKS
        if (!IsDashing && !IsJumping)
		{

			//Check de suelo
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping)	//chequea si el componente se sobrepone al suelo
			{
				LastOnGroundTime = Data.coyoteTime; // De ser asi, se ejecuta el coyote time
				KuroAnim.touchedWall = false;
            }		

			//Check de muralla derecha
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;

			//Check de muralla izquierda
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;

			//Cambio de murallas
			LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
		}
		#endregion

		#region JUMP_CHECKS

		if (IsJumping && RB.velocity.y < 0)
		{
			IsJumping = false;

			if (!IsWallJumping)
				{
                _isJumpFalling = true;
				KuroAnim.touchedWall = false;
                }
				
			
		}

		if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
		{
			IsWallJumping = false;
		}

		if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
			_isJumpCut = false;
            if (!IsJumping)
				_isJumpFalling = false;
		}

		if (!IsDashing)
		{
			//Salto
			if (CanJump() && LastPressedJumpTime > 0)
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				Jump();

				KuroAnim.startedJumping = true;
			}
			//Salto en Muro
			else if (CanWallJump() && LastPressedJumpTime > 0)
			{
				IsWallJumping = true;
				IsJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;

				_wallJumpStartTime = Time.time;
				_lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

				WallJump(_lastWallJumpDir);
			}
		}

		#endregion

		#region DASH_CHECKS

		if (CanDash() && LastPressedDashTime > 0)
		{
			// Congela el juego por un momento para establecer un delay entre el input y el direccionamiento del dash
			Sleep(Data.dashSleepTime); 

			// Si no hay direccion de movimiento, el dash es hacia adelante
			if (_moveInput != Vector2.zero)
				_lastDashDir = _moveInput;
			else
				_lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;

			IsDashing = true;
			IsJumping = false;
			IsWallJumping = false;
			_isJumpCut = false;

			StartCoroutine(nameof(StartDash), _lastDashDir);
		}
		#endregion

		#region SLIDE_CHECKS

		if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
			IsSliding = true;
		else
			IsSliding = false;

		#endregion

		#region GRAVITY

		if (!_isDashAttacking)
		{
			// Menor gravedad al deslizarce por las murallas
			if (IsSliding)
			{
				SetGravityScale(0);
			}

            // Mayor gravedad al caer o al soltar el boton de salto prematuramente
            else if (RB.velocity.y < 0 && _moveInput.y < 0)
			{
				// Aun Mayor gravedad si se preciona hacia abajo
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);

				// Se establece el maximo para no alcanzar velocidades absurdas al caer mucho tiempo
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
			}

            // Mayor gravedad al soltar el boton de salto
            else if (_isJumpCut)
			{
				SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}

			else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
			}

			else if (RB.velocity.y < 0)
			{
				// Mayor velocidad al caer
				SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                // Se establece el maximo para no alcanzar velocidades absurdas al caer mucho tiempo
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else
			{
				// Gravedad por defecto al moverse en una plataforma
				SetGravityScale(Data.gravityScale);
			}
		}
		else
		{
			// Sin gravedad al ejecutar un dash
			SetGravityScale(0);
		}

		#endregion
    }

    private void FixedUpdate()
	{
		//Control del movimiento normal
		if (!IsDashing)
		{
			if (IsWallJumping)
				Run(Data.wallJumpRunLerp);
			else
				Run(1);
		}
		else if (_isDashAttacking)
		{
			Run(Data.dashEndRunLerp);
		}

		//Control del desliz en muro
		if (IsSliding)
			{
			Slide();
			KuroAnim.touchedWall = true;
			}
    }

    #region INPUT_CALLBACKS

	//Metodos que manejan las entradas detectadas en el metodo update
    public void OnJumpInput()
	{
		LastPressedJumpTime = Data.jumpInputBufferTime;
	}

	public void OnJumpUpInput()
	{
		if (CanJumpCut() || CanWallJumpCut())
			_isJumpCut = true;
	}

	public void OnDashInput()
	{
		LastPressedDashTime = Data.dashInputBufferTime;
	}
    #endregion

    #region GENERAL_METHODS

    public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}

	private void Sleep(float duration)
    {
		//Method used so we don't need to call StartCoroutine everywhere
		//nameof() notation means we don't need to input a string directly.
		//Removes chance of spelling mistakes and will improve error messages if any
		StartCoroutine(nameof(PerformSleep), duration);
    }

	private IEnumerator PerformSleep(float duration)
    {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale with be 0 
		Time.timeScale = 1;
	}

    #endregion

	//Metodos de movimiento

    #region RUN METHODS
    private void Run(float lerpAmount)
	{
		// Calcula la direccion en la que se va a mover y la velocidad deseada 
		float targetSpeed = _moveInput.x * Data.runMaxSpeed;
		// Uso de Lerp() para suavizado en el cambio de direccion
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate

		float accelRate;

		// Obtiene un valor de aceleracion dependiendo de si estamos acelerando o tratando de desacelerar.
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;

		#endregion

		#region Add Bonus Jump Apex Acceleration
		// Incrementa la aceleracion y velocidad maxima en el punto mas alto del salto
		if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
		{
			accelRate *= Data.jumpHangAccelerationMult;
			targetSpeed *= Data.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum

		// Se establece la condicion para mantener el momentum de velocidad de movimiento
		if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			//seteo a 0 para prevenir cambios en la aceleracion
			accelRate = 0; 
		}
		#endregion

		// Calculo de la diferencia entre la velocidad actual y la deseada
		float speedDif = targetSpeed - RB.velocity.x;

		// Calcula la fuerza en el eje x aplicada al jugador
		float movement = speedDif * accelRate;

		// Convierte lo anterior en un vector y lo aplica al rigidbody del jugador
		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

	}

	private void Turn()
	{

		ChildObject.parent = null;

		//da vuelta al objeto jugador
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		ChildObject.transform.SetParent(this.transform, true);
		
		IsFacingRight = !IsFacingRight;

		
	}
    #endregion

    #region JUMP METHODS
    private void Jump()
	{
		//Nos asegura que no se pueda saltar mas de una vez por input de boton
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump

		//Aumentamos la fuerza aplicada si el personaje cae
		//Asi aseguramos que siempre se salte la misma cantidad
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}

	private void WallJump(int dir)
	{
        //Nos asegura que no se pueda saltar en el muro mas de una vez por input de boton
        LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		#region Perform Wall Jump
		Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
		force.x *= dir; // Aplica una fuerza en la direccion contraria a la muralla

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0)  //Chequea si se esta callendo y se establecen fuerzas para contrarrestar la gravedad
			force.y -= RB.velocity.y;

		//Ejecutamos la fuerza de salto
		RB.AddForce(force, ForceMode2D.Impulse);
		#endregion
	}
	#endregion

	#region DASH METHODS
	//Dash Coroutine
	private IEnumerator StartDash(Vector2 dir)
	{
	
		LastOnGroundTime = 0;
		LastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
		_isDashAttacking = true;

		SetGravityScale(0);

		// Mantenemos la velocidad del jugador a la velocidad de dash durante la fase de ataque
		while (Time.time - startTime <= Data.dashAttackTime)
		{
			RB.velocity = dir.normalized * Data.dashSpeed;
			//Pausa el ciclo hasta el siguiente frame
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		// Empieza el termino del dash, se levantan gradualmente las restricciones de movimiento del jugador (ver Update() y Run())
		SetGravityScale(Data.gravityScale);
		RB.velocity = Data.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= Data.dashEndTime)
		{
			yield return null;
		}

		//Fin del dash
		IsDashing = false;
	}

	// Cooldown entre instancias de dash
	private IEnumerator RefillDash(int amount)
	{
		// Evita poder dashear multiples veces en el suelo
		_dashRefilling = true;
		yield return new WaitForSeconds(Data.dashRefillTime);
		_dashRefilling = false;
		_dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
	}
	#endregion

	#region OTHER MOVEMENT METHODS
	private void Slide()
	{
		//Funciona igual al momento horizontal solo que en el eje y
		float speedDif = Data.slideSpeed - RB.velocity.y;	
		float movement = speedDif * Data.slideAccel;

		// Restringimos el movimiento para evitar sobre correciones 
		// La fuerza aplicada no puede ser mayor a la diferencia (negativa) speedDifference * por la cantidad de veces por segundo que se llama a fixedUpdate()
		movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif)  * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

		RB.AddForce(movement * Vector2.up);
	}
    #endregion


    #region CHECK_METHODS
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}

    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

	private bool CanWallJump()
    {
		return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
			 (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
	}

	private bool CanJumpCut()
    {
		return IsJumping && RB.velocity.y > 0;
    }

	private bool CanWallJumpCut()
	{
		return IsWallJumping && RB.velocity.y > 0;
	}

	private bool CanDash()
	{
		if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
		{
			StartCoroutine(nameof(RefillDash), 1);
		}

		return _dashesLeft > 0;
	}

	public bool CanSlide()
    {
		if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
			return true;
		else
			return false;
	}
    #endregion


    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
		Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
	}
    #endregion
}
