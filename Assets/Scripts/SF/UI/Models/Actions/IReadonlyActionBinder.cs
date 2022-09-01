using System;
using SF.Common.Data;

namespace SF.UI.Models.Actions
{
    public interface IReadonlyActionBinder
    {
        void Subscribe(ActionName name, Action<IDataProvider> action);
        void Unsubscribe(ActionName name, Action<IDataProvider> action);
    }
}