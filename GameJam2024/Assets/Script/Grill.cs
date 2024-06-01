using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Grill : InteractionArea
{
    [SerializeField] private GameObject burgerPrefab;
    [SerializeField] private float secondsToGrill;

    public override bool PlaceBurger()
    {
        if (!PlayerMovement.instance.holdingObject.hasBuns && PlayerMovement.instance.holdingObject.Contents.Count > 0)
        {
            if (PlayerMovement.instance.holdingObject.Contents[0].Type == IngredientType.Patty)
            {
                if (PlayerMovement.instance.holdingObject.Contents.Count == 1)
                {
                    return base.PlaceBurger();
                }
                else
                {
                    PlaceBottomPattyOnGrill();
                }
            }
        }
        
        return false;
    }

    public override bool CombineBurger()
    {
        if (!PlayerMovement.instance.holdingObject.hasBuns && PlayerMovement.instance.holdingObject.Contents.Count > 0)
        {
            if (PlayerMovement.instance.holdingObject.Contents[0].Type == IngredientType.Patty)
            {
                if ((PlayerMovement.instance.holdingObject.Contents[0] as Patty).State == PattyState.Raw)
                {
                    if (PlayerMovement.instance.holdingObject.Contents.Count == 1)
                    {
                        return base.SwapBurger();
                    }
                    else
                    {
                        base.CombineBurger();
                        PlaceBottomPattyOnGrill();
                        return false;
                    }
                }
            }
        }

        base.CombineBurger();

        return false;
    }

    private void PlaceBottomPattyOnGrill()
    {
        GameObject tempObject = Instantiate(burgerPrefab, transform);
        holdingObject = tempObject.GetComponent<Burger>();
        holdingObject.AddIngredient(PlayerMovement.instance.holdingObject.Contents[0].gameObject);
        holdingObject.transform.localPosition = (Vector3)holdingOffset;

        Destroy(PlayerMovement.instance.holdingObject.Contents[0].gameObject);
        PlayerMovement.instance.holdingObject.Contents.RemoveAt(0);
        PlayerMovement.instance.holdingObject.UpdateSprites();
    }

    private void FixedUpdate()
    {
        if (holdingObject != null)
        {
            if (holdingObject.Contents[0] is Patty)
            {
                (holdingObject.Contents[0] as Patty).Cook(Time.fixedDeltaTime / secondsToGrill);
            }
        }
    }
}
