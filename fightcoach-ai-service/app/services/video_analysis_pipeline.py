import cv2
import tempfile
import os
import httpx
from typing import List, Optional
from .pose_detection import detect_pose
from .fight_analyzer import FightAnalyzer
from .deepseek_coach import DeepSeekCoach


async def download_video(url: str) -> str:
    temp_path = os.path.join(tempfile.gettempdir(), f"fca_{os.urandom(8).hex()}.mp4")
    async with httpx.AsyncClient(timeout=120, follow_redirects=True) as client:
        resp = await client.get(url)
        with open(temp_path, "wb") as f:
            f.write(resp.content)
    return temp_path


def extract_frames(video_path: str, max_frames: int = 200) -> List:
    frames = []
    cap = cv2.VideoCapture(video_path)
    total = int(cap.get(cv2.CAP_PROP_FRAME_COUNT))
    step = max(1, total // max_frames)

    for i in range(0, total, step):
        cap.set(cv2.CAP_PROP_POS_FRAMES, i)
        ret, frame = cap.read()
        if ret:
            frames.append(frame)
        if len(frames) >= max_frames:
            break

    cap.release()
    return frames


async def process_video_analysis(
    video_url: str,
    video_id: str,
    user_id: str,
    discipline: str = "boxing",
    callback_url: Optional[str] = None,
    deepseek_api_key: str = "",
) -> dict:
    try:
        video_path = await download_video(video_url)
        frames = extract_frames(video_path, max_frames=200)
        all_landmarks = [detect_pose(f) for f in frames]
        analyzer = FightAnalyzer(discipline=discipline)
        result = analyzer.analyze_video_landmarks(all_landmarks)
        coach = DeepSeekCoach(api_key=deepseek_api_key)
        result["ai_coach_feedback"] = await coach.get_coaching_feedback(
            result, {"discipline": discipline}
        )
        result["status"] = "completed"

        if callback_url:
            async with httpx.AsyncClient(timeout=10) as client:
                await client.post(
                    callback_url,
                    json={
                        "video_id": video_id,
                        "user_id": user_id,
                        "analysis": result,
                    },
                )

        os.remove(video_path)
        return result

    except Exception as e:
        return {"status": "failed", "error": str(e), "video_id": video_id}
