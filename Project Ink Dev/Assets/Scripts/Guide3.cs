using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide3 : MonoBehaviour
{
    private EnumSpace.GuideStep guideStep;
    public GameObject player;
    public TextMeshProUGUI Text;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        guideStep = EnumSpace.GuideStep.Step1;
        string content = "<rotate=90>彼岸花会分裂，撞击所有部分来收集";
        Text.text = content;
        Debug.Log(Text.text);
        Guide3MesManager.Instance.guideStatus = EnumSpace.GuideStatus.InGuide;
        player.GetComponent<PlayerCrane>().enabled = false;
        player.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (guideStep)
        {
            case EnumSpace.GuideStep.Step1:
                Step1();
                break;
            case EnumSpace.GuideStep.Step2:
                Step2();
                break;
        }
        
    }

    private void Step1()
    {
        StartCoroutine("ShowText");
    }

    IEnumerator ShowText()
    {
        float t = 0;
        while(t < 1f)
        {
            t += Time.unscaledDeltaTime / 5f;
            yield return null;
        }
        string content = "<rotate=90>鹤眼：化身仙鹤后，可长按鼠标右键增加瞄准时间";
        Text.text = content;
        guideStep = EnumSpace.GuideStep.Step2;
    }

    private void Step2()
    {
        if (Input.GetMouseButton(1))
        {
            Destroy(Text.gameObject);
            //Debug.Log(Text.text);
            player.GetComponent<PlayerCrane>().enabled = true;
            player.GetComponent<ThirdPersonCamera>().enabled = true;
            Guide3MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
            Destroy(this.gameObject);
        }
    }
}
