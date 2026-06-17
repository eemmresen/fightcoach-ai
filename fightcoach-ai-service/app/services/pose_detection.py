import mediapipe as mp
import numpy as np
import cv2
from typing import Optional

mp_pose = mp.solutions.pose
mp_drawing = mp.solutions.drawing_utils

pose = mp_pose.Pose(
    static_image_mode=False,
    model_complexity=2,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5,
)


def detect_pose(frame: np.ndarray) -> Optional[list]:
    """Detects pose landmarks in a single frame. Returns list of 33 landmarks or None."""
    frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    results = pose.process(frame_rgb)

    if not results.pose_landmarks:
        return None

    landmarks = []
    for lm in results.pose_landmarks.landmark:
        landmarks.append({"x": lm.x, "y": lm.y, "z": lm.z, "visibility": lm.visibility})

    return landmarks


def extract_key_angles(landmarks: list) -> dict:
    """Extract key joint angles from landmarks for technique analysis."""
    lm = landmarks

    def angle_3d(a_idx, b_idx, c_idx):
        a = np.array([lm[a_idx]["x"], lm[a_idx]["y"], lm[a_idx]["z"]])
        b = np.array([lm[b_idx]["x"], lm[b_idx]["y"], lm[b_idx]["z"]])
        c = np.array([lm[c_idx]["x"], lm[c_idx]["y"], lm[c_idx]["z"]])
        ba = a - b
        bc = c - b
        cosine = np.dot(ba, bc) / (np.linalg.norm(ba) * np.linalg.norm(bc) + 1e-9)
        return np.degrees(np.arccos(np.clip(cosine, -1.0, 1.0)))

    return {
        "left_elbow": angle_3d(11, 13, 15),
        "right_elbow": angle_3d(12, 14, 16),
        "left_shoulder": angle_3d(13, 11, 23),
        "right_shoulder": angle_3d(14, 12, 24),
        "left_knee": angle_3d(23, 25, 27),
        "right_knee": angle_3d(24, 26, 28),
        "left_hip": angle_3d(11, 23, 25),
        "right_hip": angle_3d(12, 24, 26),
    }


def guard_position_score(landmarks: list) -> float:
    """Score guard position based on hand positions relative to face (0-100)."""
    left_wrist = landmarks[15]
    right_wrist = landmarks[16]
    nose = landmarks[0]
    left_shoulder = landmarks[11]
    right_shoulder = landmarks[12]

    lh_height = 1 - left_wrist["y"]
    rh_height = 1 - right_wrist["y"]
    face_height = 1 - nose["y"]
    shoulder_y = 1 - (left_shoulder["y"] + right_shoulder["y"]) / 2

    hands_up = (lh_height + rh_height) / 2
    min_guard = shoulder_y + 0.02
    max_guard = face_height
    if hands_up < min_guard:
        return max(0, 40 + (hands_up - shoulder_y) * 200)
    elif hands_up > max_guard:
        return max(0, 100 - (hands_up - max_guard) * 150)
    return 60 + ((hands_up - min_guard) / (max_guard - min_guard + 1e-9)) * 40


def generate_heatmap_data(frames_data: list) -> dict:
    """Generate footwork heatmap from sequence of landmarks."""
    left_foot = []
    right_foot = []

    for lm in frames_data:
        if lm is None:
            continue
        left_foot.append((lm[27]["x"], lm[27]["y"]))
        right_foot.append((lm[28]["x"], lm[28]["y"]))

    if not left_foot:
        return {"left_foot": [], "right_foot": []}

    return {
        "left_foot": [[round(x, 4), round(y, 4)] for x, y in left_foot],
        "right_foot": [[round(x, 4), round(y, 4)] for x, y in right_foot],
    }
