using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TargetController : MonoBehaviour {

    public int lockedEnemy;
    public bool lockedOn;

    public Transform selectedTarget;

    private SpriteRenderer targetAnimation;
    public FindTarget foundTargets;

    public static List<FindTarget> FoundTargets = new List<FindTarget>();

    void Start() {
        targetAnimation = GetComponent<SpriteRenderer>();
        lockedEnemy = 0;
        lockedOn = false;
       
    }
    private void FixedUpdate() {

       

        // Cambiar el target con la rueda del mouse
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && FindTarget.targetList.Count > 1 ) {
            if (lockedEnemy == FindTarget.targetList.Count - 1) {
                //Si ya se llego al final de la lista, se envia el puntero al inicio
                lockedEnemy = 0;
                selectedTarget = FindTarget.targetList[lockedEnemy].transform;
            }
            else {
                //Se mueve al siguiente enemigo
                lockedEnemy++;
                selectedTarget = FindTarget.targetList[lockedEnemy].transform;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && FindTarget.targetList.Count > 1 ) {
            if (lockedEnemy == 0) {
                //Si ya se llego al principio de la lista, se envia el puntero al final
                lockedEnemy = FindTarget.targetList.Count - 1;
                selectedTarget = FindTarget.targetList[lockedEnemy].transform;
            }
            else {
                //Se mueve al enemigo anterior
                lockedEnemy--;
                selectedTarget = FindTarget.targetList[lockedEnemy].transform;
            }
        }
    }

    void Update() {

        // si el elemento seleccionado se vuelve nulo
        if (selectedTarget == null) {
            lockedOn = false;
            targetAnimation.enabled = false;

            Debug.Log(lockedEnemy);
            Debug.Log("hay objetos: " + FindTarget.targetList.Count);

            if (FindTarget.targetList.Count >= 1) {
                FindTarget.targetList.Remove(FindTarget.targetList[lockedEnemy]);
            }
            lockedEnemy = 0;

        }

        // Si hay elementos en la lista de enemigos
        if (FindTarget.targetList.Count >= 1) {
            targetAnimation.enabled = true;
            lockedOn = true;

            selectedTarget = FindTarget.targetList[lockedEnemy].transform;
            gameObject.transform.position = selectedTarget.position;

         }

        // Si no hay nada en la lista de enemigos
        else if (FindTarget.targetList.Count == 0) {
            targetAnimation.enabled = false;
            lockedOn = false;
        }

    }
}

