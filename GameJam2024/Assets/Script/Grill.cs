using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Progress;

public class Grill : InteractionArea
{
    [SerializeField] private GameObject burgerPrefab;
    [SerializeField] private float secondsToGrill;
    [SerializeField] AudioSource AudioSource;
    private ProgressBar progressBar;
    private PattyState currentState;

    public override bool PlaceBurger()
    {
        if (!PlayerMovement.instance.GetCurrentBurger().hasBuns && PlayerMovement.instance.GetCurrentBurger().Contents.Count > 0)
        {
            if (PlayerMovement.instance.GetCurrentBurger().Contents[0].Type == IngredientType.Patty)
            {
                if (PlayerMovement.instance.GetCurrentBurger().Contents.Count == 1)
                {
                    bool temp = base.PlaceBurger();
                    CreateProgressBar();
                    return temp;
                }
                else
                {
                    PlaceBottomPattyOnGrill();
                }
            }
        }
        
        return false;
    }

    public override bool GrabBurger()
    {
        bool temp = base.GrabBurger();
        RemoveProgressBar();
        return temp;
    }

    public override bool CombineBurger()
    {
        if (!PlayerMovement.instance.GetCurrentBurger().hasBuns && PlayerMovement.instance.GetCurrentBurger().Contents.Count > 0)
        {
            if (PlayerMovement.instance.GetCurrentBurger().Contents[0].Type == IngredientType.Patty)
            {
                if ((PlayerMovement.instance.GetCurrentBurger().Contents[0] as Patty).State == PattyState.Raw)
                {
                    if (PlayerMovement.instance.GetCurrentBurger().Contents.Count == 1)
                    {
                        RemoveProgressBar();
                        bool temp = base.SwapBurger();
                        CreateProgressBar();
                        return temp;
                    }
                    else
                    {
                        RemoveProgressBar();
                        base.CombineBurger();
                        PlaceBottomPattyOnGrill();
                        return false;
                    }
                }
            }
        }

        RemoveProgressBar();
        base.CombineBurger();

        return false;
    }

    private void PlaceBottomPattyOnGrill()
    {
        GameObject tempObject = Instantiate(burgerPrefab, transform);
        holdingObject = tempObject.GetComponent<Burger>();
        holdingObject.AddIngredient(PlayerMovement.instance.GetCurrentBurger().Contents[0].gameObject);
        holdingObject.transform.localPosition = (Vector3)holdingOffset;
        CreateProgressBar();

        Destroy(PlayerMovement.instance.GetCurrentBurger().Contents[0].gameObject);
        PlayerMovement.instance.GetCurrentBurger().Contents.RemoveAt(0);
        PlayerMovement.instance.GetCurrentBurger().UpdateSprites();
    }

    private void CreateProgressBar()
    {
        //RemoveProgressBar();
        currentState = (holdingObject.Contents[0] as Patty).State;
        if (currentState == PattyState.Raw)
            progressBar = ProgressManager.instance.CreateBar(transform.position - new Vector3(0, 0.5f), secondsToGrill, (holdingObject.Contents[0] as Patty).CookedProcentage * secondsToGrill, true);
        else if (currentState == PattyState.Finished)
            progressBar = ProgressManager.instance.CreateBar(transform.position - new Vector3(0, 0.5f), secondsToGrill, ((holdingObject.Contents[0] as Patty).CookedProcentage - 1) * secondsToGrill);

    }

    private void RemoveProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.Done();
            progressBar = null;
        }
    }

    private void FixedUpdate()
    {
        if (holdingObject != null)
        {
            if (!AudioSource.isPlaying)
            {
                //AudioSource.pitch = 1f + Random.Range(-0.1f,0.1f);

                AudioSource.Play();
            }

            if (holdingObject.Contents[0] is Patty)
            {
                (holdingObject.Contents[0] as Patty).Cook(Time.fixedDeltaTime / secondsToGrill);
                if (progressBar == null)
                    CreateProgressBar();
            }
        }

        else if (AudioSource.isPlaying)
            AudioSource.Stop();
    }
}
