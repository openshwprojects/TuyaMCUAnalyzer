using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TuyaMCUAnalyzer
{
    class SinglePort
    {
        ComboBox comboBoxUART;
        Button buttonOpen;
        Label labelState;
        ComboBox comboBoxBaud;
        SerialPort serial;

        byte[] tmpBytes = new byte[2048];
        ByteRingBuffer incoming = new ByteRingBuffer();
        public delegate void PacketHandlerDelegate(byte[] data);
        PacketHandlerDelegate receiveCallback;
        
        public int totalBytesReceived;
        public SinglePort(Button BT, ComboBox CB, Label LB, PacketHandlerDelegate cb, ComboBox comboBoxBaud)
        {
            this.buttonOpen = BT;
            this.comboBoxUART = CB;
            this.labelState = LB;
            this.receiveCallback = cb;
            this.comboBoxBaud = comboBoxBaud;
            
            RemoveClickEvent(this.buttonOpen);
            this.buttonOpen.Click += buttonOpen_Click;


          //  Thread thread = new Thread(new ThreadStart(threadReceive));
          //  thread.Start();
        }
        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);

            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);

            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }
        public void refreshStats()
        {
            int s = incoming.getSize();
            labelState.Invoke((MethodInvoker)delegate {
                labelState.Text = "Currently in ringbuffer: " + s + ", total recv: "+ totalBytesReceived;
            });
        }
        public void threadReceive()
        {
            while (true)
            {
                runFrame();
                Thread.Sleep(1);
            }
        }
        public void runFrame()
        {

            if (serial != null)
            {
                if (serial.IsOpen)
                {
                    while (serial.BytesToRead > 0)
                    {
                        int c = serial.BytesToRead;
                        serial.Read(tmpBytes, 0, c);
                        incoming.addData(tmpBytes, c);
                        totalBytesReceived += c;
                    }
                }
            }
            processIncoming();
            refreshStats();
        }
        void processIncoming()
        {
            if (incoming.getSize() < 6)
            {
                return;
            }
            byte a = incoming.getByte(0);
            byte b = incoming.getByte(1);
            byte v = incoming.getByte(2);
            byte cmd = incoming.getByte(3);
            short len = incoming.getShort(4);
            if (a == 0x55 && b == 0xaa)
            {
                int totalLen = 6 + len + 1;
                if (totalLen > incoming.getSize())
                    return;
                byte[] dat = incoming.getDataFromTo(0, totalLen);
                addReceived(dat);
                incoming.consumeBytes(totalLen);
                refreshStats();
            }
            else
            {
                incoming.consumeBytes(1);
                refreshStats();
            }
        }
        void addReceived(byte[] dat)
        {
            labelState.Invoke((MethodInvoker)delegate {
                this.receiveCallback(dat);
            });
        }
        bool openPort()
        {
            try
            {
                string serialName = comboBoxUART.SelectedItem.ToString();
                int baud = int.Parse(comboBoxBaud.Text);
                serial = new SerialPort(serialName, baud, Parity.None, 8, StopBits.One);
            }
            catch (Exception ex)
            {
                //addError("Serial port create exception: " + ex.ToString() + Environment.NewLine);
                return true;
            }
            try
            {
                serial.ReadBufferSize = 4096 * 2;
                serial.ReadBufferSize = 3000000;
            }
            catch (Exception ex)
            {
                //addWarning("Setting serial port buffer size exception: " + ex.ToString() + Environment.NewLine);
            }
            try
            {
                serial.Open();
            }
            catch (Exception ex)
            {
                //addError("Serial port open exception: " + ex.ToString() + Environment.NewLine);
                onComClose();
                return true;
            }
            return false;
        }
        void onComClose()
        {
            buttonOpen.Enabled = true;
            buttonOpen.Text = "Open";
        }
        public void closePort()
        {
            if (serial != null)
            {
                serial.Close();
                serial.Dispose();
                serial = null;
            }
        }
        void tryOpenPort()
        {
            if (serial != null)
            {
                if (serial.IsOpen)
                    return;
            }
            incoming.clearBuffer();
            if (openPort() == false)
            {
                buttonOpen.Text = "Close";
            }
            else
            {
            }
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (serial == null)
            {
                tryOpenPort();
            }
            else
            {
                closePort();
                buttonOpen.Text = "Open";
            }

        }
    }
}
