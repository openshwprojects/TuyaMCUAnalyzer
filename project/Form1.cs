using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace TuyaMCUAnalyzer
{
    public partial class Form1 : Form
    {
        IDsTracker tracker;
        SinglePort portRX, portTX;

        public Form1()
        {
            InitializeComponent();
        }
        bool bUseVarsForVer0Cmd6InsteadOfDate = true;

        private string getSpecialMarker(ref List<byte> p)
        {
            if (p.Count < specialMarkerCount)
                return "";
            for(int i = 0; i < specialMarkerCount; i++)
            {
                if (p[0] != p[i])
                    return "";
            }
            string r = "";
            if (p[0] == special_marker_recv)
                r = "Received by WiFi module:";
            else if (p[0] == special_marker_sent)
                r = "Sent by WiFi module:";
            if (r.Length >0)
            {
                p.RemoveRange(0, specialMarkerCount);
                return r;
            }
            return "";
        }
        private List<byte> getNextPacket(ref List<byte> p)
        {
            for(int i = 0; i < p.Count-7; i++)
            {
                if(p[i] == 0x55 && p[i+1] == 0xAA)
                {
                    byte ver = p[i + 2];
                    byte cmd = p[i + 3];
                    byte lenA = p[i + 4];
                    byte lenB = p[i + 5];
                    byte dataOrSum = p[i + 6];
                    int end = lenB + 7;
                    if(i + end > p.Count)
                    {
                        // futher part of packet is missing
                        break;
                    }
                    int maxToGet = p.Count - i;
                    if (end > maxToGet)
                        end = maxToGet;
                    List<byte> ret = p.GetRange(i, end);
                    p.RemoveRange(0, end+i);
                    return ret;
                }

            }
            p.Clear();
            return null;
        }
        void displayPacket(List<byte> p)
        {
            byte ver = p[2];
            byte cmd = p[3];
            byte lenA = p[4];
            byte lenB = p[5];
            RichTextBoxExtensions.AppendText(richTextBox1, p[0].ToString("X2") + " "+p[1].ToString("X2") + "\t", Color.Black);
            RichTextBoxExtensions.AppendText(richTextBox1, p[2].ToString("X2") + "\t", Color.Magenta);
            RichTextBoxExtensions.AppendText(richTextBox1, p[3].ToString("X2") + "\t\t", Color.Red);
            RichTextBoxExtensions.AppendText(richTextBox1, p[4].ToString("X2") + " " + p[5].ToString("X2") + "\t", Color.Green);
            string cmdName = "Unk";
            // https://images.tuyacn.com/smart/aircondition/Guide-to-Interworking-with-the-Tuya-MCU.pdf
            switch (cmd)
            {
                case 0:
                    cmdName = "Heartbeat";
                    break;
                case 1:
                    cmdName = "Product";
                    break;
                case 2:
                    cmdName = "McuConf";
                    break;
                case 3:
                    cmdName = "WifiState";
                    break;
                case 7:
                    cmdName = "State";
                    break;
                case 8:
                    cmdName = "QueryInitStatus";
                    break;
                case 0x1C:
                    cmdName = "Date";
                    break;
            }
            string s = "";
            switch (cmd)
            {
                case 7:
                    int ofs = 6;
                    while (ofs + 4 < p.Count)
                    {
                        int sectorLen = p[ofs + 2] << 8 | p[ofs + 3];
                        int fnId = p[ofs];
                        RichTextBoxExtensions.AppendText(richTextBox1, p[ofs].ToString("X2") + " ",
                            Color.Black);
                        RichTextBoxExtensions.AppendText(richTextBox1, p[ofs + 1].ToString("X2") + " ",
                            Color.Green);
                        RichTextBoxExtensions.AppendText(richTextBox1, p[ofs + 2].ToString("X2") + " " + p[ofs + 3].ToString("X2") + " ",
                            Color.Black);
                        int dataType = p[ofs + 1];
                        
                        if (sectorLen == 1)
                        {
                            int iVal = (int)p[ofs + 4];
                            RichTextBoxExtensions.AppendText(richTextBox1, iVal.ToString("X2") + " ",
                                Color.Orange);

                        }
                        else if (sectorLen == 4)
                        {
                            int iVal = p[ofs + 4] << 24 | p[ofs + 5] << 16 | p[ofs + 6] << 8 | p[ofs + 7];
                            RichTextBoxExtensions.AppendText(richTextBox1, iVal.ToString("X8") + " ",
                                Color.Orange);
                        }
                        else
                        {
                            string varStr = "";
                            for (int si = 0; si < sectorLen; si++)
                            {
                                if (si != 0)
                                    varStr += "";
                                varStr += p[ofs + si + 4].ToString("X2");
                            }
                            RichTextBoxExtensions.AppendText(richTextBox1, varStr + " ",
                                Color.Orange);
                        }
                        ofs += (4 + sectorLen);
                    }
                    RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
                    RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
                    break;
                default:
                    for (int i = 6; i < p.Count - 1; i++)
                    {
                        s += p[i].ToString("X2");
                        s += "";
                    }
                    RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
                 //   RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
                    break;
            }
            RichTextBoxExtensions.AppendText(richTextBox1, p[p.Count-1].ToString("X2") + "\t", Color.Black);

            RichTextBoxExtensions.AppendText(richTextBox1, Environment.NewLine);
            RichTextBoxExtensions.AppendText(richTextBox1, "HEADER" + "\t", Color.Black);
            RichTextBoxExtensions.AppendText(richTextBox1, "VER=" + p[2].ToString("X2") + "\t", Color.Magenta);
            RichTextBoxExtensions.AppendText(richTextBox1, cmdName + "\t\t", Color.Red);
            RichTextBoxExtensions.AppendText(richTextBox1, "LEN" + "\t", Color.Green);
             s = "";
            if (cmd == 7)
            {
                string contentString = "";
                int ofs = 6;
                while (ofs + 4 < p.Count)
                {
                    int sectorLen = p[ofs + 2] << 8 | p[ofs + 3];
                    int fnId = p[ofs];
                    TuyaType dataType = (TuyaType) p[ofs + 1];

                    if (contentString.Length > 0)
                        contentString += ",";
                    contentString += "fnId=" + fnId;
                    contentString += " ";
                    contentString += dataType.ToString();
                    contentString += " ";
                    if (sectorLen == 1)
                    {
                        int iVal = (int)p[ofs + 4];
                        contentString += "V=" + iVal;
                        tracker.addValueInt(fnId, dataType, iVal);
                    }
                    else if (sectorLen == 4)
                    {
                        int iVal = p[ofs + 4] << 24 | p[ofs + 5] << 16 | p[ofs + 6] << 8 | p[ofs + 7];
                        contentString += "V=" + iVal;
                        tracker.addValueInt(fnId, dataType, iVal);
                    }
                    else
                    {
                        string varStr = "";
                        for(int si = 0; si < sectorLen; si++)
                        {
                            if(dataType == TuyaType.Str)
                            {
                                // ascii string
                                varStr += Convert.ToChar(p[ofs + si + 4]);
                            }
                            else
                            {
                                if (si != 0)
                                    varStr += " ";
                                varStr += p[ofs + si + 4].ToString("X2");
                            }
                        }
                        tracker.addValueStr(fnId, dataType, varStr.Replace(" ",""));
                        contentString += "V=" + varStr;
                    }
                    ofs += (4 + sectorLen);
                }
                RichTextBoxExtensions.AppendText(richTextBox1, contentString + "\t", Color.Orange);
              //  RichTextBoxExtensions.AppendText(richTextBox1, contentString + "\t", Color.Gray);
            }
            else if (cmd == 1)
            {
                string str = ASCIIEncoding.ASCII.GetString(p.ToArray(), 6, p.Count - 7);
                RichTextBoxExtensions.AppendText(richTextBox1, str + "\t", Color.Gray);
            }
            else if((cmd == 0x1C && ver == 0))
            {
                int baseOfs = 6;
                int bDateValid = p[baseOfs + 0]; // bDateValid
                if (bDateValid == 1)
                {
                    int year = p[baseOfs + 1]; //  year
                    int month = p[baseOfs + 2]; //  month
                    int day = p[baseOfs + 3]; //  day
                    int hour = p[baseOfs + 4]; //  hour
                    int minute = p[baseOfs + 5]; //  minute
                    // NOTE: some packets don't have second here?
                    int second = p[baseOfs + 6]; //  second

                    RichTextBoxExtensions.AppendText(richTextBox1, "bOk=" + bDateValid +
                        " " + year + "/" + month + "/" + day + " " +
                        hour + ":" + minute + ":" + second + "\t", Color.Gray);
                }
                else
                {
                    RichTextBoxExtensions.AppendText(richTextBox1, "INVALID date\t", Color.Gray);
                }
            }
            else if ((cmd == 5 && ver == 0) || (cmd == 0x10 && ver == 0) 
                || (bUseVarsForVer0Cmd6InsteadOfDate && cmd == 6 && ver == 0)
                || (true && cmd == 8 && ver == 0))
            {
                string contentString = "";
                int ofs = 6;
                // cmd == 0x10 && ver == 0 has some garbage at the start of the packet?
                //  https://www.elektroda.com/rtvforum/viewtopic.php?p=20293419#20293419
                // 55AA0010001201021202000400000003110200040000000258
                if (cmd == 0x10 && ver == 0)
                {
                    ofs += 2;
                }
                if (cmd == 0x08 && ver == 0)
                {
                    int bDateValid = p[ofs + 0]; // bDateValid
                    if (bDateValid == 1)
                    {
                        int year = p[ofs + 1]; //  year
                        int month = p[ofs + 2]; //  month
                        int day = p[ofs + 3]; //  day
                        int hour = p[ofs + 4]; //  hour
                        int minute = p[ofs + 5]; //  minute
                                                     // NOTE: some packets don't have second here?
                        int second = p[ofs + 6]; //  second

                        RichTextBoxExtensions.AppendText(richTextBox1, "bOk=" + bDateValid +
                            " " + year + "/" + month + "/" + day + " " +
                            hour + ":" + minute + ":" + second + "\t", Color.Gray);
                    }
                    else
                    {
                        RichTextBoxExtensions.AppendText(richTextBox1, "INVALID date\t", Color.Gray);
                    }
                    ofs += 7;
                }

                // One or multiple combined “status data unit” groups
                while (ofs + 4 < p.Count)
                {
                    int fnId = p[ofs];
                    TuyaType dataType = (TuyaType)p[ofs + 1];
                    int sectorLen = p[ofs + 2] << 8 | p[ofs + 3];

                    if (contentString.Length > 0)
                        contentString += ",";
                    contentString += "fnId=" + fnId;
                    contentString += " ";
                    contentString += dataType.ToString();
                    contentString += " ";

                
                    if (sectorLen == 1)
                    {
                        int iVal = (int)p[ofs + 4];
                        contentString += "V=" + iVal;
                    }
                    else if (sectorLen == 4)
                    {
                        int iVal = p[ofs + 4] << 24 | p[ofs + 5] << 16 | p[ofs + 6] << 8 | p[ofs + 7];
                        contentString += "V=" + iVal;
                    }
                    else
                    {
                        string varStr = "";
                        for (int si = 0; si < sectorLen; si++)
                        {
                            if (si != 0)
                                varStr += " ";
                            varStr += p[ofs + si + 4].ToString("X2");
                        }
                        contentString += "V=" + varStr;
                    }
                    ofs += (4 + sectorLen);
                }
                RichTextBoxExtensions.AppendText(richTextBox1, contentString + "\t", Color.Orange);
                //  RichTextBoxExtensions.AppendText(richTextBox1, contentString + "\t", Color.Gray);
                
            }
            else if(cmd == 6 && ver == 0)
            {
                int baseOfs = 6;
                int bDateValid = p[baseOfs+0]; // bDateValid
                if (bDateValid == 1)
                {
                    int year = p[baseOfs + 1]; //  year
                    int month = p[baseOfs + 2]; //  month
                    int day = p[baseOfs + 3]; //  day
                    int hour = p[baseOfs + 4]; //  hour
                    int minute = p[baseOfs + 5]; //  minute
                    // NOTE: some packets don't have second here?
                    int second = p[baseOfs + 6]; //  second

                    RichTextBoxExtensions.AppendText(richTextBox1, "bOk=" + bDateValid +
                        " " + year + "/" + month + "/" + day + " " +
                        hour + ":" + minute + ":" + second + "\t", Color.Gray);
                }
                else
                {
                    RichTextBoxExtensions.AppendText(richTextBox1, "INVALID date\t", Color.Gray);
                }
            
            }
            else
            {
                for (int i = 6; i < p.Count - 1; i++)
                {
                    s += p[i].ToString("X2");
                    s += "";
                }
                RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
               // RichTextBoxExtensions.AppendText(richTextBox1, s + "\t", Color.Gray);
            }
            RichTextBoxExtensions.AppendText(richTextBox1, "CHK" + "\t", Color.Black);
            switch (cmd)
            {
                case 0:

                    break;
            }
            RichTextBoxExtensions.AppendText(richTextBox1, Environment.NewLine);
            RichTextBoxExtensions.AppendText(richTextBox1, Environment.NewLine);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            refresh();
        }
        byte special_marker_sent = 0x73;
        byte special_marker_recv = 0x72;
        int specialMarkerCount = 10;
        void refresh() {
            tracker = new IDsTracker();
            richTextBox1.Clear();
            string text = richTextBoxSrc.Text;
            List<byte> r = new List<byte>();
            string ch;
            byte value;
            for (int i = 0; i < text.Length; )
            {
                if(text[i] == '/' && i < text.Length-1 && text[i+1] == '/')
                {
                    if(i < text.Length-2)
                    {
                        if(text[i+2] == 'S')
                        {
                            for(int j = 0; j < specialMarkerCount; j++)
                            {
                                r.Add(special_marker_sent);
                            }
                        }
                        if(text[i+2] == 'R')
                        {
                            for (int j = 0; j < specialMarkerCount; j++)
                            {
                                r.Add(special_marker_recv);
                            }
                        }
                    }
                    while (i < text.Length)
                    {
                        i++;
                        if(text[i] == '\n')
                        {
                            break;
                        }
                    }
                    continue;
                }
                if(text[i] == ' ' || text[i] == '\n' || text[i] == '\r' || text[i] == '\t')
                {
                    i++;
                    continue;
                }
                try
                {
                    ch = text.Substring(i, 2);
                    value = Convert.ToByte(ch, 16);
                    r.Add(value);
                    i += 2;
                }
                catch(Exception eX)
                {
                    i++;
                }
            }
            List<byte> packet = null;
            while(true)
            {
                string comment = getSpecialMarker(ref r);
                if(comment.Length>0)
                {
                    RichTextBoxExtensions.AppendText(richTextBox1, comment+Environment.NewLine, Color.Black);
                    continue;
                }
                packet = getNextPacket(ref r);
                if(packet == null)
                {
                    break;
                }
                displayPacket(packet);
            }
            tracker.display(listViewAvailableIDs);
        }
        string findSamplesPath()
        {
            string[] paths = new string[]
            {
                "./samples",
                "../samples",
                "../../samples",
            };
            foreach(string s in paths)
            {
                if (Directory.Exists(s))
                    return s;
            }
            return "";
        }
        string formatByteSize(double len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", len, sizes[order]);
            return result;
        }
        string formatByteSize(string fname)
        {
            long filelen = new FileInfo(fname).Length;
            return formatByteSize(filelen);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxBaud.SelectedIndex = 0;
            string samplesDir = findSamplesPath();
            string [] samples =  Directory.GetFiles(samplesDir);
            for(int i = 0; i < samples.Length; i++)
            {
                string path = samples[i];
                path = path.Replace('/', '\\');
                string lenStr = formatByteSize(path);
                var item2 = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Name = "Test",
                    Text = path+"    "+ lenStr,
                    Tag = path
                };
                item2.Click += exampleClickListener;
                examplesToolStripMenuItem.DropDownItems.Add(item2);
            }
            setDualCaptureEnabled(false);
            refresh();
        }
        public void loadFileBinary(string fname)
        {
            byte[] bytes = File.ReadAllBytes(fname);
            string data;
            data = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                data += bytes[i].ToString("X2");
            }
            setData(data);
        }
        public void setData(string data)
        {
            richTextBoxSrc.Text = data;
            refresh();
        }
        public void loadFileText(string fname)
        {
            string data;
            data = File.ReadAllText(fname);
            setData(data);
        }
        public void loadFile(string fname)
        {
            string ext = Path.GetExtension(fname);
            if (ext == ".bin")
            {
                loadFileBinary(fname);
            }
            else
            {
                loadFileText(fname);
            }
        }
        private void exampleClickListener(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem)sender;
            string path = it.Tag as string;
            loadFile(path);
        }

        bool refreshingComparer;
        bool isTheSame(string [] lines, int ofs)
        {
            if (lines[0].Length - 2 < ofs)
            {
                return false;
            }
            string baseText = lines[0].Substring(ofs, 2);
            for (int i = 1; i < lines.Length; i++)
            {
                if(lines[i].Length - 2 < ofs)
                {
                    return false;
                }
                if (lines[i].Substring(ofs, 2).CompareTo(baseText) != 0)
                    return false;
            }
            return true;
        }
        private void richTextBoxComparer_TextChanged(object sender, EventArgs e)
        {
            if (refreshingComparer)
                return;
            refreshingComparer = true;
            string text = richTextBoxComparer.Text;
            int at = richTextBoxComparer.SelectionStart;
            richTextBoxComparer.Text = "";
            string[] lines = text.Split(new char[] { '\n', '\r' },StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int j = 0;
                while(j < line.Length)
                {
                    if(line[j] == ' ')
                    {
                        RichTextBoxExtensions.AppendText(richTextBoxComparer," ", Color.White);
                        j++;
                        continue;
                    }
                    bool same = isTheSame(lines, j);
                    Color c;
                    if (same) {
                        c = Color.Green;
                    }
                    else
                    {
                        c = Color.Red;
                    }
                    int max = line.Length - j;
                    if (max > 2)
                        max = 2;
                    RichTextBoxExtensions.AppendText(richTextBoxComparer,line.Substring(j,max), c);
                    j += 2;
                }
                RichTextBoxExtensions.AppendText(richTextBoxComparer,Environment.NewLine);
            }
            richTextBoxComparer.SelectionStart = at;
            refreshingComparer = false;
        }

        private void ourForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://elektroda.com");
        }

        private void ourTutorialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.elektroda.com/rtvforum/forum517.html");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                loadFileBinary(fileName);
            }
        }

        private void openTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.bin|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                loadFileText(fileName);
            }
        }

        public void setDualCaptureEnabled(bool b)
        {
            comboBoxPortRX.Enabled = b;
            comboBoxPortTX.Enabled = b;
            comboBoxBaud.Enabled = b;
            buttonOpenCloseRX.Enabled = b;
            buttonOpenCloseTX.Enabled = b;
            if (checkBoxRealtimeDual.Checked != b)
            {
                checkBoxRealtimeDual.Checked = b;
            }
            if(b)
            {
                portRX = new SinglePort(buttonOpenCloseRX, comboBoxPortRX, labelRXStats, addPacketRX, comboBoxBaud);         
                portTX = new SinglePort(buttonOpenCloseTX, comboBoxPortTX, labelTXStats, addPacketTX, comboBoxBaud);
            }
        }
        public void addPacket(byte [] data, string comment, string marker, Color c)
        {
            string s;
            s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += data[i].ToString("X2");
            }
            string final = "//"+marker+" " + DateTime.Now + " " + comment + Environment.NewLine
                + s + Environment.NewLine;
            RichTextBoxExtensions.AppendText(richTextBoxSrc, final, c);
        }
        public void addPacketRX(byte [] data)
        {
            addPacket(data, "WiFi received:", "R", Color.Blue);
        }
        public void addPacketTX(byte[] data)
        {
            addPacket(data, "WiFi sent:", "S", Color.Red);
        }
        private void checkBoxRealtimeDual_CheckedChanged(object sender, EventArgs e)
        {
            setDualCaptureEnabled(checkBoxRealtimeDual.Checked);
        }

        string[] allPorts;

        void setPorts(string[] newPorts)
        {
            if (allPorts != null)
            {
                if (allPorts.Length == newPorts.Length)
                {
                    bool bChange = false;
                    for (int i = 0; i < allPorts.Length; i++)
                    {
                        if (allPorts[i] != newPorts[i])
                        {
                            bChange = true;
                            break;
                        }
                    }
                    if (bChange == false)
                    {
                        return;
                    }
                }
            }
            allPorts = newPorts;
            updateComboBox(comboBoxPortRX);
            updateComboBox(comboBoxPortTX);
        }
        public void updateComboBox(ComboBox comboBoxUART) { 
            string prevPort = "";
            if (comboBoxUART.SelectedIndex != -1)
            {
                prevPort = comboBoxUART.SelectedItem.ToString();
            }
            comboBoxUART.Items.Clear();
            int newIndex = allPorts.Length - 1;
            for (int i = 0; i < allPorts.Length; i++)
            {
                if (prevPort == allPorts[i])
                    newIndex = i;
                comboBoxUART.Items.Add(allPorts[i]);
            }
            if (newIndex != -1)
            {
                comboBoxUART.SelectedIndex = newIndex;
            }
        }
        void scanForCOMPorts()
        {
            string[] newPorts = SerialPort.GetPortNames();
            setPorts(newPorts);
        }

        private void comboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBoxSrc.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scanForCOMPorts();
           
            if (portRX != null)
            {
                portRX.runFrame();
            }
            if (portTX != null)
            {
                portTX.runFrame();
            }
        }
    }
}
