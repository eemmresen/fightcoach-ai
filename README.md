# FightCoach AI

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet" alt=".NET 10" />
  <img src="https://img.shields.io/badge/React%20Native-0.85-61DAFB?logo=react" alt="React Native" />
  <img src="https://img.shields.io/badge/Expo-SDK%2056-000020?logo=expo" alt="Expo SDK 56" />
  <img src="https://img.shields.io/badge/Python-3.14-3776AB?logo=python" alt="Python 3.14" />
  <img src="https://img.shields.io/badge/MSSQL-2022-CC2927?logo=microsoft-sql-server" alt="MSSQL 2022" />
</p>

**FightCoach AI** is an AI-powered mobile application for fight sports athletes. Upload your training or sparring video and get instant technical analysis — guard score, footwork quality, attack readiness, defense stance — all combined into a single **Fight IQ** score. Think **Strava for fighters**.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [1. Backend API (.NET)](#1-backend-api-net)
  - [2. AI Service (Python)](#2-ai-service-python)
  - [3. Mobile App (React Native)](#3-mobile-app-react-native)
- [API Endpoints](#api-endpoints)
- [Architecture Decisions](#architecture-decisions)
- [License](#license)

---

## Features

| Module | Description |
|--------|-------------|
| **AI Technique Analysis** | Upload training video → MediaPipe pose detection → per-frame scoring |
| **Fight IQ Score** | Single 0-100 score: Guard(25%) + Defense(25%) + Footwork(20%) + Attack(20%) + Consistency(10%) |
| **AI Coach Feedback** | DeepSeek-powered personalized advice after every session |
| **Growth Tracking** | Chart your Fight IQ over time, compare sessions |
| **Achievements & Badges** | Gamification — XP, ranks, badges for milestones |
| **Leaderboard** | Weekly Fight IQ rankings |
| **Subscription Tiers** | Free (3/mo) → Premium (unlimited) → Pro Coach (multi-athlete) |

---

## Architecture

```
┌──────────────────────────────────────┐
│        React Native + Expo           │  Mobile App (iOS / Android / Web)
│  Zustand · Axios · Expo Router       │
└──────────────┬───────────────────────┘
               │ HTTPS / JWT
┌──────────────▼───────────────────────┐
│   ASP.NET Core 10 Web API             │  Backend API
│   Onion Architecture                  │
│   ┌─────────┐  ┌──────────────┐      │
│   │ Domain  │  │ Application  │      │
│   └─────────┘  └──────────────┘      │
│   ┌──────────────┐  ┌─────────┐      │
│   │Infrastructure│  │ WebAPI  │       │
│   └──────────────┘  └─────────┘      │
└──────┬──────────────┬────────────────┘
       │              │
┌──────▼─────┐ ┌──────▼──────────┐
│ MSSQL      │ │ Local Storage   │
│ EF Core    │ │ (videos)        │
└────────────┘ └──────┬──────────┘
                      │ HTTP (Channel<T>)
┌─────────────────────▼────────────────┐
│    Python FastAPI AI Service          │
│    ┌────────────┐  ┌────────────┐    │
│    │ MediaPipe   │  │ DeepSeek   │    │
│    │ Pose Detect │  │ LLM Coach  │    │
│    └────────────┘  └────────────┘    │
└──────────────────────────────────────┘
```

**Key rule**: No database triggers. All business logic, logging, and state tracking live in the Application/Infrastructure layer as code (`SaveChangesAsync` override, `ActivityLoggingMiddleware`, `SubscriptionLimitService`).

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| **Mobile** | React Native 0.85 + Expo SDK 56 + Expo Router |
| **State** | Zustand |
| **Backend** | ASP.NET Core 10 Web API (C#) |
| **Architecture** | Clean / Onion Architecture |
| **ORM** | Entity Framework Core 10 |
| **Database** | Microsoft SQL Server 2022 |
| **Auth** | JWT Bearer (Access + Refresh tokens) |
| **AI Service** | Python 3.14 + FastAPI |
| **Pose Detection** | MediaPipe BlazePose |
| **LLM Coach** | DeepSeek API |
| **Video Processing** | OpenCV |
| **Message Queue** | In-memory `Channel<T>` (no external MQ) |

---

## Project Structure

```
figthClup/
├── FightCoachAI.sln                       # .NET solution
│
├── src/
│   ├── FightCoachAI.Domain/               # Entities, Enums, ValueObjects, Interfaces
│   ├── FightCoachAI.Application/          # Services, DTOs, Validators, Interfaces
│   ├── FightCoachAI.Infrastructure/       # EF Core, Repositories, JWT, Storage
│   └── FightCoachAI.WebAPI/              # Controllers, Middleware, Program.cs
│
├── fightcoach-mobile/                     # React Native + Expo mobile app
│   ├── app/                               # Expo Router file-based routes
│   │   ├── (auth)/                        # Login, Register
│   │   ├── (onboarding)/                  # Welcome, Discipline, Experience, Physical Info
│   │   ├── (tabs)/                        # Home, History, Analysis, Social, Profile
│   │   └── (modals)/                      # Achievements, Premium, Settings
│   └── src/
│       ├── components/                    # UI components
│       ├── services/                      # API client, auth
│       ├── stores/                        # Zustand stores
│       └── theme/                         # Apex Striker design system
│
├── fightcoach-ai-service/                 # Python FastAPI AI service
│   └── app/
│       ├── api/routes/                    # /analyze, /health
│       └── services/                      # Pose detection, fight analyzer, DeepSeek coach
│
└── tests/                                 # xUnit test projects
```

---

## Prerequisites

| Tool | Version | Required For |
|------|---------|-------------|
| .NET SDK | 10.0+ | Backend API |
| Node.js | 22+ | Mobile app |
| Python | 3.12+ | AI service |
| MSSQL | 2022+ (or LocalDB) | Database |

---

## Getting Started

### 1. Backend API (.NET)

```bash
cd figthClup/src/FightCoachAI.WebAPI

# Restore + run
dotnet restore
dotnet run
```

Open `http://localhost:5263/swagger` (or the port shown in terminal) to explore the API.

**Database**: Update `appsettings.json` → `ConnectionStrings.DefaultConnection` with your MSSQL connection string. When running for the first time, apply migrations manually or use EF Core's `EnsureCreated()`.

### 2. AI Service (Python)

```bash
cd figthClup/fightcoach-ai-service

# Create virtual environment (recommended)
python -m venv venv
venv\Scripts\activate     # Windows
# source venv/bin/activate  # macOS/Linux

# Install dependencies
pip install -r requirements.txt

# Set DeepSeek API key (optional — fallback feedback works without it)
set DEEPSEEK_API_KEY=sk-your-key-here   # Windows
# export DEEPSEEK_API_KEY=sk-your-key-here  # macOS/Linux

# Run
uvicorn app.main:app --port 8000
```

### 3. Mobile App (React Native)

```bash
cd figthClup/fightcoach-mobile

# Install dependencies
npm install

# Run on web
npm run web

# Run on mobile (scan QR with Expo Go)
npm run start
```

**API Connection**: Edit `src/constants/api.ts` if your backend is on a different port:
```typescript
const DEV_API = 'http://10.0.2.2:5263/api';    // Android emulator
const DEV_API_IOS = 'http://localhost:5263/api'; // iOS / Web
```

---

## API Endpoints

### Auth
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/auth/register` | No | Create account |
| POST | `/api/auth/login` | No | Login |
| POST | `/api/auth/refresh` | No | Refresh JWT |

### User
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/user/profile` | JWT | Get profile |
| PUT | `/api/user/profile` | JWT | Update profile |
| GET | `/api/user/settings` | JWT | Get settings |
| PUT | `/api/user/settings` | JWT | Update settings |
| GET | `/api/user/achievements` | JWT | Get achievements & badges |

### Videos
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/videos/upload` | JWT | Upload training video |
| GET | `/api/videos` | JWT | List user videos |
| GET | `/api/videos/{id}` | JWT | Video detail + analysis |

### Analysis
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/analysis/{videoId}` | JWT | Get analysis result |
| GET | `/api/analysis/history` | JWT | Analysis history |
| POST | `/api/analysis/callback` | No | AI callback (internal) |

### Subscription
| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/subscription/current` | JWT | Current plan |
| GET | `/api/subscription/plans` | No | All plans |

### AI Service
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/ai/analyze` | Submit video for AI analysis |
| GET | `/api/ai/health` | Health check |

---

## Architecture Decisions

| Decision | Reason |
|----------|--------|
| **Onion Architecture** | Clean separation of concerns. Domain layer has zero dependencies |
| **No database triggers** | All business logic in code — `SaveChangesAsync` override, `ActivityLoggingMiddleware` |
| **Channel<T> over RabbitMQ** | Simpler deployment. No external MQ dependency for MVP |
| **Local file storage** | Simple setup. S3/MinIO can be plugged in via `IVideoStorageService` interface |
| **Expo Router v56** | File-based routing. No external `@react-navigation/*` imports needed |
| **Platform-aware storage** | `expo-secure-store` (native) → `localStorage` (web) via abstraction |
| **Manual mapping over AutoMapper** | AutoMapper 16.x API changed drastically — manual DTO mapping is simpler and more maintainable |

---

## License

MIT
