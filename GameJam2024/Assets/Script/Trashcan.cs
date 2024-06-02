using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : InteractionArea
{
    [SerializeField] private Animator animator;
    [SerializeField] AudioSource AudioSource;
    public override bool PlaceBurger()
    {
        AudioSource.Play();
        Destroy(PlayerMovement.instance.GetCurrentBurger().gameObject);
        if (animator != null)
            animator.SetTrigger("Interact");

        return true;
    }

    public override bool GrabBurger() { return false; }

    public override bool SwapBurger() { return false; }

    public override bool CombineBurger() { return false; }
}
