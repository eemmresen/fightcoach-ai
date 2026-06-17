from typing import Optional
import numpy as np
from .pose_detection import (
    extract_key_angles,
    guard_position_score,
    generate_heatmap_data,
)


class FightAnalyzer:
    def __init__(self, discipline: str = "boxing"):
        self.discipline = discipline

    def analyze_video_landmarks(self, all_frame_landmarks: list) -> dict:
        """Analyze entire video from list of per-frame landmarks."""
        all_guard_scores = []
        footwork_scores = []
        attack_measures = []
        defense_measures = []
        valid_frames = [lm for lm in all_frame_landmarks if lm is not None]
        errors = []

        for i, landmarks in enumerate(valid_frames):
            guard = guard_position_score(landmarks)
            all_guard_scores.append(guard)

            angles = extract_key_angles(landmarks)
            footwork = self._score_footwork(landmarks)
            footwork_scores.append(footwork)

            attack = self._measure_attack_readiness(landmarks, angles)
            attack_measures.append(attack)

            defense = self._measure_defense_stance(landmarks, angles)
            defense_measures.append(defense)

            if guard < 40 and i % 5 == 0:
                errors.append(
                    {
                        "type": "guard_low",
                        "severity": "medium",
                        "timestamp": i,
                        "description": "Guard dropping below shoulder level",
                    }
                )

            if footwork < 30 and i % 5 == 0:
                errors.append(
                    {
                        "type": "footwork_cross",
                        "severity": "low",
                        "timestamp": i,
                        "description": "Feet crossing during lateral movement",
                    }
                )

        final_guard = self._safe_mean(all_guard_scores)
        final_footwork = self._safe_mean(footwork_scores)
        final_attack = self._safe_mean(attack_measures)
        final_defense = self._safe_mean(defense_measures)
        consistency_score = self._calculate_consistency(all_guard_scores)

        fight_iq = self._calculate_fight_iq(
            final_guard, final_defense, final_footwork, final_attack, consistency_score
        )
        heatmap = generate_heatmap_data(valid_frames)

        return {
            "fight_iq": round(fight_iq),
            "guard_score": round(final_guard),
            "defense_score": round(final_defense),
            "footwork_score": round(final_footwork),
            "attack_score": round(final_attack),
            "consistency_score": round(consistency_score),
            "errors": errors[:10],
            "combinations": [],
            "heatmap": heatmap,
        }

    def _score_footwork(self, landmarks: list) -> float:
        left_ankle = (landmarks[27]["x"], landmarks[27]["y"])
        right_ankle = (landmarks[28]["x"], landmarks[28]["y"])
        left_hip = (landmarks[23]["x"], landmarks[23]["y"])
        right_hip = (landmarks[24]["x"], landmarks[24]["y"])

        stance_width = abs(left_ankle[0] - right_ankle[0])
        hip_width = abs(left_hip[0] - right_hip[0])
        ideal = hip_width * 1.2
        width_score = max(0, 100 - abs(stance_width - ideal) * 200)
        center = (left_ankle[0] + right_ankle[0]) / 2
        hip_center = (left_hip[0] + right_hip[0]) / 2
        balance_score = max(0, 100 - abs(center - hip_center) * 150)
        return width_score * 0.5 + balance_score * 0.5

    def _measure_attack_readiness(self, landmarks: list, angles: dict) -> float:
        power_stance = min(angles.get("left_knee", 90), angles.get("right_knee", 90))
        power_bonus = max(0, power_stance - 120) * 1.5
        hip_rotation = abs(angles.get("left_hip", 90) - angles.get("right_hip", 90))
        base_score = 50
        return min(100, base_score + power_bonus + hip_rotation * 0.3)

    def _measure_defense_stance(self, landmarks: list, angles: dict) -> float:
        elbows_tight = min(angles.get("left_elbow", 90), angles.get("right_elbow", 90))
        elbow_score = max(0, 100 - abs(elbows_tight - 60) * 1.5)
        shoulder_square = abs(
            angles.get("left_shoulder", 90) - angles.get("right_shoulder", 90)
        )
        shoulder_score = max(0, 100 - shoulder_square * 2)
        return elbow_score * 0.6 + shoulder_score * 0.4

    def _calculate_consistency(self, scores: list) -> float:
        if len(scores) < 2:
            return 60
        std = np.std(scores)
        return max(0, 100 - std * 2.5)

    def _calculate_fight_iq(
        self, guard, defense, footwork, attack, consistency
    ) -> float:
        weights = {
            "boxing": (0.25, 0.25, 0.20, 0.20, 0.10),
            "kickboxing": (0.20, 0.20, 0.25, 0.25, 0.10),
            "muaythai": (0.20, 0.20, 0.20, 0.20, 0.20),
            "mma": (0.15, 0.20, 0.20, 0.20, 0.25),
        }
        w = weights.get(self.discipline.lower(), weights["boxing"])
        return (
            guard * w[0]
            + defense * w[1]
            + footwork * w[2]
            + attack * w[3]
            + consistency * w[4]
        )

    @staticmethod
    def _safe_mean(lst: list) -> float:
        return float(np.mean(lst)) if lst else 50.0
