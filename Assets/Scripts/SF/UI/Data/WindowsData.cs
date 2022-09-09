using System.Collections.Generic;
using SF.UI.Windows;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.UI.Data
{
    [CreateAssetMenu(fileName = nameof(WindowsData), menuName = "Data/" + nameof(WindowsData))]
    public class WindowsData: SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<Window, BaseWindow> _windows;

        public IReadOnlyDictionary<Window, BaseWindow> Windows => _windows;
    }
}