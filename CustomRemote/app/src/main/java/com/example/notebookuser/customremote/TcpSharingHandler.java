package com.example.notebookuser.customremote;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.concurrent.TimeUnit;

/**
 * Created by NotebookUser on 24.01.2016.
 */
public class TcpSharingHandler {
    private static class Tcp
    {
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

                                String command = TcpHelper.getNextString(connection.getInputStream());
                                final String name = TcpHelper.getNextString(connection.getInputStream());

                                for (Integer i = 0; i < _actionPairs.size(); i++) {
                                    final ActionPair action = _actionPairs.get(i);
                                    if (action.getButton().getParent() == null)
                                        _actionPairs.remove(action);
                                    else {
                                        if (action.getCommand().equals(command))
                                            if (!action.getButton().getText().equals(name)) {
                                                action.setName(name);
                                                action.getButton().post(new Runnable() {
                                                    @Override
                                                    public void run() {
                                                        action.getButton().setText(name);
                                                        action.getButton().setEnabled(true);
                                                    }
                                                });
                                            }
                                    }
                                }
                            }
                        } catch (Exception e) {
                            //do nothing
                            Integer i=0;
                            i++; // to delete
                        }
                    }
                });
                _tcpListenThread.start();
            }
        }

        static Thread _tcpListenThread;
    }

    private  ActionPair _action;
    public TcpSharingHandler(ActionPair action)
    {
        _action = action;
    }

    public void beginHandling()
    {
        Tcp.append(_action);
    }

    public void endHandling()
    {
        Tcp.remove(_action);
    }
}
