using UnityEngine;

public class TurretDetectorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.GetComponentInParent<TurretScript>().OnPlayerDetect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponentInParent<TurretScript>().OnPlayerLeave();
        }
    }
}
