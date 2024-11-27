import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
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
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
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
  quizzes: Quiz[] = [];
  tableColumns: string[] = ['avatar', 'name', 'description', 'actions'];
  tableDataSource: MatTableDataSource<Quiz> = new MatTableDataSource<Quiz>([]);
  tableFilterText: string = '';

  private readonly matDialog = inject(MatDialog);
  private readonly quizGameService = inject(QuizGameService);
  private readonly snackBar = inject(MatSnackBar);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.quizGameService.IsStale.subscribe({
      next: (isStale) => {
        if (isStale) {
          this.getQuizzes();
        }
      },
    });

    this.getQuizzes();
  }

  getQuizzes(): void {
    this.isLoading = true;
    this.quizGameService.getQuizzes().subscribe({
      next: (quizzes) => {
        this.isLoading = false;
        this.quizzes = quizzes;
        this.applyFilter();
      },
      error: (error) => {
        this.isLoading = false;
        this.isError = true;
        this.errorMessage = error;
      },
    });
  }

  applyFilter(): void {
    const filteredQuizzes = this.quizzes.filter((quiz) => {
      const search = this.tableFilterText.toLowerCase();
      const name = quiz.name.toLowerCase();
      const description = quiz.description.toLowerCase();

      return name.indexOf(search) >= 0 || description.indexOf(search) >= 0;
    });

    this.tableDataSource = new MatTableDataSource<Quiz>(filteredQuizzes);
    this.tableDataSource.paginator = this.paginator;
    this.tableDataSource.sort = this.sort;
    if (this.tableDataSource.paginator) {
      this.tableDataSource.paginator.firstPage();
    }
  }

  private openSnackBar(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
    });
  }

  onCreateQuiz() {
    const dialogRef = this.matDialog.open(QuizUpsertDialogComponent, {
      width: '40rem',
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.openSnackBar(result);
    });
  }

  onDeleteQuiz(quiz: Quiz) {
    const dialogRef = this.matDialog.open(QuizDeleteDialogComponent, {
      width: '40rem',
      data: quiz,
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.openSnackBar(result);
    });
  }

  onUpdateQuiz(quiz: Quiz) {
    const dialogRef = this.matDialog.open(QuizUpsertDialogComponent, {
      width: '40rem',
      data: quiz,
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.openSnackBar(result);
    });
  }
}
