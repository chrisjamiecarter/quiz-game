using Bogus;
using Microsoft.EntityFrameworkCore;
using QuizGame.Infrastructure.Contexts;
using QuizGame.Infrastructure.Models;

namespace QuizGame.Infrastructure.Services;

/// <summary>
/// Provides methods to seed the database with initial data.
/// This service adds a defined set of default Quizzes and a set of fake Games using Bogus.
/// </summary>
internal class SeederService : ISeederService
{
    private readonly QuizGameDataContext _context;

    public SeederService(QuizGameDataContext context)
    {
        _context = context;
    }

    public void SeedDatabase()
    {
        if (_context.Quiz.Any() && _context.Game.Any())
        {
            return;
        }

        SeedGameOfThronesQuiz();
        SeedHarryPotterQuiz();

        // Requires quizzes.
        SeedGames();
    }

    private record Answer(string Text, bool IsCorrect = false);

    private void AddQuestion(Guid quizId, string text, IEnumerable<Answer> answers)
    {
        var question = new QuestionModel
        {
            Id = Guid.NewGuid(),
            Text = text,
            QuizId = quizId,
        };
        _context.Question.Add(question);

        foreach (var answer in answers)
        {
            _context.Answer.Add(new AnswerModel
            {
                Id = Guid.NewGuid(),
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                QuestionId = question.Id,
            });
        }
    }

    private void SeedGameOfThronesQuiz()
    {
        var quiz = new QuizModel
        {
            Id = Guid.NewGuid(),
            Name = "The Iron Throne Challenge",
            Description = "Test your knowledge of the Seven Kingdoms with our thrilling Game of Thrones trivia quiz!",
            Games = [],
            Questions = [],
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is the motto of House Stark?",
            [
                new Answer("Winter is Coming", true ),
                new Answer("Fire and Blood"),
                new Answer("We Do Not Sow"),
                new Answer("Hear Me Roar")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is known as the 'Mother of Dragon'?",
            [
                new Answer("Cersei Lannister"),
                new Answer("Melisandre"),
                new Answer("Daenerys Targaryen", true),
                new Answer("Ygritte")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of Arya Stark's sword?",
            [
                new Answer("Oathkeeper"),
                new Answer("Needle", true),
                new Answer("Longclaw"),
                new Answer("Ice")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who was responsible for the creation of the Night King?",
            [
                new Answer("The White Walkers"),
                new Answer("The First Men"),
                new Answer("The Children of the Forest", true),
                new Answer("The Three-Eyed Raven")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which character famously declares, 'I drink and I know things'?",
            [
                new Answer("Jon Snow"),
                new Answer("Jaime Lannister"),
                new Answer("Tyrion Lannister", true),
                new Answer("Bronn")
            ]
        );

        _context.SaveChanges();
    }

    private void SeedGames()
    {
        Randomizer.Seed = new Random(19890309);

        var quizzes = _context.Quiz.Include(q => q.Questions).ToList();

        var fakeData = new Faker<GameModel>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Played, f => f.Date.Recent(7))
            .RuleFor(m => m.Quiz, f => f.PickRandom(quizzes))
            .RuleFor(m => m.QuizId, (f, m) => m.Quiz!.Id)
            .RuleFor(m => m.Score, (f, m) => f.Random.Int(0, m.Quiz!.Questions.Count));

        foreach (var fake in fakeData.Generate(20))
        {
            _context.Game.Add(fake);
        }

        _context.SaveChanges();
    }

    private void SeedHarryPotterQuiz()
    {
        var quiz = new QuizModel
        {
            Id = Guid.NewGuid(),
            Name = "The Wizarding World Challenge",
            Description = "Step into the magical world of Harry Potter and test your knowledge with our enchanting trivia quiz!",
            Games = [],
            Questions = [],
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is the name of Harry Potter's owl?",
            [
                new Answer("Crookshanks"),
                new Answer("Hedwig", true),
                new Answer("Errol"),
                new Answer("Scabbers")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which spell is used to disarm an opponent?",
            [
                new Answer("Expelliarmus", true),
                new Answer("Lumos"),
                new Answer("Accio"),
                new Answer("Avada Kedavra")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is the Half-Blood Prince?",
            [
                new Answer("Albus Dumbledore"),
                new Answer("Severus Snape", true),
                new Answer("Sirius Black"),
                new Answer("Remus Lupin")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of the magical map that shows everyone’s location in Hogwarts?",
            [
                new Answer("The Marauder's Map", true),
                new Answer("The Daily Prophet"),
                new Answer("The Elder Wand"),
                new Answer("The Invisibility Cloak")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What creature is Aragog?",
            [
                new Answer("A Basilisk"),
                new Answer("A Hippogriff"),
                new Answer("A Werewolf"),
                new Answer("A Giant Spider", true)
            ]
        );

        _context.SaveChanges();
    }
}
