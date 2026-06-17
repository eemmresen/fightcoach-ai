import os
import httpx
from typing import Optional


class DeepSeekCoach:
    def __init__(self, api_key: str = "", base_url: str = "https://api.deepseek.com"):
        self.api_key = api_key or os.getenv("DEEPSEEK_API_KEY", "")
        self.base_url = base_url

    async def get_coaching_feedback(
        self, analysis: dict, user_context: dict = None
    ) -> str:
        if not self.api_key:
            return self._fallback_feedback(analysis)

        prompt = self._build_prompt(analysis, user_context)

        async with httpx.AsyncClient(timeout=30) as client:
            try:
                resp = await client.post(
                    f"{self.base_url}/v1/chat/completions",
                    headers={"Authorization": f"Bearer {self.api_key}"},
                    json={
                        "model": "deepseek-chat",
                        "messages": [
                            {
                                "role": "system",
                                "content": "You are an elite fight coach analyzing a fighter's training video. Be specific, motivational, and actionable. Use fight sports terminology.",
                            },
                            {"role": "user", "content": prompt},
                        ],
                        "max_tokens": 500,
                        "temperature": 0.7,
                    },
                )
                data = resp.json()
                return data["choices"][0]["message"]["content"]
            except Exception:
                return self._fallback_feedback(analysis)

    def _build_prompt(self, analysis: dict, user_context: Optional[dict]) -> str:
        discipline = (user_context or {}).get("discipline", "boxing")
        experience = (user_context or {}).get("experience_level", "intermediate")

        prompt = f"""Analyze this {experience} {discipline} fighter's training session:

Fight IQ: {analysis["fight_iq"]}/100
Guard: {analysis["guard_score"]}/100
Defense: {analysis["defense_score"]}/100
Footwork: {analysis["footwork_score"]}/100
Attack: {analysis["attack_score"]}/100
Consistency: {analysis["consistency_score"]}/100

Detected Errors: {analysis.get("errors", [])[:5]}

Provide:
1. Overall Assessment (1-2 sentences)
2. 3 Key Strengths
3. 3 Areas for Improvement
4. Comparison (how this fighter compares to their level)
5. Training Recommendation (1 specific drill)"""
        return prompt

    def _fallback_feedback(self, analysis: dict) -> str:
        return f"""General Assessment: Your Fight IQ is {analysis["fight_iq"]}/100.

Your guard score ({analysis["guard_score"]}/100) is your strongest area - keep maintaining that discipline.

Focus on improving your footwork ({analysis["footwork_score"]}/100) with lateral movement and pivot drills.

Recommended drill: Mirror & Pivot - shadow box while constantly circling, maintaining stance width. Do 3 rounds of 3 minutes."""
