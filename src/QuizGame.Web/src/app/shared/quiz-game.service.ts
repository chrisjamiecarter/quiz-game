import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedGames } from './paginated-game.interface';

@Injectable({
  providedIn: 'root'
})
export class QuizGameService {
  private url = 'https://localhost:7083/api/v1/quizgame';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  
  constructor(private http: HttpClient) { }
  
  getGames(): Observable<PaginatedGames> {
    return this.http.get<PaginatedGames>(`${this.url}/games`);
  }
}
