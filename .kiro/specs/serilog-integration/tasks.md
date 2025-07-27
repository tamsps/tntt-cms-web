# Implementation Plan

- [x] 1. Install and configure Serilog packages


  - Add Serilog.AspNetCore, Serilog.Sinks.File, and Serilog.Sinks.Console NuGet packages to the project
  - Configure package references in ThieuNhiTT.Web.csproj
  - _Requirements: 1.1, 1.4_

- [x] 2. Configure Serilog in Program.cs


  - Replace default logging configuration with Serilog in Program.cs
  - Configure Serilog to read from appsettings.json
  - Set up both file and console sinks with appropriate configuration
  - _Requirements: 1.1, 1.2, 2.1_

- [x] 3. Add Serilog configuration to appsettings.json


  - Create Serilog configuration section in appsettings.json
  - Configure file sink to write to Logs directory with daily rolling
  - Set up log levels and retention policies
  - _Requirements: 1.3, 2.1, 2.2, 2.3, 3.1_

- [x] 4. Create environment-specific logging configuration


  - Add Serilog configuration to appsettings.Development.json for development-specific settings
  - Configure different log levels for Development vs Production environments
  - Add console sink for development environment only
  - _Requirements: 3.1, 3.2, 3.3_

- [x] 5. Create Logs directory structure


  - Create Logs directory in project root
  - Add .gitkeep file to ensure directory exists in version control
  - Configure directory permissions and access
  - _Requirements: 1.3, 2.1_

- [x] 6. Enhance CustomExceptionFilter with structured logging


  - Modify CustomExceptionFilter to use ILogger<CustomExceptionFilter> for structured logging
  - Log exceptions with full context including request information and stack traces
  - Implement proper log levels for different exception types
  - _Requirements: 4.1, 4.3_

- [x] 7. Add structured logging to HomeController


  - Inject ILogger<HomeController> and add logging statements for key operations
  - Log user actions like page visits, form submissions, and email sending
  - Implement structured logging with contextual properties
  - _Requirements: 4.2, 4.3_

- [x] 8. Add logging to service layer


  - Enhance BookService, LessonService, NewsService, and EmailService with logging
  - Log business operations, data access operations, and service method calls
  - Include structured properties for tracking and debugging
  - _Requirements: 4.2, 4.4_

- [ ] 9. Add logging to repository layer



  - Enhance JsonBookRepository with logging for data access operations
  - Log CRUD operations with entity information and file paths
  - Include error logging for file I/O operations
  - _Requirements: 4.4_

- [ ] 10. Test and validate logging implementation
  - Create unit tests to verify logging behavior in different scenarios
  - Test log file creation, rotation, and retention
  - Validate structured logging output format and content
  - _Requirements: 1.1, 1.2, 1.3, 2.1, 2.2, 2.3_