using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Vector3 pos;

    float AngleX = 0;
    float AngleY = 30;

    public GameObject target;

    void Start()
    {
        pos = target.transform.position;
        pos -= gameObject.transform.forward * 5;
        gameObject.transform.position = pos;

        AngleX = 0;
        AngleY = 0;
    }

    void Update()
    {
        // 카메라의 위치가 공을 기준으로 원 모양의 궤적으로 움직이게 설정한다.
        pos = target.transform.position;
        pos -= gameObject.transform.forward * 5;
        gameObject.transform.position = pos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 화면상의 마우스 위치 변화에 따라 [-90 ~ 90]으로 정규화
        AngleX = (float)Input.mousePosition.x / (float)Screen.width;
        AngleX -= 0.5f;
        AngleX *= 180;

        // y축도 동일하게 적용
        AngleY = (float)Input.mousePosition.y / (float)Screen.height;
        AngleY -= 0.5f;
        AngleY *= 180;

        // y축을 상하 90도(=180도)로 제한
        if (AngleY >= 90)
            AngleY = 90;

        if (AngleY <= -90)
            AngleY = -90;


        // 180으로 나눠 [-90, 90] 사이로 각도를 맞춰주고
        // 3.14를 곱해 radian 단위로 만들어준다.
        Vector3 dir = new Vector3(
              Mathf.Sin(AngleX / 180.0f * 3.14f)
            , Mathf.Sin(AngleY / 180.0f * 3.14f)
            , Mathf.Cos(AngleX / 180.0f * 3.14f));

        // 카메라가 바라보는 방향을 지정
        gameObject.transform.LookAt(dir + gameObject.transform.position);

    }
}
