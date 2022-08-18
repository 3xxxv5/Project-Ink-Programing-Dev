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
    // 小于十是bool
    Idle = 1,
    Dash = 2,   // 虾独有
    Walk = 3,
    // 大于十是trigger
    Charge = 11, // 蓄力青蛙独有
    Launch = 12, // 发射青蛙独有
  }

}