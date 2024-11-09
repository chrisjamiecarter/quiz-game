import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogTitle,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatStepperModule } from '@angular/material/stepper';

@Component({
  selector: 'app-quiz-upsert-dialog',
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogModule,
    MatDialogTitle,
    MatStepperModule,
    ReactiveFormsModule,
  ],
  templateUrl: './quiz-upsert-dialog.component.html',
  styleUrl: './quiz-upsert-dialog.component.css',
})
export class QuizUpsertDialogComponent {
  readonly dialogRef = inject(MatDialogRef<QuizUpsertDialogComponent>);
  private formBuilder = inject(FormBuilder);

  firstFormGroup = this.formBuilder.group({
    firstCtrl: '',
  });
  secondFormGroup = this.formBuilder.group({
    secondCtrl: '',
  });

  onCreate() {
    this.dialogRef.close();
  }
}
