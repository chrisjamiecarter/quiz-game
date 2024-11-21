import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import confetti from 'canvas-confetti';
import { ErrorComponent } from '../../error/error.component';
import { QuizGameService } from '../../shared/quiz-game.service';
import { Game } from '../../shared/game.interface';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-games-score',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './games-score.component.html',
  styleUrl: './games-score.component.css',
})
export class GamesScoreComponent implements OnInit {
  isLoading: boolean = false;
  isError: boolean = false;
  errorMessage: string = '';
  game: Game = {} as Game;

  private readonly quizGameService = inject(QuizGameService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  ngOnInit(): void {
    const gameId = this.route.snapshot.paramMap.get('id')!;
    this.loadGame(gameId);
  }

  private launchConfetti(): void {
    const duration: number = 5 * 1000;
    const end: number = Date.now() + duration;

    (function frame() {
      confetti({
        particleCount: 7,
        angle: Math.random() * 60 + 150,
        spread: 55,
        origin: { x: Math.random(), y: Math.random() - 0.2 },
      });

      if (Date.now() < end) {
        requestAnimationFrame(frame);
      }
    })();
  }

  private loadGame(gameId: string) {
    this.quizGameService.getGame(gameId).subscribe({
      next: (game: Game) => {
        this.game = game;
        this.launchConfetti();
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

  get gameMessageHeader(): string {
    const percentage = this.scorePercentage();
    switch (true) {
      case percentage >= 100:
        return 'Outstanding!';
      case percentage >= 75:
        return 'Congratulations!';
      case percentage >= 50:
        return 'Well done!';
      case percentage >= 25:
        return 'Good effort!';
      default:
        return 'Better luck next time!';
    }
  }

  get gameMessageText(): string {
    const percentage = this.scorePercentage();
    switch (true) {
      case percentage >= 100:
        return 'You’ve achieved the maximum score possible for this quiz. Why not challenge yourself with a different one?';
      case percentage >= 75:
        return 'You were so close to perfection. Try the quiz again or explore a new challenge!';
      default:
        return 'Don’t give up! Keep practicing, and you’ll see your score improve in no time.';
    }
  }

  get formattedDate(): string {
    const date = new Date(this.game.played);
    return date.toLocaleDateString();
  }

  get formattedScore(): string {
    const percentage = this.scorePercentage();
    return `${this.game.score} out of ${this.game.maxScore} - ${percentage}%`;
  }

  private scorePercentage(): number {
    return Math.floor((100 * this.game.score) / this.game.maxScore);
  }

  onHomeClicked(): void {
    this.router.navigate(['']);
  }

  onPlayAgainClicked(): void {
    this.router.navigate(['play/quiz/', this.game.quizId]);
  }
}
