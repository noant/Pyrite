package com.example.notebookuser.customremote;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.concurrent.TimeUnit;

/**
 * Created by NotebookUser on 24.01.2016.
 */
public class TcpSharingHandler {
    private static class Tcp
    {
        private static class Strings{
            public  static final String needUpdate = "needUpdate";
            public  static final String except = "except";
            public  static final String all = "all";
        }
        private static ArrayList<ActionPair> _actionPairs = new ArrayList<ActionPair>();
        public static void append(ActionPair action)
        {
            _actionPairs.add(action);
            if (!_started)
                beginListen();
        }

        public static void remove(ActionPair action)
        {
            _actionPairs.remove(action);
        }

        private static Boolean _started = false;

        public static Boolean isStarted()
        {
            return _started;
        }

        private static void beginListen() {
            if (!_started) {
                _started = true;
                _tcpListenThread = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            ServerSocket serverSocket = new ServerSocket(
                                    Integer.parseInt(
                                            new Statics().getSharingPort()
                                    )
                            );
                            while (true) {
                                Socket connection = serverSocket.accept();
                                if (TcpHelper.getNextString(connection.getInputStream()).equals(Strings.needUpdate)) {
                                    String except = TcpHelper.getNextString(connection.getInputStream());
                                    String exceptCommand = "";
                                    if (except.equals(Strings.except))
                                        exceptCommand = TcpHelper.getNextString(connection.getInputStream());

                                    HashMap<String,String> commands = TcpHelper.getAllCommandsStates();

                                    for (Integer i = 0; i < _actionPairs.size(); i++) {
                                        ActionPair actionPair = _actionPairs.get(i);
                                        if (actionPair.isNeedDelete())
                                            _actionPairs.remove(i);
                                        else if (!actionPair.getCommand().equals(exceptCommand)) {
                                            if (commands.containsKey(actionPair.getCommand()))
                                                actionPair.setStatus(commands.get(actionPair.getCommand()));
                                        }
                                    }
                                }
                            }
                        } catch (Exception e) {
                            _started=false;
                            beginListen();
                        }
                    }
                });
                _tcpListenThread.start();
            }
        }

        static Thread _tcpListenThread;
    }

    public static void append(ActionPair action)
    {
        Tcp.append(action);
    }
}
