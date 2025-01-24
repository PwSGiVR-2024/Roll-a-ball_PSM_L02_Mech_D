using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public Text ScoreText;
    public GameObject FinishText;

    private AudioSource _audioSource;
    private bool _gameEnd;
    private int _totalCoins;
    // Start is called before the first frame update
    void Start()
    {
        // subscibe to all children methods
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<CoinScript>().onCoinCollection.AddListener(CountCoins);
        }

        _audioSource = GetComponent<AudioSource>();
        _totalCoins = gameObject.transform.childCount;
        ScoreText.text = "0\\" + _totalCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       if (_gameEnd && Input.GetKey(KeyCode.Space)){
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

    public void CountCoins()
    {
        _audioSource.Play();
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
            ScoreText.text = _totalCoins.ToString() + "\\" + _totalCoins.ToString();
            FinishText.SetActive(true);
            print("You collected all the coins");
            _gameEnd = true;
        }
        else
        {
            ScoreText.text = (_totalCoins - activeCoins+1).ToString() + "\\" + _totalCoins.ToString();
            print("You have " + (activeCoins-1) + "coins to collect");
        }
    }

    public bool DoesSceneExist(int sceneBuildIndex)
    {
        return sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings;
    }
}
