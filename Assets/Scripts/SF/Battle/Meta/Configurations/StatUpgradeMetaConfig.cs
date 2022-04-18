using UnityEngine;

namespace SF.Battle.Meta.Configurations
{
    [CreateAssetMenu(fileName = "New Stat Upgrade Meta Configuration", menuName = "Meta/Stat Upgrade Configuration")]
    public class StatUpgradeMetaConfig : ScriptableObject
    {
        [SerializeField] private float _upgradeStatCorrector;

        public float UpgradeStatCorrector => _upgradeStatCorrector;
    }
}