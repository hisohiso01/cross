using UnityEngine;
using CrossRoundArena.Core;

namespace CrossRoundArena.Combat
{
    public abstract class LeaderAbility : MonoBehaviour
    {
        protected PlayerState player;

        protected virtual void Start()
        {
            player = GetComponent<PlayerState>();
        }

        // リーダー固有の「特別なアクション」が必要な場合にオーバーライド
        public abstract void ExecuteAction();

        // ターン開始時/終了時などのフック
        public virtual void OnTurnStart() { }
        public virtual void OnTurnEnd() { }
    }
}
