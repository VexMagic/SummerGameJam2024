using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public Burger holdingObject;
    public Animator animator;
    public Vector2 holdingOffset;
    public bool hasInfiniteSupply;
    public bool isTrashCan;

    private void Start()
    {
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
}
