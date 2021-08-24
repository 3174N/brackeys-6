using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue DialogueToTrigger;

    public void TriggerDialog()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(DialogueToTrigger);
    }
}
