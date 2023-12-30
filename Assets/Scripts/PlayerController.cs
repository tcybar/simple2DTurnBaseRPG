using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 目標: キーボードで移動する
    // 一回の入力で、1マス分徐々に自動で動く
    // 移動中は入力を受け付けない
    // 徐々に動く: コルーチン
    bool isMoving = false;
    void Update()
    {
        if (!isMoving)
        {
            // Rawは整数値で返す(-1, 0 , 1)
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (x != 0)
            {
                y = 0;
            }

            StartCoroutine(Move(new Vector2(x, y)));
        }
    }

    // 1マス徐々に近づける
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;
        // 現在とターゲットの場所が違うなら、近づけ続ける
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // 近づける
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime); //(現在地、目標地点、速度)
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
