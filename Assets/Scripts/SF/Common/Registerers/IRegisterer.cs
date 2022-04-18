using System;

namespace SF.Common.Registerers
{
    public interface IRegisterer<T> where T : class
    {
        event EventHandler<T> ObjectRegistered;
        event EventHandler<T> ObjectUnregistered; 
        void Add(T obj);
        void Remove(T obj);
    }
}