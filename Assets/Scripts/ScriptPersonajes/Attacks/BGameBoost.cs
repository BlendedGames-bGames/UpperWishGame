using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGameBoost : MonoBehaviour
{

    public bool canBoost;

    private float timer;
    private float originalStat;

    [SerializeField] private float cooldown;

    [SerializeField] private PlayerAttack currentWeapon;
    [SerializeField] private Animation anim;
    [SerializeField] private AudioSource BoostSFX;
    void Start()
    {
        originalStat = currentWeapon.Data.attackInputBufferTime;
    }

    // Update is called once per frame
    void Update()
    {

        #region TIMERS
        if (!canBoost)
            {
            timer += Time.deltaTime;
            if (timer > cooldown)
                {
                canBoost = true;
                timer = 0;
                currentWeapon.Data.attackInputBufferTime = originalStat;
                }
            }

        #endregion

        #region INPUT_HANDLER
        if (Input.GetKeyDown(KeyCode.Q))
            {
            
            gameBoost();
            canBoost = false;
            }

        #endregion
        }

    private void gameBoost()
        {
        currentWeapon.Data.attackInputBufferTime = 0.1f;
        BoostSFX.Play();
        anim.Play("landingFX");
        }
}
