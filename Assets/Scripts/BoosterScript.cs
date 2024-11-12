using UnityEngine;

public class booster_script : MonoBehaviour
{
    public float BoostForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(0, BoostForce, 0, ForceMode.Impulse);
    }
}
