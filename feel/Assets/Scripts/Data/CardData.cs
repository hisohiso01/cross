using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrossRoundArena.Data
{
    public enum CardType
    {
        Monster,
        Skill,
        Equipment
    }

    [Flags]
    public enum Keyword
    {
        None = 0,
        Guard = 1 << 0,     // モンスターが場にいる間、リーダーを保護
        Swift = 1 << 1,     // 召喚ターンに即攻撃可能
        Poison = 1 << 2,    // 状態異常（毒）付与
        Vampire = 1 << 3,   // ダメージ吸収・回復
        Sacrifice = 1 << 4, // 魔王用：生贄効果
        Buff = 1 << 5,      // アイドル用：他のモンスターを強化
    }

    [CreateAssetMenu(fileName = "NewCardData", menuName = "CrossRoundArena/CardData")]
    public class CardData : ScriptableObject
    {
        [Header("基本プロパティ")]
        public string cardName;
        public CardType cardType;
        public int manaCost;        // ターン経過で増えるコスト
        public int coinCost;        // 購入時に消費するコイン

        [Header("モンスター・装備用ステータス")]
        public int attack;          // 攻撃力
        public int health;          // HP（モンスターの場合）
        public int defenseBonus;    // 装備時の追加ボーナス
        
        [Header("キーワード・能力")]
        public Keyword keywords;    // ガード、速攻、毒、吸血など
        
        [Header("ビジュアル・フレーバー")]
        public Sprite cardIllust;
        [TextArea(3, 10)]
        public string abilityDescription;
        
        [Header("追加シナジー")]
        public string synergyTag;   // 「毒」「増殖」「生贄」などのタグ
    }
}
