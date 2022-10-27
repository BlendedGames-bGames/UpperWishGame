using UnityEngine;

[CreateAssetMenu(menuName = "Attack Data")]
public class AttackData : ScriptableObject
{
    #region ATTACK_PARAMETERS
    [Header("Attack")]

    public float attackTimes;                   // Cantidad de veces que se dispara un proyectil con el comando de ataque
    public float attackSpeed;                   // Velocidad del proyectil de ataque
    public float attackDistance;                // Multiplicador de distancia de ataque
    [Space(5)]
    public float attackColdown;                 // Tiempo en el que el ataque vuelve a estar disponible
    public float attacksUntilColdown;           // Cantidad de ataques que se pueden efectuar antes del tiempo de enfriamiento 
    public float attackColdownReset;            // Tiempo de combo no finalizado en lo que se resetea la cantidad de ataques necesarios para entrar en coldown
    [Space(5)]
    [Range(0.01f, 0.5f)] public float attackInputBufferTime;
    #endregion
    }
