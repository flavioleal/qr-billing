import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { QRCodeModule } from 'angularx-qrcode';
import { CURRENCY_MASK_CONFIG, CurrencyMaskConfig, CurrencyMaskModule } from 'ng2-currency-mask';
import { AlertModule } from 'ngx-bootstrap/alert';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { RequestInterceptor } from 'src/app/core/interceptors/request.interceptor';
import { SharedQrModule } from 'src/app/shared/shared-qr.module';

import { BillingFormComponent } from './components/billing-form/billing-form.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { AdminComponent } from './pages/admin/admin.component';
import { MerchantComponent } from './pages/merchant/merchant.component';

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: "right",
  allowNegative: true,
  decimal: ",",
  precision: 2,
  prefix: "R$ ",
  suffix: "",
  thousands: "."
};

@NgModule({
  declarations: [
    DashboardComponent,
    BillingFormComponent,
    AdminComponent,
    MerchantComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SharedQrModule,
    DashboardRoutingModule,
    PaginationModule.forRoot(),
    QRCodeModule,
    CurrencyMaskModule,
    AlertModule.forRoot(),
  ],
  providers: [
    { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true,
    },
  ]
})
export class DashboardModule { }
