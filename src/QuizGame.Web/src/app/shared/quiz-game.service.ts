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
import { Games } from './games.interface';
import { Question } from './question.interface';
import { QuestionCreate } from './question-create.interface';
import { Quiz } from './quiz.interface';
import { QuizCreate } from './quiz-create.interface';
import { QuizUpdate } from './quiz-update.interface';
import { Game } from './game.interface';
import { GameCreate } from './game-create.interface';

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

  addGame(request: GameCreate): Observable<Game> {
    let url = `${this.baseUrl}/games`;

    return this.http
      .post<Game>(url, request, this.httpOptions)
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

  deleteQuizQuestions(id: string): Observable<object> {
    let url = `${this.baseUrl}/quizzes/${id}/questions`;

    return this.http
      .delete<object>(url)
      .pipe(
        retry(3),
        catchError(this.handleError),
      );
  }

  editQuiz(quiz: Quiz): Observable<Quiz> {
    let url = `${this.baseUrl}/quizzes/${quiz.id}`;

    const request: QuizUpdate = {
      name: quiz.name,
      description: quiz.description,
      imageUrl: quiz.imageUrl,
    };

    return this.http
      .put<Quiz>(url, request, this.httpOptions)
      .pipe(
        retry(3),
        catchError(this.handleError),
        tap(() => this._isStale.next(true))
      );
  }

  getGame(id: string): Observable<Game> {
    return this.http
      .get<Game>(`${this.baseUrl}/games/${id}`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getGames(quizId: string | null, from: Date | null, to: Date | null, index: number, size: number, sort: string): Observable<Games> {
    let url = `${this.baseUrl}/games/page?`;

    // Optional.
    if (quizId) {
      url += `quizId=${quizId}&`;
    }

    if (from) {
      url += `from=${from.toISOString()}&`;
    }

    if (to) {
      url += `to=${to.toISOString()}&`;
    }

    if (sort != '') {
      url += `sort=${sort}&`;
    }

    // Required (not enforced, just otherwise will default to 0 and 10).
    url += `index=${index}&size=${size}`;

    return this.http.get<Games>(url);
  }

  getQuiz(id: string): Observable<Quiz> {
    return this.http
      .get<Quiz>(`${this.baseUrl}/quizzes/${id}`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getQuizQuestions(quizId: string): Observable<Question[]> {
    return this.http
      .get<Question[]>(`${this.baseUrl}/quizzes/${quizId}/questions`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getQuizzes(): Observable<Quiz[]> {
    return this.http
      .get<Quiz[]>(`${this.baseUrl}/quizzes`)
      .pipe(retry(3), catchError(this.handleError));
  }

  getQuestionAnswers(questionId: string): Observable<Answer[]> {
    return this.http
      .get<Answer[]>(`${this.baseUrl}/questions/${questionId}/answers`)
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
