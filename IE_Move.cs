using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の移動インターフェイス
/// </summary>
public interface IE_Move
{

    /// <summary>
    /// 敵の移動
    /// </summary>
    void EnemyMove(bool foundPlayer, Vector3 playerPosition);
}
