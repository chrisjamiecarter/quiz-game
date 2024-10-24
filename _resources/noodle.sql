SELECT
	 qz.[Name]
	,qz.[Description]
	,qn.[Text] AS [Question]
	,ar.[Text] AS [Answer]
	,ar.[IsCorrect]
FROM
	[dbo].[Quiz] AS qz JOIN
	[dbo].[Question] AS qn ON qz.[Id] = qn.[QuizId] JOIN
	[dbo].[Answer] AS ar ON qn.[Id] = ar.[QuestionId]
ORDER BY
	 qz.[Name]
	,qn.[Text]
	,ar.[Text]
;

SELECT
	 q.[Name] AS [Quiz]
	,g.[Played]
	,g.[Score]
FROM
	[dbo].[Quiz] AS q JOIN
	[dbo].[Game] AS g ON q.[Id] = g.[QuizId]
ORDER BY
	[Played]
;
