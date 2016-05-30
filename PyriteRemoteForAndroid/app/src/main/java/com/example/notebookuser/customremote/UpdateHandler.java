package com.example.notebookuser.customremote;

import android.os.Message;
import android.widget.LinearLayout;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by NotebookUser on 24.01.2016.
 */
public class UpdateHandler {
    public static class Tcp
    {
        private static class Strings{
            public static final String needUpdate = "needUpdate";
            public static final String yes = "yes";
            public static final String no = "no";
        }

        private static Boolean _started = false;

        public static Boolean isStarted()
        {
            return _started;
        }

        //public static List<ActionPair> AllActionPairs = new ArrayList<>();

        public static List<MainActivity> AllActivities = new ArrayList<>();

        public static void beginHandling() {
            if (!_started) {
                _started = true;
                _thread = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            Statics pars = new Statics();
                            InetAddress address = InetAddress.getByName(pars.getAddress());
                            while (true) {
                                Socket socket = new Socket();
                                socket.connect(new InetSocketAddress(address, TcpHelper.getNextActionPort()), 2000);
                                InputStream in = socket.getInputStream();
                                OutputStream out = socket.getOutputStream();

                                TcpHelper.sendString(out, Strings.needUpdate);

                                if (TcpHelper.getNextString(in).equals(Strings.yes)){
                                    //* HashMap<String, String> allStates = TcpHelper.getAllCommandsStates();
                                    //for (int i=0;i<AllActionPairs.size();i++){
                                        //ActionPair actionPair = AllActionPairs.get(i);
                                        //if (actionPair.isNeedDelete())
                                        //  AllActionPairs.remove(i);
                                        //else
                                        //  actionPair.setStatus(allStates.get(actionPair.getCommand()));
                                        //}

                                    for (final MainActivity activity :
                                         AllActivities) {
                                        ((LinearLayout)activity.findViewById(R.id.main_layout)).post(new Runnable() {
                                            @Override
                                            public void run() {
                                                activity.refresh();
                                            }
                                        });
                                    }
                                }

                                socket.close();

                                Thread.sleep(2000);
                            }
                        } catch (Exception e) {
                            _started=false;
                            beginHandling();
                        }
                    }
                });
                _thread.start();
            }
        }

        private static Thread _thread;
    }
}
