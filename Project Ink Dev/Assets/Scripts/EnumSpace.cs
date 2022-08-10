namespace EnumSpace{
    public enum ItemType
    {
        main,
        hidden
    }

    public enum InteractiveType
    {
        type1,  //按照设定轨迹移动的下落物，撞击一次即消失
        type2   //按照设定轨迹移动的下落物，撞击后分为4个部分，玩家依次撞击四个部分各一次，四个部分都消失视为撞击一次掉落物
    }
}