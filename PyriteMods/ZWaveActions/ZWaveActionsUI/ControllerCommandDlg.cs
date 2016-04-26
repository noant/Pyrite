using System;
using System.Windows.Forms;
using OpenZWaveDotNet;

namespace OZWForm
{
    public partial class ControllerCommandDlg : Form
    {
        private static ZWManager m_manager;
        private static ControllerCommandDlg m_dlg;
        private static uint homeId;

        private static ZWControllerCommand m_op;
        private static byte m_nodeId;
        private static DialogResult result;

        public ControllerCommandDlg(ZWManager _manager, uint _homeId, ZWControllerCommand _op, byte nodeId, bool securityEnabled)
        {
            m_manager = _manager;
            homeId = _homeId;
            m_op = _op;
            m_nodeId = nodeId;
            m_dlg = this;

            InitializeComponent();

            m_manager.OnNotification += new ManagedNotificationsHandler(NotificationHandler);
            switch (m_op)
            {
                case ZWControllerCommand.RequestNodeNeighborUpdate:
                    {
                        this.Text = "Обновление списка соседних узлов";
                        this.label1.Text = "Узел обновляет список соседних узлов";

                        if (!m_manager.RequestNodeNeighborUpdate(homeId, m_nodeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.AddDevice:
                    {
                        this.Text = "Добавить устройство в ZWave сеть";
                        this.label1.Text =
                            "Нажмите программную кнопку на Z-Wave устройстве для добавления его в сеть.\nДля безопасности, контроллер должен быть рядом с устройством ZWave (не более 2 метров).";

                        if (!m_manager.AddNode(homeId, securityEnabled))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.CreateNewPrimary:
                    {
                        this.Text = "Создать новый основной контроллер";
                        this.label1.Text =
                            "Введите новый контроллер в режим передачи данных.\nТекущий контроллер должен быть рядом с целевым контроллером (не более 2 метров).";

                        if (!m_manager.CreateNewPrimary(homeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.ReceiveConfiguration:
                    {
                        this.Text = "Передача конфигурации";
                        this.label1.Text =
                            "Передача сетевой конфигурации от другого устройства.\nПоместите другой контроллер в пределах 2 метров от текущего контроллера.";

                        if (!m_manager.ReceiveConfiguration(homeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.RemoveDevice:
                    {
                        this.Text = "Удалит устройство из сети";
                        this.label1.Text =
                            "Нажмите программную кнопку на устройстве ZWave для удаления его из сети.\nПо причинам безопасности, контроллер должен быть в радиусе 2-х метров от устройства.";

                        if (!m_manager.RemoveNode(homeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.TransferPrimaryRole:
                    {
                        this.Text = "Передача роли основного контроллера другому контроллеру";
                        this.label1.Text =
                            "Передача роли основного контроллера другому контроллеру.\n\nПо причинам безопасности, контроллер должен быть в радиусе 2-х метров от устройства.";

                        if (!m_manager.TransferPrimaryRole(homeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.HasNodeFailed:
                    {
                        this.ButtonCancel.Enabled = false;
                        this.Text = "Проверка узла на неисправность";
                        this.label1.Text = "Проверка узла.\nЭта команда не может быть отменена";

                        if (!m_manager.HasNodeFailed(homeId, m_nodeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.RemoveFailedNode:
                    {
                        this.ButtonCancel.Enabled = false;
                        this.Text = "Удаление неисправного узла";
                        this.label1.Text =
                            "Удаление неисправного узла.\nКоманда не может быть отменена.";

                        if (!m_manager.RemoveFailedNode(homeId, m_nodeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.ReplaceFailedNode:
                    {
                        this.ButtonCancel.Enabled = false;
                        this.Text = "Замена неисправного узла";
                        this.label1.Text = "Тестирование неисправного узла.\nКоманда не может быть отменена.";

                        if (!m_manager.ReplaceFailedNode(homeId, m_nodeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                case ZWControllerCommand.RequestNetworkUpdate:
                    {
                        this.ButtonCancel.Enabled = false;
                        this.Text = "Запрос обновления сети устройств";
                        this.label1.Text = "Запрос обновления сети устройств.";

                        if (!m_manager.RequestNetworkUpdate(homeId, m_nodeId))
                        {
                            MyControllerStateChangedHandler(ZWControllerState.Failed);
                        }
                        break;
                    }
                default:
                    {
                        m_manager.OnNotification -= NotificationHandler;
                        break;
                    }
            }
        }

        public static void NotificationHandler(ZWNotification notification)
        {
            switch (notification.GetType())
            {
                case ZWNotification.Type.ControllerCommand:
                    {
                        MyControllerStateChangedHandler(((ZWControllerState)notification.GetEvent()));
                        break;
                    }
            }
        }

        public static void MyControllerStateChangedHandler(ZWControllerState state)
        {
            bool complete = false;
            String dlgText = "";
            bool buttonEnabled = true;

            switch (state)
            {
                case ZWControllerState.Waiting:
                    {
                        if (m_op == ZWControllerCommand.ReplaceFailedNode)
                        {
                            dlgText =
                                "Нажмите програмную кнопку на узле для замены.\nПо причинам безопасности, контроллер должен быть в радиусе 2-х метров от устройства.";
                        }
                        break;
                    }
                case ZWControllerState.InProgress:
                    {
                        dlgText = "Подождите...";
                        buttonEnabled = false;
                        break;
                    }
                case ZWControllerState.Completed:
                    {
                        dlgText = "Команда выполнена.";
                        complete = true;
                        result = DialogResult.OK;
                        break;
                    }
                case ZWControllerState.Failed:
                    {
                        dlgText = "Не удалось выполнить команду.";
                        complete = true;
                        result = DialogResult.Abort;
                        break;
                    }
                case ZWControllerState.NodeOK:
                    {
                        dlgText = "Узел исправен.";
                        complete = true;
                        result = DialogResult.No;
                        break;
                    }
                case ZWControllerState.NodeFailed:
                    {
                        dlgText = "Узел неисправен.";
                        complete = true;
                        result = DialogResult.Yes;
                        break;
                    }
                case ZWControllerState.Cancel:
                    {
                        dlgText = "Команда была отменена.";
                        complete = true;
                        result = DialogResult.Cancel;
                        break;
                    }
                case ZWControllerState.Error:
                    {
                        dlgText = "Ошибка во время выполнения команды контроллера.";
                        complete = true;
                        result = DialogResult.Cancel;
                        break;
                    }
            }

            if (dlgText != "")
            {
                m_dlg.SetDialogText(dlgText);
            }

            m_dlg.SetButtonEnabled(buttonEnabled);

            if (complete)
            {
                m_dlg.SetButtonText("OK");

                m_manager.OnNotification -= NotificationHandler;
            }
        }

        private void SetDialogText(String text)
        {
            if (m_dlg.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () { SetDialogText(text); }));
            }
            else
            {
                m_dlg.label1.Text = text;
            }
        }

        private void SetButtonText(String text)
        {
            if (m_dlg.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () { SetButtonText(text); }));
            }
            else
            {
                m_dlg.ButtonCancel.Text = text;
            }
        }

        private void SetButtonEnabled(bool enabled)
        {
            if (m_dlg.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () { SetButtonEnabled(enabled); }));
            }
            else
            {
                m_dlg.ButtonCancel.Enabled = enabled;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (ButtonCancel.Text != "OK")
            {
                m_manager.OnNotification -= NotificationHandler;

                m_manager.CancelControllerCommand(homeId);
            }

            Close();
            m_dlg.DialogResult = result;
        }
    }
}