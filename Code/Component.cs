using System.Data;
using System.Diagnostics;
using CoolGame;

public class Component
{
    public GameObject gameObject;
    public bool enabled = true;

    public Component()
    {
        if(this is IRenderable renderable)
            GameLoop.RenderBufferRequest += renderable.DrawToBuffer;
        
        if(this is IUpdatable updatable)
        {
            GameLoop.UpdateRequest += updatable.Update;
            GameLoop.PostRenderUpdateRequest += updatable.LateUpdate;
        }
        Created();
    }

    public virtual void Created(){}
}