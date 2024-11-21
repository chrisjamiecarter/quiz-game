import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import {
  catchError,
  map,
  merge,
  of,
  startWith,
  Subject,
  switchMap,
} from 'rxjs';
import { Game } from '../shared/game.interface';
import { QuizGameService } from '../shared/quiz-game.service';
import { ErrorComponent } from '../error/error.component';
import { Quiz } from '../shared/quiz.interface';

@Component({
  selector: 'app-games',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    FormsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatSortModule,
    MatTableModule,
    ReactiveFormsModule,
  ],
  templateUrl: './games.component.html',
  styleUrl: './games.component.css',
})
export class GamesComponent implements AfterViewInit {
  games: Game[] = [];
  quizzes: Quiz[] = [];
  totalRecords: number = 0;
  tableColumns: string[] = ['played', 'quiz', 'score'];
  tableFilterFromDate: Date | null = null;
  tableFilterToDate: Date | null = null;
  tableFilterQuizId: string | null = null;
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  pageIndex: number = 0;
  pageSize: number = 10;
  sortBy: string = '';

  dateChanged: Subject<void> = new Subject<void>();

  quizGameService = inject(QuizGameService);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSelect) select!: MatSelect;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    merge(
      this.dateChanged.asObservable(),
      this.paginator.page,
      this.select.selectionChange,
      this.sort.sortChange
    )
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.quizGameService
            .getGames(
              this.tableFilterQuizId,
              this.tableFilterFromDate,
              this.tableFilterToDate,
              this.pageIndex,
              this.pageSize,
              this.sortBy
            )
            .pipe(
              catchError((error) => {
                this.isError = true;
                this.errorMessage = error.message;
                return of(null);
              })
            );
        }),
        map((data) => {
          this.isLoading = false;
          if (data === null) {
            return [];
          }

          this.totalRecords = data.totalRecords;
          return data.games;
        })
      )
      .subscribe((data) => (this.games = data));

    this.loadQuizzes();
  }

  getScorePercentage(score: number, maxScore: number): number {
    return Math.floor((100 * score) / maxScore);
  }

  loadQuizzes(): void {
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
      complete: () => {
        this.isLoading = false;
      },
    });
  }

  handlePageEvent(e: PageEvent) {
    this.totalRecords = e.length;
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
  }

  handleSortEvent(e: Sort) {
    if (!this.sort.active || this.sort.direction === '') {
      this.sortBy = '';
    } else {
      this.sortBy = `${e.active}-${e.direction}`;
    }
    this.pageIndex = 0;
  }

  onFilterDateChanged(): void {
    this.pageIndex = 0;
    this.dateChanged.next();
  }

  onFilterSelectionChanged(): void {
    this.pageIndex = 0;
  }

  onResetFilter(): void {
    this.pageIndex = 0;
    this.tableFilterFromDate = null;
    this.tableFilterToDate = null;
    this.tableFilterQuizId = null;
    this.dateChanged.next();
  }
}
