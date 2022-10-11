using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource ballSound;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Basket"))
        {
            gameManager.Basket(transform.position);
        }
        else if (other.CompareTag("GameOver"))
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballSound.Play();
    }
}
