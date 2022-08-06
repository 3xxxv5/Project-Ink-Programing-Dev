using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  // TODO:
  // 1.获取玩家输入，在场景中移动玩家角色游戏对象
  // 2.移动时候，除了位置 position 还要考虑转动 rotation
  // 3.需要将动画也考虑进去

  // 创建一个3D矢量，来表示玩家角色的移动
  Vector3 m_Movement;

  float horizontal;
  float vertical;

  Rigidbody m_Rigidbody;
  Animator m_Animator;

  [SerializeField]
  private float turnSpeed = 20;

  Quaternion m_Rotation = Quaternion.identity;
  // Start is called before the first frame update
  void Start()
  {
    m_Animator = GetComponent<Animator>();
    m_Rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");


  }

  private void FixedUpdate()
  {
    m_Movement.Set(horizontal, 0.0f, vertical);
    m_Movement.Normalize();

    bool hasHrizontal = !Mathf.Approximately(horizontal, 0.0f);
    bool hasVertical = !Mathf.Approximately(vertical, 0.0f);
    bool isWalking = hasHrizontal || hasVertical;

    m_Animator.SetBool("walk", isWalking);

    Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
    m_Rotation = Quaternion.LookRotation(desiredForward);
  }
  private void OnAnimatorMove()
  {
    m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

    m_Rigidbody.MoveRotation(m_Rotation);
  }
}
