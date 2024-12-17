using UnityEngine;

public class DialogueHandlerScrpit : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject TextBox;

    void Start()
    {
        DIalogueScript.e_DialogueStart += CreateDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
