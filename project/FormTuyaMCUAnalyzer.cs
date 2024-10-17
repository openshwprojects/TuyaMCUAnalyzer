using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TuyaMCUAnalyzer
{
    public partial class FormTuyaMCUAnalyzer : Form
    {
        Dictionary<int, IDTracker> vars = new Dictionary<int, IDTracker>();
        IDsTracker tracker;
        SinglePort portRX, portTX;

        public FormTuyaMCUAnalyzer()
        {
            InitializeComponent();
        }
        private bool bUseVarsForVer0Cmd6InsteadOfDate = true;
        private ListViewItem PrintLineHex = new ListViewItem(new string[] { "", "", "", "", "", "", "", "", "", "", "" });

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
                r = "IN";
            else if (p[0] == special_marker_sent)
                r = "OUT";
            if (r.Length >0)
            {
                p.RemoveRange(0, specialMarkerCount);
                return r;
            }
            return "";
        }
        private List<byte> getNextPacket(ref List<byte> p)
        {
            for(int i = 0; i < p.Count-6; i++)
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
        void parseDPData(List<byte> p, Dictionary<int, IDTracker> vars, int ofs = 6)
        {
            bool bHadColor = false;
            Color col = Color.Black;
            string contentString = "";
            while (ofs + 4 < p.Count)
            {
                int sectorLen = p[ofs + 2] << 8 | p[ofs + 3];
                int dpId = p[ofs];
                TuyaType dataType = (TuyaType)p[ofs + 1];

                if (contentString.Length > 0)
                    contentString += ",";
                contentString += "dpId=" + dpId;
                contentString += " ";
                contentString += dataType.ToString();
                contentString += " ";
                if (sectorLen == 1)
                {
                    int iVal = (int)p[ofs + 4];
                    contentString += "V=" + iVal;
                    tracker.addValueInt(dpId, dataType, iVal, vars);
                }
                else if (sectorLen == 4)
                {
                    int iVal = p[ofs + 4] << 24 | p[ofs + 5] << 16 | p[ofs + 6] << 8 | p[ofs + 7];
                    contentString += "V=" + iVal;
                    tracker.addValueInt(dpId, dataType, iVal, vars);
                }
                else
                {
                    string varStr = "";
                    string strDataForColor = "";
                    for (int si = 0; si < sectorLen; si++)
                    {
                        byte b = p[ofs + si + 4];
                        strDataForColor += Convert.ToChar(b);
                        if (dataType == TuyaType.Str && checkBoxStrTypeAsBytes.Checked == false)
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
                    tracker.addValueStr(dpId, dataType, varStr.Replace(" ", ""), vars);
                    contentString += "V=" + varStr;
                    if (checkBoxDecodeColors.Checked && parseTuyaColor(strDataForColor, out col))
                    {
                        bHadColor = true;
                    }
                }
                ofs += (4 + sectorLen);
            }
            if (bHadColor)
            {
                bHadColor = false;
                PrintLineHex.SubItems[9].Text = contentString + " Col: ■";
                PrintLineHex.SubItems[9].ForeColor = col;
            }
            else 
            {
                PrintLineHex.SubItems[9].Text = contentString;
                PrintLineHex.SubItems[9].ForeColor = Color.Black;
            }
        }
        void displayPacket(List<byte> p, Dictionary<int, IDTracker> vars)
        {
            byte ver = p[2];
            byte cmd = p[3];
            byte lenA = p[4];
            byte lenB = p[5];
            PrintLineHex.UseItemStyleForSubItems = false;
            PrintLineHex.SubItems[1].Text = p[0].ToString("X2") + " " + p[1].ToString("X2");
            PrintLineHex.SubItems[1].ForeColor = Color.Black;
            PrintLineHex.SubItems[2].Text = p[2].ToString("X2");
            PrintLineHex.SubItems[2].ForeColor = Color.Magenta;
            PrintLineHex.SubItems[3].Text = p[3].ToString("X2");
            PrintLineHex.SubItems[3].ForeColor = Color.Red;
            PrintLineHex.SubItems[4].Text = p[4].ToString("X2") + " " + p[5].ToString("X2");
            PrintLineHex.SubItems[4].ForeColor = Color.Green;
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
                case 6:
                    cmdName = "SetDP";
                    break;
                case 7:
                    cmdName = "State";
                    break;
                case 8:
                    cmdName = "QueryInitStatus";
                    break;
                case 0x10:
                    cmdName = "ObtainDPcache";
                    break;
                case 0x1C:
                    cmdName = "Date";
                    break;
                case 0x0B:
                    cmdName = "QuerySignalStrength";
                    break;
            }
            string s = "";
            switch (cmd)
            {
                case 0x22:
                case 7:
                    int ofs = 6;
                    while (ofs + 4 < p.Count)
                    {
                        int sectorLen = p[ofs + 2] << 8 | p[ofs + 3];
                        int dpId = p[ofs];
                        PrintLineHex.SubItems[5].Text = p[ofs].ToString("X2");
                        PrintLineHex.SubItems[5].ForeColor = Color.Black;
                        PrintLineHex.SubItems[6].Text = p[ofs + 1].ToString("X2");
                        PrintLineHex.SubItems[6].ForeColor = Color.Green;
                        PrintLineHex.SubItems[7].Text = p[ofs + 2].ToString("X2") + " " + p[ofs + 3].ToString("X2");
                        PrintLineHex.SubItems[7].ForeColor = Color.Black;
                        int dataType = p[ofs + 1];
                        
                        if (sectorLen == 1)
                        {
                            int iVal = (int)p[ofs + 4];
                            PrintLineHex.SubItems[8].Text = iVal.ToString("X2");
                            PrintLineHex.SubItems[8].ForeColor = Color.Orange;
                        }
                        else if (sectorLen == 4)
                        {
                            int iVal = p[ofs + 4] << 24 | p[ofs + 5] << 16 | p[ofs + 6] << 8 | p[ofs + 7];
                            PrintLineHex.SubItems[8].Text = iVal.ToString("X8");
                            PrintLineHex.SubItems[8].ForeColor = Color.Orange;
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
                            PrintLineHex.SubItems[8].Text = varStr;
                            PrintLineHex.SubItems[8].ForeColor = Color.Orange;
                        }
                        ofs += (4 + sectorLen);
                    }
                    break;
                default:
                    for (int i = 6; i < p.Count - 1; i++)
                    {
                        s += p[i].ToString("X2");
                        s += "";
                    }
                    PrintLineHex.SubItems[8].Text = s;
                    PrintLineHex.SubItems[8].ForeColor = Color.Gray;
                    break;
            }
            PrintLineHex.SubItems[10].Text = p[p.Count - 1].ToString("X2");
            PrintLineHex.SubItems[10].ForeColor = Color.Black;

            listViewDecoded.Items.AddRange(new ListViewItem[] { PrintLineHex});
            PrintLineHex.SubItems[9].Text = cmdName;
            PrintLineHex.SubItems[9].ForeColor = Color.Red;
            s = "";
            if (cmd == 7 || cmd == 0x22)
            {
                parseDPData(p, vars);
            }
            else if (cmd == 1)
            {
                string str = ASCIIEncoding.ASCII.GetString(p.ToArray(), 6, p.Count - 7);
                PrintLineHex.SubItems[9].Text = str;
                PrintLineHex.SubItems[9].ForeColor = Color.Gray;
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

                    PrintLineHex.SubItems[9].Text = "bOk=" + bDateValid + " " + year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
                    PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                }
                else
                {
                    PrintLineHex.SubItems[9].Text = "INVALID date";
                    PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                }
            }
            else if ((cmd == 5 && ver == 0) || (cmd == 0x10 && ver == 0) 
                || (bUseVarsForVer0Cmd6InsteadOfDate && cmd == 6 && ver == 0)
                || (true && cmd == 8 && ver == 0))
            {
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

                        PrintLineHex.SubItems[9].Text = "bOk=" + bDateValid + " " + year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
                        PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                    }
                    else
                    {
                        PrintLineHex.SubItems[9].Text = "INVALID date";
                        PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                    }
                    ofs += 7;
                }
                try
                {
                    parseDPData(p, vars, ofs);
                }
                catch(Exception)
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

                        PrintLineHex.SubItems[9].Text = "bOk=" + bDateValid + " " + year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
                        PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                    }
                    else
                    {
                        PrintLineHex.SubItems[9].Text = "INVALID date";
                        PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                    }
                    parseDPData(p, vars, ofs+7);
                }
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

                    PrintLineHex.SubItems[9].Text = "bOk=" + bDateValid + " " + year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
                    PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                }
                else
                {
                    PrintLineHex.SubItems[9].Text = "INVALID date";
                    PrintLineHex.SubItems[9].ForeColor = Color.Gray;
                }

            }
            else
            {
                for (int i = 6; i < p.Count - 1; i++)
                {
                    s += p[i].ToString("X2");
                    s += "";
                }
                PrintLineHex.SubItems[8].Text = s;
                PrintLineHex.SubItems[8].ForeColor = Color.Gray;
            }
            switch (cmd)
            {
                case 0:

                    break;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            refresh();
        }
        byte special_marker_sent = 0x73;
        byte special_marker_recv = 0x72;
        int specialMarkerCount = 10;
        void refresh() {
            int cursorPosition = richTextBoxSrc.SelectionStart;
            int currentLineIndex = richTextBoxSrc.GetLineFromCharIndex(cursorPosition);
            tracker = new IDsTracker();
            string[] lines = richTextBoxSrc.Lines;
            string text = "";

            if (lines.Length > 2)
            {
                text = lines[currentLineIndex - 2] + '\n' + lines[currentLineIndex - 1];
            }

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
                        if (text[i] == '\n')
                        {
                            break;
                        }
                        i++;
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
                catch(Exception)
                {
                    i++;
                }
            }
            List<byte> packet = null;
            while(true)
            {
                string comment = getSpecialMarker(ref r);
                packet = getNextPacket(ref r);
                if(packet == null)
                {
                    break;
                }
                if (checkBoxHIdeHeartbeat.Checked)
                {
                    if (packet[3] == 0)
                    {
                        continue;
                    }
                }
                if (checkBoxHideWiFiState.Checked)
                {
                    if (packet[3] == 3)
                    {
                        continue;
                    }
                }
                if (checkBoxHideDate.Checked)
                {
                    if (packet[3] == 0x1C)
                    {
                        continue;
                    }
                }
                if (comment.Length > 0)
                {
                    listViewDecoded.BeginUpdate();
                    PrintLineHex = new ListViewItem(new string[] { "", "", "", "", "", "", "", "", "", "", "" });
                    PrintLineHex.SubItems[0].Text = comment;
                    if (comment == "IN")
                    {
                        PrintLineHex.SubItems[0].BackColor = Color.Blue;
                        PrintLineHex.SubItems[0].ForeColor = Color.White;
                    }
                    if (comment == "OUT")
                    {
                        PrintLineHex.SubItems[0].BackColor = Color.Red;
                        PrintLineHex.SubItems[0].ForeColor = Color.White;
                    }
                }
                displayPacket(packet, vars);
                // Scroll to the newly added item (last item in the list)
                listViewDecoded.EnsureVisible(listViewDecoded.Items.Count - 1);
                listViewDecoded.EndUpdate();
            }
            tracker.display(listViewAvailableIDs, vars);
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
        void scanForExamplesCaptures()
        {
            try
            {
                string samplesDir = findSamplesPath();
                string[] samples = Directory.GetFiles(samplesDir);
                for (int i = 0; i < samples.Length; i++)
                {
                    string path = samples[i];
                    path = path.Replace('/', '\\');
                    string lenStr = formatByteSize(path);
                    var item2 = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "Test",
                        Text = path + "    " + lenStr,
                        Tag = path
                    };
                    item2.Click += exampleClickListener;
                    examplesToolStripMenuItem.DropDownItems.Add(item2);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("No examples found? Get sample captures from Github!");
            }
        }
        bool parseTuyaColor(string s, out Color c)
        {
            try
            {
                if (s.Length == 12)
                {
                    string[] substrings = new string[3];
                    for (int i = 0; i < 3; i++)
                    {
                        substrings[i] = s.Substring(i * 4, 4);
                    }
                    int hue = int.Parse(substrings[0], System.Globalization.NumberStyles.HexNumber);
                    int sat1000 = int.Parse(substrings[1], System.Globalization.NumberStyles.HexNumber);
                    int val1000 = int.Parse(substrings[2], System.Globalization.NumberStyles.HexNumber);
                    c = HsvToRgb(hue, sat1000 * 0.001, val1000 * 0.001);
                    return true;
                }
            }
            catch(Exception)
            {
                // TODO: show
            }
            c = Color.Black;
            return false;
        }
        public static Color HsvToRgb(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        // Method to export ListView data to CSV format
        private string ExportListViewToCsv(System.Windows.Forms.ListView listView)
        {
            StringBuilder csvBuilder = new StringBuilder();

            // Export header (column names)
            for (int col = 0; col < listView.Columns.Count; col++)
            {
                csvBuilder.Append(EscapeCsvValue(listView.Columns[col].Text));
                if (col < listView.Columns.Count - 1)
                {
                    csvBuilder.Append(",");  // Add comma between columns
                }
            }
            csvBuilder.AppendLine();  // New line after header

            // Export rows
            foreach (ListViewItem item in listView.Items)
            {
                // Add the first column value
                csvBuilder.Append(EscapeCsvValue(item.Text));
                for (int subItem = 1; subItem < item.SubItems.Count; subItem++)
                {
                    csvBuilder.Append(",");
                    csvBuilder.Append(EscapeCsvValue(item.SubItems[subItem].Text));
                }
                csvBuilder.AppendLine();  // New line after each row
            }

            return csvBuilder.ToString();
        }

        // Method to escape CSV values (in case they contain commas, quotes, etc.)
        private string EscapeCsvValue(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";  // Escape quotes by doubling them
            }
            return value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxBaud.SelectedIndex = 0;
            scanForExamplesCaptures();
            setDualCaptureEnabled(false);
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(listViewDecoded, true, null);
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
            checkBoxPauseUART.Enabled = b;
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
            if (checkBoxPauseUART.Checked)
            {
                // ignore
                return;
            }
            string s;
            s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += data[i].ToString("X2");
            }
            string final = "//"+marker+" " + DateTime.Now + " " + comment + Environment.NewLine
                + s + Environment.NewLine;
            RichTextBoxExtensions.AppendText(richTextBoxSrc, final, c);
            // autoscroll to last line
            richTextBoxSrc.SelectionStart = richTextBoxSrc.Text.Length;
            richTextBoxSrc.ScrollToCaret();
        }
        // called from SinglePort
        public void addPacketRX(byte [] data)
        {
            addPacket(data, "WiFi received:", "R", Color.Blue);
        }
        // called from SinglePort
        public void addPacketTX(byte[] data)
        {
            addPacket(data, "WiFi sent:", "S", Color.Red);
        }
        private void checkBoxRealtimeDual_CheckedChanged(object sender, EventArgs e)
        {
            setDualCaptureEnabled(checkBoxRealtimeDual.Checked);
            if (!checkBoxRealtimeDual.Checked)
            {
                portRX.closePort();
                portTX.closePort();
            }
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
        public void updateComboBox( System.Windows.Forms.ComboBox comboBoxUART) { 
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
            listViewDecoded.Items.Clear();
            portRX.totalBytesReceived = 0;
            portTX.totalBytesReceived = 0;
            vars.Clear();
        }

        private void checkBoxStrTypeAsBytes_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void checkBoxHIdeHeartbeat_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void checkBoxDecodeColors_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void checkBoxHideWiFiState_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void buttonCopyDecodedToClipboard_Click(object sender, EventArgs e)
        {
            string csv = ExportListViewToCsv(listViewDecoded);

            // Copy the CSV to the clipboard
            Clipboard.SetText(csv);

            MessageBox.Show("Data copied to clipboard.");
        }

        private void buttonCopyRawToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBoxSrc.Text);
            MessageBox.Show("Data copied to clipboard.");
        }

        private void checkBoxHideDate_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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
