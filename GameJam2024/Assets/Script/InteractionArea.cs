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
            PlayerMovement.instance.interactions.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerMovement.instance.interactions.Contains(this))
        {
            PlayerMovement.instance.interactions.Remove(this);
        }
    }

    public virtual bool PlaceBurger()
    {
        PlayerMovement.instance.GetCurrentBurger().transform.parent = transform;
        PlayerMovement.instance.GetCurrentBurger().transform.localPosition = holdingOffset;
        holdingObject = PlayerMovement.instance.GetCurrentBurger();

        return true;
    }

    public virtual bool GrabBurger()
    {
        holdingObject.transform.parent = PlayerMovement.instance.transform;
        holdingObject.transform.localPosition = PlayerMovement.instance.GetOffset();
        PlayerMovement.instance.SetCurrentBurger(holdingObject);
        holdingObject = null;

        return true;
    }

    public virtual bool SwapBurger()
    {
        holdingObject.transform.parent = PlayerMovement.instance.transform;
        holdingObject.transform.localPosition = PlayerMovement.instance.GetOffset();

        PlayerMovement.instance.GetCurrentBurger().transform.parent = transform;
        PlayerMovement.instance.GetCurrentBurger().transform.localPosition = holdingOffset;

        Burger tempHoldingObject = holdingObject;
        holdingObject = PlayerMovement.instance.GetCurrentBurger();
        PlayerMovement.instance.SetCurrentBurger(tempHoldingObject);

        return true;
    } 

    public virtual bool CombineBurger()
    {
        foreach (var item in holdingObject.Contents)
        {
            PlayerMovement.instance.GetCurrentBurger().AddIngredient(item.gameObject);
        }

        if (holdingObject.hasBuns)
            PlayerMovement.instance.GetCurrentBurger().AddBuns();

        if (!(this is Supply))
        {
            Destroy(holdingObject.gameObject);
            holdingObject = null;
        }

        return true;
    }
}
