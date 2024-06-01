using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Grill : InteractionArea
{
    [SerializeField] private GameObject burgerPrefab;

    public override bool PlaceBurger()
    {
        if (PlayerMovement.instance.holdingObject.Contents.Count == 1 && !PlayerMovement.instance.holdingObject.hasBuns)
        {
            if (PlayerMovement.instance.holdingObject.Contents[0].Type == IngredientType.Patty)
            {
                return base.PlaceBurger();
            }
        }
        else if (PlayerMovement.instance.holdingObject.Contents[^1].Type == IngredientType.Patty)
        {
            GameObject tempObject = Instantiate(burgerPrefab, transform);
            holdingObject = tempObject.GetComponent<Burger>();
            holdingObject.AddIngredientByType(IngredientType.Patty);
            holdingObject.transform.localPosition = (Vector3)holdingOffset;

            Destroy(PlayerMovement.instance.holdingObject.Contents[^1].gameObject);
            PlayerMovement.instance.holdingObject.Contents.RemoveAt(PlayerMovement.instance.holdingObject.Contents.Count - 1);
        }
        
        return false;
    }
}
