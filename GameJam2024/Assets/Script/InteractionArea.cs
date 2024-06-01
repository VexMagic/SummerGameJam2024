using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public Burger holdingObject;
    public Vector2 holdingOffset;

    private void Start()
    {
        holdingObject = GetComponentInChildren<Burger>();

        if (holdingObject != null)
            holdingObject.transform.localPosition = holdingOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.instance.interaction = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerMovement.instance.interaction == this)
        {
            PlayerMovement.instance.interaction = null;
        }
    }

    public virtual bool PlaceBurger()
    {
        PlayerMovement.instance.holdingObject.transform.parent = transform;
        PlayerMovement.instance.holdingObject.transform.localPosition = holdingOffset;
        holdingObject = PlayerMovement.instance.holdingObject;

        return true;
    }

    public virtual bool GrabBurger()
    {
        holdingObject.transform.parent = PlayerMovement.instance.transform;
        holdingObject.transform.localPosition = PlayerMovement.instance.holdingOffset;
        PlayerMovement.instance.holdingObject = holdingObject;
        //Destroy(holdingObject.gameObject);
        holdingObject = null;

        //interaction.holdingObject.transform.parent = transform;
        //interaction.holdingObject.transform.localPosition = holdingOffset;
        //holdingObject = interaction.holdingObject;
        //interaction.holdingObject = null;

        return true;
    }

    public virtual bool SwapBurger()
    {
        holdingObject.transform.parent = PlayerMovement.instance.transform;
        holdingObject.transform.localPosition = PlayerMovement.instance.holdingOffset;

        PlayerMovement.instance.holdingObject.transform.parent = transform;
        PlayerMovement.instance.holdingObject.transform.localPosition = holdingOffset;

        Burger tempHoldingObject = holdingObject;
        holdingObject = PlayerMovement.instance.holdingObject;
        PlayerMovement.instance.holdingObject = tempHoldingObject;

        return true;
    } 

    public virtual bool CombineBurger()
    {
        foreach (var item in holdingObject.Contents)
        {
            PlayerMovement.instance.holdingObject.AddIngredientByType(item.Type);
        }

        if (holdingObject.hasBuns)
            PlayerMovement.instance.holdingObject.AddBuns();

        if (!(this is Supply))
        {
            Destroy(holdingObject.gameObject);
            holdingObject = null;
        }

        return true;
    }
}
