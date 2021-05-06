using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPromptUI : MonoBehaviour
{

    public void TransferPosition(Vector3 targetPosition)
    {
        this.gameObject.transform.position = targetPosition;
    }
}
