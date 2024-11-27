import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTooltipModule } from '@angular/material/tooltip';
import { throwError } from 'rxjs';
import { ErrorComponent } from '../../error/error.component';
import { QuizGameService } from '../../shared/quiz-game.service';
import { Answer } from '../../shared/answer.interface';
import { Game } from '../../shared/game.interface';
import { Question } from '../../shared/question.interface';
import { Quiz } from '../../shared/quiz.interface';
import { GameCreate } from '../../shared/game-create.interface';
import { UserAnswer } from '../../shared/user-answer.interface';

@Component({
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false, showError: true },
    },
  ],
  selector: 'app-play-quiz',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinner,
    MatRadioModule,
    MatStepperModule,
    MatTooltipModule,
  ],
  templateUrl: './play-quiz.component.html',
  styleUrl: './play-quiz.component.css',
})
export class PlayQuizComponent implements OnInit {
  isInProgress: boolean = false;
  isLoading: boolean = false;
  isError: boolean = false;
  errorMessage: string = '';
  quiz: Quiz = {} as Quiz;
  questions: Question[] = [];
  answers: { [key: string]: Answer[] } = {};
  userAnswers: UserAnswer[] = [];

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly quizGameService = inject(QuizGameService);

  ngOnInit(): void {
    this.isLoading = true;
    const quizId = this.route.snapshot.paramMap.get('id')!;
    this.loadQuiz(quizId);
  }

  private loadQuiz(quizId: string) {
    this.quizGameService.getQuiz(quizId).subscribe({
      next: (quiz: Quiz) => {
        this.quiz = quiz;
        this.loadQuestions(quizId);
      },
      error: (error) => {
        this.isLoading = false;
        this.isError = true;
        this.errorMessage = error;
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }

  private loadQuestions(quizId: string) {
    this.quizGameService.getQuizQuestions(quizId).subscribe({
      next: (questions: Question[]) => {
        this.questions = questions;
        questions.forEach((question: Question) =>
          this.loadAnswers(question.id)
        );
      },
      error: (error) => {
        throwError(() => new Error(error));
      },
    });
  }

  private loadAnswers(questionId: string) {
    this.quizGameService.getQuestionAnswers(questionId).subscribe({
      next: (answers: Answer[]) => {
        this.answers[questionId] = answers;
      },
      error: (error) => {
        throwError(() => new Error(error));
      },
    });
  }

  private calculateScore(): number {
    let score = 0;
    this.userAnswers.forEach((ua: UserAnswer) => {
      const correctAnswer = this.answers[ua.questionId].find(
        (a) => a.isCorrect
      );
      if (correctAnswer && correctAnswer.id === ua.answerId) {
        score++;
      }
    });
    return score;
  }

  isQuestionAnswered(index: number): boolean {
    return !!this.userAnswers[index]?.answerId;
  }

  onAnswerSelect(questionId: string, answerId: string): void {
    const existingAnswer = this.userAnswers.find(
      (userAnswer: UserAnswer) => userAnswer.questionId === questionId
    );
    if (existingAnswer) {
      existingAnswer.answerId = answerId;
    } else {
      const userAnswer: UserAnswer = {
        questionId: questionId,
        answerId: answerId,
      };
      this.userAnswers.push(userAnswer);
    }
  }

  onSubmit(): void {
    this.isInProgress = true;
    const score = this.calculateScore();

    const request: GameCreate = {
      quizId: this.quiz.id,
      played: new Date(),
      score: score,
      maxScore: this.questions.length,
    };

    this.quizGameService.addGame(request).subscribe({
      next: (game: Game) => {
        this.isInProgress = false;
        this.router.navigate(['games/score', game.id]);
      },
      error: (error) => {
        this.isInProgress = false;
        (this.isError = true), (this.errorMessage = error);
      },
    });
  }
}
