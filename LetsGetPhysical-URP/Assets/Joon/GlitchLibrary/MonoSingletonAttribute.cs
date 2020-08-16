using System;

[AttributeUsage(AttributeTargets.Class)]
public class MonoSingletonAttribute : Attribute
{
    public readonly bool Persistent;
    public readonly bool DebugSingletonInEditor;

    public MonoSingletonAttribute(bool persistent = true, bool debugSingletonInEditor = true)
    {
        this.Persistent = persistent;
        this.DebugSingletonInEditor = debugSingletonInEditor;
    }
}