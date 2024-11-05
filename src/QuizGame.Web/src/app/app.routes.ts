import { Routes } from '@angular/router';
import { GamesComponent } from './games/games.component';

export const routes: Routes = [
    {
        path: '',
        component: GamesComponent,
        title: 'Quiz Game - Home',
    },
];
