using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherEffects : MonoBehaviour
{
    public enum WeatherEffect { Mosquitos, Neutral, Rainy, Windy};

    public WeatherEffect currentEffect = WeatherEffect.Neutral;

    public float weatherLength = 10f;
    public Rigidbody2D player;
    Vector2 windDirection = new Vector2(0, 0);
    public float windStrength = 1.0f;

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

                break;
            case WeatherEffect.Rainy:

                break;
            case WeatherEffect.Windy:
                if(player != null)
                {
                    player.AddForce(windDirection);
                }
                

                break;
            case WeatherEffect.Neutral:

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
            int randomInt = Random.Range(0, 2);
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
            else
            {
                currentEffect = WeatherEffect.Rainy;
                weatherLength = 10f;
            }
        }
        
    }

    void RandomWindDirection()
    {
        if(player != null)
        {
            int direction = Random.Range(0, 1);
            if(direction == 0)
            {
                windDirection = new Vector2();
                //Push player in one direction
            }
            else
            {
                //Push player in other direction
            }
        }
    }

    IEnumerator CoRoutineTest()
    {
        yield return new WaitForSeconds();
    }
}

