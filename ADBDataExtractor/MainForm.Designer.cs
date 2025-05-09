using System.Windows.Forms;

namespace ADBDataExtractor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            tabContacts = new TabPage();
            dgvContacts = new DataGridView();
            btnSaveContacts = new Button();
            btnLoadContacts = new Button();
            tabMessages = new TabPage();
            dgvMessages = new DataGridView();
            btnSaveMessages = new Button();
            btnLoadMessages = new Button();
            tabCallLogs = new TabPage();
            dgvCallLogs = new DataGridView();
            btnSaveCallLogs = new Button();
            btnLoadCallLogs = new Button();
            tabDeviceInfo = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            txtCPUInfo = new TextBox();
            txtMemoryInfo = new TextBox();
            btnSaveDeviceInfo = new Button();
            btnLoadDeviceInfo = new Button();
            btnGenerateReport = new Button();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            tabControl1.SuspendLayout();
            tabContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContacts).BeginInit();
            tabMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMessages).BeginInit();
            tabCallLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCallLogs).BeginInit();
            tabDeviceInfo.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabContacts);
            tabControl1.Controls.Add(tabMessages);
            tabControl1.Controls.Add(tabCallLogs);
            tabControl1.Controls.Add(tabDeviceInfo);
            tabControl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(760, 410);
            tabControl1.TabIndex = 0;
            // 
            // tabContacts
            // 
            tabContacts.BackColor = Color.Black;
            tabContacts.BackgroundImage = (Image)resources.GetObject("tabContacts.BackgroundImage");
            tabContacts.Controls.Add(dgvContacts);
            tabContacts.Controls.Add(btnSaveContacts);
            tabContacts.Controls.Add(btnLoadContacts);
            tabContacts.Location = new Point(4, 24);
            tabContacts.Name = "tabContacts";
            tabContacts.Padding = new Padding(3);
            tabContacts.Size = new Size(752, 382);
            tabContacts.TabIndex = 0;
            tabContacts.Text = "Contacts";
            // 
            // dgvContacts
            // 
            dgvContacts.AllowUserToAddRows = false;
            dgvContacts.AllowUserToDeleteRows = false;
            dgvContacts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvContacts.BackgroundColor = Color.Black;
            dgvContacts.BorderStyle = BorderStyle.None;
            dgvContacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContacts.GridColor = Color.DarkGreen;
            dgvContacts.Location = new Point(9, 47);
            dgvContacts.Name = "dgvContacts";
            dgvContacts.ReadOnly = true;
            dgvContacts.RowTemplate.Height = 25;
            dgvContacts.Size = new Size(1286, 608);
            dgvContacts.TabIndex = 2;
            // 
            // btnSaveContacts
            // 
            btnSaveContacts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveContacts.Location = new Point(1155, 9);
            btnSaveContacts.Name = "btnSaveContacts";
            btnSaveContacts.Size = new Size(140, 32);
            btnSaveContacts.TabIndex = 1;
            btnSaveContacts.Text = "Save to Database";
            btnSaveContacts.UseVisualStyleBackColor = true;
            btnSaveContacts.Click += btnSaveContacts_Click;
            // 
            // btnLoadContacts
            // 
            btnLoadContacts.BackColor = Color.BlueViolet;
            btnLoadContacts.Location = new Point(6, 6);
            btnLoadContacts.Name = "btnLoadContacts";
            btnLoadContacts.Size = new Size(140, 32);
            btnLoadContacts.TabIndex = 0;
            btnLoadContacts.Text = "Load Contacts";
            btnLoadContacts.UseVisualStyleBackColor = false;
            btnLoadContacts.Click += btnLoadContacts_Click;
            // 
            // tabMessages
            // 
            tabMessages.BackColor = Color.Black;
            tabMessages.Controls.Add(dgvMessages);
            tabMessages.Controls.Add(btnSaveMessages);
            tabMessages.Controls.Add(btnLoadMessages);
            tabMessages.Location = new Point(4, 24);
            tabMessages.Name = "tabMessages";
            tabMessages.Padding = new Padding(3);
            tabMessages.Size = new Size(752, 382);
            tabMessages.TabIndex = 1;
            tabMessages.Text = "Messages";
            // 
            // dgvMessages
            // 
            dgvMessages.AllowUserToAddRows = false;
            dgvMessages.AllowUserToDeleteRows = false;
            dgvMessages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMessages.BackgroundColor = Color.Black;
            dgvMessages.BorderStyle = BorderStyle.None;
            dgvMessages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMessages.GridColor = Color.DarkGreen;
            dgvMessages.Location = new Point(9, 47);
            dgvMessages.Name = "dgvMessages";
            dgvMessages.ReadOnly = true;
            dgvMessages.RowTemplate.Height = 25;
            dgvMessages.Size = new Size(1286, 608);
            dgvMessages.TabIndex = 5;
            // 
            // btnSaveMessages
            // 
            btnSaveMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveMessages.Location = new Point(1155, 9);
            btnSaveMessages.Name = "btnSaveMessages";
            btnSaveMessages.Size = new Size(140, 32);
            btnSaveMessages.TabIndex = 4;
            btnSaveMessages.Text = "Save to Database";
            btnSaveMessages.UseVisualStyleBackColor = true;
            btnSaveMessages.Click += btnSaveMessages_Click;
            // 
            // btnLoadMessages
            // 
            btnLoadMessages.BackColor = Color.BlueViolet;
            btnLoadMessages.Location = new Point(6, 6);
            btnLoadMessages.Name = "btnLoadMessages";
            btnLoadMessages.Size = new Size(140, 32);
            btnLoadMessages.TabIndex = 3;
            btnLoadMessages.Text = "Load Messages";
            btnLoadMessages.UseVisualStyleBackColor = false;
            btnLoadMessages.Click += btnLoadMessages_Click;
            // 
            // tabCallLogs
            // 
            tabCallLogs.BackColor = Color.Black;
            tabCallLogs.Controls.Add(dgvCallLogs);
            tabCallLogs.Controls.Add(btnSaveCallLogs);
            tabCallLogs.Controls.Add(btnLoadCallLogs);
            tabCallLogs.Location = new Point(4, 24);
            tabCallLogs.Name = "tabCallLogs";
            tabCallLogs.Padding = new Padding(3);
            tabCallLogs.Size = new Size(752, 382);
            tabCallLogs.TabIndex = 2;
            tabCallLogs.Text = "Call Logs";
            // 
            // dgvCallLogs
            // 
            dgvCallLogs.AllowUserToAddRows = false;
            dgvCallLogs.AllowUserToDeleteRows = false;
            dgvCallLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCallLogs.BackgroundColor = Color.Black;
            dgvCallLogs.BorderStyle = BorderStyle.None;
            dgvCallLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCallLogs.GridColor = Color.DarkGreen;
            dgvCallLogs.Location = new Point(9, 47);
            dgvCallLogs.Name = "dgvCallLogs";
            dgvCallLogs.ReadOnly = true;
            dgvCallLogs.RowTemplate.Height = 25;
            dgvCallLogs.Size = new Size(1286, 608);
            dgvCallLogs.TabIndex = 5;
            // 
            // btnSaveCallLogs
            // 
            btnSaveCallLogs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveCallLogs.Location = new Point(1155, 9);
            btnSaveCallLogs.Name = "btnSaveCallLogs";
            btnSaveCallLogs.Size = new Size(140, 32);
            btnSaveCallLogs.TabIndex = 4;
            btnSaveCallLogs.Text = "Save to Database";
            btnSaveCallLogs.UseVisualStyleBackColor = true;
            btnSaveCallLogs.Click += btnSaveCallLogs_Click;
            // 
            // btnLoadCallLogs
            // 
            btnLoadCallLogs.BackColor = Color.BlueViolet;
            btnLoadCallLogs.Location = new Point(6, 6);
            btnLoadCallLogs.Name = "btnLoadCallLogs";
            btnLoadCallLogs.Size = new Size(140, 32);
            btnLoadCallLogs.TabIndex = 3;
            btnLoadCallLogs.Text = "Load Call Logs";
            btnLoadCallLogs.UseVisualStyleBackColor = false;
            btnLoadCallLogs.Click += btnLoadCallLogs_Click;
            // 
            // tabDeviceInfo
            // 
            tabDeviceInfo.BackColor = Color.Black;
            tabDeviceInfo.Controls.Add(tableLayoutPanel1);
            tabDeviceInfo.Controls.Add(btnSaveDeviceInfo);
            tabDeviceInfo.Controls.Add(btnLoadDeviceInfo);
            tabDeviceInfo.Location = new Point(4, 24);
            tabDeviceInfo.Name = "tabDeviceInfo";
            tabDeviceInfo.Padding = new Padding(3);
            tabDeviceInfo.Size = new Size(752, 382);
            tabDeviceInfo.TabIndex = 3;
            tabDeviceInfo.Text = "Device Info";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(txtCPUInfo, 0, 1);
            tableLayoutPanel1.Controls.Add(txtMemoryInfo, 1, 1);
            tableLayoutPanel1.Location = new Point(9, 47);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1286, 608);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = Color.LightGreen;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(637, 25);
            label1.TabIndex = 0;
            label1.Text = "CPU Information";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.ForeColor = Color.LightGreen;
            label2.Location = new Point(646, 0);
            label2.Name = "label2";
            label2.Size = new Size(637, 25);
            label2.TabIndex = 1;
            label2.Text = "Memory Information";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCPUInfo
            // 
            txtCPUInfo.BackColor = Color.Black;
            txtCPUInfo.Dock = DockStyle.Fill;
            txtCPUInfo.ForeColor = Color.MediumPurple;
            txtCPUInfo.Location = new Point(3, 28);
            txtCPUInfo.Multiline = true;
            txtCPUInfo.Name = "txtCPUInfo";
            txtCPUInfo.ReadOnly = true;
            txtCPUInfo.ScrollBars = ScrollBars.Vertical;
            txtCPUInfo.Size = new Size(637, 577);
            txtCPUInfo.TabIndex = 2;
            // 
            // txtMemoryInfo
            // 
            txtMemoryInfo.BackColor = Color.Black;
            txtMemoryInfo.Dock = DockStyle.Fill;
            txtMemoryInfo.ForeColor = Color.LightGreen;
            txtMemoryInfo.Location = new Point(646, 28);
            txtMemoryInfo.Multiline = true;
            txtMemoryInfo.Name = "txtMemoryInfo";
            txtMemoryInfo.ReadOnly = true;
            txtMemoryInfo.ScrollBars = ScrollBars.Vertical;
            txtMemoryInfo.Size = new Size(637, 577);
            txtMemoryInfo.TabIndex = 3;
            // 
            // btnSaveDeviceInfo
            // 
            btnSaveDeviceInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveDeviceInfo.Location = new Point(1155, 9);
            btnSaveDeviceInfo.Name = "btnSaveDeviceInfo";
            btnSaveDeviceInfo.Size = new Size(140, 32);
            btnSaveDeviceInfo.TabIndex = 4;
            btnSaveDeviceInfo.Text = "Save to Database";
            btnSaveDeviceInfo.UseVisualStyleBackColor = true;
            btnSaveDeviceInfo.Click += btnSaveDeviceInfo_Click;
            // 
            // btnLoadDeviceInfo
            // 
            btnLoadDeviceInfo.BackColor = Color.BlueViolet;
            btnLoadDeviceInfo.Location = new Point(6, 6);
            btnLoadDeviceInfo.Name = "btnLoadDeviceInfo";
            btnLoadDeviceInfo.Size = new Size(140, 32);
            btnLoadDeviceInfo.TabIndex = 3;
            btnLoadDeviceInfo.Text = "Load Device Info";
            btnLoadDeviceInfo.UseVisualStyleBackColor = false;
            btnLoadDeviceInfo.Click += btnLoadDeviceInfo_Click;
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnGenerateReport.BackColor = Color.CadetBlue;
            btnGenerateReport.Location = new Point(12, 428);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(760, 32);
            btnGenerateReport.TabIndex = 1;
            btnGenerateReport.Text = "Generate PDF Report";
            btnGenerateReport.UseVisualStyleBackColor = false;
            btnGenerateReport.Click += btnGenerateReport_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Black;
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 472);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(784, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.ForeColor = Color.LightGreen;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(784, 494);
            Controls.Add(statusStrip1);
            Controls.Add(btnGenerateReport);
            Controls.Add(tabControl1);
            MinimumSize = new Size(800, 533);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ADB Data Extractor";
            tabControl1.ResumeLayout(false);
            tabContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvContacts).EndInit();
            tabMessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMessages).EndInit();
            tabCallLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCallLogs).EndInit();
            tabDeviceInfo.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabContacts;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.TabPage tabCallLogs;
        private System.Windows.Forms.TabPage tabDeviceInfo;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button btnLoadContacts;
        private System.Windows.Forms.Button btnSaveContacts;
        private System.Windows.Forms.DataGridView dgvContacts;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.Button btnSaveMessages;
        private System.Windows.Forms.Button btnLoadMessages;
        private System.Windows.Forms.DataGridView dgvCallLogs;
        private System.Windows.Forms.Button btnSaveCallLogs;
        private System.Windows.Forms.Button btnLoadCallLogs;
        private System.Windows.Forms.Button btnSaveDeviceInfo;
        private System.Windows.Forms.Button btnLoadDeviceInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCPUInfo;
        private System.Windows.Forms.TextBox txtMemoryInfo;
    }
}
