using System;
using UnityEngine;

public class DIalogueScript : MonoBehaviour
{

    // after creating a checkpoint, the gameObject is set to inactive
    public static event EventHandler<(string, bool, Vector3, int)> e_DialogueStart;
    public static event EventHandler<int> e_ChangeTask;

    public string TextToDisplay = "";
    public bool FireOnStartup;
    public bool SetCheckpoint; // sets checkpoint on start of game
    public int AnimationId = 0; // if 0 then no animation played at all
    public int TaksId = 0;

    void Start()
    {
        if(FireOnStartup)
        {
            e_DialogueStart?.Invoke(this, (TextToDisplay, SetCheckpoint, transform.position, AnimationId));
            e_ChangeTask?.Invoke(this, TaksId);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            e_DialogueStart?.Invoke(this, (TextToDisplay, SetCheckpoint, transform.position, AnimationId));
            e_ChangeTask?.Invoke(this, TaksId);

            gameObject.SetActive(false);
        }
    }
}
