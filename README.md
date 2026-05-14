# 🏭 Mini Factory Data Hub

A comprehensive simulation of an industrial IT data pipeline, demonstrating the integration of mock sensor data generation, backend API processing, and containerized database storage. This project was developed as a hands-on portfolio to showcase practical IT infrastructure and software development skills.

## 🚀 Architecture Overview

The system consists of three main components:
1. **Simulation (Python):** A script that generates mock temperature and status data from factory machines (`M-01`, `M-02`, `M-03`) and sends it via HTTP POST requests every 3 seconds.
2. **Backend API (C# .NET Core):** A RESTful API built with ASP.NET Core and Entity Framework Core that receives the machine data and handles database operations.
3. **Infrastructure & Database (Docker & SQL Server):** A containerized Microsoft SQL Server 2022 running on a Linux backend (WSL 2) to store the machine records efficiently.

## 🛠️ Technology Stack
- **Infrastructure:** Docker Desktop, Windows Subsystem for Linux (WSL 2 - Ubuntu)
- **Database:** Microsoft SQL Server 2022 (Containerized)
- **Backend:** C# ASP.NET Core (v10.0), Entity Framework Core
- **Scripting:** Python 3, `requests` library

## ⚙️ How It Works
1. The **SQL Server** container is spun up via Docker.
2. The **C# API** (`dotnet run`) connects to the SQL Server and opens endpoints at `http://localhost:5209/api/machinedata`.
3. The **Python Simulator** (`python simulator.py`) generates random temperatures. If a machine's temperature exceeds 85.0, it flags the status as "Warning". The data is pushed to the API in real-time.

## 📝 Key Skills Demonstrated
- Linux / Windows Subsystem for Linux (WSL) configuration.
- Docker container management and deployment.
- REST API development using modern C# .NET.
- ORM implementation with Entity Framework Core (Migrations & Code-First).
- Python scripting for automation and system simulation.
