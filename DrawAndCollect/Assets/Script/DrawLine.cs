using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject line;
    
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositionList;

    public List<GameObject> Lines;
    private bool canDraw;
    private int lineCount;
    [SerializeField] private TextMeshProUGUI lineCountText;
    private void Start()
    {
        canDraw = false;
        lineCount = 3;
        lineCountText.text = lineCount.ToString();
    }

    void Update()
    {

        if (canDraw && lineCount != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(fingerPos,fingerPositionList.Last()) > .1f)
                {
                    UpdateLine(fingerPos);
                }
            }
        }

        if (Lines.Count != 0 && lineCount != 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                lineCount--;
                lineCountText.text = lineCount.ToString();
            }
        }
        
        
    }

    void CreateLine()
    {
        line = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        Lines.Add(line);
        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider = line.GetComponent<EdgeCollider2D>();
        fingerPositionList.Clear();
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0,fingerPositionList[0]);
        lineRenderer.SetPosition(1,fingerPositionList[1]);
        edgeCollider.points = fingerPositionList.ToArray();

    }

    void UpdateLine(Vector2 fingerPos)
    {
        fingerPositionList.Add(fingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,fingerPos);
        edgeCollider.points = fingerPositionList.ToArray();
    }
    
    public void Continue()
    {

        if (BallShooter.scoredBallCount == 0)
        {
            foreach (var item in Lines)
            {
                Destroy(item.gameObject);
            }
            Lines.Clear();
            lineCount = 3;
            lineCountText.text = lineCount.ToString();
        }
        
        
    }

    public void StopDraw()
    {
        canDraw = false;
    }
    public void StartDraw()
    {
        lineCount = 3;
        canDraw = true;
    }
}
