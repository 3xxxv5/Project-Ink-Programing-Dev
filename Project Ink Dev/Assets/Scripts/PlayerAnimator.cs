using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
  protected EnumSpace.PlayStatus m_nowStatus = EnumSpace.PlayStatus.Idle;
  /// <summary>
  /// 修改动画状态<para />
  /// <param name="status">见EnumSpace.PlayStatus<para /></param>
  /// <returns>无返回</returns>
  /// </summary>
  protected void PlayerStatusChange(EnumSpace.PlayStatus status, Animator animator)
  {
    // 播放status动画
    animator.SetTrigger(status.ToString());
  }
}
