from fastapi import APIRouter, BackgroundTasks
from pydantic import BaseModel
from typing import Optional
from app.services.video_analysis_pipeline import process_video_analysis
import os

router = APIRouter()


class AnalyzeRequest(BaseModel):
    video_url: str
    video_id: str
    user_id: str
    discipline: str = "boxing"
    callback_url: Optional[str] = None


class AnalyzeResponse(BaseModel):
    video_id: str
    status: str
    message: str


@router.post("/analyze", response_model=AnalyzeResponse)
async def analyze_video(request: AnalyzeRequest, background_tasks: BackgroundTasks):
    background_tasks.add_task(
        process_video_analysis,
        request.video_url,
        request.video_id,
        request.user_id,
        request.discipline,
        request.callback_url,
        os.getenv("DEEPSEEK_API_KEY", ""),
    )
    return AnalyzeResponse(
        video_id=request.video_id, status="processing", message="Video analysis started"
    )
