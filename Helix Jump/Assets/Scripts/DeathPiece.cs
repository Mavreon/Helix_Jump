using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPiece : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void OnHit()
    {
        GameManager.singleton.RestartLevel();
    }
}
