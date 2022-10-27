using UnityEngine;

[CreateAssetMenu(menuName = "Player Status Data")]	
public class PlayerStatusData : ScriptableObject
{
    #region STATS_PARAMETERS
    [Header("Stats")]

    public float health;
    public float stamina;
    [Space(5)]
    public int strenghtRaw;                                 // Influye en el daño de los ataques
    public float technikeRaw;                               // Influye en la cantidad de ataques
    public float constitutionRaw;                           // Influye en la bonificacion de vida
    public float dexterityRaw;                              // Influye en la velocidad de movimiento y en la velocidad de ataque

    #endregion

    [Space(20)]

    #region DAMAGE_PARAMETERS
    [Header("Damage")]

    public float invencibilityTime;                         // Tiempo de invencibilidad despues de dañó
    public float invencibilityColdown;                      // Tiempo entre el cual se puede entrar a invencibilidad denuevo
    [Space(5)]
    public float damageKnockbackAmount;                     // Distancia de retroceso con daño
    public float damageKnockbackSpeed;                      // Velocidad a la que se alcanza la distancia de retroceso
    [Space(5)]
    public Vector2 damageKnockbackEndSpeed;					// Velocidad a la cual termina el knockback del daño.
    [Space(5)]
    [Range(0f, 1f)] public float damageKnockbackLerp;       // Restriccion de movimiento cuando se recibe daño.

    #endregion

    }
