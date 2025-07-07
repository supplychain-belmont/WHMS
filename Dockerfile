# ----------------
# Etapa 1: Build
# ----------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar sln y proyectos
COPY ./Indotalent.sln ./
COPY ./Indotalent/ ./Indotalent/
COPY ./Indotalent.Application/ ./Indotalent.Application/
COPY ./Indotalent.Domain/ ./Indotalent.Domain/

# Restaurar dependencias
RUN dotnet restore Indotalent/Indotalent.csproj

# Build (sin publicar)
RUN dotnet build Indotalent/Indotalent.csproj -c Release -o /app/build

# ------------------------
# Etapa 2: Publicación
# ------------------------
FROM build AS publish
RUN dotnet publish Indotalent/Indotalent.csproj -c Release -o /app/publish

# ------------------------
# Etapa 3: Runtime final
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY ./Indotalent/Utils/Images ./Utils/Images

# Puerto expuesto (ajústalo si usas otro)
EXPOSE 5007

# Comando para iniciar la API
ENTRYPOINT ["dotnet", "Indotalent.dll"]
