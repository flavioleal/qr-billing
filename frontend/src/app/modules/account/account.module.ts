import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedQrModule } from 'src/app/shared/shared-qr.module';
import { AlertModule } from 'ngx-bootstrap/alert';



@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedQrModule,
    AlertModule.forRoot(),
  ]
})
export class AccountModule { }
