FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/FightCoachAI.Domain/FightCoachAI.Domain.csproj src/FightCoachAI.Domain/
COPY src/FightCoachAI.Application/FightCoachAI.Application.csproj src/FightCoachAI.Application/
COPY src/FightCoachAI.Infrastructure/FightCoachAI.Infrastructure.csproj src/FightCoachAI.Infrastructure/
COPY src/FightCoachAI.WebAPI/FightCoachAI.WebAPI.csproj src/FightCoachAI.WebAPI/

RUN dotnet restore src/FightCoachAI.WebAPI/FightCoachAI.WebAPI.csproj

COPY src/ ./src/

WORKDIR /src/src/FightCoachAI.WebAPI
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
RUN mkdir -p /app/uploads/videos

EXPOSE 8080
ENTRYPOINT ["dotnet", "FightCoachAI.WebAPI.dll"]
