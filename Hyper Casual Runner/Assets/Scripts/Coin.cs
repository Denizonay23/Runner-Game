using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //ses para toplama
            FindObjectOfType<AudioManager>().PlaySound("Coin");


            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);

        }
    }
}
