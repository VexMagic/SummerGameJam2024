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
    public Vector2 holdingOffset;
    public Burger holdingObject;
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

    //private void CombineObject()
    //{
    //    Debug.Log("Combine");
    //    foreach (var item in interaction.holdingObject.Contents)
    //    {
    //        holdingObject.AddIngredientByType(item.Type);
    //    }

    //    if (interaction.holdingObject.hasBuns)
    //        holdingObject.AddBuns();

    //    if (!interaction.hasInfiniteSupply)
    //    {
    //        Destroy(interaction.holdingObject.gameObject);
    //        interaction.holdingObject = null;
    //    }
    //}

    //private void SwapObject()
    //{
    //    Debug.Log("Swap");
    //    interaction.holdingObject.transform.parent = transform;
    //    interaction.holdingObject.transform.localPosition = holdingOffset;

    //    holdingObject.transform.parent = interaction.transform;
    //    holdingObject.transform.localPosition = interaction.holdingOffset;

    //    Burger tempHoldingObject = interaction.holdingObject;
    //    interaction.holdingObject = holdingObject;
    //    holdingObject = tempHoldingObject;
    //}

    //private void GrabObject()
    //{
    //    Debug.Log("Grab");
    //    if (!interaction.hasInfiniteSupply)
    //    {
    //        interaction.holdingObject.transform.parent = transform;
    //        interaction.holdingObject.transform.localPosition = holdingOffset;
    //        holdingObject = interaction.holdingObject;
    //        interaction.holdingObject = null;
    //        animator.SetBool("IsCarrying", true);
    //    }
    //    else
    //    {
    //        GameObject tempObject = Instantiate(burgerPrefab, transform);
    //        holdingObject = tempObject.GetComponent<Burger>();
    //        foreach (var item in interaction.holdingObject.Contents)
    //        {
    //            holdingObject.AddIngredientByType(item.Type);
    //        }
    //        if (interaction.holdingObject.hasBuns)
    //        {
    //            holdingObject.AddBuns();
    //        }
    //        holdingObject.transform.localPosition = holdingObject.transform.localPosition + (Vector3)holdingOffset;
    //        animator.SetBool("IsCarrying", true);
    //    }
    //}

    //private void PlaceObject()
    //{
    //    Debug.Log("Place");
    //    if (!interaction.isTrashCan)
    //    {
    //        holdingObject.transform.parent = interaction.transform;
    //        holdingObject.transform.localPosition = interaction.holdingOffset;
    //        interaction.holdingObject = holdingObject;
    //        holdingObject = null;
    //        animator.SetBool("IsCarrying", false);
    //    }
    //    else
    //    {
    //        Destroy(holdingObject.gameObject);
    //        holdingObject = null;
    //        animator.SetBool("IsCarrying", false);
    //        if (interaction.animator != null)
    //            interaction.animator.SetTrigger("Interact");
    //    }
    //}

    private void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + (movement * moveSpeed));
    }
}
