using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class DialogueHandlerScrpit : MonoBehaviour
{
    public GameObject DialogueObject;
    public GameObject Player;
    public float DialogueBoxShowTime = 7f;
    public float TextSpeed = 0.01f;

    private Text _dialogueText;

    void Start()
    {
        if (DialogueObject == null || Player == null)
        {
            print("DialogueObject or Player is empty! This functionality will be disabled!");
        }
        else
        {
            DIalogueScript.e_DialogueStart += CreateDialogue;
            _dialogueText = DialogueObject.gameObject.GetComponentInChildren<Text>();
        }
    }


    public void CreateDialogue(object sender, (string, bool, Vector3, int) dialogueData)
    {
        string textToDisplay = dialogueData.Item1;
        bool setCheckpoint = dialogueData.Item2;
        Vector3 checkpointPosition = dialogueData.Item3;
        int animationId = dialogueData.Item4;

        Vector3 offScreenPosition = new Vector3(DialogueObject.transform.position.x, -600f, DialogueObject.transform.position.z);
        DialogueObject.transform.position = offScreenPosition;

        // Dialogue box animation
        DialogueObject.SetActive(true);
        _dialogueText.text = ""; 
        DialogueObject.transform.DOMoveY(checkpointPosition.y, 1).SetEase(Ease.InCubic).OnComplete(() => {
            StartCoroutine(AnimateText(textToDisplay));
        });
        StartCoroutine(DelaydBoxClosing());

        if (setCheckpoint)
        {
            // I do this this way to not fire too many events, for spawnpoints to function, player must also so it's not a problem
            // also i get the player y, becouse the checkpoints can have different sizes ect
            Player.GetComponent<MoveController>().SetSpawnPoint(null, new Vector3(checkpointPosition.x, Player.transform.position.y, checkpointPosition.z));
        }

        if (animationId != 0)
        {
            // animation fire logic
        }
    }

    IEnumerator AnimateText(string text)
    {
        foreach (char letter in text)
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    // This closes the dialoguebox after few seconds
    IEnumerator DelaydBoxClosing()
    {
        yield return new WaitForSeconds(DialogueBoxShowTime);
        DialogueObject.transform.DOMoveY(-600, 1).SetEase(Ease.InCubic).OnComplete(() => {
            DialogueObject.SetActive(false);
        });
    }
}
