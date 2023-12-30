using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 目標: PlayerｍのAnimatorを操作して、移動アニメーションを再生する
    // InputX
    // InputY
    // IsMoving

    Animator animator;

    private void Awake()
    {
        // 自分についているAnimatorコンポーネントを取得する
        animator = GetComponent<Animator>();
    }

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

            // 入力があるなら
            if (x != 0 || y != 0)
            {
                // 移動方向をアニメーターに渡す
                animator.SetFloat("InputX", x);
                animator.SetFloat("InputY", y);

                StartCoroutine(Move(new Vector2(x, y)));
            }
        }

        animator.SetBool("IsMoving", isMoving);
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
