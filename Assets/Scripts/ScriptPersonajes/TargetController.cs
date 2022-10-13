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


    void Start() {
        targetAnimation = GetComponent<SpriteRenderer>();

        lockedEnemy = 0;
        lockedOn = false;
       
    }


    void Update() {
        // si el elemento seleccionado se vuelve nulo
        if (selectedTarget == null ) {
            targetAnimation.enabled = false;
            lockedOn = false;


            Debug.Log(lockedEnemy);
            Debug.Log("hay objetos" + foundTargets.targetList.Count);
            foundTargets.targetList.Remove(foundTargets.targetList[lockedEnemy]);
            lockedEnemy = 0;
        }

        // Si no hay nada en la lista de enemigos
        else if (foundTargets.targetList.Count == 0) {
            targetAnimation.enabled = false;
            lockedOn = false;
        }

        // Si hay elementos en la lista de enemigos
        else if (foundTargets.targetList.Count >= 1) {
            targetAnimation.enabled = true;
            lockedOn = true;

            selectedTarget = foundTargets.targetList[lockedEnemy].transform;

            gameObject.transform.position = selectedTarget.position;

        }



        // Cambiar el target con la rueda del mouse
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && foundTargets.targetList.Count >= 1 && selectedTarget != null) {
             if (lockedEnemy == foundTargets.targetList.Count - 1) {
                //Si ya se llego al final de la lista, se envia el puntero al inicio
                lockedEnemy = 0;
                selectedTarget = foundTargets.targetList[lockedEnemy].transform;
            }
             else {
                //Se mueve al siguiente enemigo
                lockedEnemy++;
                selectedTarget = foundTargets.targetList[lockedEnemy].transform;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && foundTargets.targetList.Count >= 1 && selectedTarget != null) {
            if (lockedEnemy == 0) {
                //Si ya se llego al principio de la lista, se envia el puntero al final
                lockedEnemy = foundTargets.targetList.Count - 1;
                selectedTarget = foundTargets.targetList[lockedEnemy].transform;
            }
            else {
                //Se mueve al enemigo anterior
                lockedEnemy--;
                selectedTarget = foundTargets.targetList[lockedEnemy].transform;
            }
        }
    }
}