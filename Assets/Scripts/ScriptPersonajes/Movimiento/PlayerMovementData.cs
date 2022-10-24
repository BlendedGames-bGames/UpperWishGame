using UnityEngine;

[CreateAssetMenu(menuName = "Player Movement Data")]		// Permite crear un objeto scriptable de los datos del jugador desde el menu de opciones de unity, arrastrar el objeto hacia el objeto jugador
public class PlayerMovementData : ScriptableObject
{

    #region GRAVITY_PARAMETERS

    [Header("Gravity")]
	[HideInInspector] public float gravityStrength;			//Fuerza de aceleracion hacia abajo necesaria para la altura de salto (jumpHeight) y tiempo de duracion de salto (jumpTimeToApex).
	[HideInInspector] public float gravityScale;			//Multiplicador de fuerza de gravedad sobre el jugador (ajuestar en ProjectSettings/Physics2D).
															
	[Space(5)]
	public float fallGravityMult;							//Multiplicador de la fuerza de gravedad al caer.
	public float maxFallSpeed;								//Velocidad maxima de caida (terminal velocity).
	[Space(5)]
	public float fastFallGravityMult;						//Multiplicador de la fuerza de gravedad al caer y presionar el input hacia abajo.
	public float maxFastFallSpeed;                          //Velocidad maxima de caida (terminal velocity) en caida rapida.

    #endregion

    [Space(20)]

    #region RUN_PARAMETERS

    [Header("Run")]
	public float runMaxSpeed;								//Velocidad objetivo de corrida a alcanzar.
	public float runAcceleration;							//Velocidad de aceleracion a la que se alcanza la velocidad maxima.
	[HideInInspector] public float runAccelAmount;			//La fuerza vectorial aplicada al jugador (multiplicada por la diferencia de velocidad(speedDiff)).
	public float runDecceleration;							//Velocidad a la que se desacelera desde el movimiento.
	[HideInInspector] public float runDeccelAmount;         //La fuerza vectorial aplicada al jugador (multiplicada por la diferencia de velocidad(speedDiff)).
    [Space(5)]
	[Range(0f, 1)] public float accelInAir;					//Multiplicadores de aceleracion en el aire.
	[Range(0f, 1)] public float deccelInAir;
	[Space(5)]
	public bool doConserveMomentum = true;

    #endregion

    [Space(20)]

    #region JUMP_PARAMETERS

    [Header("Jump")]
	public float jumpHeight;								//Altura del salto del jugador.
	public float jumpTimeToApex;							//Tiempo entre que se aplica la fuerza de salto y se alcanza la altura maxima (jumpHeight).
	[HideInInspector] public float jumpForce;				//La fuerza vectorial hacia arriba aplicada al jugador.

	[Header("Both Jumps")]
	public float jumpCutGravityMult;						//Multiplicador que incrementa la gravedad al soltar el boton de salto.
	[Range(0f, 1)] public float jumpHangGravityMult;		//Reduce la gravedad al acercarce a la altura de salto deseada.
	public float jumpHangTimeThreshold;						//Tiempo en el que se mantiene en la altura de salto deseada.
	[Space(0.5f)]
	public float jumpHangAccelerationMult; 
	public float jumpHangMaxSpeedMult; 				

	[Header("Wall Jump")]
	public Vector2 wallJumpForce;							//La fuerza vectorial aplicada al saltar en la muralla.
	[Space(5)]
	[Range(0f, 1f)] public float wallJumpRunLerp;			//Reduce el movimiento del personaje al saltar en la muralla.
	[Range(0f, 1.5f)] public float wallJumpTime;			//Tiempo de duracion de la restriccion de movimiento al saltar en muralla.
	public bool doTurnOnWallJump;                           //Rotacion del jugador en direccion de la muralla

    #endregion

    [Space(20)]

    #region OTHER_PARAMETERS

    [Header("Slide")]
	public float slideSpeed;								// Velocidad de a la que el jugador se desliza por las paredes.
	public float slideAccel;								// Velocidad a la que se alcanza la velocidad de deslizar por las paredes.

    [Header("Assists")]
	[Range(0.01f, 0.5f)] public float coyoteTime;			// Tiempo en el que aun se puede saltar pese a dejar una plataforma
	[Range(0.01f, 0.5f)] public float jumpInputBufferTime;  // Buffer de input de salto.

    #endregion

    [Space(20)]

    #region DASH_PARAMETERS

    [Header("Dash")]
	public int dashAmount;									// Distancia objetivo del dash.
	public float dashSpeed;									// Velocidad a la que se alcanza la distancia objetivo del dash.
	public float dashSleepTime;								// Delay entre el input de dash y la aplicacion de la fuerza.
	[Space(5)]
	public float dashAttackTime;
	[Space(5)]
	public float dashEndTime;								// Suavizado al finalizar el dash.
	public Vector2 dashEndSpeed;							// Velocidad a la cual termina el dash.
	[Range(0f, 1f)] public float dashEndRunLerp;			// Restriccion de movimiento mientras se ejecuta un dash.
	[Space(5)]
	public float dashRefillTime;							// tiempo en el que el dash vuelve a estar disponible
	[Space(5)]
	[Range(0.01f, 0.5f)] public float dashInputBufferTime;

    #endregion

    //Llamadas de unity para validar en el inspector
    private void OnValidate()
    {
		//Se calcula la fuerza de gravedad usando la formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
		
		//Se calcula la fuerza de gravedad del componente RIGIDBODY2D en relacion a la gravedad de unity
		gravityScale = gravityStrength / Physics2D.gravity.y;

		//Calcula la fuerza de aceleracion y deceleracion al corren en  base a la formula: cantidad = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		//Calcula la fuerza de salto en base a la formula: (initialJumpVelocity = gravity * timeToJumpApex)
		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

		#region Variable Ranges
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		#endregion
	}
}