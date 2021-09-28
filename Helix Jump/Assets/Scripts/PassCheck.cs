using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Ball"))
        {
            Debug.Log("Ball Checked");
            GameManager.singleton.AddScore(2);
            FindObjectOfType<BallController>().perfectPass++;
            Debug.Log("Perfect Pass is Increased");
        }
        
    }
}
