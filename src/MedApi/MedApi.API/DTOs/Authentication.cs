namespace MedApi.API.DTOs;

public record LoginRequest(string Identifier, DateOnly DateOfBirth);
public record LoginResponse(string Token, string FullName, string Identifier);