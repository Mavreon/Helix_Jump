using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    //Properties...
    private Vector2 lastTapPos;
    private Vector3 startRotation;
    private  Quaternion startOrientation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();


    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        startOrientation = transform.rotation;

        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    // Start is called before the first frame update...
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button Down Called");
            lastTapPos = Input.mousePosition;
        }
           
        if(Input.GetMouseButton(0))
        {
            Vector2 currentTapPos = Input.mousePosition;
            if (currentTapPos == lastTapPos)
                return;

            float swipe = lastTapPos.x - currentTapPos.x;
            Debug.Log(swipe);
            if(swipe != 0.0f)
            {
                transform.Rotate(Vector3.up * swipe);
            }
            lastTapPos = currentTapPos;
        }

        if (Input.GetMouseButtonUp(0))
            lastTapPos = Vector2.zero;
    }

    public void ResetHelixOrientation()
    {
        transform.rotation = startOrientation;
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if (!stage)
        {
            Debug.Log("No stage " + stageNumber + " found in allStages list. Are all stages assigned in the list? ");
            return;
        }
        //Change color of background of the stage...
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        //Change color of ball in stage...
        FindObjectOfType<BallController>().GetComponent<MeshRenderer>().material.color = allStages[stageNumber].stageBallColor;

        //Reset helix rotation...***

        //Destroy the old levels if there are any...
        foreach (GameObject go in spawnedLevels)
            Destroy(go);

        //create new level/platforms...
        float levelDistance = helixDistance / stage.Levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for(int i = 0; i < stage.Levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            //Creates a new level...
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Level Spawned");
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            //Creating the gaps...
            int partsToDisable = 12 - stage.Levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }
            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                    leftParts.Add(t.gameObject);
            }

            //Creating the deathparts
            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.Levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if(!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPiece>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }
}
