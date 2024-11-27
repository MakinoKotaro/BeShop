using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵が攻撃を受けるインターフェイス
/// </summary>
public interface IE_TakeDamage
{
    /// <summary>
    /// 敵が攻撃を受ける
    /// </summary>
    void EnemyTakeDamage(float damageAmount);
}
