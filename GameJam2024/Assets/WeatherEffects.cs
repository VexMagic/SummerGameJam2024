using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherEffects : MonoBehaviour
{
    public enum WeatherEffect { Mosquitos, Neutral, Rainy, Windy};

    public WeatherEffect currentEffect;

    public float weatherLength = 10f;
    //public GameObject playerObject;
    //public Transform playerTransform;
    public SpriteRenderer playerSprite;
    public Rigidbody2D player;
    Vector2 windDirection = new Vector2(0, 0);
    public float windStrength = 0.5f;

    

    void Start()
    {
        currentEffect = WeatherEffect.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        weatherLength -= Time.deltaTime;

        if (weatherLength < 0) 
        {
            RandomWeather();
        }


    }

    void FixedUpdate()
    {
        UpdateWeather();
    }


    void UpdateWeather()
    {
        

        switch(currentEffect)
        { 
            case WeatherEffect.Mosquitos:
                playerSprite.color = Color.red;
                break;
            case WeatherEffect.Rainy:
                playerSprite.color = Color.blue;

                break;
            case WeatherEffect.Windy:
                playerSprite.color = Color.green;
                if(player != null)
                {
                    //playerTransform.x -= windDirection.x;
                    //player.AddForce(windDirection * windStrength);
                }
                

                break;
            case WeatherEffect.Neutral:

                playerSprite.color = Color.white;
                break;
        }


    }

    void RandomWeather()
    {
        if(currentEffect != WeatherEffect.Neutral)
        {
            currentEffect = WeatherEffect.Neutral;
            weatherLength = 10f;
            
        }
        else
        {
            int randomInt = UnityEngine.Random.Range(0, 2);
            if (randomInt == 0)
            {
                RandomWindDirection();
                currentEffect = WeatherEffect.Windy;
                weatherLength = 10f;
            }
            else if (randomInt == 1)
            {
                currentEffect = WeatherEffect.Mosquitos;
                weatherLength = 10f;
                
            }
            else if(randomInt == 2)
            {
                currentEffect = WeatherEffect.Rainy;
                weatherLength = 10f;
                
            }
        }
        
    }

    void RandomWindDirection()
    {
        int direction = UnityEngine.Random.Range(0, 1);
        //int plusMinus = UnityEngine.Random.Range(0, 1);

        if (direction == 0)
        {
            windDirection = Vector2.left;

            //Push player in one direction
        }
        else
        {
            windDirection = Vector2.right;
            //Push player in other direction
        }
    }

    //public IEnumerator CoRoutineTest()
    //{
    //    yield return new WaitForSeconds(10);
    //}


}

