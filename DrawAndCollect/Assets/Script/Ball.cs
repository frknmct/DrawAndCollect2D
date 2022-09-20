using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("BallScored"))
        {
            gameObject.SetActive(false);
            gameManager.Continue(transform.position);
        }
        else if (col.gameObject.CompareTag("GameOver"))
        {
            gameManager.GameOver();
            gameObject.SetActive(false);
        }
    }
}
