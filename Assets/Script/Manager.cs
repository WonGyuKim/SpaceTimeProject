using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    Vector3 gravity;
    float timeInterval;
    int position;
    Vector3 velocityBefore;
    List<KeyPosition> posList;
    GameObject actor;
    Actor actorScript;
    int updateNum;
    Vector3[,] dest;
    Vector3[,] vel;
    Vector3[,] force;
    float[] objFunc;

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

        foreach(KeyPosition kp in posList)
        {
            Debug.Log(kp.transform.position);
        }

        actor = Instantiate(Resources.Load("Actor")) as GameObject;
        actor.transform.position = posList[0].transform.position;
        actorScript = actor.GetComponent<Actor>();
        Debug.Log(actor);

        // position = 0;
        objFunc = new float[posList.Count];
        dest = new Vector3[posList.Count - 1, (int)timeInterval];
        vel = new Vector3[posList.Count - 1, (int)timeInterval];
        force = new Vector3[posList.Count - 1, (int)timeInterval];

        for (int i = 0; i < posList.Count - 1; i++)
        {
            for (int j = 0; j < timeInterval; j++)
            {
                vel[i, j] = (posList[i + 1].transform.position - posList[i].transform.position) / timeInterval;
                dest[i, j] = posList[i].transform.position + j * vel[i, j];
            }
        }

        for (int i = 0; i < posList.Count - 1; i++)
        {
            for (int j = 0; j < timeInterval; j++)
            {
                if (j < timeInterval - 1)
                    force[i, j] = actorScript.Mass * (vel[i, j + 1] - vel[i, j]) / timeInterval - actorScript.Mass * gravity;
                else if (j == timeInterval - 1 && i < posList.Count - 2)
                    force[i, j] = actorScript.Mass * (vel[i + 1, 0] - vel[i, j]) / timeInterval - actorScript.Mass * gravity;
                else
                    force[i, j] = actorScript.Mass * (vel[i, j] - vel[i, j]) / timeInterval - actorScript.Mass * gravity;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (position < posList.Count - 1 /*&& updateNum == timeInterval*/)
        //{
        //    Vector3 velocity = (posList[position + 1].transform.position - posList[position].transform.position) * Time.deltaTime;
        //    actor.transform.position += velocity;
        //    velocityBefore = velocity;
        //    updateNum = 0;

        //    Vector3 distance = posList[position + 1].transform.position - actor.transform.position;
        //    float distanceVal = distance.magnitude;

        //    if (distanceVal < 0.1)
        //        position++;
        //}
        //updateNum++;

    }
}
