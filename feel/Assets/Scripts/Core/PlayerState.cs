using System.Collections.Generic;
using UnityEngine;
using CrossRoundArena.Data;

namespace CrossRoundArena.Core
{
    public class PlayerState : MonoBehaviour
    {
        [Header("リーダー設定")]
        public LeaderData leader;
        public int currentHp;
        public bool isGhost;
        public int revivalCount;
        public int ghostTurnCounter; // 復活までのターン（3ターン）

        [Header("リソース管理")]
        public int currentMana;
        public int maxMana;
        public int coins;

        [Header("カード管理")]
        public List<CardData> deck = new List<CardData>();
        public List<CardData> hand = new List<CardData>();
        public List<CardData> field = new List<CardData>(); // 召喚されたモンスター

        public void Initialize(LeaderData data)
        {
            leader = data;
            currentHp = data.maxHp;
            maxMana = 0; // 先攻/後攻で初期値が異なる
            currentMana = 0;
            coins = 0;
            revivalCount = 2;
            isGhost = false;
        }

        // 基本的な動作の追加（ドロー、ダメージ、回復など）
        public void TakeDamage(int damage)
        {
            if (isGhost) return;
            currentHp -= damage;
            if (currentHp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isGhost = true;
            currentHp = 0;
            ghostTurnCounter = 3;
            Debug.Log($"{leader.leaderName} は幽霊状態になりました。");
        }

        public void DrawCard()
        {
            if (deck.Count > 0)
            {
                CardData card = deck[0];
                deck.RemoveAt(0);
                hand.Add(card);
            }
        }
    }
}
