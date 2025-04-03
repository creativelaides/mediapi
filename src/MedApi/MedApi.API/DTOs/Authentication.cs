namespace MedApi.API.DTOs;

public record LoginRequest(string Identifier, DateTime DateOfBirth);
public record LoginResponse(string Token, string FullName, string Identifier);