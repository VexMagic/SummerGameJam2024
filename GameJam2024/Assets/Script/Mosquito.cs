using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 direction;
    private Vector3 position;
    private float moveSpeed = 0.1f;
    public bool active = false;

    [SerializeField]
    private float timer = 10f;

    private void Start()
    {
        timer = GetComponentInParent<WeatherEffects>().weatherLength;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        position = transform.position;
        direction = (transform.position - playerTransform.position);


        if(timer < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject playerObject = collision.gameObject;
            playerObject.GetComponent<PlayerMovement>().debuff = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject playerObject = collision.gameObject;
            playerObject.GetComponent<PlayerMovement>().debuff = false;
        }
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) > 1)
        {
            transform.position -= (direction * moveSpeed).normalized;
        }
        else
        {
            transform.position = PlayerMovement.instance.transform.position;   
        }
    }
}
