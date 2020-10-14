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

    // 공의 이동과 관련된 변수들
    // 공은 카메라가 보는 방향을 기준으로 이동
    Vector3 camForward;
    Vector3 camRight;
    float moveConstant = 0.25f; // 버튼 누름에 따른 이동 강도를 결정하는 상수

    // 추락지역에 떨어지거나 성문을 완전히 파괴하지 못하면 이동할 시작지점의 좌표를 저장
    Vector3 startPos;

    /* state */
    Vector3 acceleration; //한 프레임마다 업데이트할 공의 속도 변화량
    float speed; //공의 z축 속력... 문은 z축으로 부딪힐 수 있으며, 그 속도에 비례하여 피해량을 계산함
    bool canJump; // 점프가 가능한가?
    bool isJump; // (플레이어가 점프키를 누른) 상태인가?

    /* ability */
    public float speedRate; //공의 이동속도를 나타내는 스탯... 기본 1로 지정
    public float maxSpeed; //컨트롤을 통한 공의 최대 이동속도를 제한... 최대 이동속도 도달시 해당 방향으로는 가속을 받지 못함

    void Start()
    {
        Debug.Log(gameObject.transform.forward);
        Debug.Log(GameObject.FindWithTag("door").transform.forward);
        rb = gameObject.GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;

        /* state */
        isJump = true;

        /* ability */
        speedRate = 1.0f;
        maxSpeed = 30;
    }

    private void OnCollisionEnter(Collision other)
    {
        float damage = (int)(speed);
        isJump = false;

        // 땅에 부딪혀야만 점프할 수 있음
        if (other.gameObject.tag == "ground")
        {
            canJump = true;
            isJump = false;
        }

        // 늪지대에 들어가는 순간 현재 속도를 줄인다
        if (other.gameObject.tag == "swamp")
        {
            rb.velocity *= 0.5f;
        }

        // 성문에 부딪히면 현재 속도에 비례하여 데미지를 입힘
        if (other.gameObject.tag == "door")
        {
            Debug.Log("쾅! " + damage);
            // 속도비례 데미지 주기!
            other.gameObject.GetComponent<DoorStat>().DoorTakeDamage(damage);

            SoundManager.Instance.PlayDoorSound(); //성문 부딪혔을 때 재생

            // 시작지점으로 복귀
            gameObject.transform.position = startPos;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }

        if (other.gameObject.tag == "wall")
        {
            SoundManager.Instance.PlayWallSound();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // 늪지대를 밟고있다면 이동속도 및 최대 이동속도 감소
        if (other.gameObject.tag == "swamp")
        {
            speedRate = 0.5f;
            maxSpeed = 15;
        }

        else
        {
            speedRate = 1.0f;
            maxSpeed = 30;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // (점프, 추락 등의 이유로) 바닥에서 떨어지면 점프 불가능
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

            SoundManager.Instance.PlayFallSound();
            gameObject.transform.position = startPos;
            rb.velocity = new Vector3(0, 0, 0); //추락 이후 속도값을 없앰
            rb.angularVelocity = new Vector3(0, 0, 0); //추락 이후 공의 회전을 없앰
        }
    }

    void Update()
    {
        // 점프
        // FixedUpdate에 넣으면 가끔 점프가 먹히지 않을 때가 있어 update에 넣었음
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //rb.AddForce(new Vector3(0, 1, 0) * 400);
            rb.velocity += new Vector3(0, 5, 0);
            canJump = false;
            isJump = true;

            SoundManager.Instance.PlayJumpSound(); //점프할 때 재생
        }

        // 공의 현재 z축 속력
        speed = rb.velocity.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * 카메라가 바라보는 방향에 대한 코드
         * 카메라가 바라보는 방향을 기준으로 y값은 0으로 만들고, (x, z) 값은 정규화해준다. (x^2 + z^2 = 1)
         * 기존 벡터가 (a, b, c)이고 바꿔줄 벡터가 (x, 0, z)라고 하면
         * (a:c) = (x:z)에서 x = (a/c)*z 이고, x^2+z^2 = 1 에서 z = c / sqrt(a^2+c^2) 이다.
         */

        camForward = cam.transform.forward;
        camForward.y = 0;
        // temp = sqrt(a^2+c^2) 값이 0 으로 반올림되는 것을 방지
        float temp = Mathf.Sqrt(cam.transform.forward.x * cam.transform.forward.x + cam.transform.forward.z * cam.transform.forward.z);
        if (temp == 0) temp = 0.000001f;
        camForward.z = cam.transform.forward.z / temp;
        camForward.x = cam.transform.forward.x / cam.transform.forward.z * camForward.z;

        camRight = cam.transform.right;
        camRight.y = 0;
        // temp = sqrt(a^2+c^2) 값이 0 으로 반올림되는 것을 방지
        temp = Mathf.Sqrt(cam.transform.right.x * cam.transform.right.x + cam.transform.right.z * cam.transform.right.z);
        if (temp == 0) temp = 0.000001f;
        camRight.z = cam.transform.right.z / temp;
        camRight.x = cam.transform.right.x / cam.transform.right.z * camRight.z;


        /* 공의 이동과 관련한 코드
         * 카메라가 바라보는 방향으로 RigidBody의 velocity를 직접 더하여 W/A/S/D키를 눌렀을 때 전/좌/후/우 방향으로 속도 부여
         * SpaceBar 입력시 y축 방향으로 속도 부여
         * (10/13) 대각선으로 누른 경우, 각 방향으로의 가속에 대해 Sqrt(2)로 나눠 한 방향으로 이동할 때와 동일한 속도를 부여받음
         * (10/13) 점프 중에는 방향키 조정이 먹지 않도록 함 --> 점프를 통한 가속효과 제거 및 점프구간의 난이도 증가
         */

        acceleration = new Vector3(0, 0, 0);

        // 전진 관련
        if (Input.GetKey(KeyCode.W) && !isJump)
        {
            // 앞+좌
            if (Input.GetKey(KeyCode.A))
            {
                acceleration += camForward * moveConstant * speedRate / Mathf.Sqrt(2);
                acceleration -= camRight * moveConstant * speedRate / Mathf.Sqrt(2);
            }

            // 앞+우
            else if (Input.GetKey(KeyCode.D))
            {
                acceleration += camForward * moveConstant * speedRate / Mathf.Sqrt(2);
                acceleration += camRight * moveConstant * speedRate / Mathf.Sqrt(2);
            }

            // 앞
            else
            {
                acceleration += camForward * moveConstant * speedRate;
            }
        }

        // 후진 관련
        else if (Input.GetKey(KeyCode.S) && !isJump)
        {
            // 후+좌
            if (Input.GetKey(KeyCode.A))
            {
                acceleration -= camForward * moveConstant * speedRate / Mathf.Sqrt(2);
                acceleration -= camRight * moveConstant * speedRate / Mathf.Sqrt(2);
            }

            // 후+우
            else if (Input.GetKey(KeyCode.D))
            {
                acceleration -= camForward * moveConstant * speedRate / Mathf.Sqrt(2);
                acceleration += camRight * moveConstant * speedRate / Mathf.Sqrt(2);
            }

            // 후진
            else
            {
                acceleration -= camForward * moveConstant * speedRate;
            }
        }

        // 좌
        else if (Input.GetKey(KeyCode.A) && !isJump)
        {
             acceleration -= camRight * moveConstant * speedRate;
        }

        // 우
        else if (Input.GetKey(KeyCode.D) && !isJump)
        {
            acceleration += camRight * moveConstant * speedRate;
        }

        // 가만히 있으면 이동속도가 점점 줄어들어야 함
        // float 값이 튀는 것을 막기 위해 일정수치 이하가 되면 0으로 만듦
        else
        {
            float stopMoveRate = 0.99f;

            rb.velocity = new Vector3(rb.velocity.x * stopMoveRate, rb.velocity.y, rb.velocity.z * stopMoveRate);

            if (rb.velocity.x <= 0.001f && rb.velocity.x >= -0.001f)
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

            if (rb.velocity.z <= 0.001f && rb.velocity.z >= -0.001f)
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        // 컨트롤로 늘어날 수 있는 공의 최대속도 제한
        if (rb.velocity.x >= maxSpeed || rb.velocity.x <= -maxSpeed)
            acceleration.x = 0;
        if (rb.velocity.z >= maxSpeed || rb.velocity.z <= -maxSpeed)
            acceleration.z = 0;

        // 컨트롤을 통한 이동속도의 변화 적용
        rb.velocity += acceleration;

    }
}
