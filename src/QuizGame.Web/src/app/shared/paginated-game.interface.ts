import { Game } from './game.interface';

export interface PaginatedGames {
  totalRecords: number;
  games: Game[];
}
