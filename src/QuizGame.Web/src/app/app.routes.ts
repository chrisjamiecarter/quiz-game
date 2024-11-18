import { Routes } from '@angular/router';
import { GamesComponent } from './games/games.component';
import { QuizzesComponent } from './quizzes/quizzes.component';
import { PlayComponent } from './play/play.component';
import { PlayQuizComponent } from './play/play-quiz/play-quiz.component';

export const routes: Routes = [
    {
        path: '',
        component: PlayComponent,
        title: 'Quiz Game - Home',
    },
    {
        path: 'games',
        component: GamesComponent,
        title: 'Quiz Game - Games',
    },
    {
        path: 'play/quiz/:id',
        component: PlayQuizComponent,
        title: 'Quiz Game - Play',
    },
    {
        path: 'quizzes',
        component: QuizzesComponent,
        title: 'Quiz Game - Quizzes',
    },
];
