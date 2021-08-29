using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnStart : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isTriggered)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            _isTriggered = true;
        }
    }
}
