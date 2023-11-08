import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ILoginInput } from './interfaces/login-input.interface';
import { Observable, take } from 'rxjs';
import { ApiOutput } from 'src/app/core/interfaces/api-output.inteface';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  url = `${environment.API}/account/login`;

  constructor(protected http: HttpClient) { }

  login(input: ILoginInput): Observable<any> {
    return this.http.post<any>(this.url, input).pipe(take(1));
  }
}
