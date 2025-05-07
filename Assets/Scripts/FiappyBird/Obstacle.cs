using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    GameManager gameManager;

    public Transform bottomObj;
    public Transform topObj;

    public float highPosY = 1.0f;
    public float lowPosY = -1.0f;

    public float holeSizeMin = 1.0f;
    public float holeSizeMax = 3.0f;

    public float widthPadding = 4.0f;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public Vector3 SetRandomPos(Vector3 lastPos, int count)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); // ���Ʒ� ������Ʈ ���� ���� �Ÿ�
        float objPos = holeSize / 2;

        bottomObj.localPosition = new Vector3(0, objPos, 0); // �� ������Ʈ�� y��ġ
        topObj.localPosition = new Vector3(0, -objPos, 0); // �Ʒ� ������Ʈ�� y��ġ

        Vector3 lastPosition = lastPos + new Vector3(widthPadding, 0, 0);
        lastPosition.y = Random.Range(lowPosY, highPosY);

        transform.position = lastPosition;

        return lastPosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            gameManager.Addscore(1);
        }
    }
}
