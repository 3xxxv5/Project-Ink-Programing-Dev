using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//��ɫ����������
public abstract class BasePlayer : MonoBehaviour
{
    protected Transform playerTran;
    protected CharacterController playerController;
    protected bool beginCD = false;
    protected float CDCount = 1;
    protected EnumSpace.PlayStatus moveStatus;

    public GameObject characterGO;
    //��ɫĬ���ƶ��ٶ�Ϊ10
    [Header("Ĭ���ٶ�")]
    public float moveSpeed = 10f;
    [Header("��̾���")]
    public float dashDis = 20f;
    [Header("���ʱ��")]
    public float lastTime = 0.5f;
    [Header("���CD")]
    public float dashCD = 2f;
    [Header("�������")]
    public AnimationCurve curv = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1.0f, 0f, 0f));

    //���г�ʼ��ʱ����
    protected void Init()
    {
        playerTran = this.transform;
        playerController = this.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public EnumSpace.PlayStatus GetPlayerMoveStatus()
    {
        return moveStatus;
    }

    public void ChangeMoveStatus(EnumSpace.PlayStatus status)
    {
        moveStatus = status;
    }

    public void SetStatusToIdle()
    {
        moveStatus = EnumSpace.PlayStatus.Idle;
    }

    public void SetToFaint()
	{
        moveStatus = EnumSpace.PlayStatus.Faint;
	}

    public void IsInCD()
    {
        if (beginCD)
        {
            CDCount -= Time.deltaTime / dashCD;
            if (CDCount <= 0)
            {
                beginCD = false;
            }
        }
    }

    //���߼��
    protected void LetMove()
    {
        var camera = Camera.main;
        var screenRay = (camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        Ray ray = new Ray(screenRay.origin + screenRay.direction * 4.3f, screenRay.direction);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, dashDis))
        {
            //print(hitInfo.transform);
            transform.DOMove(hitInfo.point - ray.direction * 1.0f, lastTime).SetEase(curv);
        }
        else
        {
            transform.DOMove(ray.origin + ray.direction * dashDis, lastTime).SetEase(curv);
        }
    }

    //���󷽷���������������
    protected abstract void MouseClick();
    //����walk����
    protected abstract void PlayWalkAnim();

    //��ɫ�ƶ�
    protected void PlayerMove()
    {
        float moveX = 0, moveY = 0, moveZ = 0;

        //ǰ���ƶ�
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ -= moveSpeed * Time.deltaTime;
        }
        //�����ƶ�
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX += moveSpeed * Time.deltaTime;
        }
        if (moveX != 0 || moveZ != 0)
        {
            ChangeMoveStatus(EnumSpace.PlayStatus.Walk);
            PlayWalkAnim();
        }
        playerController.Move(playerTran.TransformDirection(new Vector3(moveX, moveY, moveZ)));

        MouseClick();
    }
}
