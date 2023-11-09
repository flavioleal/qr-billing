import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { TokenService } from "../services/token.service";

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  constructor(
    private tokenService: TokenService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    const expired = this.tokenService.tokenExpired();

    if (!expired) {
      req = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + this.tokenService.getToken()),
      });
    }

    return next.handle(req).pipe(
      map<HttpEvent<any>, any>((evt: HttpEvent<any>) => {
        return evt;
      })
    );
  }
}
