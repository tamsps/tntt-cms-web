# Design Document

## Overview

This design implements Serilog as the primary logging framework for the ASP.NET Core application. Serilog will replace the default Microsoft logging provider and provide structured logging capabilities with file-based output stored in a dedicated Logs directory.

## Architecture

### Logging Pipeline
```
Application Code → ILogger<T> → Serilog → File Sink → Logs/app-{Date}.log
                                      → Console Sink (Development only)
```

### Key Components
- **Serilog Configuration**: Centralized in Program.cs using appsettings.json
- **File Sink**: Writes logs to rolling files in Logs directory
- **Console Sink**: Provides console output for development
- **Structured Logging**: JSON-like structured data in log entries

## Components and Interfaces

### 1. Serilog Configuration
- **Location**: Program.cs
- **Configuration Source**: appsettings.json with environment-specific overrides
- **Sinks**: File (primary) and Console (development)

### 2. File Sink Configuration
- **Output Directory**: `Logs/` (relative to application root)
- **File Pattern**: `app-{Date}.log` (daily rolling)
- **Retention**: 30 days of log files
- **File Size Limit**: 100MB per file with rollover

### 3. Log Levels by Environment
- **Development**: Information and above
- **Production**: Warning and above
- **Staging**: Information and above

### 4. Enhanced Exception Filter
- **Purpose**: Integrate with existing CustomExceptionFilter
- **Functionality**: Log exceptions with full context before handling

## Data Models

### Log Entry Structure
```json
{
  "Timestamp": "2025-01-27T10:30:00.123Z",
  "Level": "Information",
  "MessageTemplate": "User {UserId} performed {Action}",
  "Properties": {
    "UserId": "12345",
    "Action": "Login",
    "RequestId": "abc-123",
    "SourceContext": "ThieuNhiTT.Web.Controllers.HomeController"
  }
}
```

### Configuration Schema
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/app-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30,
          "fileSizeLimitBytes": 104857600
        }
      }
    ]
  }
}
```

## Error Handling

### Exception Logging Strategy
1. **Unhandled Exceptions**: Logged by Serilog's built-in exception handling
2. **Custom Exception Filter**: Enhanced to log before redirecting
3. **Business Logic Exceptions**: Logged at appropriate levels in service methods
4. **Validation Errors**: Logged as warnings with context

### Log Level Guidelines
- **Fatal**: Application crashes, critical system failures
- **Error**: Exceptions that affect functionality but don't crash the app
- **Warning**: Unexpected conditions that don't prevent operation
- **Information**: General application flow, user actions
- **Debug**: Detailed diagnostic information for troubleshooting

## Testing Strategy

### Unit Testing
- Test log output verification using Serilog test sinks
- Verify log levels are respected in different environments
- Test structured logging property extraction

### Integration Testing
- Verify log files are created in correct directory
- Test log rotation and retention policies
- Validate log format and structure

### Manual Testing
- Verify logs appear in Logs directory after application startup
- Test exception logging through CustomExceptionFilter
- Confirm different log levels work in different environments

## Implementation Considerations

### Package Dependencies
- `Serilog.AspNetCore`: Main Serilog integration for ASP.NET Core
- `Serilog.Sinks.File`: File output sink
- `Serilog.Sinks.Console`: Console output for development

### Performance Considerations
- Asynchronous logging to prevent blocking application threads
- File buffering to optimize I/O operations
- Log level filtering to reduce overhead in production

### Security Considerations
- Avoid logging sensitive data (passwords, tokens, PII)
- Implement log sanitization for user input
- Secure log file access permissions

### Deployment Considerations
- Ensure Logs directory is writable by application
- Configure log retention based on storage capacity
- Consider centralized logging for production environments