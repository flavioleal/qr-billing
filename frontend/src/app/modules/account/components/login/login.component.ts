import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../account.service';
import { ILoginInput } from '../../interfaces/login-input.interface';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/core/services/token.service';
import { AlertComponent } from 'ngx-bootstrap/alert';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  loading: boolean = false;

  alerts: any[] = [];

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
        if (resp) {
          this.tokenService.setToken(resp.token);
          if (resp.user.role == 'admin') {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate(['/']);
          }
        } else {
          this.loading = false;
          this.router.navigate(['/login']);
        }
      },
      error: (ex) => {
        this.loading = false;
        if (ex.status == 400) {
          this.addAlert("danger", `${ex.error}`)
        } else {
          this.addAlert("danger", `Houve um erro interno na aplicação, por favor tente novamente mais tarde ou consulte o suporte`)
        }
      }
    })
    this.loading = true;
  }

  addAlert(type: string, message: string): void {
    this.alerts.push({
      type: type,
      msg: message,
      timeout: 5000
    });
  }

  onClosedAlert(dismissedAlert: AlertComponent): void {
    this.alerts = this.alerts.filter(alert => alert !== dismissedAlert);
  }

}
