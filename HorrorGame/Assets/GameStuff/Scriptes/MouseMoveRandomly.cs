using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MouseMoveRandomly : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timerForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool vaildPath;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DelayBetweenPathChange()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timerForNewPath);
        GetNewPath();
        vaildPath = !navMeshAgent.CalculatePath(target, path);
        if (vaildPath)
        {
            //Debug.Log("False Path");
        }

        while (!vaildPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            vaildPath = navMeshAgent.CalculatePath(target, path);

        }
        inCoRoutine = false;

    }

    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }

    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(DelayBetweenPathChange());
        }
    }
}
