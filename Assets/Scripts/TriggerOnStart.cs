using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnStart : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isTriggered && !(FindObjectOfType<GameManager>().Level1 || FindObjectOfType<GameManager>().Level2 || FindObjectOfType<GameManager>().Level3))
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            _isTriggered = true;
        }
    }
}
