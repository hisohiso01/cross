using UnityEngine;
using CrossRoundArena.Core;

namespace CrossRoundArena.Combat
{
    // 戦士: HP消費で強力効果
    public class WarriorAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            if (player.currentHp > 5)
            {
                player.TakeDamage(5); // HPを5消費して
                Debug.Log($"{player.leader.leaderName}: 自分の血を捧げ、攻撃力を上げる!");
                // ここで次の攻撃を強化するフラグを立てるなどの処理
            }
        }
    }

    // 神官: 回復・耐久
    public class PriestAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            if (player.currentHp < player.leader.maxHp)
            {
                player.currentHp += 5; // HPを5回復
                Debug.Log($"{player.leader.leaderName}: 祈りで癒しを得る!");
            }
        }
    }

    // 盗賊: カード奪取
    public class ThiefAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            // 他プレイヤーからカードをランダムに一枚奪う処理などは
            // 本来 GameManager 等を通じて行うが、一旦デバッグ
            Debug.Log($"{player.leader.leaderName}: 相手の懐からカードを盗み出した!");
        }
    }

    // 魔法使い: 魔法コストで高火力
    public class WizardAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            if (player.currentMana >= 5)
            {
                player.currentMana -= 5;
                Debug.Log($"{player.leader.leaderName}: 魔力解放! 大ダメージの魔法を放つ!");
            }
        }
    }

    // 研究者: 状態異常（毒）
    public class ResearcherAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            // カードのキーワードを一時的に「毒」に変える、などの処理
            Debug.Log($"{player.leader.leaderName}: 毒薬を作成! 次の攻撃に猛毒を付与!");
        }
    }

    // アイドル: バフ支援
    public class IdolAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            foreach (var monster in player.field)
            {
                // 全モンスターの攻撃力アップなどの処理
                Debug.Log($"{player.leader.leaderName}: ステージで応援! モンスター達が力づく!");
            }
        }
    }

    // 魔王: 生贄召喚
    public class DevilAbility : LeaderAbility
    {
        public override void ExecuteAction()
        {
            if (player.field.Count > 0)
            {
                player.field.RemoveAt(0); // 生贄
                player.currentMana += 10; // 大量マナ獲得
                Debug.Log($"{player.leader.leaderName}: 生贄を捧げ、暗黒の力を得た!");
            }
        }
    }
}
