import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedGames } from './paginated-game.interface';

@Injectable({
  providedIn: 'root'
})
export class QuizGameService {
  private url = 'http://localhost:5283/api/v1/quizgame';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  
  constructor(private http: HttpClient) { }
  
  getGames(pageIndex: number, pageSize: number, sortBy: string): Observable<PaginatedGames> {
    return this.http.get<PaginatedGames>(`${this.url}/games/page?sortBy=${sortBy}&page=${pageIndex + 1}&size=${pageSize}`);
  }
}
