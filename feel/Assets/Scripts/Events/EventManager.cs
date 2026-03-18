using System.Collections.Generic;
using UnityEngine;
using CrossRoundArena.Core;
using CrossRoundArena.Managers;

namespace CrossRoundArena.Events
{
    public enum EventType
    {
        Merchant,   // 商人
        BossBattle, // 討伐
        Sandstorm,  // 砂嵐
        Typhoon,    // 台風
        MetalEvent  // メタル
    }

    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        // ラウンド終了時に GameManager から呼び出す
        public void ExecuteRandomEvent(List<PlayerState> players)
        {
            // 本来はランダムだが、デバッグ用にメッセージを出す
            EventType randomEvent = (EventType)Random.Range(0, 5);
            Debug.Log($"[イベント発生] {randomEvent}");

            switch (randomEvent)
            {
                case EventType.Merchant:
                    HandleMerchant(players);
                    break;
                case EventType.Sandstorm:
                    HandleSandstorm(players);
                    break;
                case EventType.Typhoon:
                    HandleTyphoon(players);
                    break;
                case EventType.BossBattle:
                    HandleBossBattle(players);
                    break;
                case EventType.MetalEvent:
                    HandleMetalEvent(players);
                    break;
            }
        }

        private void HandleSandstorm(List<PlayerState> players)
        {
            foreach (var player in players)
            {
                foreach (var monster in player.field)
                {
                    // モンスターのHPを減らす（HPは CardData ではなく PlayerState でインスタンス化した個別のHP管理が必要）
                    // ここでは一旦デバッグログのみ
                    Debug.Log($"{player.leader.leaderName} のモンスターに砂嵐ダメージ!");
                }
            }
        }

        private void HandleTyphoon(List<PlayerState> players)
        {
            foreach (var player in players)
            {
                player.hand.Clear();
                for (int i = 0; i < 5; i++)
                {
                    player.DrawCard();
                }
                Debug.Log($"{player.leader.leaderName} の手札が台風でリセットされました!");
            }
        }

        private void HandleMerchant(List<PlayerState> players)
        {
            // 商人イベントロジック（UIを通じてカード購入を選択させる）
            Debug.Log("商人が現れました! カード購入フェーズへ...");
        }

        private void HandleBossBattle(List<PlayerState> players)
        {
            // ボス出現（全員で協力、または横取り）
            Debug.Log("ボスが出現! 討伐イベント開始!");
        }

        private void HandleMetalEvent(List<PlayerState> players)
        {
            // 高報酬モンスター出現
            Debug.Log("メタルモンスター飛来! チャンス到来!");
        }
    }
}
