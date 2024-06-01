using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : InteractionArea
{
    [SerializeField] private Animator animator;

    public override bool PlaceBurger()
    {
        Destroy(PlayerMovement.instance.holdingObject.gameObject);
        if (animator != null)
            animator.SetTrigger("Interact");

        return true;
    }

    public override bool GrabBurger() { return false; }

    public override bool SwapBurger() { return false; }

    public override bool CombineBurger() { return false; }
}
