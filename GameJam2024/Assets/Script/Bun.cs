using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer IngredientSprite;

    private void Update()
    {
        IngredientSprite.sortingOrder = (int)(transform.parent.transform.position.y * -10);
    }
}
