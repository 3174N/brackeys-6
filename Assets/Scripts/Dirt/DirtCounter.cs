using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCounter : MonoBehaviour
{
    public int DirtNum;

    public void RemoveDirt()
    {
        DirtNum--;

        if (DirtNum <= 0)
        {
            FindObjectOfType<GameManager>().Level1 = true;
            FindObjectOfType<GameManager>().Load();
        }
    }
}
