using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public List<InteractionArea> interactions;

    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] AudioSource AudioSource;
    //public Vector2 holdingOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Burger leftObject;
    public Burger rightObject;
    private bool leftBurger;
    Vector2 movement;

    private float debuffModifier = 0.5f;
    public bool debuff;

    private void Awake()
    {
        instance = this;
        animator.SetBool("IsCarrying", true);
    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement != Vector2.zero)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
        }

        animator.SetBool("IsWalking", movement != Vector2.zero);
    }

    private void OnLeft()
    {
        leftBurger = true;
        Interact();
    }

    private void OnRight()
    {
        leftBurger = false;
        Interact();
    }

    private void Interact()
    {
        InteractionArea interaction = GetClosestInteraction();

        if (interaction != null) 
        {
            if (GetCurrentBurger() != null && interaction.holdingObject != null)
            {
                if (GetCurrentBurger().hasBuns && interaction.holdingObject.hasBuns)
                {
                    interaction.SwapBurger();
                }
                else
                {
                    interaction.CombineBurger();
                }
            }
            else if (interaction.holdingObject != null)
            {
                if (interaction.GrabBurger())
                {
                    animator.SetBool("IsCarrying", true);
                }
            }
            else if (GetCurrentBurger() != null)
            {
                if (interaction.PlaceBurger())
                {
                    SetCurrentBurger(null);
                    //animator.SetBool("IsCarrying", false);
                }
            }
        }
    }

    private void FootStep()
    {
        AudioSource.Play();
    }

    private InteractionArea GetClosestInteraction()
    {
        InteractionArea closest = interactions[0];
        float distance = Vector2.Distance(closest.transform.position, transform.position);
        for (int i = 1; i < interactions.Count; i++)
        {
            float tempDistance = Vector2.Distance(interactions[i].transform.position, transform.position);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                closest = interactions[i];
            }
        }
        return closest;
    }

    public Burger GetCurrentBurger()
    {
        if (leftBurger)
            return leftObject;
        else
            return rightObject;
    }

    public void SetCurrentBurger(Burger burger)
    {
        if (leftBurger)
            leftObject = burger;
        else
            rightObject = burger;
    }

    public Vector2 GetOffset()
    {
        if (leftBurger)
            return leftOffset;
        else
            return rightOffset;
    }

    private void FixedUpdate()
    {
        if (debuff)
            rBody.MovePosition(rBody.position + (movement * moveSpeed* debuffModifier));
        else
            rBody.MovePosition(rBody.position + (movement * moveSpeed));
    }
}
