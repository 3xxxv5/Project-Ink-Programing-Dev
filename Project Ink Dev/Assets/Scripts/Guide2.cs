using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Guide2 : MonoBehaviour
{
    private EnumSpace.GuideStep guideStep;
    private float threshold = 0.2f;
    private float timer = 0f;

    public GameObject player;
    public TextMeshProUGUI Text;

    void OnEnable()
    {
        guideStep = EnumSpace.GuideStep.Step1;
        Text.text = "化身青蛙可使用【蛙跳】蓄力技能增加冲撞距离，请长按鼠标左键蓄力";
        Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.InGuide;
        player.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    void Update()
    {
        switch (guideStep)
        {
            case EnumSpace.GuideStep.Step1:
                //引导玩家蓄力
                Step1();
                break;
            case EnumSpace.GuideStep.Step2:
                //引导玩家移动准心
                Step2();
                break;
        }
    }

    void Step1()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(Input.GetMouseButton(0) && timer == 0)
        {
            player.GetComponent<ThirdPersonCamera>().enabled = true;
            player.GetComponent<PlayerFrog>().enabled = true;
            StartCoroutine(ChargeStart());
        }
        if (Input.GetMouseButtonUp(0))
        {
            DOTween.Clear(true);
            StopAllCoroutines();
        }
    }

    IEnumerator ChargeStart()
    {
        while(timer < 1f)
        {
            timer += Time.unscaledDeltaTime / threshold;
            yield return null;
        }
        Text.text = "";
        guideStep = EnumSpace.GuideStep.Step2;
        player.GetComponent<ThirdPersonCamera>().enabled = true;
        Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
    }

    void Step2()
    {
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        //移动到物体上
        if (Physics.Raycast(ray, out hitInfo))
        {
            //GuideMesManager.Instance.guideStatus = EnumSpace.GuideStatus.InGuide;
            var item = hitInfo.transform.gameObject.GetComponent<PowerFlower3>();
            Debug.Log(hitInfo.transform.gameObject.name);
            if(item != null)
            {
                Debug.Log(123456789);
                Text.text = "在花朵变为非透明时可撞击";
                guideStep = EnumSpace.GuideStep.Step3;
            }
            //
        }
    }
}
