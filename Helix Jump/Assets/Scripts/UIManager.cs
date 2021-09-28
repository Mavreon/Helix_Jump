using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Properties...
    public static UIManager singleton;
    public Text scoreText;
    public Text bestScoreText;

    private void Awake()
    {
        if (!singleton)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.singleton.score.ToString();
        bestScoreText.text = "BEST : " + GameManager.singleton.bestScore.ToString();
    }
}
