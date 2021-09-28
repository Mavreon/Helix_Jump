using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{ 
    //Properties...
    public float force = 100.0f;
    private Rigidbody ballRB;
    private bool ignoreNextCollision;
    private Vector3 startPos;
    public int perfectPass = 0;
    public bool isSuperSpeedActive;
    private void Awake()
    {
        startPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            ballRB.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSuperSpeedActive)
        {
            if(!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject);
                Debug.Log("Destroying Platform");
            }
        }

        DeathPiece deathPiece = collision.gameObject.GetComponent<DeathPiece>();
        if (deathPiece)
        {
            deathPiece.OnHit();
        }

        ballRB.velocity = Vector3.zero;
        ballRB.AddForce(Vector3.up * force, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    //Set ball position to it's default start position when a level is reset...
    public void ResetBallPosition()
    {
        transform.position = startPos;
    }
}
