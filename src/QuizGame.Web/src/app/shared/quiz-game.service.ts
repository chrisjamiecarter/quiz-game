import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, Subject } from 'rxjs';
import { Games } from './games.interface';
import { Quiz } from './quiz.interface';
import { QuizCreate } from './quiz-create.interface';
import { QuestionCreate } from './question-create.interface';
import { Question } from './question.interface';
import { AnswerCreate } from './answer-create.interface';
import { Answer } from './answer.interface';

@Injectable({
  providedIn: 'root'
})
export class QuizGameService {
  private baseUrl = 'https://localhost:7083/api/v1/quizgame';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  private _apiError = new BehaviorSubject<string>('');
  public ApiError = this._apiError.asObservable();

  private _isLoading = new BehaviorSubject<boolean>(true);
  public IsLoading = this._isLoading.asObservable();

  private _quizzes = new BehaviorSubject<Quiz[]>([]);
  public Quizzes = this._quizzes.asObservable();

  http = inject(HttpClient);
  
  addAnswer(request: AnswerCreate):Observable<Answer> {
    let url = `${this.baseUrl}/answers`;

    return this.http.post<Answer>(url, request, this.httpOptions);
  }

  addQuestion(request: QuestionCreate):Observable<Question> {
    let url = `${this.baseUrl}/questions`;

    return this.http.post<Question>(url, request, this.httpOptions);
  }

  addQuiz(request: QuizCreate):Observable<Quiz> {
    let url = `${this.baseUrl}/quizzes`;

    return this.http.post<Quiz>(url, request, this.httpOptions).pipe(
      map((quiz) => {
        this.getQuizzes();
        return quiz;
      })
    );
  }

  deleteQuiz(id: string): Observable<object> {
    let url = `${this.baseUrl}/quizzes/${id}`;

    return this.http.delete<object>(url);
  }

  getGames(index: number, size: number, sort: string): Observable<Games> {
    let url = `${this.baseUrl}/games/page?`;

    if(sort != '') {
      url += `sort=${sort}&`;
    }

    url += `index=${index}&size=${size}`;

    console.log("index", index);
    console.log("size", size);
    console.log("sort", sort);
    console.log("url", url);
    return this.http.get<Games>(url);
  }

  getQuizzes(): void {
    this._isLoading.next(true);
    this.http.get<Quiz[]>(`${this.baseUrl}/quizzes`).subscribe({
      next: (quizzes) => {
        this._quizzes.next(quizzes);
        this._isLoading.next(false);
      },
      error: (error) => {
        console.error('Error getting quizzes:', error);
        this._apiError.next(`Error getting quizzes: ${error.message}`);
        this._isLoading.next(false);
        },
    });
  }
}
