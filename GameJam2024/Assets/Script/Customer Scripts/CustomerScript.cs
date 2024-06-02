using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    //ADD food reference for order
    public float customerTimer = 60f;
    bool receiveOrder = false;

    public GameObject exampleBurger;
    public ProgressBar progressBar;
    private GameObject desiredBurger;
    private int randomNum;
    private Burger burger;

    public CustomerSpace space;

    [SerializeField] Vector2 orderOffset;
    [SerializeField] SpriteRenderer orderBubble;
    [SerializeField] List<Ingredient> customerContents = new List<Ingredient>();
    [SerializeField] SpriteRenderer TopBun;
    [SerializeField] SpriteRenderer BottomBun;
    public float Height;

    [SerializeField] GameObject Patty;
    [SerializeField] GameObject Ketchup;
    [SerializeField] GameObject Onion;
    [SerializeField] GameObject Lettuce;

    [SerializeField] List<Color> SkinColors;
    [SerializeField] List<Sprite> PossibleHats;
    [SerializeField] [Range(0f,1f)] float HatChance;
    [SerializeField] SpriteRenderer HatPosition;
    [SerializeField] AudioSource HappyCustomer;

    [SerializeField] int CorrectBurgerPoints;

    const bool DictionaryDebug = false;

    void Start()
    {
        customerTimer = 15f;
        desiredBurger = Instantiate(exampleBurger, transform);
        desiredBurger.transform.localPosition = orderOffset;
        desiredBurger.SetActive(true);
        burger = desiredBurger.GetComponent<Burger>();
        CustomerOrder();
        orderBubble.size = new Vector2(2, burger.Height + 0.5f);

        GetComponent<SpriteRenderer>().color = SkinColors[Random.Range(0,SkinColors.Count)];

        if (Random.Range(0f,1f) >= HatChance)
            HatPosition.sprite = PossibleHats[Random.Range(0, PossibleHats.Count)];

        if (space == null)
        {
            Debug.LogError("NO CUSTOMER SPACE AT START");
        }
    }

    void Update()
    {
        customerTimer -= Time.deltaTime;

        if (customerTimer < 0)
        {
            if (space != null)
            {
                space.CustomerLeaves();
            }
            else
            {
                Debug.LogError("NO CUSTOMER SPACE WHEN RUNNING OUT OF TIME");
            }
            Destroy(gameObject);
        }
    }

    public void CustomerOrder()
    {
        randomNum = Random.Range(0, 4);
        burger.hasBuns = true;

        switch (randomNum)
        {
            case 0:
                // Patty, Ketchup, Onion burger
                AddIngredientsToBurger(new List<GameObject> { Patty, Ketchup, Onion });
                break;
            case 1:
                // Patty, Ketchup, Onion, Lettuce burger
                AddIngredientsToBurger(new List<GameObject> { Patty, Ketchup, Onion, Lettuce });
                break;
            case 2:
                // Double-patty burger with lettuce
                AddIngredientsToBurger(new List<GameObject> { Patty, Patty, Lettuce });
                break;
            case 3:
                AddIngredientsToBurger(new List<GameObject> { Patty, Ketchup, Patty, Ketchup });
                break;
            case 4:
                AddIngredientsToBurger(new List<GameObject> { Lettuce, Lettuce, Lettuce, Onion });
                break;

        }
    }

    void AddIngredientsToBurger(List<GameObject> ingredients)
    {
        foreach (GameObject ingredient in ingredients)
        {
            burger.AddIngredient(ingredient);

            if (burger.Contents[^1].Type == IngredientType.Patty)
                (burger.Contents[^1] as Patty).Cook(1f);
            
            InternalOrderCheck(ingredient);
            burger.UpdateSprites();
        }
    }

    void InternalOrderCheck(GameObject ingredient)
    {
        //GameObject newIngredient = ingredient;
        customerContents.Add(ingredient.GetComponent<Ingredient>());
    }    

    public bool ReceiveOrder(Burger otherBurger)
    {
        Debug.Log("Receive Order");
        if (Compare(burger, otherBurger))
        {
            if (space != null)
            {
                space.CustomerLeaves();
                progressBar.Done();
            }
            else
            {
                Debug.LogError("NO SPACE WHEN RECEIVING ORDER");
            }

            if (customerContents.All(otherBurger.Contents.Contains) && !customerContents.SequenceEqual(otherBurger.Contents))
                GameObject.Find("Game Manager").GetComponent<GameManager>().GivePoints(Mathf.FloorToInt(CorrectBurgerPoints / 2f));

            else
                GameObject.Find("Game Manager").GetComponent<GameManager>().GivePoints(CorrectBurgerPoints);

            StartCoroutine(PlayEffects());
            return true;
        }
        return false;
    }

    bool Compare(Burger order, Burger received)
    {
        Dictionary<IngredientType, int> orderCount = CountIngredients(customerContents, true);
        Dictionary<IngredientType, int> receivedCount = CountIngredients(received.Contents, false);

        foreach (IngredientType ingredient in orderCount.Keys)
        {
            if (!receivedCount.ContainsKey(ingredient) || receivedCount[ingredient] != orderCount[ingredient])
            {
                return false;
            }
        }
        return true;
    }

    Dictionary<IngredientType, int> CountIngredients(List<Ingredient> ingredients, bool isOrder)
    {
        Dictionary<IngredientType, int> ingredientCount = new Dictionary<IngredientType, int>();
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.Type == IngredientType.Patty && !isOrder)
            {
                if ((ingredient as Patty).State != PattyState.Finished)
                {
                    Debug.Log((ingredient as Patty).State);
                    continue;
                }
            }

            if (ingredientCount.ContainsKey(ingredient.Type))
            {
                ingredientCount[ingredient.Type]++;
            }
            else
            {
                ingredientCount.Add(ingredient.Type, 1);
            }
        }

        if (DictionaryDebug)
        {
            string tempDebug = string.Empty;

            foreach (var item in ingredientCount)
            {
                tempDebug += item.Key + ": " + item.Value + " - ";
            }

            Debug.Log(tempDebug);
        }

        return ingredientCount;
    }

    public void CustomerLeaves()
    {
        if (burger != null)
        {
            Destroy(burger.gameObject);
        }
        else
        {
            Debug.LogError("Burger object is null.");
        }
    }

    IEnumerator PlayEffects()
    {
        HappyCustomer.Play();
        HideSprites();
        yield return new WaitUntil(() => !HappyCustomer.isPlaying);
        Destroy(gameObject);
    }

    public void HideSprites()
    {
        orderBubble.enabled = false;
        HatPosition.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
