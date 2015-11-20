package com.example.notebookuser.customremote;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public abstract class Delegate {
    public Delegate(Object tag)
    {
        _tag=tag;
    }
    private Object _tag;
    public Object getTag()
    {
        return  _tag;
    }
    abstract void invoke(Object obj);
}
