FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:8080;http://+:443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Tutor2019.Apps.DockerWebMvc/Tutor2019.Apps.DockerWebMvc.csproj", "Tutor2019.Apps.DockerWebMvc/"]
RUN dotnet restore "Tutor2019.Apps.DockerWebMvc/Tutor2019.Apps.DockerWebMvc.csproj"
COPY . .
WORKDIR "/src/Tutor2019.Apps.DockerWebMvc"
RUN dotnet build "Tutor2019.Apps.DockerWebMvc.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Tutor2019.Apps.DockerWebMvc.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Tutor2019.Apps.DockerWebMvc.dll"]