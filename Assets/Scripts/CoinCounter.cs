using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public Text ScoreText;
    public Text FinishText;
    private bool GameEnd;
    private int TotalCoins;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // subscibe to all children methods
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<CoinScript>().onCoinCollection.AddListener(count);
        }
            
        audioSource = GetComponent<AudioSource>();
        TotalCoins = gameObject.transform.childCount;
        ScoreText.text = "0\\" + TotalCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       if (GameEnd && Input.GetKey(KeyCode.Space)){
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if (DoesSceneExist(currentScene + 1))
            {
                SceneManager.LoadScene(currentScene + 1);
            }
            else
            {
                // if scene doesn't exist, go to menu instead
                SceneManager.LoadScene(0);
            }
        }
    }

    public void count()
    {
        audioSource.Play();
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
            ScoreText.text = TotalCoins.ToString() + "\\" + TotalCoins.ToString();
            FinishText.gameObject.SetActive(true);
            print("You collected all the coins");
            GameEnd = true;
        }
        else
        {
            ScoreText.text = (TotalCoins - activeCoins+1).ToString() + "\\" + TotalCoins.ToString();
            print("You have " + (activeCoins-1) + "coins to collect");
        }
    }

    public bool DoesSceneExist(int sceneBuildIndex)
    {
        return sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings;
    }
}
