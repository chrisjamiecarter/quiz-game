import { AbstractControl, FormArray } from '@angular/forms';

export function validateCorrectAnswers(
  control: AbstractControl
): { [key: string]: any } | null {
  if (control instanceof FormArray) {
    const correctCount = control.controls.filter(
      (c) => c.value.isCorrect
    ).length;
    return correctCount === 1 ? null : { invalidCorrectAnswers: true };
  }
  return null;
}
