using UnityEngine;

using SF.UI.Data;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class WindowsController: MonoBehaviour, IWindowController
    {
        [SerializeField] private WindowData _data;

        public IWindow Create(WindowType type)
        {
            if (_data.WindowDictionary.ContainsKey(type))
            {
                var createdWindow = _data.WindowDictionary[type];
                return createdWindow;
            }
            
            Debug.LogError($"{type.ToString()} window type don't added to dictionary");
            return null;
        }
    }
}