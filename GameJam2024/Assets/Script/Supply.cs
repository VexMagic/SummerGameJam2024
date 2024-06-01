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
        PlayerMovement.instance.SetCurrentBurger(tempObject.GetComponent<Burger>());
        foreach (var item in holdingObject.Contents)
        {
            PlayerMovement.instance.GetCurrentBurger().AddIngredientByType(item.Type);
        }
        if (holdingObject.hasBuns)
        {
            PlayerMovement.instance.GetCurrentBurger().AddBuns();
        }
        PlayerMovement.instance.GetCurrentBurger().transform.localPosition = (Vector3)PlayerMovement.instance.GetOffset();

        return true;
    }

    public override bool SwapBurger() { return false; }
}
