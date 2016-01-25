package com.example.notebookuser.customremote;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.InetAddress;
import java.net.MulticastSocket;
import java.util.ArrayList;

/**
 * Created by NotebookUser on 24.01.2016.
 */
public class UdpSharingHandler {
    private static class Udp
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

        private static void beginListen(){
            _udpListenThread = new Thread(new Runnable() {
                @Override
                public void run() {
                    try {
                        MulticastSocket socket = new MulticastSocket(Integer.parseInt(new com.example.notebookuser.customremote.Statics().getUdpPort()));
                        InetAddress group = InetAddress.getByName(new com.example.notebookuser.customremote.Statics().getUdpAddress());
                        socket.joinGroup(group);
                        _started = true;
                        while (true) {
                            byte[] buff = new byte[2048];
                            DatagramPacket packet = new DatagramPacket(buff, buff.length);
                            socket.receive(packet);
                            final String[] data = getStrArray(new String(packet.getData()));
                            final Boolean isEnabled = data[0].equals("1");
                            final String name = data[1];

                            for (int i=0;i<_actionPairs.size();i++) {
                                final ActionPair actionPair = _actionPairs.get(i);
                                if (actionPair.getCommand().equals(data[2])) {
                                    if (actionPair.getButton().getParent() == null)
                                        _actionPairs.remove(actionPair);
                                    else {
                                        actionPair.getButton().post(new Runnable() {
                                            @Override
                                            public void run() {
                                                if (actionPair.getButton().isEnabled() != isEnabled)
                                                    actionPair.getButton().setEnabled(isEnabled);
                                                if (!actionPair.getButton().getText().equals(name))
                                                    actionPair.getButton().setText(name);
                                            }
                                        });
                                    }
                                }
                            }

                            if (_actionPairs.size()==0) {
                                _started = false;
                                break;
                            }
                        }
                    }
                    catch (IOException e)
                    {
                    }
                }
            });
            _udpListenThread.start();
        }

        private static String _splitter = "#";

        private static String[] getStrArray(String str)
        {
            String[] arr = str.split(_splitter+_splitter);
            for (int i=0; i<arr.length; i++)
                arr[i] = unScreen(arr[i]);
            return arr;
        }

        private static String unScreen(String str)
        {
            return str.replace("\""+_splitter, _splitter);
        }

        static Thread _udpListenThread;
    }

    private  ActionPair _action;
    public UdpSharingHandler(ActionPair action)
    {
        _action = action;
    }

    public void beginUdpHandling()
    {
        Udp.append(_action);
    }

    public void endUdpHandling()
    {
        Udp.remove(_action);
    }
}
