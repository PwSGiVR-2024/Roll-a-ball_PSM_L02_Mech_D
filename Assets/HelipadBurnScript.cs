using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HelipadBurnScript : MonoBehaviour
{
    public int AnimationId = 1;
    public GameObject Fires;
    public Image BlinkScreen;

    public float BlinkTime = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DIalogueScript.e_AnimationStart += StartSequence;
    }

    public void StartSequence(object o, int id)
    {
        if (id != AnimationId)
        {
            return;
        }

        StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        Color kolorObrazu = BlinkScreen.color;

        kolorObrazu.a = 0;
        BlinkScreen.color = kolorObrazu;

        while (kolorObrazu.a < 1f)
        {
            kolorObrazu.a += Time.deltaTime / BlinkTime;
            kolorObrazu.a = Mathf.Clamp01(kolorObrazu.a);
            BlinkScreen.color = kolorObrazu;
            yield return null;
        }

        Fires.SetActive(true);

        while (kolorObrazu.a > 0f)
        {
            kolorObrazu.a -= Time.deltaTime / BlinkTime;
            kolorObrazu.a = Mathf.Clamp01(kolorObrazu.a);
            BlinkScreen.color = kolorObrazu;
            yield return null;
        }
    }

}
