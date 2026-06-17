from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    deepseek_api_key: str = ""
    deepseek_base_url: str = "https://api.deepseek.com"
    video_upload_dir: str = "./uploads"
    max_video_duration: int = 1800

    class Config:
        env_file = ".env"


settings = Settings()
