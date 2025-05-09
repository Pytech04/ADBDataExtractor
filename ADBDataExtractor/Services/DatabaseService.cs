using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using ADBDataExtractor.Models;

namespace ADBDataExtractor.Services
{
    public class DatabaseService
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=ADBExtract;Integrated Security=True";

        /// <summary>
        /// Initializes the database by creating tables if they don't exist
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                // Create Contacts table if it doesn't exist
                conn.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ContactsTable')
                    BEGIN
                        CREATE TABLE ContactsTable (
                            ID INT IDENTITY PRIMARY KEY,
                            Name NVARCHAR(100),
                            PhoneNumber NVARCHAR(50),
                            Email NVARCHAR(100)
                        );
                    END");

                // Create Messages table if it doesn't exist
                conn.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MessagesTable')
                    BEGIN
                        CREATE TABLE MessagesTable (
                            ID INT IDENTITY PRIMARY KEY,
                            Sender NVARCHAR(100),
                            Content NVARCHAR(MAX),
                            Timestamp NVARCHAR(50)
                        );
                    END");

                // Create CallLogs table if it doesn't exist
                conn.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CallLogsTable')
                    BEGIN
                        CREATE TABLE CallLogsTable (
                            ID INT IDENTITY PRIMARY KEY,
                            PhoneNumber NVARCHAR(50),
                            CallType NVARCHAR(20),
                            Duration NVARCHAR(50)
                        );
                    END");

                // Create DeviceInfo table if it doesn't exist
                conn.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DeviceInfoTable')
                    BEGIN
                        CREATE TABLE DeviceInfoTable (
                            ID INT IDENTITY PRIMARY KEY,
                            CPUInfo NVARCHAR(MAX),
                            MemoryInfo NVARCHAR(MAX)
                        );
                    END");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize database: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts or updates contacts in the database
        /// </summary>
        /// <param name="contacts">List of contacts to insert</param>
        public static void InsertContacts(List<Contact> contacts)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                // Make sure the table exists
                InitializeDatabase();

                // Insert each contact, checking for duplicates
                foreach (var contact in contacts)
                {
                    // Check if contact already exists based on phone number
                    var existingContact = conn.QueryFirstOrDefault<Contact>(
                        "SELECT * FROM ContactsTable WHERE PhoneNumber = @PhoneNumber",
                        new { contact.PhoneNumber });

                    if (existingContact == null)
                    {
                        // Insert new contact
                        conn.Execute(
                            "INSERT INTO ContactsTable (Name, PhoneNumber, Email) VALUES (@Name, @PhoneNumber, @Email)",
                            contact);
                    }
                    else
                    {
                        // Update existing contact
                        conn.Execute(
                            "UPDATE ContactsTable SET Name = @Name, Email = @Email WHERE PhoneNumber = @PhoneNumber",
                            contact);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert contacts: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts or updates messages in the database
        /// </summary>
        /// <param name="messages">List of messages to insert</param>
        public static void InsertMessages(List<TextMessage> messages)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                // Make sure the table exists
                InitializeDatabase();

                // Insert each message, checking for duplicates based on sender, content and timestamp
                foreach (var message in messages)
                {
                    // Check if message already exists
                    var existingMessage = conn.QueryFirstOrDefault<TextMessage>(
                        "SELECT * FROM MessagesTable WHERE Sender = @Sender AND Content = @Content AND Timestamp = @Timestamp",
                        new { message.Sender, message.Content, message.Timestamp });

                    if (existingMessage == null)
                    {
                        // Insert new message
                        conn.Execute(
                            "INSERT INTO MessagesTable (Sender, Content, Timestamp) VALUES (@Sender, @Content, @Timestamp)",
                            message);
                    }
                    // No need to update existing messages as they don't change
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert messages: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts or updates call logs in the database
        /// </summary>
        /// <param name="callLogs">List of call logs to insert</param>
        public static void InsertCallLogs(List<CallLog> callLogs)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                // Make sure the table exists
                InitializeDatabase();

                // Insert each call log, checking for duplicates
                foreach (var callLog in callLogs)
                {
                    // Check if call log already exists
                    var existingCallLog = conn.QueryFirstOrDefault<CallLog>(
                        "SELECT * FROM CallLogsTable WHERE PhoneNumber = @PhoneNumber AND CallType = @CallType AND Duration = @Duration",
                        new { callLog.PhoneNumber, callLog.CallType, callLog.Duration });

                    if (existingCallLog == null)
                    {
                        // Insert new call log
                        conn.Execute(
                            "INSERT INTO CallLogsTable (PhoneNumber, CallType, Duration) VALUES (@PhoneNumber, @CallType, @Duration)",
                            callLog);
                    }
                    // No need to update existing call logs as they don't change
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert call logs: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts or updates device information in the database
        /// </summary>
        /// <param name="deviceInfo">Device info to insert</param>
        public static void InsertDeviceInfo(DeviceInfo deviceInfo)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();

                // Make sure the table exists
                InitializeDatabase();

                // Insert device info
                conn.Execute(
                    "INSERT INTO DeviceInfoTable (CPUInfo, MemoryInfo) VALUES (@CPUInfo, @MemoryInfo)",
                    deviceInfo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert device info: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all contacts from the database
        /// </summary>
        /// <returns>List of contacts</returns>
        public static List<Contact> GetAllContacts()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                return conn.Query<Contact>("SELECT * FROM ContactsTable ORDER BY Name").AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve contacts: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all messages from the database
        /// </summary>
        /// <returns>List of messages</returns>
        public static List<TextMessage> GetAllMessages()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                return conn.Query<TextMessage>("SELECT * FROM MessagesTable ORDER BY Timestamp DESC").AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve messages: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all call logs from the database
        /// </summary>
        /// <returns>List of call logs</returns>
        public static List<CallLog> GetAllCallLogs()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                return conn.Query<CallLog>("SELECT * FROM CallLogsTable").AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve call logs: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the top 5 most recent messages
        /// </summary>
        /// <returns>List of top 5 recent messages</returns>
        public static List<TextMessage> GetTop5RecentMessages()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                return conn.Query<TextMessage>("SELECT TOP 5 * FROM MessagesTable ORDER BY Timestamp DESC").AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve recent messages: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the top 5 most recent call logs
        /// </summary>
        /// <returns>List of top 5 recent call logs</returns>
        public static List<CallLog> GetTop5RecentCallLogs()
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                return conn.Query<CallLog>("SELECT TOP 5 * FROM CallLogsTable ORDER BY ID DESC").AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve recent call logs: {ex.Message}");
            }
        }
    }
