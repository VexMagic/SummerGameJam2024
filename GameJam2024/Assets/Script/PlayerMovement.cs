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
    public Vector2 holdingOffset;
    public Burger holdingObject;
    Vector2 movement;

    

    private float moveDebuff = 0.5f;
    public bool debuff = false;

    private void Awake()
    {
        instance = this;
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

    private void OnInteract()
    {
        InteractionArea interaction = GetClosestInteraction();

        if (interaction != null) 
        {
            if (holdingObject != null && interaction.holdingObject != null)
            {
                if (holdingObject.hasBuns && interaction.holdingObject.hasBuns)
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
            else if (holdingObject != null)
            {
                if (interaction.PlaceBurger())
                {
                    holdingObject = null;
                    animator.SetBool("IsCarrying", false);
                }
            }
        }
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

    private void FixedUpdate()
    {
        if(debuff)
        { rBody.MovePosition(rBody.position + (movement * moveSpeed * moveDebuff)); }
        else
        { rBody.MovePosition(rBody.position + (movement * moveSpeed)); }
        
    }


}
