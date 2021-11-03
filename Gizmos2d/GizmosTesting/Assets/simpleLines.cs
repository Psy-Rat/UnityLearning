using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class simpleLines : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Logging test");
        Debug.DrawLine(
                new Vector3(0, 0), 
                new Vector3(50, 50), 
                Color.red, 
                100f);  
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Vector3 target = Input.mousePosition;
        Debug.DrawLine(Vector3.zero, target, Color.green);
        DrawGrid(5, 5, new Vector3(50, 50), target, Color.green);
    }

    private void DrawGrid(int rowNum, int colNum, Vector3 start, Vector3 end, Color color)
    {

        int rowC = Mathf.Max(rowNum + 1, 2);
        int colC = Mathf.Max(colNum + 1, 2);

        Gizmos.color = color;

        for (int i = 0; i < rowC; i++)
        {
            float alpha = ((float)i) / (float)(rowC - 1);
            int hOffset =  Mathf.RoundToInt(start.x * alpha + end.x * (1f - alpha));
            Gizmos.DrawLine(
                new Vector3(hOffset, start.y),
                new Vector3(hOffset, end.y));
        }

        for (int i = 0; i < colC; i++)
        {
            float alpha = ((float)i) / (float)(colC - 1);
            int vOffset = Mathf.RoundToInt(start.y * alpha + end.y * (1f - alpha));
            Gizmos.DrawLine(
                new Vector3(start.x, vOffset),
                new Vector3(end.x, vOffset));
        }
    }

}
