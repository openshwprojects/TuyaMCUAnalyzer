namespace TuyaMCUAnalyzer
{
    partial class FormTuyaMCUAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTuyaMCUAnalyzer));
            this.richTextBoxDecoded = new System.Windows.Forms.RichTextBox();
            this.listViewAvailableIDs = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ourForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ourTutorialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ourYoutubeChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxSrc = new System.Windows.Forms.RichTextBox();
            this.checkBoxHideDate = new System.Windows.Forms.CheckBox();
            this.buttonCopyRawToClipboard = new System.Windows.Forms.Button();
            this.buttonCopyDecodedToClipboard = new System.Windows.Forms.Button();
            this.checkBoxHideWiFiState = new System.Windows.Forms.CheckBox();
            this.checkBoxDecodeColors = new System.Windows.Forms.CheckBox();
            this.checkBoxHIdeHeartbeat = new System.Windows.Forms.CheckBox();
            this.checkBoxStrTypeAsBytes = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxPauseUART = new System.Windows.Forms.CheckBox();
            this.labelTXStats = new System.Windows.Forms.Label();
            this.labelRXStats = new System.Windows.Forms.Label();
            this.buttonOpenCloseTX = new System.Windows.Forms.Button();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonOpenCloseRX = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxPortTX = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPortRX = new System.Windows.Forms.ComboBox();
            this.checkBoxRealtimeDual = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBoxComparer = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxDecoded
            // 
            this.richTextBoxDecoded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDecoded.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxDecoded.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxDecoded.Name = "richTextBoxDecoded";
            this.richTextBoxDecoded.Size = new System.Drawing.Size(1468, 523);
            this.richTextBoxDecoded.TabIndex = 1;
            this.richTextBoxDecoded.Text = "";
            // 
            // listViewAvailableIDs
            // 
            this.listViewAvailableIDs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAvailableIDs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.type,
            this.columnHeader1,
            this.columnHeader2});
            this.listViewAvailableIDs.HideSelection = false;
            this.listViewAvailableIDs.Location = new System.Drawing.Point(1508, 455);
            this.listViewAvailableIDs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewAvailableIDs.Name = "listViewAvailableIDs";
            this.listViewAvailableIDs.Size = new System.Drawing.Size(484, 685);
            this.listViewAvailableIDs.TabIndex = 2;
            this.listViewAvailableIDs.UseCompatibleStateImageBehavior = false;
            this.listViewAvailableIDs.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 37;
            // 
            // type
            // 
            this.type.Text = "Type";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "VCnt";
            this.columnHeader1.Width = 72;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Values";
            this.columnHeader2.Width = 181;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1230, 1011);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "VCnt is a number of unique vals received";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.examplesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2019, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openTextToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(220, 34);
            this.openToolStripMenuItem.Text = "Open binary..";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openTextToolStripMenuItem
            // 
            this.openTextToolStripMenuItem.Name = "openTextToolStripMenuItem";
            this.openTextToolStripMenuItem.Size = new System.Drawing.Size(220, 34);
            this.openTextToolStripMenuItem.Text = "Open text...";
            this.openTextToolStripMenuItem.Click += new System.EventHandler(this.openTextToolStripMenuItem_Click);
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
            this.examplesToolStripMenuItem.Text = "Examples";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ourForumToolStripMenuItem,
            this.ourTutorialsToolStripMenuItem,
            this.ourYoutubeChannelToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ourForumToolStripMenuItem
            // 
            this.ourForumToolStripMenuItem.Name = "ourForumToolStripMenuItem";
            this.ourForumToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.ourForumToolStripMenuItem.Text = "Our forum";
            this.ourForumToolStripMenuItem.Click += new System.EventHandler(this.ourForumToolStripMenuItem_Click);
            // 
            // ourTutorialsToolStripMenuItem
            // 
            this.ourTutorialsToolStripMenuItem.Name = "ourTutorialsToolStripMenuItem";
            this.ourTutorialsToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.ourTutorialsToolStripMenuItem.Text = "Our tutorials";
            this.ourTutorialsToolStripMenuItem.Click += new System.EventHandler(this.ourTutorialsToolStripMenuItem_Click);
            // 
            // ourYoutubeChannelToolStripMenuItem
            // 
            this.ourYoutubeChannelToolStripMenuItem.Name = "ourYoutubeChannelToolStripMenuItem";
            this.ourYoutubeChannelToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.ourYoutubeChannelToolStripMenuItem.Text = "Our Youtube Channel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(422, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Raw packets data in hex format (example: \"55AA0300\" etc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 1145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(570, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tuya packets display (each packet is displayed twice - hex version and decoded)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1511, 331);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(429, 120);
            this.label4.TabIndex = 7;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2019, 1211);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.checkBoxHideDate);
            this.tabPage1.Controls.Add(this.buttonCopyRawToClipboard);
            this.tabPage1.Controls.Add(this.buttonCopyDecodedToClipboard);
            this.tabPage1.Controls.Add(this.checkBoxHideWiFiState);
            this.tabPage1.Controls.Add(this.checkBoxDecodeColors);
            this.tabPage1.Controls.Add(this.checkBoxHIdeHeartbeat);
            this.tabPage1.Controls.Add(this.checkBoxStrTypeAsBytes);
            this.tabPage1.Controls.Add(this.buttonClear);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.checkBoxRealtimeDual);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.listViewAvailableIDs);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(2011, 1178);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Decode tool";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(24, 89);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxSrc);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxDecoded);
            this.splitContainer1.Size = new System.Drawing.Size(1468, 1051);
            this.splitContainer1.SplitterDistance = 524;
            this.splitContainer1.TabIndex = 19;
            // 
            // richTextBoxSrc
            // 
            this.richTextBoxSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSrc.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxSrc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxSrc.Name = "richTextBoxSrc";
            this.richTextBoxSrc.Size = new System.Drawing.Size(1468, 524);
            this.richTextBoxSrc.TabIndex = 10;
            this.richTextBoxSrc.Text = "";
            this.richTextBoxSrc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBoxHideDate
            // 
            this.checkBoxHideDate.AutoSize = true;
            this.checkBoxHideDate.Location = new System.Drawing.Point(521, 16);
            this.checkBoxHideDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxHideDate.Name = "checkBoxHideDate";
            this.checkBoxHideDate.Size = new System.Drawing.Size(107, 24);
            this.checkBoxHideDate.TabIndex = 18;
            this.checkBoxHideDate.Text = "Hide Date";
            this.checkBoxHideDate.UseVisualStyleBackColor = true;
            this.checkBoxHideDate.CheckedChanged += new System.EventHandler(this.checkBoxHideDate_CheckedChanged);
            // 
            // buttonCopyRawToClipboard
            // 
            this.buttonCopyRawToClipboard.Location = new System.Drawing.Point(942, 5);
            this.buttonCopyRawToClipboard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCopyRawToClipboard.Name = "buttonCopyRawToClipboard";
            this.buttonCopyRawToClipboard.Size = new System.Drawing.Size(246, 35);
            this.buttonCopyRawToClipboard.TabIndex = 17;
            this.buttonCopyRawToClipboard.Text = "Copy raw to clipboard";
            this.buttonCopyRawToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyRawToClipboard.Click += new System.EventHandler(this.buttonCopyRawToClipboard_Click);
            // 
            // buttonCopyDecodedToClipboard
            // 
            this.buttonCopyDecodedToClipboard.Location = new System.Drawing.Point(1196, 5);
            this.buttonCopyDecodedToClipboard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCopyDecodedToClipboard.Name = "buttonCopyDecodedToClipboard";
            this.buttonCopyDecodedToClipboard.Size = new System.Drawing.Size(246, 35);
            this.buttonCopyDecodedToClipboard.TabIndex = 16;
            this.buttonCopyDecodedToClipboard.Text = "Copy decoded to clipboard";
            this.buttonCopyDecodedToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyDecodedToClipboard.Click += new System.EventHandler(this.buttonCopyDecodedToClipboard_Click);
            // 
            // checkBoxHideWiFiState
            // 
            this.checkBoxHideWiFiState.AutoSize = true;
            this.checkBoxHideWiFiState.Location = new System.Drawing.Point(370, 16);
            this.checkBoxHideWiFiState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxHideWiFiState.Name = "checkBoxHideWiFiState";
            this.checkBoxHideWiFiState.Size = new System.Drawing.Size(143, 24);
            this.checkBoxHideWiFiState.TabIndex = 15;
            this.checkBoxHideWiFiState.Text = "Hide WiFi state";
            this.checkBoxHideWiFiState.UseVisualStyleBackColor = true;
            this.checkBoxHideWiFiState.CheckedChanged += new System.EventHandler(this.checkBoxHideWiFiState_CheckedChanged);
            // 
            // checkBoxDecodeColors
            // 
            this.checkBoxDecodeColors.AutoSize = true;
            this.checkBoxDecodeColors.Checked = true;
            this.checkBoxDecodeColors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDecodeColors.Location = new System.Drawing.Point(636, 16);
            this.checkBoxDecodeColors.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxDecodeColors.Name = "checkBoxDecodeColors";
            this.checkBoxDecodeColors.Size = new System.Drawing.Size(178, 24);
            this.checkBoxDecodeColors.TabIndex = 14;
            this.checkBoxDecodeColors.Text = "Decode Tuya Colors";
            this.checkBoxDecodeColors.UseVisualStyleBackColor = true;
            this.checkBoxDecodeColors.CheckedChanged += new System.EventHandler(this.checkBoxDecodeColors_CheckedChanged);
            // 
            // checkBoxHIdeHeartbeat
            // 
            this.checkBoxHIdeHeartbeat.AutoSize = true;
            this.checkBoxHIdeHeartbeat.Location = new System.Drawing.Point(217, 16);
            this.checkBoxHIdeHeartbeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxHIdeHeartbeat.Name = "checkBoxHIdeHeartbeat";
            this.checkBoxHIdeHeartbeat.Size = new System.Drawing.Size(141, 24);
            this.checkBoxHIdeHeartbeat.TabIndex = 13;
            this.checkBoxHIdeHeartbeat.Text = "Hide heartbeat";
            this.checkBoxHIdeHeartbeat.UseVisualStyleBackColor = true;
            this.checkBoxHIdeHeartbeat.CheckedChanged += new System.EventHandler(this.checkBoxHIdeHeartbeat_CheckedChanged);
            // 
            // checkBoxStrTypeAsBytes
            // 
            this.checkBoxStrTypeAsBytes.AutoSize = true;
            this.checkBoxStrTypeAsBytes.Location = new System.Drawing.Point(12, 16);
            this.checkBoxStrTypeAsBytes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxStrTypeAsBytes.Name = "checkBoxStrTypeAsBytes";
            this.checkBoxStrTypeAsBytes.Size = new System.Drawing.Size(210, 24);
            this.checkBoxStrTypeAsBytes.TabIndex = 12;
            this.checkBoxStrTypeAsBytes.Text = "Display STR type as hex ";
            this.checkBoxStrTypeAsBytes.UseVisualStyleBackColor = true;
            this.checkBoxStrTypeAsBytes.CheckedChanged += new System.EventHandler(this.checkBoxStrTypeAsBytes_CheckedChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(822, 5);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(112, 35);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.checkBoxPauseUART);
            this.panel1.Controls.Add(this.labelTXStats);
            this.panel1.Controls.Add(this.labelRXStats);
            this.panel1.Controls.Add(this.buttonOpenCloseTX);
            this.panel1.Controls.Add(this.comboBoxBaud);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.buttonOpenCloseRX);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBoxPortTX);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBoxPortRX);
            this.panel1.Location = new System.Drawing.Point(1515, 89);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 237);
            this.panel1.TabIndex = 9;
            // 
            // checkBoxPauseUART
            // 
            this.checkBoxPauseUART.AutoSize = true;
            this.checkBoxPauseUART.Location = new System.Drawing.Point(10, 205);
            this.checkBoxPauseUART.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPauseUART.Name = "checkBoxPauseUART";
            this.checkBoxPauseUART.Size = new System.Drawing.Size(288, 24);
            this.checkBoxPauseUART.TabIndex = 10;
            this.checkBoxPauseUART.Text = "Pause capture (but keep port inuse)";
            this.checkBoxPauseUART.UseVisualStyleBackColor = true;
            // 
            // labelTXStats
            // 
            this.labelTXStats.AutoSize = true;
            this.labelTXStats.Location = new System.Drawing.Point(6, 132);
            this.labelTXStats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTXStats.Name = "labelTXStats";
            this.labelTXStats.Size = new System.Drawing.Size(68, 20);
            this.labelTXStats.TabIndex = 9;
            this.labelTXStats.Text = "TX stats";
            // 
            // labelRXStats
            // 
            this.labelRXStats.AutoSize = true;
            this.labelRXStats.Location = new System.Drawing.Point(6, 57);
            this.labelRXStats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRXStats.Name = "labelRXStats";
            this.labelRXStats.Size = new System.Drawing.Size(71, 20);
            this.labelRXStats.TabIndex = 8;
            this.labelRXStats.Text = "RX stats";
            // 
            // buttonOpenCloseTX
            // 
            this.buttonOpenCloseTX.Location = new System.Drawing.Point(232, 85);
            this.buttonOpenCloseTX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenCloseTX.Name = "buttonOpenCloseTX";
            this.buttonOpenCloseTX.Size = new System.Drawing.Size(112, 35);
            this.buttonOpenCloseTX.TabIndex = 7;
            this.buttonOpenCloseTX.Text = "Open";
            this.buttonOpenCloseTX.UseVisualStyleBackColor = true;
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.FormattingEnabled = true;
            this.comboBoxBaud.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.comboBoxBaud.Location = new System.Drawing.Point(87, 157);
            this.comboBoxBaud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(134, 28);
            this.comboBoxBaud.TabIndex = 6;
            this.comboBoxBaud.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaud_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 162);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 20);
            this.label8.TabIndex = 5;
            this.label8.Text = "Baud:";
            // 
            // buttonOpenCloseRX
            // 
            this.buttonOpenCloseRX.Location = new System.Drawing.Point(232, 18);
            this.buttonOpenCloseRX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenCloseRX.Name = "buttonOpenCloseRX";
            this.buttonOpenCloseRX.Size = new System.Drawing.Size(112, 35);
            this.buttonOpenCloseRX.TabIndex = 4;
            this.buttonOpenCloseRX.Text = "Open";
            this.buttonOpenCloseRX.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "WiFi TX:";
            // 
            // comboBoxPortTX
            // 
            this.comboBoxPortTX.FormattingEnabled = true;
            this.comboBoxPortTX.Location = new System.Drawing.Point(87, 88);
            this.comboBoxPortTX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxPortTX.Name = "comboBoxPortTX";
            this.comboBoxPortTX.Size = new System.Drawing.Size(134, 28);
            this.comboBoxPortTX.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "WiFi RX:";
            // 
            // comboBoxPortRX
            // 
            this.comboBoxPortRX.FormattingEnabled = true;
            this.comboBoxPortRX.Location = new System.Drawing.Point(87, 18);
            this.comboBoxPortRX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxPortRX.Name = "comboBoxPortRX";
            this.comboBoxPortRX.Size = new System.Drawing.Size(134, 28);
            this.comboBoxPortRX.TabIndex = 0;
            // 
            // checkBoxRealtimeDual
            // 
            this.checkBoxRealtimeDual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRealtimeDual.AutoSize = true;
            this.checkBoxRealtimeDual.Location = new System.Drawing.Point(1515, 51);
            this.checkBoxRealtimeDual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxRealtimeDual.Name = "checkBoxRealtimeDual";
            this.checkBoxRealtimeDual.Size = new System.Drawing.Size(288, 24);
            this.checkBoxRealtimeDual.TabIndex = 8;
            this.checkBoxRealtimeDual.Text = "Realtime Dual UART Capture Mode";
            this.checkBoxRealtimeDual.UseVisualStyleBackColor = true;
            this.checkBoxRealtimeDual.CheckedChanged += new System.EventHandler(this.checkBoxRealtimeDual_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.richTextBoxComparer);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(2011, 1178);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compare tool";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(674, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Here is a simple compare tool. Enter a hex string in each line, the differences w" +
    "ill be hightlihtred";
            // 
            // richTextBoxComparer
            // 
            this.richTextBoxComparer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBoxComparer.Location = new System.Drawing.Point(4, 84);
            this.richTextBoxComparer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxComparer.Name = "richTextBoxComparer";
            this.richTextBoxComparer.Size = new System.Drawing.Size(2003, 1089);
            this.richTextBoxComparer.TabIndex = 0;
            this.richTextBoxComparer.Text = "";
            this.richTextBoxComparer.TextChanged += new System.EventHandler(this.richTextBoxComparer_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormTuyaMCUAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2019, 1244);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1750, 1300);
            this.Name = "FormTuyaMCUAnalyzer";
            this.Text = "TuyaMCU Explorer/Analyzer for OpenBeken - Elektroda.com ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxDecoded;
        private System.Windows.Forms.ListView listViewAvailableIDs;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBoxComparer;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ourForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ourTutorialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ourYoutubeChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTextToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonOpenCloseRX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxPortTX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPortRX;
        private System.Windows.Forms.CheckBox checkBoxRealtimeDual;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonOpenCloseTX;
        private System.Windows.Forms.Label labelTXStats;
        private System.Windows.Forms.Label labelRXStats;
        private System.Windows.Forms.RichTextBox richTextBoxSrc;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxStrTypeAsBytes;
        private System.Windows.Forms.CheckBox checkBoxHIdeHeartbeat;
        private System.Windows.Forms.CheckBox checkBoxDecodeColors;
        private System.Windows.Forms.CheckBox checkBoxHideWiFiState;
        private System.Windows.Forms.CheckBox checkBoxPauseUART;
        private System.Windows.Forms.Button buttonCopyRawToClipboard;
        private System.Windows.Forms.Button buttonCopyDecodedToClipboard;
        private System.Windows.Forms.CheckBox checkBoxHideDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

