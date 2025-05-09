using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ADBDataExtractor.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace ADBDataExtractor.Services
{
    public class ReportService
    {
        /// <summary>
        /// Generates a simple PDF report with the provided content
        /// </summary>
        /// <param name="filePath">Path to save the PDF file</param>
        /// <param name="content">Content to include in the report</param>
        public static void GenerateSimpleReport(string filePath, string content)
        {
            try
            {
                using var fs = new FileStream(filePath, FileMode.Create);
                var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(doc, fs);

                doc.Open();

                // Add title
                var titleFont = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD);
                var title = new Paragraph("ADB Data Extraction Report", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20;
                doc.Add(title);

                // Add timestamp
                var timestampFont = FontFactory.GetFont("Helvetica", 10, iTextSharp.text.Font.ITALIC);
                var timestamp = new Paragraph($"Generated: {DateTime.Now}", timestampFont);
                timestamp.Alignment = Element.ALIGN_RIGHT;
                timestamp.SpacingAfter = 20;
                doc.Add(timestamp);

                // Add content
                var contentFont = FontFactory.GetFont("Helvetica", 12);
                var paragraph = new Paragraph(content, contentFont);
                doc.Add(paragraph);

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to generate report: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates a detailed PDF report with contacts, messages, call logs, and device info
        /// </summary>
        /// <param name="filePath">Path to save the PDF file</param>
        /// <param name="contacts">List of contacts</param>
        /// <param name="messages">List of messages</param>
        /// <param name="callLogs">List of call logs</param>
        /// <param name="deviceInfo">Device information</param>
        public static void GenerateDetailedReport(
            string filePath,
            List<Contact> contacts,
            List<TextMessage> messages,
            List<CallLog> callLogs,
            DeviceInfo deviceInfo)
        {
            try
            {
                using var fs = new FileStream(filePath, FileMode.Create);
                var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                // Add title and header
                AddReportHeader(doc);

                // Add summary section
                AddSummarySection(doc, contacts, messages, callLogs, deviceInfo);

                // Add contacts section if available
                if (contacts.Count > 0)
                {
                    AddContactsSection(doc, contacts);
                }

                // Add messages section if available
                if (messages.Count > 0)
                {
                    doc.NewPage();
                    AddMessagesSection(doc, messages);
                }

                // Add call logs section if available
                if (callLogs.Count > 0)
                {
                    doc.NewPage();
                    AddCallLogsSection(doc, callLogs);
                }

                // Add device info section
                if (deviceInfo != null && (!string.IsNullOrEmpty(deviceInfo.CPUInfo) || !string.IsNullOrEmpty(deviceInfo.MemoryInfo)))
                {
                    doc.NewPage();
                    AddDeviceInfoSection(doc, deviceInfo);
                }

                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to generate detailed report: {ex.Message}");
            }
        }

        private static void AddReportHeader(Document doc)
        {
            // Add title
            var titleFont = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD);
            var title = new Paragraph("ADB Data Extraction Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 10;
            doc.Add(title);

            // Add subtitle with timestamp
            var subtitleFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.ITALIC);
            var subtitle = new Paragraph($"Generated on {DateTime.Now:yyyy-MM-dd} at {DateTime.Now:HH:mm:ss}", subtitleFont);
            subtitle.Alignment = Element.ALIGN_CENTER;
            subtitle.SpacingAfter = 20;
            doc.Add(subtitle);

            // Add horizontal line
            var line = new LineSeparator(1f, 100f, new BaseColor(200, 200, 200), Element.ALIGN_CENTER, -5f);
            doc.Add(line);
        }

        private static void AddSummarySection(
            Document doc,
            List<Contact> contacts,
            List<TextMessage> messages,
            List<CallLog> callLogs,
            DeviceInfo deviceInfo)
        {
            // Section title
            var sectionTitleFont = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.BOLD);
            var sectionTitle = new Paragraph("Data Summary", sectionTitleFont);
            sectionTitle.SpacingBefore = 20;
            sectionTitle.SpacingAfter = 10;
            doc.Add(sectionTitle);

            // Create summary table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Set column widths
            float[] columnWidths = { 1f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            var headerFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.BOLD);
            var headerCell = new PdfPCell(new Phrase("Data Type", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Summary", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            // Add rows
            var cellFont = FontFactory.GetFont("Helvetica", 12);

            // Contacts row
            var cell = new PdfPCell(new Phrase("Contacts", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase($"Total contacts: {contacts.Count}", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            // Messages row
            cell = new PdfPCell(new Phrase("Messages", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase($"Total messages: {messages.Count}", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            // Call logs row
            cell = new PdfPCell(new Phrase("Call Logs", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            string callLogSummary = $"Total calls: {callLogs.Count}";
            if (callLogs.Count > 0)
            {
                var incoming = callLogs.Count(c => c.CallType == "Incoming");
                var outgoing = callLogs.Count(c => c.CallType == "Outgoing");
                var missed = callLogs.Count(c => c.CallType == "Missed");
                callLogSummary += $"\nIncoming: {incoming}, Outgoing: {outgoing}, Missed: {missed}";
            }
            cell = new PdfPCell(new Phrase(callLogSummary, cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            // Device Info row
            cell = new PdfPCell(new Phrase("Device Info", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            string deviceInfoStatus = "Not available";
            if (deviceInfo != null && (!string.IsNullOrEmpty(deviceInfo.CPUInfo) || !string.IsNullOrEmpty(deviceInfo.MemoryInfo)))
            {
                deviceInfoStatus = "CPU and Memory info available";
            }
            cell = new PdfPCell(new Phrase(deviceInfoStatus, cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            doc.Add(table);
        }

        private static void AddContactsSection(Document doc, List<Contact> contacts)
        {
            // Section title
            var sectionTitleFont = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.BOLD);
            var sectionTitle = new Paragraph("Contacts", sectionTitleFont);
            sectionTitle.SpacingBefore = 20;
            sectionTitle.SpacingAfter = 10;
            doc.Add(sectionTitle);

            // Create contacts table
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Set column widths
            float[] columnWidths = { 1.5f, 1.5f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            var headerFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.BOLD);
            var headerCell = new PdfPCell(new Phrase("Name", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Phone Number", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Email", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            // Add contacts
            var cellFont = FontFactory.GetFont("Helvetica", 10);
            foreach (var contact in contacts)
            {
                table.AddCell(new PdfPCell(new Phrase(contact.Name ?? "", cellFont)) { Padding = 5f });
                table.AddCell(new PdfPCell(new Phrase(contact.PhoneNumber ?? "", cellFont)) { Padding = 5f });
                table.AddCell(new PdfPCell(new Phrase(contact.Email ?? "", cellFont)) { Padding = 5f });
            }

            doc.Add(table);
        }

        private static void AddMessagesSection(Document doc, List<TextMessage> messages)
        {
            // Section title
            var sectionTitleFont = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.BOLD);
            var sectionTitle = new Paragraph("Messages", sectionTitleFont);
            sectionTitle.SpacingBefore = 20;
            sectionTitle.SpacingAfter = 10;
            doc.Add(sectionTitle);

            // Create messages table
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Set column widths
            float[] columnWidths = { 1.5f, 3f, 1.5f };
            table.SetWidths(columnWidths);

            // Add table headers
            var headerFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.BOLD);
            var headerCell = new PdfPCell(new Phrase("Sender", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Content", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Timestamp", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            // Add messages
            var cellFont = FontFactory.GetFont("Helvetica", 8);
            foreach (var message in messages.Take(20)) // Limit to 20 messages to avoid massive report
            {
                table.AddCell(new PdfPCell(new Phrase(message.Sender ?? "", cellFont)) { Padding = 5f });

                // Truncate long messages
                string content = message.Content ?? "";
                if (content.Length > 100)
                {
                    content = content.Substring(0, 97) + "...";
                }
                table.AddCell(new PdfPCell(new Phrase(content, cellFont)) { Padding = 5f });

                table.AddCell(new PdfPCell(new Phrase(message.Timestamp ?? "", cellFont)) { Padding = 5f });
            }

            doc.Add(table);

            // Add note if there are more messages
            if (messages.Count > 20)
            {
                var noteFont = FontFactory.GetFont("Helvetica", 8, iTextSharp.text.Font.ITALIC);
                var note = new Paragraph($"Note: Showing 20 of {messages.Count} messages.", noteFont);
                note.Alignment = Element.ALIGN_RIGHT;
                note.SpacingBefore = 5;
                doc.Add(note);
            }
        }

        private static void AddCallLogsSection(Document doc, List<CallLog> callLogs)
        {
            // Section title
            var sectionTitleFont = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.BOLD);
            var sectionTitle = new Paragraph("Call Logs", sectionTitleFont);
            sectionTitle.SpacingBefore = 20;
            sectionTitle.SpacingAfter = 10;
            doc.Add(sectionTitle);

            // Create call logs table
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Set column widths
            float[] columnWidths = { 2f, 1f, 1f };
            table.SetWidths(columnWidths);

            // Add table headers
            var headerFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.BOLD);
            var headerCell = new PdfPCell(new Phrase("Phone Number", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Type", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Duration", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            // Add call logs
            var cellFont = FontFactory.GetFont("Helvetica", 10);
            foreach (var callLog in callLogs.Take(30)) // Limit to 30 call logs
            {
                table.AddCell(new PdfPCell(new Phrase(callLog.PhoneNumber ?? "", cellFont)) { Padding = 5f });
                table.AddCell(new PdfPCell(new Phrase(callLog.CallType ?? "", cellFont)) { Padding = 5f });
                table.AddCell(new PdfPCell(new Phrase(callLog.Duration ?? "", cellFont)) { Padding = 5f });
            }

            doc.Add(table);

            // Add note if there are more call logs
            if (callLogs.Count > 30)
            {
                var noteFont = FontFactory.GetFont("Helvetica", 8, iTextSharp.text.Font.ITALIC);
                var note = new Paragraph($"Note: Showing 30 of {callLogs.Count} call logs.", noteFont);
                note.Alignment = Element.ALIGN_RIGHT;
                note.SpacingBefore = 5;
                doc.Add(note);
            }
        }

        private static void AddDeviceInfoSection(Document doc, DeviceInfo deviceInfo)
        {
            // Section title
            var sectionTitleFont = FontFactory.GetFont("Helvetica", 14, iTextSharp.text.Font.BOLD);
            var sectionTitle = new Paragraph("Device Information", sectionTitleFont);
            sectionTitle.SpacingBefore = 20;
            sectionTitle.SpacingAfter = 10;
            doc.Add(sectionTitle);

            // Create device info table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            // Set column widths
            float[] columnWidths = { 1f, 3f };
            table.SetWidths(columnWidths);

            // Add table headers
            var headerFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.BOLD);
            var headerCell = new PdfPCell(new Phrase("Property", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            headerCell = new PdfPCell(new Phrase("Value", headerFont));
            headerCell.BackgroundColor = new BaseColor(0, 100, 0); // Dark Green
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            headerCell.Padding = 5f;
            table.AddCell(headerCell);

            // Add device info
            var cellFont = FontFactory.GetFont("Helvetica", 10);

            // CPU Info
            var cell = new PdfPCell(new Phrase("CPU Info", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(deviceInfo.CPUInfo ?? "Not available", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            // Memory Info
            cell = new PdfPCell(new Phrase("Memory Info", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(deviceInfo.MemoryInfo ?? "Not available", cellFont));
            cell.Padding = 5f;
            table.AddCell(cell);

            doc.Add(table);
        }
    }
}