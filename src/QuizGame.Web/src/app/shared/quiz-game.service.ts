import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedGames } from './paginated-game.interface';
import { Quiz } from './quiz.interface';

@Injectable({
  providedIn: 'root'
})
export class QuizGameService {
  private baseUrl = 'https://localhost:7083/api/v1/quizgame';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  
  constructor(private http: HttpClient) { }
  
  getGames(index: number, size: number, sort: string): Observable<PaginatedGames> {
    let url = `${this.baseUrl}/games/page?`;

    if(sort != '') {
      url += `sort=${sort}&`;
    }

    url += `index=${index}&size=${size}`;

    console.log("index", index);
    console.log("size", size);
    console.log("sort", sort);
    console.log("url", url);
    return this.http.get<PaginatedGames>(url);
  }

  getQuizzes(): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.baseUrl}/quizzes`);
  }
}
