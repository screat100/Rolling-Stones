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
    Vector3 camForward;
    Vector3 camRight;
    float power = 0.25f;

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
            Debug.Log(damage);
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
            rb.velocity += new Vector3(0, 5, 0);
            canJump = false;
        }

        speed = rb.velocity.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * 카메라가 바라보는 방향에 대한 코드
         * 카메라가 바라보는 방향을 기준으로 y값은 0으로 만들고
         * x, z값은 정규화해준다.
         * 기존 벡터가 (a, b, c)이고 바꿔줄 벡터가 (x, 0, z)라고 하면
         * (a:c) = (x:z)에서 x = (a/c)*z 이고, x^2+z^2 = 1 에서 z = c / sqrt(a^2+c^2) 이다.
         */

        camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.z = cam.transform.forward.z / Mathf.Sqrt(cam.transform.forward.x * cam.transform.forward.x + cam.transform.forward.z * cam.transform.forward.z);
        camForward.x = cam.transform.forward.x / cam.transform.forward.z * camForward.z;

        Debug.Log(camForward);


        camRight = cam.transform.right;
        camRight.y = 0;
        camRight.z = cam.transform.right.z / Mathf.Sqrt(cam.transform.right.x * cam.transform.right.x + cam.transform.right.z * cam.transform.right.z);
        camRight.x = cam.transform.right.x / cam.transform.right.z * camRight.z;

        Debug.Log(camRight);

        /* 공의 이동과 관련한 코드
         * 카메라가 바라보는 방향으로 RigidBody의 velocity를 직접 더하여 W/A/S/D키를 눌렀을 때 전/좌/후/우 방향으로 가속도 부여
         * SpaceBar 입력시 y축 방향으로 AddForce
         */

        // 전진 관련
        if (Input.GetKey(KeyCode.W) && canJump)
        {

            // 앞+좌
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity += camForward * power / Mathf.Sqrt(2);
                rb.velocity -= camRight * power / Mathf.Sqrt(2);
            }

            // 앞+우
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += camForward * power / Mathf.Sqrt(2);
                rb.velocity += camRight * power / Mathf.Sqrt(2);
            }

            // 앞
            else
            {
                rb.velocity += camForward * power;
            }
        }


        // 후진 관련
        else if (Input.GetKey(KeyCode.S) && canJump)
        {
            // 후+좌
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity -= camForward * power / Mathf.Sqrt(2);
                rb.velocity -= camRight * power / Mathf.Sqrt(2);
            }

            // 후+우
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity -= camForward * power / Mathf.Sqrt(2);
                rb.velocity += camRight * power / Mathf.Sqrt(2);
            }

            // 후진
            else
            {
                rb.velocity -= camForward * power;
            }
        }

        // 좌
        else if (Input.GetKey(KeyCode.A) && canJump)
        {
            rb.velocity -= camRight * power;
        }

        // 우
        else if (Input.GetKey(KeyCode.D) && canJump)
        {
            rb.velocity += camRight * power;
        }

    }
}
