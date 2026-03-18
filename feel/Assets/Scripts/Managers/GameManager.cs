using System.Collections.Generic;
using UnityEngine;
using CrossRoundArena.Core;
using CrossRoundArena.Data;
using CrossRoundArena.Events;

namespace CrossRoundArena.Managers
{
    public enum MatchState
    {
        Setup,
        PlayerTurn,
        WaitOtherPlayer,
        EventPhase,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("対戦プレイヤー")]
        public List<PlayerState> activePlayers = new List<PlayerState>();
        public int currentPlayerIndex = 0;
        
        [Header("現在の状態")]
        public MatchState currentState;
        public int currentRound = 1;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            // 初期化フェーズ
            InitializeMatch();
        }

        public void InitializeMatch()
        {
            currentState = MatchState.Setup;
            // 本来はリーダー選択UIなどを経るが、一旦プロトタイプ用に仮配置
            Debug.Log("ゲーム開始: セットアップ中...");
            
            // 全プレイヤーの初期ドロー（6枚）
            foreach (var player in activePlayers)
            {
                for (int i = 0; i < 6; i++)
                {
                    player.DrawCard();
                }
            }
            
            StartNextTurn();
        }

        // ターン管理
        public void StartNextTurn()
        {
            currentState = MatchState.PlayerTurn;
            PlayerState currentPlayer = activePlayers[currentPlayerIndex];
            
            // ターン開始処理
            PrepareTurn(currentPlayer);
            
            Debug.Log($"ラウンド {currentRound}: {currentPlayer.leader.leaderName} のターン開始");
        }

        private void PrepareTurn(PlayerState player)
        {
            // 1枚ドロー
            player.DrawCard();
            
            // コイン獲得
            player.coins += 10; // 仮の値
            
            // 最大コスト+1 (最大10)
            if (player.maxMana < 10) player.maxMana++;
            
            // コスト全回復
            player.currentMana = player.maxMana;
        }

        public void EndTurn()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex >= activePlayers.Count)
            {
                currentPlayerIndex = 0;
                // 全員が終わったらイベントフェーズへ
                StartEventPhase();
            }
            else
            {
                StartNextTurn();
            }
        }

        private void StartEventPhase()
        {
            currentState = MatchState.EventPhase;
            Debug.Log($"ラウンド {currentRound} 終了! イベント発生!");
            
            // イベントマネージャーを呼び出し
            if (EventManager.Instance != null)
            {
                EventManager.Instance.ExecuteRandomEvent(activePlayers);
            }
            
            // 幽霊状態の更新
            UpdateGhostStates();
            
            // ラウンド加算
            currentRound++;
            
            // 次のターンへ
            Invoke(nameof(StartNextTurn), 3.0f); // 3秒後に次のラウンドへ
        }

        private void UpdateGhostStates()
        {
            foreach (var player in activePlayers)
            {
                if (player.isGhost)
                {
                    player.ghostTurnCounter--;
                    if (player.ghostTurnCounter <= 0 && player.revivalCount > 0)
                    {
                        // 復活
                        player.isGhost = false;
                        player.currentHp = 2; // HP2で復活
                        player.revivalCount--;
                        Debug.Log($"{player.leader.leaderName} が復活しました。");
                    }
                }
            }
        }
    }
}
