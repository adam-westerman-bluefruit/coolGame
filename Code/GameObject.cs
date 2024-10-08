using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class GameObject : IUpdatable
{
    public static event Action<GameObject> Created;
    public static event Action<GameObject> Destroyed;
    public Transform transform;
    public List<Component> components = new List<Component>();

    public GameObject()
    {
        transform = new Transform(Vector2.Zero, Vector2.One, 0);
        Created?.Invoke(this);
    }

    public GameObject(Vector2 position)
    {
        transform = new Transform(position, Vector2.One, 0);
        Created?.Invoke(this);
    }

    public GameObject(Vector2 position, Component component)
    {
        transform = new Transform(position, Vector2.One, 0);
        AddComponent(component);
        Created?.Invoke(this);
    }

    public GameObject(Vector2 position, List<Component> componentsList)
    {
        transform = new Transform(position, Vector2.One, 0);
        foreach(Component component in componentsList)
        {
            AddComponent(component);
        }
        Created?.Invoke(this);
    }

    ~GameObject()
    {
        Destroyed?.Invoke(this);
    }

    public void AddComponent(Component component)
    {
        component.gameObject = this;
        components.Add(component);
        component.Awake();
    }

    public void RemoveComponent(Component component)
    {
        components.Remove(component);
    }

    public T GetComponent<T>() where T : Component
    {
        return components.OfType<T>().FirstOrDefault();
    }

    public List<RenderData> GetRenderData()
    {
        List<RenderData> compositRenderData = new List<RenderData>();
        foreach(Component component in components)
        {
            if(component is IRenderable renderable)
            {
                RenderData renderData = renderable.GetRenderData();
                AddRectTransformToRenderData(ref renderData);
                compositRenderData.Add(renderData);
            }
        }
        return compositRenderData;
    }

    private void AddRectTransformToRenderData(ref RenderData renderData)
    {
        renderData.rect.X = transform.Rect.X;
        renderData.rect.Y = transform.Rect.Y;
        renderData.rect.Width *= transform.Rect.Width;
        renderData.rect.Height *= transform.Rect.Height;
    }

    public void Update(GameTime gameTime)
    {
        foreach(Component component in components)
        {
            if(component is IUpdatable updatable)
            {
                updatable.Update(gameTime);
            }
        }
    }

    public void LateUpdate(GameTime gameTime)
    {
        foreach(Component component in components)
        {
            if(component is IUpdatable updatable)
            {
                updatable.LateUpdate(gameTime);
            }
        }
    }
}