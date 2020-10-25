using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    GameObject player;
    Rigidbody rb; 

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // 늪지대에 들어가는 순간 현재 속도를 줄인다
        if (other.gameObject.tag == "Player")
        {
            rb.velocity *= 0.5f;
            SoundManager.Instance.PlaySwampSound();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // 늪지대를 밟고있다면 이동속도 및 최대 이동속도 감소
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Ball>().canJump = false;
            player.GetComponent<Ball>().speedRate = 0.5f;
            player.GetComponent<Ball>().maxSpeed = 15;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // 늪지대를 밟고있다면 이동속도 및 최대 이동속도 감소
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Ball>().speedRate = 1.0f;
            player.GetComponent<Ball>().maxSpeed = 30;
        }
    }


}
