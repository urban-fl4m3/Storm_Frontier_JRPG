using System;
using UnityEngine;

namespace SF.Battle.TargetSelection
{
    [Serializable]
    public class CustomTargetSelectionData
    {
        [SerializeField] private bool _isInstantCast;
        [SerializeField] private bool _onlyAllyTeam;
        [SerializeField] private bool _onlyOppositeTeam;
        [SerializeField] private bool _selfSelect;
        [SerializeField] private bool _targetSelect;
        [SerializeField] private bool _selectAll;

        public bool IsInstant => _isInstantCast;
        public bool OnlyAllTeam => _onlyAllyTeam;
        public bool OnlyOppositeTeam => _onlyOppositeTeam;
        public bool SelfSelect => _selfSelect;
        public bool TargetSelect => _targetSelect;
        public bool SelectAll => _selectAll;
    }
}