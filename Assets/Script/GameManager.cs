using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("--Level Objects----")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Rim;
    [SerializeField] private GameObject RimIncreaser;
    [SerializeField] private GameObject[] SpawnLocations;
    [SerializeField] private AudioSource[] Sounds;
    [SerializeField] private ParticleSystem[] Efects;

    SceneManager sceneManager;


    [Header("--Uý Objects----")]
    [SerializeField] private Image[] Goals;
    [SerializeField] private Sprite FinishedGoal;
    [SerializeField] private int RequiredBasketGoal;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI LevelName;
    int BasketCount;
    float FingerPosX;
    void Start()
    {
        LevelName.text =SceneManager.GetActiveScene().name;
        for (int i = 0; i < RequiredBasketGoal; i++)
        {
            Goals[i].gameObject.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale!=0)
        {
            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        FingerPosX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (TouchPosition.x-FingerPosX>-1 && TouchPosition.x - FingerPosX < 1.05)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position,
                            new Vector3(TouchPosition.x-FingerPosX, Platform.transform.position.y, Platform.transform.position.z), 5f);

                        }
                        break;
                    
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Platform.transform.position.x > -1)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position,
                    new Vector3(Platform.transform.position.x - .3f, Platform.transform.position.y, Platform.transform.position.z), .050f);

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Platform.transform.position.x < 1.05)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position,
                    new Vector3(Platform.transform.position.x + .3f, Platform.transform.position.y, Platform.transform.position.z), .050f);

            }
        }

        
    }

    public void Basket(Vector3 poss)
    {
        BasketCount++;
        Goals[BasketCount - 1].sprite = FinishedGoal;
        Efects[0].transform.position = poss;
        Efects[0].gameObject.SetActive(true);
        Sounds[1].Play();
        if (BasketCount >= 1)
            Invoke("StartSpawn", 3f);

        if (BasketCount == RequiredBasketGoal)
        {
            Win();
        }
    }
    public void Win()
    {
        Sounds[3].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.GetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale = 0;
    }
    public void GameOver()
    {

        Sounds[2].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void IncreaseSizeOfBasket(Vector3 poss)
    {
        Efects[1].transform.position = poss;
        Efects[1].gameObject.SetActive(true);
        Sounds[0].Play();
        Rim.transform.localScale = new Vector3(55f, 55f, 55f);

    }
    public void StartSpawn()
    {
        int rnd = Random.Range(0, SpawnLocations.Length - 1);
        RimIncreaser.transform.position = SpawnLocations[rnd].transform.position;
        RimIncreaser.SetActive(true);
    }

    public void ButtonsOptions(string value)
    {

        switch (value)
        {
            case "Pause":
                Panels[0].SetActive(true);
                Time.timeScale = 0;
                break;

            case "Resume":Time.timeScale= 1; Panels[0].SetActive(false);
                break;

            case "Try Again":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1; 
                //Panels[0].SetActive(false);
                break;

            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                Time.timeScale = 1;
                //Panels[0].SetActive(false);
                break;

            case "Quit":
                Application.Quit();
                break;

        }
    }
}
