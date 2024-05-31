using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public InteractionArea interaction;

    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 holdingOffset;
    [SerializeField] private GameObject holdingObject;
    Vector2 movement;

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
        if (interaction != null) 
        {
            if (holdingObject != null && interaction.holdingObject != null)
            {
                if (!interaction.hasInfiniteSupply)
                    SwapObject();
            }
            else if (interaction.holdingObject != null)
            {
                GrabObject();
            }
            else if (holdingObject != null)
            {
                PlaceObject();
            }
        }
    }

    private void SwapObject()
    {
        interaction.holdingObject.transform.parent = transform;
        interaction.holdingObject.transform.localPosition = holdingOffset;

        holdingObject.transform.parent = interaction.transform;
        holdingObject.transform.localPosition = Vector2.zero;

        GameObject tempHoldingObject = interaction.holdingObject;
        interaction.holdingObject = holdingObject;
        holdingObject = tempHoldingObject;
    }

    private void GrabObject()
    {
        if (!interaction.hasInfiniteSupply)
        {
            interaction.holdingObject.transform.parent = transform;
            interaction.holdingObject.transform.localPosition = holdingOffset;
            holdingObject = interaction.holdingObject;
            interaction.holdingObject = null;
            animator.SetBool("IsCarrying", true);
        }
        else
        {
            holdingObject = Instantiate(interaction.holdingObject, transform);
            holdingObject.transform.localPosition = holdingOffset;
            animator.SetBool("IsCarrying", true);
        }
    }

    private void PlaceObject()
    {
        if (!interaction.isTrashCan)
        {
            holdingObject.transform.parent = interaction.transform;
            holdingObject.transform.localPosition = Vector2.zero;
            interaction.holdingObject = holdingObject;
            holdingObject = null;
            animator.SetBool("IsCarrying", false);
        }
        else
        {
            Destroy(holdingObject);
            holdingObject = null;
            animator.SetBool("IsCarrying", false);
        }
    }

    private void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + (movement * moveSpeed));
    }
}
