from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.config import settings
from app.api.routes import analysis, health

app = FastAPI(
    title="FightCoach AI Service",
    description="AI-powered fight sports analysis service",
    version="1.0.0",
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

app.include_router(health.router, prefix="/api/ai", tags=["health"])
app.include_router(analysis.router, prefix="/api/ai", tags=["analysis"])
