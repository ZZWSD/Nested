using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public float gridSize = 1f;
    public float moveSpeed = 6f;

    private bool isMoving = false;
    private Vector3 facing = Vector3.forward;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) return;

        // 轉向
        if (Input.GetKeyDown(KeyCode.A))
            Turn(-90);
        if (Input.GetKeyDown(KeyCode.D))
            Turn(90);

        // 前進 / 後退
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Move(facing));
        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Move(-facing));
    }
    void Turn(float angle)
    {
        transform.Rotate(0, angle, 0);
        facing = transform.forward;
    }

    IEnumerator Move(Vector3 dir)
    {
        isMoving = true;

        Vector3 start = transform.position;
        Vector3 end = start + dir.normalized * gridSize;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.position = new Vector3(
            Mathf.Round(end.x),
            start.y,
            Mathf.Round(end.z)
        );
            isMoving = false; 
    }
}
