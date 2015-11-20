package com.example.notebookuser.customremote;

import android.os.AsyncTask;
import android.os.SystemClock;
import android.util.Log;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class RemoteAction extends AsyncTask<Void, Void, String> {
    String _operation;
    String _state;
    Delegate _callback;
    public RemoteAction(String operation, String state ,Delegate callback)
    {
        _operation=operation;
        _callback = callback;
        _state = state;
    }
    @Override
    protected String doInBackground(Void... unused)
    {
        return new TcpHelper().Do(_operation, _state);
    }

    @Override
    protected void onProgressUpdate(Void... unused)
    {}

    @Override
    protected void onPostExecute(String returnVal)
    {
        _callback.invoke(returnVal);
    }
}

