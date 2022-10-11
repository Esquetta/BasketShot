using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Level1"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level1"));
        }
        else
        {
            PlayerPrefs.SetInt("Level1",1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level1"));

        }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
