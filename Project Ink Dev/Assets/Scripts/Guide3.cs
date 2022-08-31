using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guide3 : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI Text;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        string content = "鹤眼：化身仙鹤后，可长按鼠标右键增加瞄准时间";
        Text.text = content;
        Debug.Log(Text.text);
        Guide3MesManager.Instance.guideStatus = EnumSpace.GuideStatus.InGuide;
        player.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Destroy(Text.gameObject);
            //Debug.Log(Text.text);
            player.GetComponent<ThirdPersonCamera>().enabled = true;
            Guide3MesManager.Instance.guideStatus = EnumSpace.GuideStatus.OutGuide;
            Destroy(this.gameObject);
        }
    }
}
