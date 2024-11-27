using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 敵の特殊行動のインターフェイス
/// </summary>
public interface IE_SpecialMove
{
    /// <summary>
    /// 敵の特殊行動
    /// </summary>
    void EnemySpecialMove();
}
