using System;
using System.Collections.Generic;
using CombatPOC.Interfaces;
using CombatPOC.UI;

namespace CombatPOC.Managers;

public class EntityManager
{
    List<IEntity> _entities = [];
    static private int _entityNumber = 0;
    static private int _objectNumber = 0;
    public static int CreateNewEntityID()
    {
        _entityNumber++;
        return _entityNumber - 1;
    }
    public static int CreateNewComponentID()
    {
        _objectNumber++;
        return _objectNumber - 1;
    }
    public void AddEntity(IEntity entity)
    {
        _entities.Add(entity);
    }
    public IEntity GetEntity(int entityID)
    {
        return _entities[entityID];
    }
    public T GetComponentbyType<T>(int EntityID) where T: IComponent
    {
        return _entities[EntityID].GetComponent<T>();
    }
}