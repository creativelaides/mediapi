# MedApi 🏥

![Development Status](https://img.shields.io/badge/status-development-orange)
![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
[![License: MPL 2.0](https://img.shields.io/badge/License-MPL_2.0-brightgreen.svg)](https://opensource.org/licenses/MPL-2.0)

![Docker](https://img.shields.io/badge/Docker-✔-success)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)

> Sistema de Gestión de Citas Médicas (En desarrollo activo 🔧)

## 📋 Descripción
API REST para gestión de citas médicas construida con arquitectura limpia y tecnologías modernas:
.NET 8

- Entity Framework Core
- SQL Server
- JWT Authentication
- Docker
- MediatR
- CQRS

## 🚀 Características Principales

- Autenticación JWT segura
- Reserva inteligente de citas
- Migraciones automatizadas con EF Core
- Entornos Dockerizados
- Seed de datos iniciales con Bogus
- Documentación Swagger integrada

## 📦 Instalación

- Requisitos Previos
- .NET 8 SDK
- Docker Desktop
- SQL Server 2022

### Pasos Iniciales

```BASH
# Clonar repositorio
git clone https://github.com/tu-usuario/medapi.git

# Configurar variables de entorno
cp appsettings.Development.example.json appsettings.Development.json

# Ejecutar migraciones
dotnet ef database update --project src/MedApi.Infrastructure/

# Iniciar Servicios
docker-compose up -d --build
```

## 🛠 Uso
Autenticación
```http
POST /api/auth/login
Content-Type: application/json

{
  "identifier": "1234567890",
  "dateOfBirth": "1985-05-15"
}
```
### Endpoints Principales
Método	Endpoint	Descripción
GET	/api/appointments/available	Listar citas disponibles
POST	/api/appointments/reserve	Reservar cita médica
DELETE	/api/appointments/{id}	Cancelar cita
Swagger Documentation

## 🧩 Estructura del Proyecto
```bash
src/
├── MedApi.API/           # Capa de presentación
├── MedApi.Application/   # Lógica de negocio
├── MedApi.Domain/        # Entidades del dominio
└── MedApi.Infrastructure/# Implementación de infraestructura
```
## 🤝 Contribución

``` bash
# 1. Hacer fork del proyecto
# 2. Crear rama feature/nueva-funcionalidad
# 3. Enviar Pull Request
```

## 📄 Licencia
Distribuido bajo licencia MPL 2.0. Ver LICENSE para más detalles.

__Nota:__ Proyecto en fase de desarrollo activo - Reportar issues en seguimiento de problemas


[![Open in GitHub](https://img.shields.io/badge/View%20on-GitHub-black?logo=github)](https://github.com/tu-usuario/medapi)