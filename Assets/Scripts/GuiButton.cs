using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiButton : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private string targetMessage;


    void OnMouseUp()
    {
        if (target == null) { return; }
        target.SendMessage(targetMessage);
    }
}
