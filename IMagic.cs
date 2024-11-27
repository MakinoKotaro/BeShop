using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 魔法発射のインターフェイス
/// </summary>
public interface IMagic 
{
    void Cast(Transform castPoint);
}
