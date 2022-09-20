using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallShooter : MonoBehaviour
{

    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject ballShootCenter;
    [SerializeField] private GameObject bucket;
    [SerializeField] private GameObject[] bucketPoints;
    private int currentBallIndex;
    private int randomBucketPointIndex;
    bool _lock;

    public static int scoredBallCount;
    public static int shootedBallCount;

    private void Start()
    {
        shootedBallCount = 0;
        scoredBallCount = 0;
    }
    public void StartGame()
    {
        StartCoroutine(BallShootSystem());
    }
    IEnumerator BallShootSystem()
    {
        while (true)
        {
            if (!_lock)
            {
                yield return new WaitForSeconds(.5f);
                

                if (shootedBallCount != 0 && shootedBallCount % 5 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        BallShootSet();
                    }
                    
                    scoredBallCount = 2;
                    shootedBallCount++;
                }
                else
                {
                    BallShootSet();
                    scoredBallCount = 1;
                    shootedBallCount++;
                }
                yield return new WaitForSeconds(0.7f);
                randomBucketPointIndex = Random.Range(0, bucketPoints.Length - 1);
                bucket.transform.position = bucketPoints[randomBucketPointIndex].transform.position;
                bucket.SetActive(true);
                _lock = true;
                Invoke("CheckBall",5f);

            }
            else
            {
                yield return null;
            }
        } 
    }
    public void Continue()
    {
        if (scoredBallCount == 1)
        {
            _lock = false;
            bucket.SetActive(false);
            CancelInvoke();
            scoredBallCount--;
        }
        else
        {
            scoredBallCount--;
        }
        
        
    }
    float GiveAngle(float value1,float value2)
    {
        return Random.Range(value1, value2);
    }
    Vector3 GivePos(float incomingAngle)
    {
        return Quaternion.AngleAxis(incomingAngle, Vector3.forward) * Vector3.right;
    }
    void CheckBall()
    {
        if(_lock)
            GetComponent<GameManager>().GameOver();
    }
    void BallShootSet()
    {
        Balls[currentBallIndex].transform.position = ballShootCenter.transform.position;
        Balls[currentBallIndex].SetActive(true);
        Balls[currentBallIndex].GetComponent<Rigidbody2D>().AddForce(GivePos(GiveAngle(70f,110f)) * 750);
        if (currentBallIndex != Balls.Length - 1)
        {
            currentBallIndex++;
        }
        else
        {
            currentBallIndex = 0;
        } 
    }
    
}
