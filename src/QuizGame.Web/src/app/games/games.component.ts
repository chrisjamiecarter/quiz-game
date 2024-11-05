import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { QuizGameService } from '../shared/quiz-game.service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Game } from '../shared/game.interface';
import { catchError, map, merge, of, startWith, switchMap } from 'rxjs';
import { PaginatedGames } from '../shared/paginated-game.interface';
import { ErrorComponent } from '../error/error.component';

@Component({
  selector: 'app-games',
  standalone: true,
  imports: [CommonModule, ErrorComponent, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule],
  templateUrl: './games.component.html',
  styleUrl: './games.component.css'
})
export class GamesComponent implements AfterViewInit {
  games: Game[] = [];
  totalRecords: number = 0;
  tableColumns: string[] = ['id', 'quizid', 'played', 'score']
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  
  quizGameService = inject(QuizGameService);
  
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  //@ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.loadGames()

    //this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
    
    //merge(this.sort.sortChange, this.paginator.page)
    //merge(this.paginator.page)
    //.pipe(
    //   startWith({}),
    //   switchMap(() => {
    //     this.isLoading = true;
    //     return this.quizGameService.getGames().pipe(catchError(() => of(null)));
    //   }),
    //   map(data => {
    //     this.isLoading = false;
    //     if (data === null) {
    //       return [];
    //     }

    //     this.totalRecords = data.totalRecords;
    //     return data.games;
    //   }),
    // )
    // .subscribe(data => (this.games = data));
  }

  private loadGames(): void {
    this.quizGameService.getGames().subscribe((response: PaginatedGames) => {
      this.totalRecords = response.totalRecords;
      this.games = response.games;
    }, error => {
      console.log("error", error);
      this.isLoading = false;
      this.isError = true;
      this.errorMessage = error.message;
    })
  }
}
