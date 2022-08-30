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
    public GameObject target;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        guideStep = EnumSpace.GuideStep.Step1;
        string content = "化身青蛙可使用【蛙跳】蓄力技能增加冲撞距离，请长按鼠标左键蓄力";
        Text.text = content;
        Debug.Log(Text.text);
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
            //player.GetComponent<ThirdPersonCamera>().enabled = true;
            player.GetComponent<PlayerFrog>().enabled = true;
            StartCoroutine(ChargeStart());
        }
        if (Input.GetMouseButtonUp(0))
        {
            //DOTween.Clear(true);
            
            guideStep = EnumSpace.GuideStep.Step2;
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
        string content = "在花朵变为非透明时可撞击";
        Text.text = content;
        Debug.Log(Text.text);
        //guideStep = EnumSpace.GuideStep.Step2;
        //player.GetComponent<ThirdPersonCamera>().enabled = true;
        //Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
    }

    void Step2()
    {
        player.GetComponent<ThirdPersonCamera>().enabled = true;
        if (target == null)
        {
            string content = "";
            Text.text = content;
            Debug.Log(Text.text);
            Guide2MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
            Destroy(this.gameObject);
        }
    }
}
