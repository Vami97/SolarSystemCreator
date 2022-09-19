using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyModPanel : MonoBehaviour
{
    public BodyModification bodMod;

    private void OnDisable()
    {
        bodMod.CompleteEdit();
    }
}
