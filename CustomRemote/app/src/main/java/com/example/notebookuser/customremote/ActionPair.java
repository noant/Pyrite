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

    public Boolean getIsEnabled(){
        return _isEnabled;
    }

    public void setIsEnabled(Boolean value){
        _isEnabled = value;
    }
    public void setName(String name) { _name=name; }

    public Boolean getIsCategory(){
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
