using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : InteractionArea
{
    [SerializeField] private GameObject burgerPrefab;

    public override bool PlaceBurger() { return false; }

    public override bool GrabBurger()
    {
        GameObject tempObject = Instantiate(burgerPrefab, PlayerMovement.instance.transform);
        PlayerMovement.instance.holdingObject = tempObject.GetComponent<Burger>();
        foreach (var item in holdingObject.Contents)
        {
            PlayerMovement.instance.holdingObject.AddIngredientByType(item.Type);
        }
        if (holdingObject.hasBuns)
        {
            PlayerMovement.instance.holdingObject.AddBuns();
        }
        PlayerMovement.instance.holdingObject.transform.localPosition = (Vector3)PlayerMovement.instance.holdingOffset;

        return true;
    }

    public override bool SwapBurger() { return false; }
}
