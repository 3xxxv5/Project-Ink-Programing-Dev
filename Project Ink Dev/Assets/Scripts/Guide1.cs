using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide1 : MonoBehaviour
{
    private EnumSpace.GuideStep guideStep;
    private float duration = 2.0f;
    public GameObject player;
    [Header("朝向的掉落物")]
    public GameObject target;

    private float timer = 0f;
    public TextMeshProUGUI Text;

    void OnEnable()
    {
        guideStep = EnumSpace.GuideStep.Step1;
        Text.text = "点击左键撞击花朵来收集";
        //初始时禁用相机跟随，禁止视角移动
        player.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    void Update()
    {
        switch (guideStep)
        {
            case EnumSpace.GuideStep.Step1:
                //引导玩家点击鼠标冲刺
                Step1();
                break;
            case EnumSpace.GuideStep.Step2:
                //引导玩家移动准心
                Step2();
                break;
            case EnumSpace.GuideStep.Step3:
                //引导玩家再次点击鼠标冲刺
                Step3();
                break;
        }
    }

    void Step1()
    {
        //锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        //只响应第一次点击
        if (Input.GetMouseButtonDown(0) && timer == 0)
        {
            //第一次点击后开启相机跟随，以及视角允许转动
            //可能需要更改
            player.GetComponent<ThirdPersonCamera>().enabled = true;
            //冲刺结束后修改文字
            StartCoroutine(FinishStep1());
        }
    }

    IEnumerator FinishStep1()
    {
        while(timer < 1f)
        {
            timer += Time.unscaledDeltaTime / duration;
            yield  return null;
        }
        timer = 0;
        Text.text = "移动准心瞄准目标";
        Debug.Log(Text.text);
        guideStep = EnumSpace.GuideStep.Step2;
    }

    void Step2()
    {
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        //移动到物体上
        if(Physics.Raycast(ray, out hitInfo))
        {
            Text.text = "撞击！";
            Debug.Log(Text.text);
            guideStep = EnumSpace.GuideStep.Step3;
        }
    }

    void Step3()
    {
        if (Input.GetMouseButtonDown(0) && timer <= 0)
        {
            Text.text = "收集所有花朵吧";
            Debug.Log(Text.text);
            StartCoroutine(FinishGuide());
        }
        
    }

    IEnumerator FinishGuide()
    {
        while(timer < 1f)
        {
            timer += Time.unscaledDeltaTime / duration;
            yield return null;
        }
        GuideMesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
        Destroy(Text);
        Destroy(this);
    }
}
