package com.example.notebookuser.customremote;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class Statics {

    private static class Temp
    {
        static String val_addr;
        static Integer val_port;
        static String fn_addr =  MyApp.getAppContext().getFilesDir().getPath().toString() + "/address";
        static String fn_port = MyApp.getAppContext().getFilesDir().getPath().toString() + "/port";
    }

    public String getAddress()
    {
        if (Temp.val_addr==null)
        {
            Temp.val_addr = getValue(Temp.fn_addr);
        }
        return Temp.val_addr;
    }

    public void setAddress(String address)
    {
        setValue(Temp.fn_addr, address);
        Temp.val_addr=address;
    }

    public String getPort(){
        if (Temp.val_port==null)
        {
            Temp.val_port = Integer.parseInt(getValue(Temp.fn_port));
        }
        return Temp.val_port.toString();
    }

    public void setPort(Integer port)
    {
        setValue(Temp.fn_port, port.toString());
        Temp.val_port=port;
    }

    private String getValue(String key)
    {
        try {
            FileInputStream fin = new FileInputStream(key);
            byte[] buff = new byte[(int)new File(key).length()];
            fin.read(buff);
            fin.close();
            return new String(buff);
        }
        catch (FileNotFoundException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
        return "";
    }

    private void setValue(String key, String value)
    {
        try {
            FileOutputStream fos = new FileOutputStream(key);
            fos.write(value.getBytes());
            fos.close();
        }catch (FileNotFoundException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    public void init()
    {
        File f_addr = new File(Temp.fn_addr);
        File f_port = new File(Temp.fn_port);
        try {
            if (!f_addr.exists()) {
                f_addr.createNewFile();
                setAddress("127.0.0.1");
            }
            if (!f_port.exists()) {
                f_port.createNewFile();
                setPort(6001);
            }
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}
