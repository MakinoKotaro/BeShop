using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法の要素の継承スクリプト
/// </summary>
public abstract class MagicBase : MonoBehaviour, IMagic
{
    
    private string magicName; //魔法の名前
    private float manaCost; //魔法のマナコスト
    private float magicDamage; //魔法のダメージ

    //====プロパティたち=======
    public string MagicName { get => magicName; set => magicName = value; }
    internal float ManaCost { get => manaCost; set => manaCost = value; }
    internal float MagicDamage { get => magicDamage; set => magicDamage = value; }
    //===============================


    /// <summary>
    /// マナを消費する処理
    /// </summary>
    /// <param name="magicName"></param>
    /// <param name="manaCost"></param>
    protected void SpendMana(string magicName, float manaCost)
    {
        Debug.Log("マナコスト " + manaCost + " で魔法 " + magicName + " を使った。");
    }

    /// <summary>
    /// /魔法発射の継承メソッド
    /// </summary>
    /// <param name="castPoint"></param>
    public abstract void Cast(Transform castPoint);

    /// <summary>
    /// 魔法の挙動の継承メソッド
    /// </summary>
    /// <param name="targetPoint"></param>
    public abstract void Behaviour(Vector3 targetPoint);
}
