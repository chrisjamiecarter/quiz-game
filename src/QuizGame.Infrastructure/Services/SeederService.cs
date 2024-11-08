using Bogus;
using Microsoft.EntityFrameworkCore;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Contexts;

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

        SeedBreakingBadQuiz();
        SeedGameOfThronesQuiz();
        SeedHarryPotterQuiz();
        SeedLordOfTheRingsQuiz();

        // Requires quizzes.
        SeedGames();
    }

    private record AnswerRecord(string Text, bool IsCorrect = false);

    private void AddQuestion(Guid quizId, string text, IEnumerable<AnswerRecord> answers)
    {
        var question = new Question
        {
            Id = Guid.NewGuid(),
            Text = text,
            QuizId = quizId,
        };
        _context.Question.Add(question);

        foreach (var answer in answers)
        {
            _context.Answer.Add(new Answer
            {
                Id = Guid.NewGuid(),
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                QuestionId = question.Id,
            });
        }
    }

    private void SeedBreakingBadQuiz()
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "The Breaking Bad Challenge",
            Description = "Test your knowledge of Walter White’s transformation and the dark world of Breaking Bad!",
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is Walter White's street name in the drug trade?",
            [
                new AnswerRecord("Heisenberg", true),
                new AnswerRecord("Scarface"),
                new AnswerRecord("Jesse"),
                new AnswerRecord("Gus")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is Walter White's former student and business partner?",
            [
                new AnswerRecord("Hank Schrader"),
                new AnswerRecord("Skyler White"),
                new AnswerRecord("Jesse Pinkman", true),
                new AnswerRecord("Mike Ehrmantraut")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What type of business does Gustavo 'Gus' Fring use as a front for his drug empire?",
            [
                new AnswerRecord("A car wash"),
                new AnswerRecord("A chicken restaurant", true),
                new AnswerRecord("A laundromat"),
                new AnswerRecord("A bar")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which chemical element does Walter White choose for his methamphetamine logo?",
            [
                new AnswerRecord("Li (Lithium)"),
                new AnswerRecord("Br (Bromine)", true),
                new AnswerRecord("Hg (Mercury)"),
                new AnswerRecord("Na (Sodium)")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of the lawyer often called 'Better Call Saul'?",
            [
                new AnswerRecord("Howard Hamlin"),
                new AnswerRecord("Saul Goodman", true),
                new AnswerRecord("Chuck McGill"),
                new AnswerRecord("Kim Wexler")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the street drug that Walter White produces?",
            [
                new AnswerRecord("Heroin"),
                new AnswerRecord("Methamphetamine", true),
                new AnswerRecord("Cocaine"),
                new AnswerRecord("Ecstasy")
            ]
        );

        _context.SaveChanges();
    }

    private void SeedGameOfThronesQuiz()
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "The Iron Throne Challenge",
            Description = "Test your knowledge of the Seven Kingdoms with our thrilling Game of Thrones trivia quiz!",
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is the motto of House Stark?",
            [
                new AnswerRecord("Winter is Coming", true ),
                new AnswerRecord("Fire and Blood"),
                new AnswerRecord("We Do Not Sow"),
                new AnswerRecord("Hear Me Roar")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is known as the 'Mother of Dragon'?",
            [
                new AnswerRecord("Cersei Lannister"),
                new AnswerRecord("Melisandre"),
                new AnswerRecord("Daenerys Targaryen", true),
                new AnswerRecord("Ygritte")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of Arya Stark's sword?",
            [
                new AnswerRecord("Oathkeeper"),
                new AnswerRecord("Needle", true),
                new AnswerRecord("Longclaw"),
                new AnswerRecord("Ice")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who was responsible for the creation of the Night King?",
            [
                new AnswerRecord("The White Walkers"),
                new AnswerRecord("The First Men"),
                new AnswerRecord("The Children of the Forest", true),
                new AnswerRecord("The Three-Eyed Raven")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which character famously declares, 'I drink and I know things'?",
            [
                new AnswerRecord("Jon Snow"),
                new AnswerRecord("Jaime Lannister"),
                new AnswerRecord("Tyrion Lannister", true),
                new AnswerRecord("Bronn")
            ]
        );

        _context.SaveChanges();
    }

    private void SeedGames()
    {
        Randomizer.Seed = new Random(19890309);

        var quizzes = _context.Quiz.Include(q => q.Questions).ToList();

        var fakeData = new Faker<Game>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Played, f => f.Date.Recent(30))
            .RuleFor(m => m.Quiz, f => f.PickRandom(quizzes))
            .RuleFor(m => m.QuizId, (f, m) => m.Quiz!.Id)
            .RuleFor(m => m.Score, (f, m) => f.Random.Int(0, m.Quiz!.Questions.Count));

        foreach (var fake in fakeData.Generate(100))
        {
            _context.Game.Add(fake);
        }

        _context.SaveChanges();
    }

    private void SeedHarryPotterQuiz()
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "The Wizarding World Challenge",
            Description = "Step into the magical world of Harry Potter and test your knowledge with our enchanting trivia quiz!",
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is the name of Harry Potter's owl?",
            [
                new AnswerRecord("Crookshanks"),
                new AnswerRecord("Hedwig", true),
                new AnswerRecord("Errol"),
                new AnswerRecord("Scabbers")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which spell is used to disarm an opponent?",
            [
                new AnswerRecord("Expelliarmus", true),
                new AnswerRecord("Lumos"),
                new AnswerRecord("Accio"),
                new AnswerRecord("Avada Kedavra")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is the Half-Blood Prince?",
            [
                new AnswerRecord("Albus Dumbledore"),
                new AnswerRecord("Severus Snape", true),
                new AnswerRecord("Sirius Black"),
                new AnswerRecord("Remus Lupin")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of the magical map that shows everyone’s location in Hogwarts?",
            [
                new AnswerRecord("The Marauder's Map", true),
                new AnswerRecord("The Daily Prophet"),
                new AnswerRecord("The Elder Wand"),
                new AnswerRecord("The Invisibility Cloak")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What creature is Aragog?",
            [
                new AnswerRecord("A Basilisk"),
                new AnswerRecord("A Hippogriff"),
                new AnswerRecord("A Werewolf"),
                new AnswerRecord("A Giant Spider", true)
            ]
        );

        _context.SaveChanges();
    }

    private void SeedLordOfTheRingsQuiz()
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Name = "The One Ring Quest",
            Description = "Dive into the epic world of Middle-earth with our Lord of the Rings trivia quiz!",
        };
        _context.Quiz.Add(quiz);

        AddQuestion(
            quiz.Id,
            "What is the Elvish word for 'friend' that opens the doors of Moria?",
            [
                new AnswerRecord("Mellon", true),
                new AnswerRecord("Aiya"),
                new AnswerRecord("Elendil"),
                new AnswerRecord("Galad")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who is the steward of Gondor during the War of the Ring?",
            [
                new AnswerRecord("Theoden"),
                new AnswerRecord("Denethor", true),
                new AnswerRecord("Faramir"),
                new AnswerRecord("Boromir")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of Frodo’s sword?",
            [
                new AnswerRecord("Orcrist"),
                new AnswerRecord("Glamdring"),
                new AnswerRecord("Sting", true),
                new AnswerRecord("Anduril")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Which creature did Gollum refer to as ‘my precious’?",
            [
                new AnswerRecord("The Arkenstone"),
                new AnswerRecord("The One Ring", true),
                new AnswerRecord("The Palantír"),
                new AnswerRecord("The Silmaril")
            ]
        );

        AddQuestion(
            quiz.Id,
            "Who kills the Witch-king of Angmar?",
            [
                new AnswerRecord("Aragorn"),
                new AnswerRecord("Legolas"),
                new AnswerRecord("Éowyn", true),
                new AnswerRecord("Gandalf")
            ]
        );

        AddQuestion(
            quiz.Id,
            "What is the name of the inn where Frodo first meets Aragorn?",
            [
                new AnswerRecord("The Green Dragon"),
                new AnswerRecord("The Golden Perch"),
                new AnswerRecord("The Prancing Pony", true),
                new AnswerRecord("The Ivy Bush")
            ]
        );

        _context.SaveChanges();
    }
}
