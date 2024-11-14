import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogTitle,
  MatDialogContent,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { QuizGameService } from '../../shared/quiz-game.service';
import { Quiz } from '../../shared/quiz.interface';

@Component({
  selector: 'app-quiz-delete-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatDialogTitle,
    MatDialogContent,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './quiz-delete-dialog.component.html',
  styleUrl: './quiz-delete-dialog.component.css',
})
export class QuizDeleteDialogComponent implements OnInit {
  inProgress: boolean = false;
  quizForm!: FormGroup;
  
  readonly dialogRef = inject(MatDialogRef<QuizDeleteDialogComponent>);
  data: Quiz = inject(MAT_DIALOG_DATA);
  private formBuilder = inject(FormBuilder);
  private quizGameService = inject(QuizGameService);

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      name: this.data.name,
      description: this.data.description,
    });
  }

  onDelete() {
    this.quizGameService.deleteQuiz(this.data.id);
    this.dialogRef.close();
  }
}
