using UnityEngine;

public class ShrimpAnimator : PlayerAnimator
{
  private Animator m_Animator;
  void Start()
  {
    // 获取动画组件
    this.m_Animator = GetComponent<Animator>();
  }
  public void Walk()
  {
    PlayerStatusChange(EnumSpace.PlayStatus.Walk, this.m_Animator);
    this.m_nowStatus = EnumSpace.PlayStatus.Walk;
  }

  public void Idle()
  {
    PlayerStatusChange(EnumSpace.PlayStatus.Idle, this.m_Animator);
    this.m_nowStatus = EnumSpace.PlayStatus.Idle;
  }

  /// <summary>
  /// 播放冲刺动画，播放完成后会回到Idle
  /// </summary>
  public void Dash()
  {
    PlayerStatusChange(EnumSpace.PlayStatus.Dash, this.m_Animator);
    // 动画播放完自动回到idle
    this.m_nowStatus = EnumSpace.PlayStatus.Idle;
  }
}
