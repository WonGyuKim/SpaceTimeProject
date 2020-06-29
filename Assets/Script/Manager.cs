using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    Vector3 gravity;
    int timeInterval;
    int position;
    Vector3 velocityBefore;
    Vector3 force;
    List<KeyPosition> posList;
    GameObject actor;
    Actor actorScript;
    int updateNum;

    // Start is called before the first frame update
    void Start()
    {
        gravity = new Vector3(0, 9.8f, 0);
        velocityBefore = new Vector3(0, 0, 0);
        timeInterval = 10;
        updateNum = 0;
        posList = new List<KeyPosition>();

        foreach (KeyPosition kp in FindObjectsOfType<KeyPosition>())
        {
            Debug.Log(kp.Time);
            posList.Add(kp);
        }

        
        posList.Sort(delegate(KeyPosition k1, KeyPosition k2)
        {
            if (k1.Time < k2.Time) return -1;
            else return 1;
        });
        posList.Add(posList[posList.Count - 1]);

        foreach(KeyPosition kp in posList)
        {
            Debug.Log(kp.transform.position);
        }

        actor = Instantiate(Resources.Load("Actor")) as GameObject;
        actor.transform.position = posList[0].transform.position;
        position = 0;
        actorScript = actor.GetComponent<Actor>();
        Debug.Log(actor);


    }

    // Update is called once per frame
    void Update()
    {
        if (position < posList.Count - 1 /*&& updateNum == timeInterval*/)
        {
            //Vector3 velocity = (posList[position + 1].transform.position - posList[position].transform.position) / timeInterval;
            Vector3 velocity = (posList[position + 1].transform.position - posList[position].transform.position) * Time.deltaTime;
            // Debug.Log("velocity : " + velocity);
            //Vector3 accel = (velocity - velocityBefore) / timeInterval;
            //Debug.Log("accel : " + accel);
            //force = actorScript.Mass * accel - (gravity * actorScript.Mass / timeInterval);
            //actor.transform.position += force;
            actor.transform.position += velocity;
            velocityBefore = velocity;
            updateNum = 0;

            Vector3 distance = posList[position + 1].transform.position - actor.transform.position;
            float distanceVal = distance.magnitude;

            if (distanceVal < 0.1)
                position++;
        }
        updateNum++;

    }
}
