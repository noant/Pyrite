package com.example.notebookuser.customremote;

import android.content.res.Resources;
import android.os.StrictMode;
import android.support.annotation.NonNull;

import java.io.Console;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.ResponseCache;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

/**
 * Created by NotebookUser on 25.10.2015.
 */
public class TcpHelper {
    public static class Strings{
        public static final String command_getAllCommand="getallcommands";
        public static final String command_getCategoryCommands="getcategorycommands";
        public static final String command_endFastActions="endfastactions";
    }
    public void Do(String command, String state)
    {
        try {
            Statics pars = new Statics();

            InetAddress address = InetAddress.getByName(pars.getAddress());

            Socket socketAction = new Socket(address, getNextActionPort());

            InputStream in = socketAction.getInputStream();
            OutputStream out = socketAction.getOutputStream();

            sendString(out, command);
            sendString(out, state);
        }
        catch (UnknownHostException e1){
            e1.printStackTrace();
        }
        catch (IOException e1){
            e1.printStackTrace();
        }
    }

    public int getNextActionPort()
    {
        try {
            Statics pars = new Statics();

            //get action port
            InetAddress address = InetAddress.getByName(pars.getAddress());
            Socket socketGetActionPort = new Socket();
            socketGetActionPort.connect(new InetSocketAddress(address, Integer.parseInt(pars.getPort())), 5000);
            InputStream inGetActionPort = socketGetActionPort.getInputStream();
            int port = Integer.parseInt(getNextString(inGetActionPort));
            //

            return port;
        }
        catch (UnknownHostException e1){
            e1.printStackTrace();
        }
        catch (IOException e1){
            e1.printStackTrace();
        }
        return 1;
    }

    public static String getNextString(InputStream in)
    {
        try {
             byte[] buff = new byte[in.read()];
            in.read(buff);
            return new String(buff);
        }
        catch (IOException e1){
            e1.printStackTrace();
            return e1.getMessage();
        }
    }

    public static void sendString(OutputStream out, String value)
    {
        try {
            byte[] bytesToSend = value.getBytes();
            out.write(bytesToSend.length);
            out.write(bytesToSend);
        }
        catch (IOException e1){
            e1.printStackTrace();
        }
    }

    public ActionPair[] getAllCommands()
    {
        return getAllCommands(null);
    }

    public ActionPair[] getAllCommands(String category)
    {
        ActionPair[] actionPairs = new ActionPair[1];
        try {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);

            Statics pars = new Statics();
            Socket socket = new Socket();
            socket.connect(new InetSocketAddress(InetAddress.getByName(pars.getAddress()), getNextActionPort()), 5000);

            OutputStream out = socket.getOutputStream();

            InputStream in = socket.getInputStream();

            if (category==null)
            {
                sendString(out, Strings.command_getAllCommand);
            }
            else
            {
                sendString(out, Strings.command_getCategoryCommands);
                sendString(out, category);
            }

            int actionPairsCnt = in.read();

            actionPairs = new ActionPair[actionPairsCnt];

            boolean categoriesLoading=false;

            for (int i=0;i<actionPairsCnt;i++)
            {
                String name = getNextString(in);

                if (name.equals(Strings.command_endFastActions)) {
                    categoriesLoading = true;
                    i--;
                    continue;
                }
                if (categoriesLoading)
                {
                    actionPairs[i]=new ActionPair(name);
                }
                else{
                    Boolean enabled = getNextString(in).equals("1");
                    String command = getNextString(in);
                    ActionPair ap = new ActionPair(name, command, enabled);
                    actionPairs[i]=ap;
                }
            }

            return actionPairs;
        }
        catch (UnknownHostException e1){
            e1.printStackTrace();
            actionPairs[0] = new ActionPair(e1.getMessage(), "error", false);
        }
        catch (IOException e1){
            e1.printStackTrace();
            actionPairs[0] = new ActionPair(e1.getMessage(), "error", false);
        }

        return actionPairs;
    }
}
