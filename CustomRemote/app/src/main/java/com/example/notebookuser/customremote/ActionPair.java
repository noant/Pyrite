package com.example.notebookuser.customremote;

import android.widget.Button;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class ActionPair {
    private String _name;
    private String _command;
    private Boolean _isCategory=false;
    private Boolean _isEnabled=true;
    private Button _button;

    public void setButton(Button b)
    {
        _button = b;
        if (!_isCategory)
        {
            new UdpSharingHandler(this).beginUdpHandling();
        }
    }

    public Boolean isEnabled()
    {
        return  _isEnabled;
    }

    public Button getButton()
    {
        return _button;
    }

    public String getName(){
        return _name;
    }
    public String getCommand(){
        return _command;
    }

    public void setName(String name) { _name=name; }

    public Boolean isCategory(){
        return _isCategory;
    }

    public ActionPair(String name, String command, Boolean isEnabled)
    {
        _isEnabled = isEnabled;
        _name=name;
        _command=command;
    }

    public ActionPair(String category)
    {
        _isCategory = true;
        _name = category;
    }
}
