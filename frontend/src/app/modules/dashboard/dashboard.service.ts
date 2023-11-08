import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiOutput } from 'src/app/core/interfaces/api-output.inteface';
import { IPagination } from 'src/app/core/interfaces/pagination.interface';
import { environment } from 'src/environments/environment';
import { IListBillingOutput } from './interfaces/list-billing.interface';
import { IBillingFilter } from './interfaces/billing-filter.interface';
import { IAddBillingInput } from './interfaces/add-billing.interface';
import { Observable, take } from 'rxjs';
import { ICancelBilling } from './interfaces/cancel-billing.interface';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {


  url = `${environment.API}/billing`;

  constructor(protected http: HttpClient) { }

  getByFilter(filter: IBillingFilter) {
    const params = this.buildHttpParams(filter)
    return this.http.get<ApiOutput<IPagination<IListBillingOutput>>>(this.url, {
      params
    });
  }

  add(input: IAddBillingInput): Observable<ApiOutput<boolean>> {
    return this.http.post<ApiOutput<boolean>>(this.url, input).pipe(take(1));
  }

  cancel(idBilling: string, input: ICancelBilling): Observable<ApiOutput<boolean>> {
    return this.http.put<ApiOutput<boolean>>(`${this.url}/cancel/${idBilling}`, input).pipe(take(1));
  }


  private buildHttpParams(filter: any): HttpParams {
    var httpParams = new HttpParams();
    Object.keys(filter).forEach(function (key) {
      httpParams = httpParams.set(key, filter[key] !== null ? filter[key] : '');
    });
    return httpParams;
  }
}
