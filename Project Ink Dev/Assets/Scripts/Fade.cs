using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    MeshRenderer mr;
    public float i = 0;
    bool isOne = false;
    
    void Start(){
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    void Update(){

        if(!isOne)
        {
            i += Time.deltaTime;
            mr.material.color = new Color(mr.material.color.r,mr.material.color.g,mr.material.color.b,i);
            if(i>=1)
                isOne = true;
        }
        else
        {
            i -= Time.deltaTime;
            mr.material.color = new Color(mr.material.color.r,mr.material.color.g,mr.material.color.b,i);
            if(i<=0)
                isOne = false;
        }
    }
}