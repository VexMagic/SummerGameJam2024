using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class WeatherEffects : MonoBehaviour
{
    public enum WeatherEffect { Mosquitos, Neutral, Rainy, Windy };

    public static WeatherEffect currentEffect;


    public float weatherLength = 10f;
    //public GameObject playerObject;
    //public Transform playerTransform;
    //public SpriteRenderer playerSprite;
    public Rigidbody2D player;
    Vector2 windDirection = new Vector2(0, 0);
    public float windStrength = 2.5f;
    private float resetReference;
    private int previousWeather;
    [SerializeField] float WeatherDelay;
    bool AwaitingWeather;

    [SerializeField]
    private ParticleSystem mosquitosPrefab;

    public GameObject particleSystemLeft;
    public GameObject particleSystemRight;
    public GameObject particleSystemRain;
    //public Transform particleTransform;

    [SerializeField] AudioSource WeatherSound;
    [SerializeField] AudioClip Raining;
    [SerializeField] AudioClip Wind;
    [SerializeField] AudioClip Buzzing;

    [SerializeField] AudioClip WarningRain;
    [SerializeField] AudioClip WarningWind;
    [SerializeField] AudioClip WarningMosqito;

    void Start()
    {
        currentEffect = WeatherEffect.Neutral;
        resetReference = weatherLength;
    }

    // Update is called once per frame
    void Update()
    {
        weatherLength -= Time.deltaTime;

        if (weatherLength < 0 && !AwaitingWeather)
        {
            RandomWeather();
            //Generate a random weather type
        }
    }

    void FixedUpdate()
    {
        UpdateWeather();
    }

    //Method for the switch-case of weather. The colors are just temporary to ensure they are switching as intended.
    //Add more effects for the different weather types!
    void UpdateWeather()
    {
        switch (currentEffect)
        {
            case WeatherEffect.Mosquitos:
                //playerSprite.color = Color.red;
                break;
            case WeatherEffect.Rainy:
                //playerSprite.color = Color.blue;

                break;
            case WeatherEffect.Windy:
                //playerSprite.color = Color.green;
                if (player != null)
                {
                    //playerTransform.x -= windDirection.x;
                    player.AddForce(windDirection * windStrength);
                }
                break;
            case WeatherEffect.Neutral:

                //playerSprite.color = Color.white;
                break;
        }
    }

    //Method for choosen a random weather.
    void RandomWeather()
    {
        //If the weather type is not Neutral (aka, rainy, windy or mosquito) then it returns to neutral
        if (currentEffect != WeatherEffect.Neutral)
        {
            if (particleSystemLeft.active == true || particleSystemRight.active == true)
            {
                particleSystemLeft.SetActive(false);
                particleSystemRight.SetActive(false);
            }

            if (particleSystemRain == true)
            {
                particleSystemRain.SetActive(false);
            }

            currentEffect = WeatherEffect.Neutral;
            weatherLength = 10f;

            if (WeatherSound.isPlaying)
                WeatherSound.Stop();
        }
        else //If the weather is going from neutral then it chooses a random other type to switch to
        {
            int randomInt = 0;
            do
            {
                randomInt = UnityEngine.Random.Range(0, 3);
            } while (randomInt == previousWeather);
            previousWeather = randomInt;

            switch (randomInt)
            {
                case 0:
                    //RandomWindDirection();
                    currentEffect = WeatherEffect.Windy;
                    //weatherLength = resetReference;
                    break;
                case 1:
                    currentEffect = WeatherEffect.Mosquitos;
                    //ParticleSystem newMosquitos = Instantiate(mosquitosPrefab, transform);
                    //Mosquito newMosquito = newMosquitos.GetComponent<Mosquito>();
                    //newMosquito.playerTransform = player.gameObject.transform;
                    //weatherLength = resetReference;
                    break;
                case 2:
                    //particleSystemRain.SetActive(true);
                    currentEffect = WeatherEffect.Rainy;
                    //weatherLength = resetReference;
                    break;
            }

            StartCoroutine(StartWeather());
        }
    }

    //Choose a random direction for the wind
    void RandomWindDirection()
    {
        int direction = UnityEngine.Random.Range(0, 2);
        //int plusMinus = UnityEngine.Random.Range(0, 1);

        if (direction == 0)
        {
            windDirection = Vector2.left;
            particleSystemLeft.SetActive(true);
            //particleTransform.position = new Vector3(15,0,0);
            //particleTransform.localRotation = Quaternion.Euler(-180, 0, 0);   
            //Push player in one direction
        }
        else
        {
            windDirection = Vector2.right;
            particleSystemRight.SetActive(true);
            //particleTransform.position = new Vector3(-15, 0, 0);
            //particleTransform.localRotation = Quaternion.Euler(0, 0, 0);
            //Push player in other direction
        }
    }

    public IEnumerator StartWeather()
    {
        AwaitingWeather = true;

        switch (currentEffect)
        {
            case WeatherEffect.Windy:
                WeatherSound.clip = WarningWind;
                break;
            case WeatherEffect.Mosquitos:
                WeatherSound.clip = WarningMosqito;
                break;
            case WeatherEffect.Rainy:
                WeatherSound.clip = WarningRain;
                break;
        }

        if (WeatherSound.clip != null)
            WeatherSound.Play();

        yield return new WaitForSeconds(WeatherDelay);

        switch (currentEffect)
        {
            case WeatherEffect.Windy:
                RandomWindDirection();
                weatherLength = resetReference;
                WeatherSound.clip = Wind;
                break;
            case WeatherEffect.Mosquitos:
                ParticleSystem newMosquitos = Instantiate(mosquitosPrefab, transform);
                Mosquito newMosquito = newMosquitos.GetComponent<Mosquito>();
                newMosquito.playerTransform = player.gameObject.transform;
                weatherLength = resetReference;
                WeatherSound.clip = Buzzing;
                break;
            case WeatherEffect.Rainy:
                particleSystemRain.SetActive(true);
                weatherLength = resetReference;
                WeatherSound.clip = Raining;
                break;
        }

        WeatherSound.Play();
        AwaitingWeather = false;
    }
}

