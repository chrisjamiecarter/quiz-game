import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ErrorComponent } from '../error/error.component';
import { QuizGameService } from '../shared/quiz-game.service';
import { Quiz } from '../shared/quiz.interface';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-play',
  standalone: true,
  imports: [CommonModule, ErrorComponent, MatButtonModule, MatCardModule, MatGridListModule, MatIconModule, MatProgressSpinnerModule, RouterLink],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css',
})
export class PlayComponent implements AfterViewInit  {
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  quizzes: Quiz[] = [];

  quizGameService = inject(QuizGameService);

  ngAfterViewInit(): void {
    this.getQuizzes();
  }

  getQuizzes(): void {
    this.isLoading = true;
    this.quizGameService.getQuizzes().subscribe({
      next: (quizzes) => {
        this.isLoading = false;
        this.quizzes = quizzes;
      },
      error: (error) => {
        this.isLoading = false;
        this.isError = true;
        this.errorMessage = error;
      },
    });
  }
}
