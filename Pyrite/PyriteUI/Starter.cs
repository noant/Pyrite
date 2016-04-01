using HotKeysLibrary;
using System;

namespace PyriteUI
{
    public static class Starter
    {
        static System.Windows.Forms.NotifyIcon _notifyIcon;

        static MainWindow _mainWindow;
        static WFast _wFast;
        static HotKeysManager _hkManager;

        public static void ShowFastWindow()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_wFast == null)
                {
                    _wFast = new WFast();
                    if (!_wFast.Refresh())
                    {
                        _wFast = null;
                        return;
                    }
                    _wFast.Show();
                    _wFast.Closing += (oo, ee) =>
                    {
                        _wFast = null;
                    };
                }
            }));
        }

        public static void Initialize()
        {
            _hkManager = new HotKeysManager();
            _hkManager.AddHotKey(new HotKeyCombination(new System.Windows.Forms.Keys[] {
                System.Windows.Forms.Keys.LWin,
                System.Windows.Forms.Keys.Y
            }, new HotKeyEventHandler(() =>
            {
                ShowFastWindow();
            })));

            _hkManager.EnableHotKeys();

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon(FSHelper.GetImgLocation("logo_32.ico"));
            _notifyIcon.Visible = true;

            _notifyIcon.MouseClick += (o, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (_mainWindow == null)
                    {
                        _mainWindow = new MainWindow();
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                _mainWindow.Show();
                            }),
                        System.Windows.Threading.DispatcherPriority.Normal, null);
                        _mainWindow.Closing += (oo, ee) =>
                        {
                            _mainWindow = null;
                        };
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ShowFastWindow();
                }
            };
        }
    }
}
