# ADB Data Extractor

ADB Data Extractor is a Windows Forms application designed to extract, manage, and store data from Android devices. It provides a user-friendly interface to handle contacts, messages, call logs, and device information, with options to save the data to a local SQL Server database and generate reports.

---
##UI

![image](https://github.com/user-attachments/assets/24a355b3-b7ae-442d-ba11-143003f7c6c0)

## Features

- **Contacts Management**:
  - Load contacts from an Android device.
  - Save contacts to a SQL Server database.
  - View and manage contacts in a tabular format.

- **Messages Management**:
  - Load text messages from an Android device.
  - Save messages to a SQL Server database.
  - View and manage messages in a tabular format.

- **Call Logs Management**:
  - Load call logs from an Android device.
  - Save call logs to a SQL Server database.
  - View and manage call logs in a tabular format.

- **Device Information**:
  - Retrieve CPU and memory information from an Android device.
  - Save device information to a SQL Server database.

- **Report Generation**:
  - Generate PDF reports for the extracted data.

---

## Technologies Used

- **Programming Language**: C# (Version 10.0)
- **Framework**: .NET 6
- **Database**: SQL Server (Local instance)
- **UI Framework**: Windows Forms
- **Libraries**:
  - [Dapper](https://github.com/DapperLib/Dapper) for database operations.
  - [System.Data.SqlClient](https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient) for SQL Server connectivity.

---

## Installation

### Prerequisites

1. Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and ensure it is running locally.
2. Install [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).
3. Install [ADB (Android Debug Bridge)](https://developer.android.com/studio/command-line/adb) and ensure it is added to your system's PATH.

### Steps

1. Clone the repository:
2. Open the solution in Visual Studio 2022.
3. Build the solution to restore dependencies.
4. Run the application.

---

## Usage

### 1. Initialize the Database
- The application will automatically create the required tables in the database when it runs for the first time. Ensure the database connection string in `DatabaseService.cs` is configured correctly:
  
### 2. Extract Data
- Connect your Android device via USB and ensure USB debugging is enabled.
- Use the **Load** buttons in the application to fetch data (e.g., contacts, messages, call logs, or device info).

### 3. Save Data
- Use the **Save to Database** buttons to store the extracted data in the SQL Server database.

### 4. Generate Reports
- Click the **Generate PDF Report** button to create a report of the extracted data.

---

## Project Structure

- **Models**:
  - `Contact.cs`: Represents a contact with fields like `Name`, `PhoneNumber`, and `Email`.
  - `TextMessage.cs`: Represents a text message with fields like `Sender`, `Content`, and `Timestamp`.
  - `CallLog.cs`: Represents a call log with fields like `PhoneNumber`, `CallType`, and `Duration`.
  - `DeviceInfo.cs`: Represents device information with fields like `CPUInfo` and `MemoryInfo`.

- **Services**:
  - `DatabaseService.cs`: Handles database initialization and CRUD operations.
  - `ADBService.cs`: Interacts with the Android device using ADB commands.
  - `ReportService.cs`: Generates PDF reports for the extracted data.

- **UI**:
  - `MainForm.cs`: The main Windows Forms interface for interacting with the application.

---

## Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request with a detailed description of your changes.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Acknowledgments

- [Dapper](https://github.com/DapperLib/Dapper) for simplifying database operations.
- [ADB](https://developer.android.com/studio/command-line/adb) for enabling communication with Android devices.

  
