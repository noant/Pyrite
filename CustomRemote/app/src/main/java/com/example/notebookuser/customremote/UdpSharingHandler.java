package com.example.notebookuser.customremote;

import android.provider.ContactsContract;

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

        private static Integer _hashesCleanCnt = 1000;
        private static ArrayList<String> _datagramStrings = new ArrayList<String>();

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
                            byte[] buff = new byte[512];
                            DatagramPacket sourcePacket = new DatagramPacket(buff, buff.length);
                            socket.receive(sourcePacket);

                            final String datagramString = new String(sourcePacket.getData());

                            if (_datagramStrings.contains(datagramString)) {
                                if (_datagramStrings.size() >= _hashesCleanCnt)
                                    _datagramStrings.clear();
                                continue;
                            }
                            else
                                _datagramStrings.add(datagramString);

                            new Thread(new Runnable() { //async datagram handling
                                @Override
                                public void run() {
                                    try {
                                        final String[] data = getStrArray(datagramString);

                                        final String name = data[0];
                                        final String command = data[1];

                                        for (int i = 0; i < _actionPairs.size(); i++) {
                                            final ActionPair actionPair = _actionPairs.get(i);
                                            if (actionPair.getCommand().equals(command)) {
                                                if (actionPair.getButton().getParent() == null) {
                                                    _actionPairs.remove(actionPair);
                                                } else {
                                                    actionPair.getButton().post(new Runnable() {
                                                        @Override
                                                        public void run() {
                                                            if (!actionPair.getButton().getText().equals(name)) {
                                                                actionPair.getButton().setText(name);
                                                                actionPair.setName(name);
                                                                actionPair.getButton().setEnabled(true);
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Integer i=0;
                                        i++; // to delete
                                    }
                                }
                            }).start();

                            if (_actionPairs.size()==0) {
                                _started = false;
                                break;
                            }
                        }
                    }
                    catch (IOException e)
                    {
                        Exception ee = e;// to delete
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
