using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float verticalMoveRate = 20;
    public float horizontalMoveRate = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody Ball = other.gameObject.GetComponent<Rigidbody>();

            Ball.velocity = gameObject.transform.forward * horizontalMoveRate;
            Ball.velocity += new Vector3(0, transform.up.y, 0) * verticalMoveRate;

            // 점프대 작동 중에는 플레이어의 움직임 제한
            other.GetComponent<Ball>().canMove = false;
            other.GetComponent<Ball>().canJump = false;

            FindObjectOfType<SoundManager>().PlayJumpPadSound();
        }
    }
}
