using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ADBDataExtractor.Models;
using ADBDataExtractor.Services;

namespace ADBDataExtractor
{
    public partial class MainForm : Form
    {
        // Lists to store the extracted data
        private List<Contact> contacts = new List<Contact>();
        private List<TextMessage> messages = new List<TextMessage>();
        private List<CallLog> callLogs = new List<CallLog>();
        private DeviceInfo deviceInfo = new DeviceInfo();

        public MainForm()
        {
            InitializeComponent();
            ApplyTheme();
            CheckADBConnection();
        }

        private void ApplyTheme()
        {
            // Apply green and black theme
            this.BackColor = Color.Black;
            tabControl1.BackColor = Color.DarkGreen;

            // Apply theme to all buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    ApplyButtonTheme(btn);
                }
            }

            // Apply theme to buttons in tab pages
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (Control ctrl in tab.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        ApplyButtonTheme(btn);
                    }
                }
            }
        }

        private void ApplyButtonTheme(Button btn)
        {
            btn.BackColor = Color.DarkGreen;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.LightGreen;
            btn.FlatAppearance.BorderSize = 1;
        }

        private void CheckADBConnection()
        {
            try
            {
                string output = ADBService.ExecuteADBCommand("devices");
                if (output.Contains("List of devices attached") && !output.Contains("device", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("No Android devices detected. Please connect a device with USB debugging enabled.",
                        "ADB Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (output.Contains("device", StringComparison.OrdinalIgnoreCase))
                {
                    statusLabel.Text = "Device connected";
                    statusLabel.ForeColor = Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to ADB: {ex.Message}\n\nMake sure ADB is installed and added to your PATH.",
                    "ADB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "ADB not found";
                statusLabel.ForeColor = Color.Red;
            }
        }

        #region Contact Tab Methods
        private void btnLoadContacts_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Loading contacts...";

                // Execute ADB command to get contacts
                string output = ADBService.ExecuteADBCommand("shell content query --uri content://contacts/phones");
                contacts = ParseContacts(output);

                // Display contacts in DataGridView
                dgvContacts.DataSource = null;
                dgvContacts.DataSource = contacts;

                statusLabel.Text = $"Loaded {contacts.Count} contacts";
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error loading contacts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading contacts";
            }
        }

        private List<Contact> ParseContacts(string output)
        {
            var result = new List<Contact>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int id = 1;

            foreach (var line in lines)
            {
                try
                {
                    // Extract name using regex
                    var nameMatch = Regex.Match(line, @"display_name=([^,]+)");

                    // Extract phone number using regex
                    var phoneMatch = Regex.Match(line, @"data1=([^,]+)");

                    // Extract email (might not be present in all contacts)
                    var emailMatch = Regex.Match(line, @"data4=([^,]+)");

                    if (nameMatch.Success && phoneMatch.Success)
                    {
                        var contact = new Contact
                        {
                            ID = id++,
                            Name = nameMatch.Groups[1].Value.Trim(),
                            PhoneNumber = phoneMatch.Groups[1].Value.Trim(),
                            Email = emailMatch.Success ? emailMatch.Groups[1].Value.Trim() : ""
                        };
                        result.Add(contact);
                    }
                }
                catch
                {
                    // Skip invalid entries
                    continue;
                }
            }

            return result;
        }

        private void btnSaveContacts_Click(object sender, EventArgs e)
        {
            try
            {
                if (contacts.Count == 0)
                {
                    MessageBox.Show("No contacts to save. Please load contacts first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Saving contacts to database...";

                DatabaseService.InsertContacts(contacts);

                Cursor = Cursors.Default;
                statusLabel.Text = $"Saved {contacts.Count} contacts to database";
                MessageBox.Show($"Successfully saved {contacts.Count} contacts to database!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error saving contacts to database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error saving to database";
            }
        }
        #endregion

        #region Messages Tab Methods
        private void btnLoadMessages_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Loading messages...";

                // Execute ADB command to get SMS messages
                string output = ADBService.ExecuteADBCommand("shell content query --uri content://sms");
                messages = ParseMessages(output);

                // Display messages in DataGridView
                dgvMessages.DataSource = null;
                dgvMessages.DataSource = messages;

                statusLabel.Text = $"Loaded {messages.Count} messages";
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error loading messages: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading messages";
            }
        }

        private List<TextMessage> ParseMessages(string output)
        {
            var result = new List<TextMessage>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int id = 1;

            foreach (var line in lines)
            {
                try
                {
                    // Extract address (sender) using regex
                    var senderMatch = Regex.Match(line, @"address=([^,]+)");

                    // Extract body (content) using regex
                    var contentMatch = Regex.Match(line, @"body=([^,]+)");

                    // Extract date using regex
                    var dateMatch = Regex.Match(line, @"date=(\d+)");

                    if (senderMatch.Success && contentMatch.Success)
                    {
                        // Convert timestamp to readable date if available
                        string timestamp = "Unknown";
                        if (dateMatch.Success && long.TryParse(dateMatch.Groups[1].Value, out long ticks))
                        {
                            try
                            {
                                // Convert Unix timestamp (milliseconds) to DateTime
                                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                    .AddMilliseconds(ticks).ToLocalTime();
                                timestamp = date.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            catch
                            {
                                timestamp = dateMatch.Groups[1].Value;
                            }
                        }

                        var message = new TextMessage
                        {
                            ID = id++,
                            Sender = senderMatch.Groups[1].Value.Trim(),
                            Content = contentMatch.Groups[1].Value.Trim(),
                            Timestamp = timestamp
                        };
                        result.Add(message);
                    }
                }
                catch
                {
                    // Skip invalid entries
                    continue;
                }
            }

            return result;
        }

        private void btnSaveMessages_Click(object sender, EventArgs e)
        {
            try
            {
                if (messages.Count == 0)
                {
                    MessageBox.Show("No messages to save. Please load messages first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Saving messages to database...";

                DatabaseService.InsertMessages(messages);

                Cursor = Cursors.Default;
                statusLabel.Text = $"Saved {messages.Count} messages to database";
                MessageBox.Show($"Successfully saved {messages.Count} messages to database!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error saving messages to database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error saving to database";
            }
        }
        #endregion

        #region Call Logs Tab Methods
        private void btnLoadCallLogs_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Loading call logs...";

                // Execute ADB command to get call logs
                string output = ADBService.ExecuteADBCommand("shell content query --uri content://call_log/calls");
                callLogs = ParseCallLogs(output);

                // Display call logs in DataGridView
                dgvCallLogs.DataSource = null;
                dgvCallLogs.DataSource = callLogs;

                statusLabel.Text = $"Loaded {callLogs.Count} call logs";
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error loading call logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading call logs";
            }
        }

        private List<CallLog> ParseCallLogs(string output)
        {
            var result = new List<CallLog>();
            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int id = 1;

            foreach (var line in lines)
            {
                try
                {
                    // Extract phone number using regex
                    var numberMatch = Regex.Match(line, @"number=([^,]+)");

                    // Extract type (incoming, outgoing, missed) using regex
                    var typeMatch = Regex.Match(line, @"type=(\d+)");

                    // Extract duration using regex
                    var durationMatch = Regex.Match(line, @"duration=(\d+)");

                    if (numberMatch.Success && typeMatch.Success)
                    {
                        // Convert call type number to readable text
                        string callType = "Unknown";
                        if (typeMatch.Success && int.TryParse(typeMatch.Groups[1].Value, out int typeCode))
                        {
                            switch (typeCode)
                            {
                                case 1: callType = "Incoming"; break;
                                case 2: callType = "Outgoing"; break;
                                case 3: callType = "Missed"; break;
                                case 4: callType = "Voicemail"; break;
                                case 5: callType = "Rejected"; break;
                                case 6: callType = "Blocked"; break;
                                default: callType = $"Unknown ({typeCode})"; break;
                            }
                        }

                        // Convert duration seconds to readable format
                        string duration = "Unknown";
                        if (durationMatch.Success && int.TryParse(durationMatch.Groups[1].Value, out int seconds))
                        {
                            TimeSpan time = TimeSpan.FromSeconds(seconds);
                            duration = time.Hours > 0
                                ? $"{time.Hours}h {time.Minutes}m {time.Seconds}s"
                                : $"{time.Minutes}m {time.Seconds}s";
                        }

                        var callLog = new CallLog
                        {
                            ID = id++,
                            PhoneNumber = numberMatch.Groups[1].Value.Trim(),
                            CallType = callType,
                            Duration = duration
                        };
                        result.Add(callLog);
                    }
                }
                catch
                {
                    // Skip invalid entries
                    continue;
                }
            }

            return result;
        }

        private void btnSaveCallLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (callLogs.Count == 0)
                {
                    MessageBox.Show("No call logs to save. Please load call logs first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Saving call logs to database...";

                DatabaseService.InsertCallLogs(callLogs);

                Cursor = Cursors.Default;
                statusLabel.Text = $"Saved {callLogs.Count} call logs to database";
                MessageBox.Show($"Successfully saved {callLogs.Count} call logs to database!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error saving call logs to database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error saving to database";
            }
        }
        #endregion

        #region Device Info Tab Methods
        private void btnLoadDeviceInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Loading device info...";

                // Execute ADB commands to get CPU and memory info
                string cpuInfo = ADBService.ExecuteADBCommand("shell cat /proc/cpuinfo");
                string memInfo = ADBService.ExecuteADBCommand("shell cat /proc/meminfo");

                // Parse and display info
                deviceInfo = new DeviceInfo
                {
                    ID = 1,
                    CPUInfo = cpuInfo,
                    MemoryInfo = memInfo
                };

                // Display in text boxes
                txtCPUInfo.Text = cpuInfo;
                txtMemoryInfo.Text = memInfo;

                statusLabel.Text = "Loaded device info";
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error loading device info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading device info";
            }
        }

        private void btnSaveDeviceInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCPUInfo.Text) && string.IsNullOrEmpty(txtMemoryInfo.Text))
                {
                    MessageBox.Show("No device info to save. Please load device info first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Saving device info to database...";

                DatabaseService.InsertDeviceInfo(deviceInfo);

                Cursor = Cursors.Default;
                statusLabel.Text = "Saved device info to database";
                MessageBox.Show("Successfully saved device info to database!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error saving device info to database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error saving to database";
            }
        }
        #endregion

        #region Report Generation
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.Text = "Generating report...";

                // Create a SaveFileDialog to let user choose where to save the PDF
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "PDF files (*.pdf)|*.pdf";
                    dialog.FileName = $"ADB_Extract_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // Generate the detailed report
                        ReportService.GenerateDetailedReport(
                            dialog.FileName,
                            contacts,
                            messages,
                            callLogs,
                            deviceInfo);

                        statusLabel.Text = "Report generated successfully";
                        MessageBox.Show($"Report generated successfully and saved to:\n{dialog.FileName}",
                            "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ask if user wants to open the PDF
                        if (MessageBox.Show("Do you want to open the report now?", "Open Report",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                            {
                                FileName = dialog.FileName,
                                UseShellExecute = true
                            });
                        }
                    }
                    else
                    {
                        statusLabel.Text = "Report generation cancelled";
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error generating report";
            }
        }
        #endregion
    }
}