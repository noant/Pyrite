package com.example.notebookuser.customremote;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class ActionPair {
    private String _name;
    private String _command;
    private Boolean _isCategory=false;

    public String getName(){
        return _name;
    }
    public String getCommand(){
        return _command;
    }
    public void setName(String name) { _name=name; }

    public Boolean getIsCategory(){
        return _isCategory;
    }

    public ActionPair(String name, String command)
    {
        _name=name;
        _command=command;
    }

    public ActionPair(String category)
    {
        _isCategory = true;
        _name = category;
    }
}
