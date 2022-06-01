using SF.UI.Data;
using SF.UI.Windows;
using UnityEngine;

namespace SF.UI.Controller
{
    public interface IWindowController
    {
        TWindow Create<TWindow>(WindowType type) where TWindow : Component, IWindow;
    }
}