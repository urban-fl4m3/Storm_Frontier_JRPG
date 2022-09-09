using SF.Common.Factories;
using SF.UI.Models.Actions;

namespace SF.UI.Windows
{
    public interface IWindow : IFactoryInstance
    {
        IReadonlyActionBinder Actions { get; }
        
        void Show();
        void Hide();
        void Close();
    }
}