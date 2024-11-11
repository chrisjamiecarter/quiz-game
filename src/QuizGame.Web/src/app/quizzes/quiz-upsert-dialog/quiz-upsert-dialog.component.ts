import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {
  MatDialogModule,
  MatDialogTitle,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';

@Component({
  selector: 'app-quiz-upsert-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDialogContent,
    MatDialogModule,
    MatDialogTitle,
    MatIconModule,
    MatInputModule,
    MatStepperModule,
    ReactiveFormsModule,
  ],
  templateUrl: './quiz-upsert-dialog.component.html',
  styleUrl: './quiz-upsert-dialog.component.css',
})
export class QuizUpsertDialogComponent implements OnInit {
  quizForm!: FormGroup;
  questionsForm!: FormGroup;

  readonly dialogRef = inject(MatDialogRef<QuizUpsertDialogComponent>);
  private formBuilder = inject(FormBuilder);

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: '',
    });

    this.questionsForm = this.formBuilder.group({
      questions: this.formBuilder.array([
        this.createQuestionFormGroup()
      ]),
    });
  }

  get questions(): FormArray {
    return this.questionsForm.get('questions') as FormArray;
  }

  createAnswerFormGroup(): FormGroup {
    return this.formBuilder.group({
      text: ['', Validators.required],
      isTrue: false,
    });
  }

  createQuestionFormGroup(): FormGroup {
    return this.formBuilder.group({
      text: ['', Validators.required],
      answers: this.formBuilder.array([
        this.createAnswerFormGroup(),
        this.createAnswerFormGroup(),
        this.createAnswerFormGroup(),
        this.createAnswerFormGroup(),
      ]),
    });
  }

  onAddQuestion(): void {
    this.questions.push(this.createQuestionFormGroup());
  }

  onCreate() {
    this.dialogRef.close();
  }

  onDeleteQuestion(index: number): void {
    this.questions.removeAt(index);
  }

  onGetAnswers(index: number): FormArray {
    return this.questions.at(index).get('answers') as FormArray;
  }
}
