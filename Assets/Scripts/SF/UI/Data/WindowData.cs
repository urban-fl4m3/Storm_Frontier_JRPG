using System.Collections.Generic;
using SF.UI.Windows;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.UI.Data
{
    [CreateAssetMenu(fileName = nameof(WindowData), order = 1, menuName = "Data/" + nameof(WindowData))]
    public class WindowData: SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<WindowType, IWindow> _windowDictionary;

        public Dictionary<WindowType, IWindow> WindowDictionary => _windowDictionary;
    }
}