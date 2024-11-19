import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { catchError, map, merge, of, startWith, switchMap } from 'rxjs';
import { Game } from '../shared/game.interface';
import { QuizGameService } from '../shared/quiz-game.service';
import { ErrorComponent } from '../error/error.component';

@Component({
  selector: 'app-games',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatTableModule,
  ],
  templateUrl: './games.component.html',
  styleUrl: './games.component.css',
})
export class GamesComponent implements AfterViewInit {
  games: Game[] = [];
  totalRecords: number = 0;
  tableColumns: string[] = ['played', 'quiz', 'score'];
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  pageIndex: number = 0;
  pageSize: number = 10;
  sortBy: string = '';

  quizGameService = inject(QuizGameService);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.quizGameService
            .getGames(this.pageIndex, this.pageSize, this.sortBy)
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
  }

  getScorePercentage(score: number, maxScore: number): number {
    return Math.floor((100 * score) / maxScore);
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
}
