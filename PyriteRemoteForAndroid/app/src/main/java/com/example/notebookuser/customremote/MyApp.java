package com.example.notebookuser.customremote;

import android.app.Application;
import android.content.Context;

/**
 * Created by NotebookUser on 26.10.2015.
 */
public class MyApp extends Application {

    private static Context context;

    public void onCreate(){
        super.onCreate();
        MyApp.context = getApplicationContext();
    }

    public static Context getAppContext() {
        return MyApp.context;
    }
}
