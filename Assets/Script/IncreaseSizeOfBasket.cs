using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseSizeOfBasket : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private int TextTime;
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        StartCoroutine(StartTheTimer());
    }


    IEnumerator StartTheTimer()
    {

        TimeText.text = TextTime.ToString();

        while (true)
        {

            yield return new WaitForSeconds(1f);

            TextTime--;
            TimeText.text = TextTime.ToString();

            if (TextTime == 0)
            {
                gameObject.SetActive(false);
                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        gameManager.IncreaseSizeOfBasket(gameObject.transform.position);
    }
}
