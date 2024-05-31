using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public InteractionArea interaction;

    [SerializeField] private Rigidbody2D rBody;
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
    }

    private void OnInteract()
    {
        if (interaction != null) 
        {
            if (holdingObject != null && interaction.holdingObject != null)
            {
                interaction.holdingObject.transform.parent = transform;
                interaction.holdingObject.transform.localPosition = holdingOffset;

                holdingObject.transform.parent = interaction.transform;
                holdingObject.transform.localPosition = Vector2.zero;

                GameObject tempHoldingObject = interaction.holdingObject;
                interaction.holdingObject = holdingObject;
                holdingObject = tempHoldingObject;
            }
            else if (interaction.holdingObject != null)
            {
                interaction.holdingObject.transform.parent = transform;
                interaction.holdingObject.transform.localPosition = holdingOffset;
                holdingObject = interaction.holdingObject;
                interaction.holdingObject = null;
            }
            else if (holdingObject != null)
            {
                holdingObject.transform.parent = interaction.transform;
                holdingObject.transform.localPosition = Vector2.zero;
                interaction.holdingObject = holdingObject;
                holdingObject = null;
            }
        }
    }

    private void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + (movement * moveSpeed));
    }
}
