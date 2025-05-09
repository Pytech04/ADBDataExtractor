using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ADBDataExtractor.Services
{
    public class ADBService
    {
        public static string ExecuteADBCommand(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            try
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // If there was an error with the ADB command, throw an exception
                if (!string.IsNullOrEmpty(error) && error.Contains("error", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"ADB Error: {error}");
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute ADB command: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if ADB is installed and available
        /// </summary>
        /// <returns>True if ADB is available, false otherwise</returns>
        public static bool IsADBAvailable()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "adb",
                        Arguments = "version",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output.Contains("Android Debug Bridge");
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of connected Android devices
        /// </summary>
        /// <returns>List of connected device IDs</returns>
        public static List<string> GetConnectedDevices()
        {
            var devices = new List<string>();
            try
            {
                string output = ExecuteADBCommand("devices");
                var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Skip the first line which is the header
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string deviceId = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        devices.Add(deviceId);
                    }
                }
            }
            catch
            {
                // Return empty list if there's an error
            }

            return devices;
        }
    }
}