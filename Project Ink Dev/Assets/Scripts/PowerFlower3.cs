using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EnumSpace;

public class PowerFlower3 : Item
{
    MeshRenderer mr;
    float matAlpha = 0.8f;
    public float keepTime = 3.0f;
    private float rebirthTimeTemp;
    private float keepTimeTemp;
    bool canPick = true;
    private Rigidbody rb;


    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        mr.sharedMaterial.SetFloat("_AlphaScale", matAlpha);
        keepTimeTemp = keepTime;
        init();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0f, -m_velocity, 0f);
        rebirthTimeTemp = m_rebirthTime;
    }

    void Boom(Collider other)
    {
        PoolManager.release(VFXPrefab, other.transform.position, other.transform.rotation);
    }

    void SetCameraShock()
    {
        CameraStatusController.Instance().SetShock();
        Camera.main.DOShakePosition(0.5f);

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(CameraStatusController.Instance().SetCommon);
    }

    void CollideWithPlayerBehavior(Collider other)
    {
        var stageNum = GameMesMananger.getCurStageNum();
        if (itemType == ItemType.Main)
        {

            if (interactiveType == InteractiveType.Type1)
            {
                //main_item_collection+1
                GameMesMananger.SetCurMainItemNumAdd(stageNum);

                // Camera.main.DOShakeRotation(0.5f);

                Destroy(this.gameObject);

            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
            }
        }

        if (itemType == ItemType.Hidden)
        {
            if (interactiveType == InteractiveType.Type1)
            {
                //hidden_item_collection+1
                GameMesMananger.SetCurHiddenItemNumAdd(stageNum);

                //Camera.main.DOShakeRotation(0.5f);

                Destroy(this.gameObject);
            }
            if (interactiveType == InteractiveType.Type2)
            {
                //doSth
            }
        }

        DropMusicPlay.PlayMusic();
        Boom(other);
        if (CameraStatusController.Instance().GetCameraStatus() == CameraStatus.Common)
        {
            SetCameraShock();
        }
        GameMesMananger.updateUI();
        CanOpenNewStage.UpdateStage();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (other.gameObject.GetComponent<Player>().GetPlayerMoveStatus() == PlayStatus.Dash)
                {
                    if(canPick)
                        CollideWithPlayerBehavior(other);
                }
                break;
        }
        //DefaultOnTriggerEnterImplement(other);
    }

    public override void SetFallingTrack()
    {
        var m = rb.mass;
        Vector3 force = new Vector3(0f, -m * m_acceleratetion, 0f);
        rb.AddForce(force);
    }

    public override void DropRebirth()
    {
        if (m_rebirthTime < 0.00001f)
        {
            m_rebirthTime = rebirthTimeTemp;

            switch (itemType)
            {
                case ItemType.Main:
                    Debug.Log("Rebirth");
                    transform.position = m_originalPosition;
                    break;
                case ItemType.Hidden:
                    Debug.Log("Destroy");
                    Destroy(this.gameObject);
                    break;
            }
        }

        m_rebirthTime -= Time.deltaTime;
    }

    void Fade()
	{
        if (!canPick)
        {
            keepTime += Time.deltaTime;
            mr.material.SetFloat("_AlphaScale", 1-matAlpha);
            if (keepTime >= keepTimeTemp)
                canPick = true;
        }
        else
        {
            keepTime -= Time.deltaTime;
            mr.material.SetFloat("_AlphaScale", matAlpha);
            if (keepTime <= 0)
                canPick = false;
        }
    }

    void Update()
    {
        Fade();
        DropRebirth();
    }

    void FixedUpdate()
    {
        SetFallingTrack();
    }
}
