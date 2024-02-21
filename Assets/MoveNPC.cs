using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MoveNPC : MonoBehaviour
{
    private Transform rallyPoint1;
    private Transform rallyPoint2;
    private Vector3 rallyPoint2Random;
    private Transform[] transforms;
    public bool bShouldMove = false;
    public float lerpSpeed; // how fast you lerp between two points

    private float startTime;
    private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        int randomNum = rnd.Next(-20, 20); // randomize where the npc stands 

        if (!gameObject.GetComponentInChildren<TextMeshPro>().text.Equals("Sample text"))
        {
            bShouldMove = true;
        }
        rallyPoint1 = GameObject.Find("RallyPoint1").transform;
        rallyPoint2 = GameObject.Find("RallyPoint2").transform;
        rallyPoint2Random = new Vector3(rallyPoint2.position.x+randomNum, rallyPoint2.position.y, rallyPoint2.position.z);
        //rallyPoint2.position = rallyPoint2Random;
        transforms = new Transform[] { rallyPoint1, rallyPoint2};

        startTime = Time.time;
        journeyLength = Vector3.Distance(rallyPoint1.position, rallyPoint2.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (bShouldMove)
            moveThisNPC();
    }

    void moveThisNPC()
    {
        float distCovered = (Time.time - startTime) * lerpSpeed;

        float fractionOfJourney = distCovered / journeyLength;

        

        if(fractionOfJourney < 1) 
        {
            gameObject.transform.position = Vector3.Lerp(rallyPoint1.position, new Vector3(rallyPoint2.position.x, rallyPoint2.position.y, rallyPoint2.position.z), fractionOfJourney);
        }   
        else
        {
            bShouldMove = false; // stop moving once npc gets to the rally point
        }
    
    }
}
