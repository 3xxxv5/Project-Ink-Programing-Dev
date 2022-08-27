using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EnumSpace;

public class PowerFlower3 : Item
{
    MeshRenderer mr;
    float matAlpha = 0.9f;
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
        var stageNum = GameMesMananger.Instance().getCurStageNum();
        if (itemType == ItemType.Main)
        {

            if (interactiveType == InteractiveType.Type1)
            {
                //main_item_collection+1
                GameMesMananger.Instance().SetCurMainItemNumAdd(stageNum);
                
            }
        }

        if (itemType == ItemType.Hidden)
        {
            if (interactiveType == InteractiveType.Type1)
            {
                //hidden_item_collection+1
                GameMesMananger.Instance().SetCurHiddenItemNumAdd(stageNum);

                if (GameMesMananger.Instance().save != null)
                {
                    if (GameMesMananger.Instance().save.itemMap.Find(x => x.Equals(gameObject.name)) == null)
                    {
                        GameMesMananger.Instance().save.itemMap.Add(gameObject.name);
                    }
                }

                //Camera.main.DOShakeRotation(0.5f);
            }
        }

        DropMusicPlay.PlayMusic();
        Boom(other);
        if (CameraStatusController.Instance().GetCameraStatus() == CameraStatus.Common)
        {
            SetCameraShock();
        }
        GameUIManager.updateUI();
        CanOpenNewStage.UpdateStage();
        Destroy(this.gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                //if (PlayerStatusManager.Instance().GetPlayerMoveStatus() == PlayStatus.Dash ||
                //    PlayerStatusManager.Instance().GetPlayerMoveStatus() == PlayStatus.Launch)
                {
                    Debug.Log(matAlpha - 0.05f);
                    Debug.Log(mr.sharedMaterials[1].GetFloat("_AlphaScale"));
                    if (mr.sharedMaterials[0].GetFloat("_AlphaScale") > matAlpha-0.05f)
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
                    //Debug.Log("Rebirth");
                    transform.position = m_originalPosition;
                    break;
                case ItemType.Hidden:
                    //Debug.Log("Destroy");
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
            //mr.material.SetFloat("_AlphaScale", 1-matAlpha);
            foreach(var m in mr.sharedMaterials)
			{
                m.SetFloat("_AlphaScale", 1 - matAlpha);
			}
            if (keepTime >= keepTimeTemp)
                canPick = true;
        }
        else
        {
            keepTime -= Time.deltaTime;
            //mr.material.SetFloat("_AlphaScale", matAlpha);
            foreach (var m in mr.sharedMaterials)
            {
                m.SetFloat("_AlphaScale", matAlpha);
            }
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
