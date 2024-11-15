import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ErrorComponent } from '../error/error.component';
import { Quiz } from '../shared/quiz.interface';
import { QuizGameService } from '../shared/quiz-game.service';
import { QuizUpsertDialogComponent } from './quiz-upsert-dialog/quiz-upsert-dialog.component';
import { QuizDeleteDialogComponent } from './quiz-delete-dialog/quiz-delete-dialog.component';

@Component({
  selector: 'app-quizzes',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatTableModule,
  ],
  templateUrl: './quizzes.component.html',
  styleUrl: './quizzes.component.css',
})
export class QuizzesComponent implements AfterViewInit {
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  tableColumns: string[] = ['name', 'description', 'actions'];
  tableDataSource: MatTableDataSource<Quiz> = new MatTableDataSource<Quiz>([]);

  matDialog = inject(MatDialog);
  quizGameService = inject(QuizGameService);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.quizGameService.Quizzes.subscribe({
      next: (quizzes) => {
        this.tableDataSource = new MatTableDataSource<Quiz>(quizzes);
        this.tableDataSource.paginator = this.paginator;
        this.tableDataSource.sort = this.sort;
      },
    });

    this.quizGameService.ApiError.subscribe({
      next: (errorMessage) => {
        this.isError = errorMessage !== '';
        this.errorMessage = errorMessage;
      },
    });

    this.quizGameService.IsLoading.subscribe({
      next: (isLoading) => this.isLoading = isLoading,
    })

    this.quizGameService.getQuizzes();
  }

  onCreateQuiz() {
    this.matDialog.open(QuizUpsertDialogComponent, {
      width: '40rem',
    });
  }

  onDeleteQuiz(quiz: Quiz) {
    this.matDialog.open(QuizDeleteDialogComponent, {
      width: '40rem',
      data: quiz,
    });
  }

  onUpdateQuiz(quiz: Quiz) {
    this.matDialog.open(QuizUpsertDialogComponent, {
      width: '40rem',
      data: quiz,
    });
  }
}
