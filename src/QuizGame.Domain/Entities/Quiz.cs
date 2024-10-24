﻿namespace QuizGame.Domain.Entities;

/// <summary>
/// Represents a Quiz entity within the Domain layer.
/// </summary>
public class Quiz
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = "";

    public required ICollection<Game> Games { get; set; } = [];

    public required ICollection<Question> Questions { get; set; } = [];
}
