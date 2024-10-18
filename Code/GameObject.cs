using System;
using System.Collections.Generic;
using System.Linq;
using CoolGame;
using Microsoft.Xna.Framework;

public class GameObject
{
    public Transform transform;
    public List<Component> components = new List<Component>();

    #region 'structors
    public GameObject()
    {
        transform = new Transform(Vector2.Zero, Vector2.One, 0);
    }

    public GameObject(Vector2 position)
    {
        transform = new Transform(position, Vector2.One, 0);
    }

    public GameObject(Vector2 position, Component component)
    {
        transform = new Transform(position, Vector2.One, 0);
        AddComponent(component);
    }

    public GameObject(Vector2 position, List<Component> componentsList)
    {
        transform = new Transform(position, Vector2.One, 0);
        foreach(Component component in componentsList)
        {
            AddComponent(component);
        }
    }

    ~GameObject()
    {
    }

    #endregion


    #region component management
    public void AddComponent(Component component)
    {
        component.gameObject = this;
        components.Add(component);
        //component.Awake();
    }

    public void RemoveComponent(Component component)
    {
        components.Remove(component);
    }

    public T GetComponent<T>() where T : Component
    {
        return components.OfType<T>().FirstOrDefault();
    }
    #endregion
}