package com.example.notebookuser.customremote;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class ActionPair {
    private String _status;
    private String _command;
    private Boolean _isCategory=false;
    private Boolean _isNeedDelete=false;

    public String getStatus(){
        return _status;
    }

    public void setStatus(String value){
        _status = value;
        if (_statusUpdated!=null)
            _statusUpdated.run(this);
    }

    public String getCommand(){
        return _command;
    }

    private ActionPairRunnable _statusUpdated;

    public void setOnStatusUpdated(ActionPairRunnable event){
        _statusUpdated = event;
    }

    public Boolean isCategory(){
        return _isCategory;
    }

    public ActionPair(String command, boolean isCategory)
    {
        _command = command;
        _isCategory = isCategory;
    }

    public void Do()
    {
        new Thread(new Runnable() {
            public void run() {
                setStatus(
                    TcpHelper.Do(
                        getCommand(),
                        getStatus()
                ));
            }
        }).start();
    }

    public void needDelete()
    {
        _isNeedDelete=true;
    }
    public  boolean isNeedDelete()
    {
        return  _isNeedDelete;
    }
}
