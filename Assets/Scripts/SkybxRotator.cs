using UnityEngine;

public class SkybxRotator : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(gameObject.transform.eulerAngles, 0.1f);
    }
}
