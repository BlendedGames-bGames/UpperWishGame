using UnityEngine;

[CreateAssetMenu(menuName = "Player Bullet Data")]
public class BulletData : ScriptableObject
{

    #region BULLET_PARAMETERS

    [Header("Bullet")]

    public float hitCount;          //Cantidad de veces que hace daño el proyectil
    public float hitRadious;        //Distancia afectada por la explocion del disparo
    public float hitDamage;         //Cantidad de daño que hace el proyectil
    
    public float travelSpeed;       //Velocidad a la que se desplaza el proyectil
    public float travelRicochet;    //Cantidad de veces que rebota el proyectil en superficies o enemigos

    #endregion
    }
