using System;
using UnityEngine;

namespace SF.Battle.TargetSelection
{
    [Serializable]
    public struct TargetSelectionData
    {
        [SerializeField] private TargetPick _pick;

        public TargetPick Pick => _pick;

        public TargetSelectionData(TargetPick pick)
        {
            _pick = pick;
        }
    }
}