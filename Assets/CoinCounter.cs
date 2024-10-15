using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void count()
    {
        int activeCoins = 0;
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf)
            {
                activeCoins += 1;
            }
        }

        if (activeCoins == 1)
        {
            print("You collected all the coins");
        }
        else
        {
            print("You have " + (activeCoins-1) + "coins to collect");
        }
    }
}
