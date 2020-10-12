using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 공을 따라다닐 카메라
    // 카메라가 바라보는 방향을 참고하여 공의 움직임을 결정하기 때문에 가져옴
    public GameObject cam;

    // rb는 공의 rigidBody
    Rigidbody rb;

    // power는 버튼 누름에 따른 이동 강도를 결정
    Vector3 ballForward;
    Vector3 ballRight;
    float power = 0.125f;

    // 공은 바닥에 붙어있을 때에만 점프할 수 있음
    bool canJump;

    // 추락지역에 떨어지거나 성문을 완전히 파괴하지 못하면 이동할 시작지점의 좌표를 저장
    Vector3 startPos;

    float speed;

    void Start()
    {
        Debug.Log(gameObject.transform.forward);
        Debug.Log(GameObject.FindWithTag("door").transform.forward);
        rb = gameObject.GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        float damage = (int)(speed);

        // 땅에 부딪혀야만 점프할 수 있음
        if (other.gameObject.tag == "ground")
        {
            canJump = true;
        }

        // 성문에 부딪히면 현재 속도에 비례하여 데미지를 입힘
        if (other.gameObject.tag == "door")
        {
            Debug.Log("쾅! " + damage);
            // 속도비례 데미지 주기!
            other.gameObject.GetComponent<DoorStat>().DoorTakeDamage(damage);

            // 시작지점으로 복귀
            gameObject.transform.position = startPos;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }


    }

    private void OnCollisionExit(Collision other)
    {
        // 바닥에서 떨어지면 점프 불가능
        if (other.gameObject.tag == "ground")
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 추락지역에 떨어지면 최초 시작지점으로 이동
        if (other.gameObject.tag == "fall")
        {
            Debug.Log("Falled !");
            gameObject.transform.position = startPos;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //rb.AddForce(new Vector3(0, 1, 0) * 400);
            rb.velocity += new Vector3(0, 5, 0);
            canJump = false;
        }

        speed = rb.velocity.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* 공의 이동과 관련한 코드
         * RigidBody의 AddForce를 사용하여 카메라가 바라보는 방향을 기준으로 W/A/S/D키를 눌렀을 때 전/좌/후/우 방향으로 가속도 부여
         * SpaceBar 입력시 y축 방향으로 AddForce
         */

        ballForward = cam.transform.forward;
        ballRight = cam.transform.right;

        ballForward.y = 0;
        ballRight.y = 0;

        // 전진
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(cam.transform.forward * power);
            rb.velocity += ballForward * power;
        }

        // 후진
        if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(-cam.transform.forward * power);
            rb.velocity -= ballForward * power;
        }

        // 좌로 굴러
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(-cam.transform.right * power);
            rb.velocity -= ballRight * power;
        }

        // 우로 굴러
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(cam.transform.right * power);
            rb.velocity += ballRight * power;
        }

    }
}
