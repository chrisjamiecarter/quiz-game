import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  BehaviorSubject,
  catchError,
  Observable,
  retry,
  tap,
  throwError,
} from 'rxjs';
import { Answer } from './answer.interface';
import { AnswerCreate } from './answer-create.interface';
import { AnswerUpdate } from './answer-update.interface';
import { Games } from './games.interface';
import { Question } from './question.interface';
import { QuestionCreate } from './question-create.interface';
import { QuestionUpdate } from './question-update.interface';
import { Quiz } from './quiz.interface';
import { QuizCreate } from './quiz-create.interface';
import { QuizUpdate } from './quiz-update.interface';

@Injectable({
  providedIn: 'root',
})
export class QuizGameService {
  private baseUrl = 'https://localhost:7083/api/v1/quizgame';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  private _isStale = new BehaviorSubject<boolean>(false);
  public IsStale = this._isStale.asObservable();

  http = inject(HttpClient);

  addAnswer(request: AnswerCreate): Observable<Answer> {
    let url = `${this.baseUrl}/answers`;

    return this.http
      .post<Answer>(url, request, this.httpOptions)
      .pipe(retry(3), catchError(this.handleError));
  }

  addQuestion(request: QuestionCreate): Observable<Question> {
    let url = `${this.baseUrl}/questions`;

    return this.http
      .post<Question>(url, request, this.httpOptions)
      .pipe(retry(3), catchError(this.handleError));
  }

  addQuiz(request: QuizCreate): Observable<Quiz> {
    let url = `${this.baseUrl}/quizzes`;

    return this.http
    .post<Quiz>(url, request, this.httpOptions)
    .pipe(
      retry(3),
      catchError(this.handleError),
      tap(() => this._isStale.next(true))
    );
  }

  deleteQuestion(id: string): Observable<object> {
    let url = `${this.baseUrl}/questions/${id}`;

    return this.http
    .delete<object>(url)
    .pipe(
      retry(3),
      catchError(this.handleError),
    );
  }

  deleteQuiz(id: string): Observable<object> {
    let url = `${this.baseUrl}/quizzes/${id}`;

    return this.http
    .delete<object>(url)
    .pipe(
      retry(3),
      catchError(this.handleError),
      tap(() => this._isStale.next(true))
    );
  }

  editQuiz(quiz: Quiz): Observable<Quiz> {
    let url = `${this.baseUrl}/quizzes/${quiz.id}`;

    const request: QuizUpdate = {
      name: quiz.name,
      description: quiz.description,
    };

    return this.http
      .put<Quiz>(url, request, this.httpOptions)
      .pipe(
        retry(3),
        catchError(this.handleError),
        tap(() => this._isStale.next(true))
      );
  }

  getAnswersByQuestionId(questionId: string): Observable<Answer[]> {
    return this.http
      .get<Answer[]>(`${this.baseUrl}/answers/question/${questionId}`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getGames(index: number, size: number, sort: string): Observable<Games> {
    let url = `${this.baseUrl}/games/page?`;

    if (sort != '') {
      url += `sort=${sort}&`;
    }

    url += `index=${index}&size=${size}`;
    
    return this.http.get<Games>(url);
  }

  getQuestionsByQuizId(quizId: string): Observable<Question[]> {
    return this.http
      .get<Question[]>(`${this.baseUrl}/questions/quiz/${quizId}`)
      .pipe(retry(3), catchError(this.handleError));
  }
  
  getQuiz(id: string): Observable<Quiz> {
    return this.http
      .get<Quiz>(`${this.baseUrl}/quizzes/${id}`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getQuizzes(): Observable<Quiz[]> {
    return this.http
      .get<Quiz[]>(`${this.baseUrl}/quizzes`)
      .pipe(retry(3), catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = error.message;
    }
    return throwError(() => new Error(errorMessage));
  }
}
