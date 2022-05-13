using SF.UI.Data;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public interface IWindowController
    {
        IWindow Create(WindowType type);
    }
}