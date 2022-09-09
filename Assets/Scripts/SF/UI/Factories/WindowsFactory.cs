using SF.Common.Data;
using SF.Common.Factories;
using UnityEngine;
using SF.UI.Data;
using SF.UI.Windows;

namespace SF.UI.Factories
{
    public class WindowsFactory : MonoBehaviour, IFactory<Window, IWindow>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private WindowsData _data;

        public IWindow Create(Window type, IDataProvider dataProvider = null)
        {
            if (_data.Windows.ContainsKey(type))
            {
                var window = _data.Windows[type];
                var instance = Instantiate(window, _canvas.transform);
                 
                instance.SetFactoryMeta(dataProvider);

                return instance;
            }

            Debug.LogError($"{type} window type don't added to dictionary");
            return default;
        }
    }
}