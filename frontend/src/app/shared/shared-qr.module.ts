import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FieldErrorsComponent } from './field-errors/field-errors.component';

const components = [
  FieldErrorsComponent
];

@NgModule({
  declarations: [
   ...components
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ...components
  ]
})
export class SharedQrModule { }
