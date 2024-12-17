using System;
using UnityEngine;

public class DIalogueScript : MonoBehaviour
{

    // after creating a checkpoint, the gameObject is set to inactive
    public static event EventHandler<(string, bool, int)> e_DialogueStart;

    public string TextToDisplay = "";
    public bool FireOnStartup;
    public bool SetCheckpoint; // sets checkpoint on start of game
    public int AnimationId = 0; // if 0 then no animation played at all

    void Start()
    {
        if(FireOnStartup)
        {
            e_DialogueStart?.Invoke(this, (TextToDisplay, SetCheckpoint, AnimationId));
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        e_DialogueStart?.Invoke(this, (TextToDisplay, SetCheckpoint, AnimationId));
        gameObject.SetActive(false);
    }
}
