import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Quiz } from '../shared/quiz.interface';
import { QuizGameService } from '../shared/quiz-game.service';

@Component({
  selector: 'app-quizzes',
  standalone: true,
  imports: [MatPaginatorModule, MatSortModule, MatTableModule],
  templateUrl: './quizzes.component.html',
  styleUrl: './quizzes.component.css',
})
export class QuizzesComponent implements AfterViewInit {
  tableColumns: string[] = ['name', 'description'];
  tableDataSource: MatTableDataSource<Quiz> = new MatTableDataSource<Quiz>([]);
  
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
}
