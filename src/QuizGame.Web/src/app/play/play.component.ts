import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ErrorComponent } from '../error/error.component';
import { QuizGameService } from '../shared/quiz-game.service';
import { Quiz } from '../shared/quiz.interface';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-play',
  standalone: true,
  imports: [
    CommonModule,
    ErrorComponent,
    MatButtonModule,
    MatCardModule,
    MatGridListModule,
    MatIconModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    RouterLink,
  ],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css',
})
export class PlayComponent implements AfterViewInit {
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage: string = '';
  quizzes: Quiz[] = [];
  pagedQuizzes: Quiz[] = [];
  pageIndex: number = 0;
  pageSize: number = 4;

  private readonly quizGameService = inject(QuizGameService);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  ngAfterViewInit(): void {
    this.loadQuizzes();
    this.paginator.page.subscribe({
      next: () => {
        this.pagedQuizzes = this.getPagedQuizzes();
      }  
    });  
  }  
  
    get quizCount(): number {
      return this.quizzes.length;
    }
  
  getPagedQuizzes(): Quiz[] {
    const startIndex: number = (this.pageSize * this.pageIndex); 
    const endIndex: number = startIndex + this.pageSize; 
    return this.quizzes.slice(startIndex, endIndex);
  }    
  
  loadQuizzes(): void {
    this.isLoading = true;
    this.quizGameService.getQuizzes().subscribe({
      next: (quizzes) => {
        this.isLoading = false;
        this.quizzes = quizzes;
        this.pagedQuizzes = this.getPagedQuizzes();
      },
      error: (error) => {
        this.isLoading = false;
        this.isError = true;
        this.errorMessage = error;
      },
    });
  }
  
  handlePageEvent(e: PageEvent) {
    this.pageIndex = e.pageIndex;
  }
}
