import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Quiz } from '../shared/quiz.interface';
import { QuizGameService } from '../shared/quiz-game.service';
import { QuizUpsertDialogComponent } from './quiz-upsert-dialog/quiz-upsert-dialog.component';
import { QuizDeleteDialogComponent } from './quiz-delete-dialog/quiz-delete-dialog.component';

@Component({
  selector: 'app-quizzes',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatPaginatorModule, MatSortModule, MatTableModule],
  templateUrl: './quizzes.component.html',
  styleUrl: './quizzes.component.css',
})
export class QuizzesComponent implements AfterViewInit {
  tableColumns: string[] = ['name', 'description', 'actions'];
  tableDataSource: MatTableDataSource<Quiz> = new MatTableDataSource<Quiz>([]);
  
  matDialog = inject(MatDialog);
  quizGameService = inject(QuizGameService);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.quizGameService.getQuizzes().subscribe((records) => {
      this.tableDataSource = new MatTableDataSource<Quiz>(records);
      this.tableDataSource.paginator = this.paginator;
      this.tableDataSource.sort = this.sort;
    });
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
    });
  }
}
