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
        static Integer val_sharingPort;
        static String val_udpaddr;
        static String fn_addr =  MyApp.getAppContext().getFilesDir().getPath().toString() + "/address";
        static String fn_port = MyApp.getAppContext().getFilesDir().getPath().toString() + "/port";
        static String fn_sharingPort = MyApp.getAppContext().getFilesDir().getPath().toString() + "/udpport";
        static String fn_udpaddr = MyApp.getAppContext().getFilesDir().getPath().toString() + "/udpaddress";
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

    public String getUdpAddress()
    {
        if (Temp.val_udpaddr == null)
        {
            Temp.val_udpaddr = getValue(Temp.fn_udpaddr);
        }
        return Temp.val_udpaddr;
    }

    public void setUdpAddress(String address)
    {
        setValue(Temp.fn_udpaddr, address);
        Temp.fn_udpaddr=address;
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

    public String getSharingPort(){
        if (Temp.val_sharingPort ==null)
        {
            Temp.val_sharingPort = Integer.parseInt(getValue(Temp.fn_sharingPort));
        }
        return Temp.val_sharingPort.toString();
    }

    public void setSharingPort(Integer port)
    {
        setValue(Temp.fn_sharingPort, port.toString());
        Temp.val_sharingPort =port;
    }

    private String getValue(String key)
    {
        try {
            FileInputStream fin = new FileInputStream(key);
            byte[] buff = new byte[(int)new File(key).length()];
            fin.read(buff);
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
        File f_udpPort = new File(Temp.fn_sharingPort);
        File f_udpaddr = new File(Temp.fn_udpaddr);
        try {
            //f_addr.delete();
            if (!f_addr.exists()) {
                f_addr.createNewFile();
                setAddress("127.0.0.1");
            }
            //f_port.delete();
            if (!f_port.exists()) {
                f_port.createNewFile();
                setPort(6001);
            }
            //f_udpaddr.delete();
            if (!f_udpaddr.exists()) {
                f_udpaddr.createNewFile();
                setUdpAddress("239.192.100.1");
            }
            if (!f_udpPort.exists()) {
                f_udpPort.createNewFile();
                setSharingPort(6000);
            }
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}
