import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { IUserOutput } from 'src/app/modules/account/interfaces/user-output.interface';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  user?: IUserOutput;

  public userSubject = new BehaviorSubject<IUserOutput>({});
  constructor(private router: Router, private tokenService: TokenService) {

  }

  get $user(): Observable<IUserOutput> {
    const user = this.tokenService.decryptToken();
    this.userSubject.next(user);
    this.user = user;
    return this.userSubject.asObservable();
  }

  logout() {
    this.userSubject.next({});
    this.tokenService.removeToken();
    window.localStorage.clear();
    this.router.navigate(['/login']);
  }


}
