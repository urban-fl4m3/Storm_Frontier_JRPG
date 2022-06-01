using UnityEngine;
using SF.UI.Data;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class WindowsController : MonoBehaviour, IWindowController
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private WindowData _data;

        public TWindow Create<TWindow>(WindowType type) where TWindow : Component, IWindow
        {
            if (_data.WindowDictionary.ContainsKey(type))
            {
                var createdWindow = _data.WindowDictionary[type];

                return Instantiate((TWindow) createdWindow, _canvas.transform);
            }

            Debug.LogError($"{type.ToString()} window type don't added to dictionary");
            return default;
        }
    }
}