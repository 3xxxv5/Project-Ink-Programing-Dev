namespace EnumSpace
{
  public enum ItemType
  {
    Main,
    Hidden
  }

  public enum InteractiveType
  {
    Type1,  //按照设定轨迹移动的下落物，撞击一次即消失
    Type2   //按照设定轨迹移动的下落物，撞击后分为4个部分，玩家依次撞击四个部分各一次，四个部分都消失视为撞击一次掉落物
  }

  public enum PlayStatus
  {
    // 全部为Trigger
    Idle = 1,
    Walk = 2,
    Dash = 3, // 冲刺虾独有
    Charge = 11, // 蓄力鹤、青蛙独有
    Launch = 12, // 发射鹤、青蛙独有
    Faint,
  }

  public enum CameraStatus
  {
    Shock,
    Common
  }

    public enum BulletTimeStatus
    {
        IN,
        OUT,
    }
}