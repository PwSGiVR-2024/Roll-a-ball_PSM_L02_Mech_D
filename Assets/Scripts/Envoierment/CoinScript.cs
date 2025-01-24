using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour
{
    public UnityEvent onCoinCollection;

    void Update()
    {
        transform.Rotate(20 * Time.deltaTime,0,0);
    }

    void OnTriggerEnter(Collider collider)
    {
        onCoinCollection.Invoke();
        gameObject.SetActive(false);
    }
}
