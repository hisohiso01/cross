using UnityEngine;

namespace CrossRoundArena.Data
{
    [CreateAssetMenu(fileName = "NewLeaderData", menuName = "CrossRoundArena/LeaderData")]
    public class LeaderData : ScriptableObject
    {
        [Header("基本設定")]
        public string leaderName;
        public int maxHp;
        
        [Header("ビジュアル・演出")]
        public Sprite icon;
        [TextArea(3, 10)]
        public string description;
        
        // 特徴に応じたロジックを将来的に追加しやすくするため
        // リーダー固有の特殊アクションIDなど
        public string leaderLogicKey;
    }
}
