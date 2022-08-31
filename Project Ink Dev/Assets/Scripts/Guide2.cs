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
    private bool isFinished = false;

    public GameObject player;
    public TextMeshProUGUI Text;
    public GameObject target;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        guideStep = EnumSpace.GuideStep.Step1;
        string content = "<rotate=90>化身青蛙可使用，蛙跳，蓄力技能增加冲撞距离，请长按鼠标左键蓄力";
        Text.text = content;
        Text.fontSize = 80;
        Debug.Log(Text.text);
        Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.InGuide;

        player.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    void Update()
    {
        //player.transform.LookAt(transform.position);
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
        if (Input.GetMouseButtonUp(0) && isFinished)
        {
            //DOTween.Clear(true);
            string content = "<rotate=90>在花朵变为非透明时可撞击";
            Text.text = content;
            Text.fontSize = 120;
            Debug.Log(Text.text);
            guideStep = EnumSpace.GuideStep.Step2;
            //StopAllCoroutines();
        }
    }

    IEnumerator ChargeStart()
    {
        while(timer < 1f)
        {
            timer += Time.unscaledDeltaTime / threshold;
            yield return null;
        }
        timer = 0;
        isFinished = true;
        //guideStep = EnumSpace.GuideStep.Step2;
        //player.GetComponent<ThirdPersonCamera>().enabled = true;
        //Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
    }

    void Step2()
    {
        //player.GetComponent<ThirdPersonCamera>().enabled = true;
        //player.transform.LookAt(transform.position);
        //if (target == null)
        //{
        //    Destroy(Text.gameObject);
        //    //Debug.Log(Text.text);
        //    Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
        //    Destroy(this.gameObject);
        //}
        timer = 0;
        StartCoroutine(TextOut());
    }

    IEnumerator TextOut()
    {
        while(timer < 1f)
        {
            timer += Time.unscaledDeltaTime / 10f;
            yield return null;
        }

        Destroy(Text.gameObject);
        Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
        Destroy(this.gameObject);
    }
}
