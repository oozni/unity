using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    public GameObject originBlock = null;

    private Vector3 desiredPos;
    private Vector3 prevBlockPos;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    Transform lastBlock = null;

    private const float BoundSize = 3.5f;
    private const float MovingBoundSize = 3.0f;
    private const float StackMovingSpeed = 5.0f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin = 0.1f;

    private float blockTransition = 0f;
    private float secondaryPos = 0f;

    public int Score { get { return stackCount; } }
    public int Combo { get { return comboCount; } }
    public int MaxCombo { get => maxCombo; }
    public int BestScore { get =>  bestScore; }
    public int BestCombo { get => bestCombo; }

    private int stackCount = -1;
    private int comboCount = 0;
    private int maxCombo = 0;
    private int bestScore = 0;
    private int bestCombo = 0;

    private const string bestScoreKey = "BestScore";
    private const string bestComboKey = "BestCombo";

    public Color prevColor;
    public Color nextColor;

    private bool isMovingX = true;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if (originBlock == null)
        {
            Debug.Log("block is null");
            return;
        }

        bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);
        bestCombo = PlayerPrefs.GetInt(bestComboKey, 0);

        prevColor = RandomColor();
        nextColor = RandomColor();

        prevBlockPos = Vector3.down;

        SpawnBlock();
        SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGameOver) return;

            if (PlaceBlock())
            {
                SpawnBlock();
            }
            else
            {
                Debug.Log("GameOver");
                UpdateScore();

                isGameOver = true;
                GaemOverEffect();
            }
        }
        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPos, StackMovingSpeed * Time.deltaTime);
    }

    private bool SpawnBlock()
    {
        if(lastBlock != null)
        {
            prevBlockPos = lastBlock.localPosition;
        }

        GameObject newBlock = null;
        Transform newTrans = null;

        newBlock = Instantiate(originBlock);

        if (newBlock == null )
        {
            Debug.Log("newBlock is false");
            return false;
        }

        ColorChange(newBlock);

        newTrans = newBlock.transform;
        newTrans.parent = this.transform;
        newTrans.localPosition = prevBlockPos + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount++;

        desiredPos = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTrans;

        isMovingX = !isMovingX;

        return true;
    }

    Color RandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    private void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.Log("Randerer is null");
            return;
        }

        rn.material.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = RandomColor();
        }
    }

    private void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        float movePos = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;
        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePos * MovingBoundSize, stackCount, secondaryPos);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPos, stackCount, - movePos * MovingBoundSize);
        }
    }

    private bool PlaceBlock()
    {
        Vector3 lastPos = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPos.x - lastPos.x;
            bool isNegativeNum = (deltaX < 0) ? true : false;

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPos.x - lastPos.x) / 2f;
                lastBlock.localPosition = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPos = lastBlock.localPosition;
                tempPos.x = middle;
                lastBlock.localPosition = lastPos;
                lastPos = tempPos;

                float rubbleHalfScale = deltaX / 2f;
                CreateRubble
                    (
                        new Vector3
                        (
                            isNegativeNum
                            ? lastPos.x + stackBounds.x / 2 + rubbleHalfScale 
                            : lastPos.x - stackBounds.x / 2 - rubbleHalfScale
                            , lastPos.y, lastPos.z
                        )
                        ,new Vector3 (deltaX, 1, stackBounds.y)
                    );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPos + Vector3.up;
            }
        }
        else
        {
            float deltaZ = prevBlockPos.z - lastPos.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPos.z + lastPos.z) / 2f;
                lastBlock.localPosition = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPos = lastBlock.localPosition;
                tempPos.z = middle;
                lastBlock.localPosition = lastPos;
                lastPos = tempPos;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble
                    (
                        new Vector3
                        (
                            lastPos.x, lastPos.y, isNegativeNum
                            ? lastPos.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPos.z - stackBounds.y / 2 - rubbleHalfScale
                        ),
                        new Vector3(stackBounds.x, 1, deltaZ)
                    );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPos + Vector3.up;
            }
        }

        secondaryPos = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }

    private void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        go.AddComponent<Rigidbody>();
        go.name = "Rubble";
    }

    private void ComboCheck()
    {
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        if ((comboCount % 5) == 0)
        {
            Debug.Log(" == 5 Combo Success! == ");
            stackBounds += new Vector3(0.5f, 0.5f);
            stackBounds.x = (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
            stackBounds.y = (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
                
        }
    }

    private void UpdateScore()
    {
        if (bestScore < stackCount)
        {
            Debug.Log(" == 최고 점수 갱신 == ");
            bestScore = stackCount;
            bestCombo = maxCombo;

            PlayerPrefs.SetInt(bestScoreKey, bestScore);
            PlayerPrefs.SetInt(bestComboKey, bestCombo);
        }
    }

    private void GaemOverEffect()
    {
        int childCouunt = this.transform.childCount;

        for (int i = 1; i < 20; i++)
        {
            if (childCouunt < i) break;

            GameObject go = transform.GetChild(childCouunt - 1).gameObject;

            if (go.name.Equals("Rubble")) continue;

            Rigidbody rigid = go.AddComponent<Rigidbody>();

            rigid.AddForce
                (
                    (Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f)) * 100f
                );
        }
    }
}
