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
import { Quiz } from '../../shared/quiz.interface';
import { QuizGameService } from '../../shared/quiz-game.service';
import { QuizCreate } from '../../shared/quiz-create.interface';
import { QuestionCreate } from '../../shared/question-create.interface';
import { Question } from '../../shared/question.interface';
import { AnswerCreate } from '../../shared/answer-create.interface';
import { Answer } from '../../shared/answer.interface';

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
  private quizGameService = inject(QuizGameService);

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: '',
    });

    this.questionsForm = this.formBuilder.group({
      questions: this.formBuilder.array([this.createQuestionFormGroup()]),
    });
  }

  get questions(): FormArray {
    return this.questionsForm.get('questions') as FormArray;
  }

  createAnswerFormGroup(): FormGroup {
    return this.formBuilder.group({
      text: ['', Validators.required],
      isCorrect: false,
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
    const quizRequest: QuizCreate = {
      name: this.quizForm.value.name,
      description: this.quizForm.value.description,
    };

    this.quizGameService
      .addQuiz(quizRequest)
      .subscribe((quizResponse: Quiz) => {
        const questions = this.questionsForm.value.questions;

        questions.forEach(
          (question: {
            text: string;
            answers: Array<{ text: string; isCorrect: boolean }>;
          }) => {
            const questionRequest: QuestionCreate = {
              quizId: quizResponse.id,
              text: question.text,
            };

            this.quizGameService
              .addQuestion(questionRequest)
              .subscribe((questionResponse: Question) => {
                question.answers.forEach((answer) => {
                  const answerRequest: AnswerCreate = {
                    questionId: questionResponse.id,
                    text: answer.text,
                    isCorrect: answer.isCorrect,
                  };

                  this.quizGameService.addAnswer(answerRequest);
                });
              });
          }
        );

        this.dialogRef.close();
      });
  }

  onDeleteQuestion(index: number): void {
    this.questions.removeAt(index);
  }

  onGetAnswers(index: number): FormArray {
    return this.questions.at(index).get('answers') as FormArray;
  }
}
