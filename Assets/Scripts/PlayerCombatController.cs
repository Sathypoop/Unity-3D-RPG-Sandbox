using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : NetworkBehaviour
{
    [SerializeField] private Animator animator;

    public float cooldownTimer = 1.5f;
    private float currentTime = 0;

    public bool readyToAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
            enabled= false;
    }

    void Update()
    {
        if (!readyToAttack && currentTime < cooldownTimer)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            readyToAttack = true;
        }

        if(Input.GetMouseButtonDown(0) && readyToAttack)
        {
            animator.Play("Attack", 1);
            readyToAttack= false;
        }
    }
}
