using UnityEngine;

namespace SF.Battle.Meta.Data
{
    [CreateAssetMenu(fileName = "StatUpgradeMetaData", menuName = "Meta/Stat Upgrade Data")]
    public class StatUpgradeMetaData : ScriptableObject
    {
        [SerializeField] private float _upgradeStatCorrector;

        public float UpgradeStatCorrector => _upgradeStatCorrector;
    }
}