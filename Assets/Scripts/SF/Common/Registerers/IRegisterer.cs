using System;
using System.Collections.Generic;

namespace SF.Common.Registerers
{
    public interface IRegisterer<T> where T : class
    {
        event EventHandler<T> ObjectRegistered;
        event EventHandler<T> ObjectUnregistered; 
        
        bool Add(T obj);
        bool Remove(T obj);
        IEnumerable<T> GetAll();
    }
}