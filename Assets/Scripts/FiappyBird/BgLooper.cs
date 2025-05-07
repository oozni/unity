using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int obstacleCount = 0;
    public Vector3 obstaclePos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstaclePos = obstacles[0].transform.position;

        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstaclePos = obstacles[i].SetRandomPos(obstaclePos, obstacleCount);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObj = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObj * 5;
            collision.transform.position = pos;

            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstaclePos = obstacle.SetRandomPos(obstaclePos, obstacleCount);
        }
    }
}
