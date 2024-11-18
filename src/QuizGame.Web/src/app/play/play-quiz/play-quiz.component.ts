import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-play-quiz',
  standalone: true,
  imports: [],
  templateUrl: './play-quiz.component.html',
  styleUrl: './play-quiz.component.css'
})
export class PlayQuizComponent implements OnInit {
  quizId!: string;
  
  readonly route = inject(ActivatedRoute);
  
  ngOnInit(): void {
    this.quizId = this.route.snapshot.paramMap.get('id')!;
  }
}
