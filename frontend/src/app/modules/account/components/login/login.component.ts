import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../account.service';
import { ILoginInput } from '../../interfaces/login-input.interface';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/core/services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  loading: boolean = false;

  constructor(public fb: FormBuilder,
    public router: Router,
    private accountService: AccountService,
    private tokenService: TokenService) {
    this.form = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loading = false;
  }

  onSubmit(): void {
    const { username, password } = this.form.value;
    const input: ILoginInput = {
      username,
      password
    };
    this.accountService.login(input).subscribe({
      next: (resp) => {
        if(resp){
          this.tokenService.setToken(resp.token);
          this.router.navigate(['/billing']);
        } else {
          this.router.navigate(['/login']);
        }
      }
    })
    this.loading = true;
  }

}
