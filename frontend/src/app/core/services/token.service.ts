import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IUserOutput } from 'src/app/modules/account/interfaces/user-output.interface';

const KEY = 'authToken';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  jwtHelper = new JwtHelperService();
  constructor() { }

  hasToken() {
    return !!this.getToken();
  }

  setToken(token: string) {
    this.removeToken();
    window.localStorage.setItem(KEY, token);
  }

  getToken() {
    return window.localStorage.getItem(KEY);
  }

  removeToken() {
    window.localStorage.removeItem(KEY);
  }

  decryptToken(): IUserOutput {
    var user = this.jwtHelper.decodeToken(this.getToken() as string);
    return user as IUserOutput
  }

  tokenExpired() {
    const expired = this.jwtHelper.isTokenExpired(this.getToken())
    return expired;
  }
}
