package com.example.notebookuser.customremote;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.InetAddress;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class Statics {

    private static class Temp{
        static String val_addr;
        static Integer val_port;
        static String fn_addr =  MyApp.getAppContext().getFilesDir().getPath().toString() + "/address";
        static String fn_port = MyApp.getAppContext().getFilesDir().getPath().toString() + "/port";
    }

    public String getAddress(){
        if (Temp.val_addr==null)
        {
            try {
                FileInputStream fin = new FileInputStream(Temp.fn_addr);
                byte[] buff = new byte[(int)new File(Temp.fn_addr).length()];
                fin.read(buff);
                Temp.val_addr = new String(buff);
                return getAddress();
            }
            catch (FileNotFoundException e)
            {
                e.printStackTrace();
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            return "127.0.0.1";
        }
        else return Temp.val_addr;
    }

    public void setAddress(String address)
    {
//        if (true) return;
        try {
            FileOutputStream fos = new FileOutputStream(Temp.fn_addr);
            fos.write(address.getBytes());
            Temp.val_addr=address;
        }catch (FileNotFoundException e)
        {
            e.printStackTrace();
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }

    public String getPort(){
        if (Temp.val_port==null)
        {
            try {
                FileInputStream fin = new FileInputStream(Temp.fn_port);
                byte[] buff = new byte[(int)new File(Temp.fn_port).length()];
                fin.read(buff);
                Temp.val_port = Integer.parseInt(new String(buff));
                return getPort();
            }
            catch (FileNotFoundException e)
            {
                e.printStackTrace();
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
            return "1";
        }
        else return Temp.val_port.toString();
    }

    public void setPort(Integer port)
    {
//        if (true) return;
        try {
            FileOutputStream fos = new FileOutputStream(Temp.fn_port);
            fos.write(port.toString().getBytes());
            Temp.val_port=port;
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
//        if (true)return;
        File f_addr = new File(Temp.fn_addr);
        File f_port = new File(Temp.fn_port);
        try {
            if (!f_addr.exists()) {
                f_addr.createNewFile();
                setAddress("127.0.0.1");
            }
            if (!f_port.exists()) {
                f_port.createNewFile();
                setPort(1);
            }
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}
