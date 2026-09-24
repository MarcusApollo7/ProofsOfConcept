using System;
using System.Collections.Generic;

namespace CombatPOC.Interfaces;

public interface IEntity
{
    int EntityID {get; }
    List<IComponent> Components {get; }
    public T GetComponent<T>() where T : IComponent
    {
        foreach (IComponent component in Components)
        {
            if (component is T typedComponent)
            {
                return typedComponent;
            }
        }
        throw new InvalidOperationException($"Component of type {typeof(T).Name} was not found.");
    }
}