import { Routes } from '@angular/router';
import { GamesComponent } from './games/games.component';
import { QuizzesComponent } from './quizzes/quizzes.component';

export const routes: Routes = [
    {
        path: '',
        component: QuizzesComponent,
        title: 'Quiz Game - Home',
    },
    {
        path: 'games',
        component: GamesComponent,
        title: 'Quiz Game - Games',
    },
    {
        path: 'quizzes',
        component: QuizzesComponent,
        title: 'Quiz Game - Quizzes',
    },
];
