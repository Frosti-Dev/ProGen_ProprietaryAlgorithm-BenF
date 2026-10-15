using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool debugEnabled = false;
    
    public Transform pointA;
    public Transform pointB;
    public Vector3 resetPoint;

    public float moveSpeed = 2;

    public float waitTime;

    [Header("Location")]
    public bool atPointA;
    public bool atPointB;
    

    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = pointB.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if(transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }

        

    }

    private void LateUpdate()
    {
        if (transform.position == pointA.position)
        {
            atPointA = true;
        }

        if( transform.position.y < resetPoint.y)
        {
            atPointA = false;
        }

        if (transform.position == pointB.position)
        {
            atPointB = true;
        }

        if (transform.position.y > resetPoint.y)
        {
            atPointB = false;
        }
    }
    //IEnumerator Buffer()
    //{
    //    yield return new WaitForSeconds(waitTime);
        
    //    if (resetPoint)
    //    {
    //        atPointA = false;
    //        atPointB = true;
    //    }

    //    if(atPointB)
    //    {
    //        atPointA = true;
    //        atPointB = false;
    //    }
    //}
}
